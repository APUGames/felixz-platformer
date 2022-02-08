using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 26.0f;
    [SerializeField] float jumpSpeed = 26.0f;
    [SerializeField] float climbSpeed = 26.0f;
    Collider2D playerCollider; 
    Rigidbody2D playerCharacter; 
    Animator playerAnimator;
    float gravityScaleAtStart;
    bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();

        gravityScaleAtStart = playerCharacter.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Jump();
        Climb();
    }

    private void Run()
    {
        //Value between -1 to +1
        float horizontalMove = Input.GetAxis("Horizontal");
        Vector2 runVeloctiy = new Vector2(horizontalMove*runSpeed, playerCharacter.velocity.y);
        playerCharacter.velocity = runVeloctiy;

        playerAnimator.SetBool("run", true);

        bool hspeed = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;

        playerAnimator.SetBool("run", hspeed);

        print(runVeloctiy);
        

    }

    private void FlipSprite()
    {
        bool hMovement = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;
        if(hMovement)
        {
            transform.localScale =  new Vector2(Mathf.Sign(playerCharacter.velocity.x), 1.0f);
        }
    }

    private void Jump()
    {

        //Refreshes double jump once contact with ground is made
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            doubleJump = true;
        }

        

        if(Input.GetButtonDown("Jump"))
        {
            if(!playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            //will stop this function if not true
            
        
                if (doubleJump)
                {
                    //Function continues, allowing a mid-air jump
                    playerCharacter.velocity= new Vector2(0.0f, 0.0f);
                    doubleJump = false;
                }

                else if (!doubleJump)
                {
                
                    return;
                }
            
        }
            //Get new Y Velocity based on controllable variable
            Vector2 jumpVelocity = new Vector2(0.0f, jumpSpeed);
            playerCharacter.velocity += jumpVelocity;
        }
    }



    private void Climb(){
        //will stop this function if true
        if(!playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            
            playerAnimator.SetBool("climb", false);
            playerCharacter.gravityScale = gravityScaleAtStart;

            return;
        }

        //"Vertical" from Input Axes
        float verticalMove = Input.GetAxis("Vertical");
        //X needs to remain the same and we need to change Y
        Vector2 climbVelocity = new Vector2(playerCharacter.velocity.x, verticalMove*climbSpeed);
        playerCharacter.velocity = climbVelocity;
        playerCharacter.gravityScale = 0.0f;

        bool vspeed = Mathf.Abs(playerCharacter.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("climb", vspeed);

        


    }


}
