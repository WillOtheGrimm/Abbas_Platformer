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
    public float apexHeight = 5;
    float gravity;
    float gravityMultiplier = -2, initialJumpVelMultiplier = 2;
    float initialJumpVel;
    bool isJumping = false;

    
    public float coyoteTime;


    //task 2
    float terminalSpeed = -5;
    public float terminalHorizontalSpeed;



    public float accelerationTime;
    public float decelerationTime;
    public float maxSpeed;

    float acceleration;
    float deceleration;
    bool canJump = false;

    //Vector2 velocity;
    bool isDashing = false;
    public float dashingDistance;


    public float dashingTime;




    float variableJumpTime = 0;




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



        if (canJump && Input.GetKey(KeyCode.Space))
        {

            
            variableJumpTime += Time.deltaTime;

            if (variableJumpTime >= 1)
            {
                variableJumpTime = 1;
            }
            Debug.Log(variableJumpTime);
        }


        if (canJump && Input.GetKeyUp(KeyCode.Space))
        {
            //Debug.Log("key release");
            isJumping = true;
            canJump = false;

        }



            if (Input.GetKeyDown(KeyCode.LeftShift)  && !IsGrounded())
        {
            isDashing = true;
        }

    }


    private void FixedUpdate()
    {
       Vector2 playerInput = new Vector2();
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerInput = Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerInput = Vector2.right;
        }




        MovementUpdate(playerInput);




       //Debug.Log(playerInput.ToString());
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
        else 
        {
            velocity.x += Mathf.Sign(velocity.x) * -1 * deceleration * Time.fixedDeltaTime;

            //Make sure that in velocity is low, it just sets it to 0
                if (velocity.x < 0.30f && velocity.x > -0.30f)
                {
                    velocity.x = 0;
                }
        }






        if (isJumping )
        {
            if (variableJumpTime < 0.2)
            {
                velocity.y += initialJumpVel * variableJumpTime + 2;
            }


            velocity.y += initialJumpVel * variableJumpTime;
            if(IsGrounded())
            {
                variableJumpTime = 0;
            }
            isJumping = false;
            //canJump = false;



        }

        ////////////////////////////////////////////////////COYOTE TIME ///////////////////////////////////////////////////





        if (velocity.y < terminalSpeed)
        {
            velocity.y = terminalSpeed;
        }
        //Debug.Log(velocity.y);







        if (!IsGrounded())
        {
            coyoteTime -= Time.fixedDeltaTime;
            if (coyoteTime <= 0 )
            {
                canJump = false;
            }
            else if (coyoteTime >= 0 && !isJumping)
            {
                canJump = true;
            }

        velocity.y += gravity * Time.fixedDeltaTime;

        }

        if (IsGrounded())
        {
        canJump = true;
        coyoteTime = 0.3f;
        isDashing = false;
        //variableJumpTime = 0;
        }



       
        //---------------------------------------Dashing--------------------------------------------------

        if (isDashing)
        {

            Debug.Log("Dashing");

            dashingTime -= Time.deltaTime;
            if(dashingTime <=0 )
            {
                velocity.x = 0;
                isDashing = false;
                dashingTime = 0.19f;
            }


            if(velocity.x < 0)
            {

            velocity.x -= dashingDistance;
                /*if (velocity.x <= -terminalHorizontalSpeed)
                {
                    velocity.x += Mathf.Sign(velocity.x) * -1 * deceleration * Time.fixedDeltaTime;
                    isDashing = false;
                }*/
            }


            else if(velocity.x > 0 )
            {
                velocity.x += dashingDistance;
                /*if (velocity.x >= terminalHorizontalSpeed)
                {
                    velocity.x += Mathf.Sign(velocity.x) * -1 * deceleration * Time.fixedDeltaTime;
                }*/


            }



           

        }

            /*if (velocity.x > 0 && velocity.x > terminalHorizontalSpeed && isDashing)
            {
                velocity.x = 0;
                isDashing = false;
            }
            else if (velocity.x < 0 && velocity.x < -terminalHorizontalSpeed && isDashing)
            {
                velocity.x = 0;
                isDashing = false;
            }*/

       // Debug.Log(velocity.x);


        //This sets my velocity to the vector velocity
        rb.velocity = velocity;



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





        if (Physics2D.Raycast(transform.position, Vector2.down, 0.72f, LayerMask.GetMask("Ground")))
        {
            Debug.DrawRay(transform.position, Vector2.down * 0.72f, Color.red);
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
