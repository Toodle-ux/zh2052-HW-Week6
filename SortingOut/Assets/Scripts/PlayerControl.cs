using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerControl : MonoBehaviour
{
    // the rigidbody of the player
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    
    // make sure the players can move one grid each time
    private bool isMoving;

    private float timeToMove = 0.2f;

    private Vector3 origPos, targetPos;

    // public float forceAmount = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !isMoving)
        {
            // disable the box collider so that the player itself won't be raycasted
            boxCollider.enabled = false;
            
            //raycast on the up
            RaycastHit2D hitW = Physics2D.Raycast(transform.position, Vector2.up);
            
            // enable the box collider again
            boxCollider.enabled = true;

            // move the player when there isn't a wall on the up
            // this part is adapted from Alessia's code
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
            boxCollider.enabled = false;
            RaycastHit2D hitA = Physics2D.Raycast(transform.position, Vector2.left);
            boxCollider.enabled = true;

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
            boxCollider.enabled = false;
            RaycastHit2D hitS = Physics2D.Raycast(transform.position, Vector2.down);
            boxCollider.enabled = true;

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
            boxCollider.enabled = false;
            RaycastHit2D hitD = Physics2D.Raycast(transform.position, Vector2.right);
            boxCollider.enabled = true;

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
    // this part is adapted from the tutorials
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
