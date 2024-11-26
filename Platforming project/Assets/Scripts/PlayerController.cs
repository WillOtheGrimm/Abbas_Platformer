using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    public float acceleration;
    public float jumpHeight;
    FacingDirection currentDirection;

    //Jumping mechanic
    public float apexTime;
    public float apexHeight;
    float gravity;
    public float gravityMultiplier, initialJumpVelMultiplier;
    float initialJumpVel;
    float currentVel;
    float yPosition;

    int isNotGroundedValue;



    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        yPosition = transform.position.y;
        


        gravity = gravityMultiplier * apexHeight / Mathf.Pow(apexTime, 2);
        initialJumpVel = initialJumpVelMultiplier * apexHeight / apexTime;

    }

    // Update is called once per frame
    void Update()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        Vector2 playerInput = new Vector2();
        MovementUpdate(playerInput);


        //Debug.Log(rb.gravityScale);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        float inputX = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector2(inputX * acceleration, 0));

        /*  if (Input.GetKeyDown(KeyCode.Space))
          {
              Debug.Log("jump");
              rb.AddForce(new Vector2(0, 1 * jumpHeight), ForceMode2D.Impulse);

          }*/


        if (Input.GetKeyDown(KeyCode.Space)  && IsGrounded())
        {
            currentVel = initialJumpVel;
            Debug.Log(currentVel);
        }
        else if (!IsGrounded())
        {
            isNotGroundedValue = 1;
        }
        else if (IsGrounded())
        {
            isNotGroundedValue = 0;
            currentVel = 0;
            //yPosition = transform.position.y;
        }

            yPosition =   transform.position.y + currentVel * Time.deltaTime + 0.5f * gravity * isNotGroundedValue *  Time.deltaTime * Time.deltaTime  ;
            transform.position = new Vector2 (transform.position.x,yPosition);
            currentVel = currentVel + gravity * Time.deltaTime;

            

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
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.66f, LayerMask.GetMask("Ground")))
        {
            Debug.DrawRay(transform.position, Vector2.down * 0.66f, Color.red);
            return true;

        }







        return false;
    }




    public FacingDirection GetFacingDirection()
    {

        if (Input.GetAxis("Horizontal") > 0)
        {
            currentDirection = FacingDirection.right;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            currentDirection = FacingDirection.left;
        }
        return currentDirection;



    }
}
