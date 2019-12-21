using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToClosestEnemy : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMoveScript;
    private SpriteRenderer sr;

    // FIND CLOSEST ENEMY
    public List<GameObject> enemies = new List<GameObject>();
    Vector2 bestTargetEnemy;
    GameObject closestEnemy;
    float closestDistanceSqrEnemy = Mathf.Infinity;

    float step = 360;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        closestDistanceSqrEnemy = Mathf.Infinity; // Reset Closest Distance so it can recalculate the target     

        // ROTATING ENEMY DETECTOR
        GameObject closestEnemyPos = GetClosestEnemyGameObject(); // Get the position of the closest circle

        if (closestEnemyPos != null)
        {
            Vector3 enemyTargetDir = closestEnemyPos.transform.position - transform.position; // Find the direction towards this enemy

            float angle = Mathf.Atan2(enemyTargetDir.y, enemyTargetDir.x) * Mathf.Rad2Deg; // Get that as an angle

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Rotate towards the enemy
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z); // Don't rotate on X and Y

            Debug.DrawRay(transform.position, enemyTargetDir, Color.red);

            // MAKE THE ENEMY DETECTOR MORE TRANSPARENT THE FURTHER AWAY THE ENEMY IS. THIS ISN'T WORKING
            float alphaValue = 1 / Vector3.Distance(transform.position, closestEnemyPos.transform.position);
            if (alphaValue > 0.3f)
            {
                sr.color = new Color(1, 1, 1, alphaValue);
                print("Alpha Value: " + alphaValue);
            }

            else
            {
                alphaValue = 0;
                sr.color = new Color(1, 1, 1, alphaValue);
            }
        }
    }

    #region GET CLOSEST ENEMY
    // FINDING THE CLOSEST ENEMY BY GAMEOBJECT
    GameObject GetClosestEnemyGameObject()
    {
        if (enemies.Count == 0)
            return null;

        Vector3 currentPos = transform.position; // Player position

        foreach (GameObject potentialEnemy in enemies) // Go through all potential circle centers
        {
            float dist = Vector3.Distance(potentialEnemy.transform.position, currentPos); // Set dist to be the distance between a potential target
                                                                                          // and the player
            if (dist < closestDistanceSqrEnemy) // If the distance to a potential target is lower than the last target...
            {
                closestEnemy = potentialEnemy; // Set the bestTarget to be the closest one we found
                closestDistanceSqrEnemy = dist; // Set the closest distance to be the distance to the target circle 
            }
        }

        return closestEnemy;
    }
    #endregion
}
