using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerControl : MonoBehaviour
{

    // make sure the players can move one grid each time
    private bool isMoving;

    // the time to move for each step
    private float timeToMove = 0.2f;

    // start and end position for each step
    private Vector3 origPos, targetPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !isMoving)
        {

            //raycast on the up to check if there is a wall
            RaycastHit2D hitW = Physics2D.Raycast(transform.position, Vector2.up,1f,LayerMask.GetMask("Wall"),-1,1);

            // move the player when there isn't a wall
            if (hitW.collider != null && hitW.distance < 0.9f && hitW.collider.gameObject.tag == "CantMove")
            {
                Debug.Log("hit a wall");
            }
            else
            {
                StartCoroutine(MovePlayer(Vector3.up));
            }
        }
        
        // the rest WSAD controller
        if (Input.GetKey(KeyCode.A) && !isMoving)
        {
            RaycastHit2D hitA = Physics2D.Raycast(transform.position, Vector2.left,1f,LayerMask.GetMask("Wall"),-1,1);

            if (hitA.collider != null && hitA.distance < 0.9f && hitA.collider.gameObject.tag == "CantMove")
            {
                Debug.Log("hit a wall");
            }
            else
            {
                StartCoroutine(MovePlayer(Vector3.left));
            }
        }
        
        if (Input.GetKey(KeyCode.S) && !isMoving)
        {
            RaycastHit2D hitS = Physics2D.Raycast(transform.position, Vector2.down,1f,LayerMask.GetMask("Wall"),-1,1);

            if (hitS.collider != null && hitS.distance < 0.9f && hitS.collider.gameObject.tag == "CantMove")
            {
                Debug.Log("hit a wall");
            }
            else
            {
                StartCoroutine(MovePlayer(Vector3.down));
            }
        }
        
        if (Input.GetKey(KeyCode.D) && !isMoving)
        {
            RaycastHit2D hitD = Physics2D.Raycast(transform.position, Vector2.right,1f,LayerMask.GetMask("Wall"),-1,1);

            if (hitD.collider != null && hitD.distance < 0.9f && hitD.collider.gameObject.tag == "CantMove")
            {
                Debug.Log("hit a wall");
            }
            else
            {
                StartCoroutine(MovePlayer(Vector3.right));
            }
        }
        
        
    }

    // move the player by grid
    private IEnumerator MovePlayer(Vector3 direction)
    {
        // make sure that the player can only move one grid each time
        isMoving = true;

        float elapsedTime = 0;
        
        origPos = transform.position;
        targetPos = origPos + direction;

        // the smooth movement between start position and end position
        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // make sure the player literally stops at the target position in case an inaccuracy
        transform.position = targetPos;
        
        // set the isMoving to false so that the player can move the next step
        isMoving = false;
    }

}
