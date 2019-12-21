using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Joystick joystick;

    //Inverted2DCollider circleColScript;
    [SerializeField] private ScoreController scoreContScript;
    private Rigidbody2D rb;
    [HideInInspector] public bool playerCanMove;
    private bool moving = false;

    [Space(10)]

    [SerializeField] private float moveSpeed;

    [Space(10)]

    // Explosion forces when destroying stuff
    [SerializeField] private float explosionForce;
    private Vector2 explosionPos;
    private bool fadeForce;

    [Space(10)]

    // FIND CLOSEST CIRCLE CENTER
    public List<GameObject> circles = new List<GameObject>();    
    Vector2 bestTargetCircle;
    GameObject closestCircle;
    float closestDistanceSqrCircle = Mathf.Infinity;

    // LINE DEBUGGING:
    //[SerializeField] private Color lineColor = new Color(0, 1, 0);
    //[SerializeField] private GameObject myLine;
    //private LineRenderer lr;

    private void Start()
    {
        //lr = myLine.GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();

        joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();

        playerCanMove = true;
        fadeForce = false;
    }

    private void FixedUpdate()
    {
        if (playerCanMove)
        {
            float moveX = joystick.Horizontal * moveSpeed;
            float moveY = joystick.Vertical * moveSpeed;

            Vector2 move = new Vector3(moveX, moveY);

            transform.position += Vector3.ClampMagnitude(move, moveSpeed) * Time.deltaTime;

            if (moveX > 0 || moveY > 0) { moving = true; }
            else { moving = false; }
        }
    }

    private void Update()
    {
        if (moving)
        {
            Vector2 closestCirclePos = GetClosestCircleGameObject().transform.position; // Get the position of the closest circle
            closestDistanceSqrCircle = Mathf.Infinity; // Reset Closest Distance so it can recalculate the target

            // DEBUG CLOSEST CIRCLE
            //Debug.DrawLine(transform.position, closestCirclePos, new Color(1, 0, 1));
        }

        if (fadeForce)
        {
            playerCanMove = false;
            rb.velocity = new Vector2(transform.position.x - Time.deltaTime * 5f, transform.position.y - Time.deltaTime * 5f);
            rb.angularVelocity -= Time.deltaTime * 5f;

            if (rb.angularVelocity > 0.1f || rb.velocity.magnitude > 0.1f)
            {
                playerCanMove = true;
                fadeForce = false;

                rb.angularVelocity = 0;
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void PlayerDeath()
    {
        playerCanMove = false;
        scoreContScript.UpdateHighScore();
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemySpawner"))
        {
            explosionPos = new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y);
            rb.AddForce(explosionPos * explosionForce, ForceMode2D.Impulse);
            StartCoroutine(FadeForce());
        }
    }

    IEnumerator FadeForce()
    {
        playerCanMove = false;
        yield return new WaitForSeconds(0.3f);
        fadeForce = true;
    }

    #region DEBUGGING CLOSEST CIRCLE
    // DEBUGGING ONLY
    /*
    public void DrawLine(Vector3 start, Vector2 end, Color color)
    {
        myLine.transform.position = start;

        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.02f;
        lr.endWidth = 0.01f;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
    */
    #endregion

    #region GET CLOSEST CIRCLE
    // FINDING THE CLOSEST CIRCLE CENTER BY GAMEOBJECT
    GameObject GetClosestCircleGameObject ()
    {
        Vector3 currentPos = transform.position; // Player position

        foreach (GameObject potentialCircle in circles) // Go through all potential circle centers
        {
            float dist = Vector3.Distance(potentialCircle.transform.position, currentPos); // Set dist to be the distance between a potential target
                                                                                           // and the player
            potentialCircle.GetComponent<Inverted2DCollider>().ColliderOff();

            if (dist < closestDistanceSqrCircle) // If the distance to a potential target is lower than the last target...
            {
                closestCircle = potentialCircle; // Set the bestTarget to be the closest one we found
                closestDistanceSqrCircle = dist; // Set the closest distance to be the distance to the target circle 
            }
        }

        if (closestCircle.GetComponent<Inverted2DCollider>().circleColliderOn == false)
        {
            closestCircle.GetComponent<Inverted2DCollider>().ColliderOn();
        }

        return closestCircle;
    }
    #endregion
}
