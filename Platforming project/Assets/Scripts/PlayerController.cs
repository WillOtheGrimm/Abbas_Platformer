using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    FacingDirection currentDirection;

    //Jumping mechanic
    public float apexTime;
    public float apexHeight;
    float gravity;
    public float gravityMultiplier, initialJumpVelMultiplier;
    float initialJumpVel;
    bool isJumping = false;

    public float coyoteTime;
    float currentTime;


    //task 2
    public float terminalSpeed;




    public float accelerationTime;
    public float decelerationTime;
    public float maxSpeed;

    float acceleration;
    float deceleration;

    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first fr`ame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;




    }

    // Update is called once per frame
    void Update()
    {
        gravity = gravityMultiplier * apexHeight / Mathf.Pow(apexTime, 2);
        initialJumpVel = initialJumpVelMultiplier * apexHeight / apexTime;




        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        //Vector2 playerInput = new Vector2();
        //MovementUpdate(playerInput);

        //Debug.Log(rb.gravityScale);



        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }


        Debug.Log(rb.velocity.x);

    }


    private void FixedUpdate()
    {
        /* if (isJumping)
         {
             rb.AddForce(new Vector2(0, gravity));
         }*/




        Vector2 playerInput = new Vector2();
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerInput += Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerInput += Vector2.right;
        }




        MovementUpdate(playerInput);





    }

    private void MovementUpdate(Vector2 playerInput)
    {

        Vector2 velocity = rb.velocity;


        //This handles the movement by adding acceleration when it receives an input.
        if (playerInput.x != 0)
        {
            velocity += playerInput * acceleration * Time.fixedDeltaTime;
        }
        //If no input start decelerating.
        else if (playerInput.x == 0)
        {
            velocity.x += Mathf.Sign(velocity.x) * -1 * deceleration * Time.fixedDeltaTime;

            //Make sure that in velocity is low, it just sets it to 0
                if (velocity.x < 0.15f && velocity.x > -0.15f)
                {
                    velocity.x = 0;
                }
        }








        if (isJumping)
        {
            velocity.y = initialJumpVel;
        }

        if (!IsGrounded())
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }

        //This handles terminal velocity
        if (velocity.y <= terminalSpeed)
        {
            velocity.y = terminalSpeed;
        }





        //This sets my velocity to the vector velocity
        rb.velocity = velocity;














        /*float inputX = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector2(inputX * acceleration, 0));*/






        /*   if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
           {
               Debug.Log("Jump!");
               rb.AddForce(new Vector2(0, initialJumpVel), ForceMode2D.Impulse);
           }




           if (IsGrounded())
           {
               isJumping = false;

           }
           else if (!IsGrounded())

           {
               currentTime += Time.deltaTime;
               if (currentTime >= coyoteTime)
               {
                   isJumping = true;
                   currentTime = 0;
               }


           }



               Debug.Log(rb.velocity.y);
   */
        //Debug.Log(isJumping);
















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
