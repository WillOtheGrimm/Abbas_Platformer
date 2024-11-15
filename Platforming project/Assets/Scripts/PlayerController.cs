using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody2D rb;
    public float speed;
    public float jumpHeight;
    FacingDirection currentDirection;
    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        Vector2 playerInput = new Vector2();
        MovementUpdate(playerInput);


        Debug.Log(rb.gravityScale);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        float inputX = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector2(inputX * speed, 0));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            rb.AddForce(new Vector2(0, 1 * jumpHeight));

        }

        

        

    }

    public bool IsWalking()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            return true;
        }
        else return false;
    }



    public bool IsGrounded()
    {
        return true;
    }




    public FacingDirection GetFacingDirection()
    {

        if (Input.GetAxis("Horizontal") > 0)
        {
            currentDirection = FacingDirection.right;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            currentDirection= FacingDirection.left;
        }
         return currentDirection;



    }
}
