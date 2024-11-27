using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    public float acceleration;
    FacingDirection currentDirection;

    //Jumping mechanic
    public float apexTime;
    public float apexHeight;
    float gravity;
    public float gravityMultiplier, initialJumpVelMultiplier;
    float initialJumpVel;
    bool hasJumped;

    public float coyoteTime;
    float currentTime;


    //task 2
    public float terminalSpeed;

    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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


    private void FixedUpdate()
    {
        if (hasJumped)
        {
            rb.AddForce(new Vector2(0, gravity));
        }


    }

    private void MovementUpdate(Vector2 playerInput)
    {
        float inputX = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector2(inputX * acceleration, 0));

        /*if (inputX == 0 && IsGrounded())
        {
            rb.velocity = new Vector2 ( 0, rb.velocity.y);
        }*/


        //Debug.Log(coyoteTime);





        if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
        {
            rb.AddForce(new Vector2(0, initialJumpVel), ForceMode2D.Impulse);
        }




        if (IsGrounded())
        {
            hasJumped = false;

        }
        else if (!IsGrounded())

        {
            currentTime += Time.deltaTime;
            if (currentTime >= coyoteTime)
            {
                hasJumped = true;
                currentTime = 0;
            }


        }



        if (rb.velocity.y <= terminalSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, terminalSpeed);
        }
            Debug.Log(rb.velocity.y);

        //Debug.Log(hasJumped);


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
