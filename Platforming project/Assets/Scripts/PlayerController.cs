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


    //task 2
    public float terminalSpeed;




    public float accelerationTime;
    public float decelerationTime;
    public float maxSpeed;

    float acceleration;
    float deceleration;
    bool canJump = false;

    Vector2 velocity;
    public float dashingDistance;

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



        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            canJump = false;
        }


        


        //Debug.Log(rb.velocity.x);





    }


    private void FixedUpdate()
    {
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

        velocity = rb.velocity;


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
                if (velocity.x < 0.30f && velocity.x > -0.30f)
                {
                    velocity.x = 0;
                }
        }







        ////////////////////////////////////////////////////COYOTE TIME ///////////////////////////////////////////////////





      /*  if (velocity.y <= terminalSpeed)
        {
            velocity.y = terminalSpeed;
        }*/
        Debug.Log(velocity.y);




        if (isJumping && canJump)
        {
            velocity.y = initialJumpVel;
            isJumping = false;
            //canJump = false;
        }



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
        }
        
        if (IsGrounded())
        {
        canJump = true;
        coyoteTime = 0.3f;
        }



        if (Input.GetKeyDown(KeyCode.LeftShift))
        velocity.y += gravity * Time.fixedDeltaTime;
        {
          // Dashing();
        }








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




    /* public void Dashing()
     {
         Debug.Log("Dashing");
         velocity.x += dashingDistance;



     }
 */

}
