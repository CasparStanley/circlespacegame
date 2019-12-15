using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    public int zoomedTimes = 0;
    private int nextOrthoSize = 1;
    private bool hasZoomedOut = false;

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ZoomOut());
        }
    }

    private IEnumerator ZoomOut()
    {
        if (!hasZoomedOut)
        {
            hasZoomedOut = true;
            nextOrthoSize += 1;

            Debug.Log("Actual camera ortho size: " + mainCam.orthographicSize + '\n' + " & nextOrthoSize: " + nextOrthoSize);

            gameObject.transform.localScale *= 1.4f;

            zoomedTimes++;
        }

        yield return new WaitForSeconds(0.5f);
        hasZoomedOut = false;
    }

    private void Update()
    {
        if (hasZoomedOut)
        {
            mainCam.orthographicSize += Time.deltaTime;
        }
    }
}
