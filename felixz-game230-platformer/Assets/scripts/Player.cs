using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool isAlive = true;
    [SerializeField] Vector2 deathSeq = new Vector2(26f,26f);
    AudioSource audioSource;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] float runSpeed = 26.0f;
    [SerializeField] float jumpSpeed = 26.0f;
    [SerializeField] float climbSpeed = 26.0f;
    [SerializeField] bool normalJumpEnabled = false;
    [SerializeField] bool doubleJumpEnabled = false;
    

    CapsuleCollider2D playerBodyCollider; 
    BoxCollider2D playerFeetCollider;
    Rigidbody2D playerCharacter; 
    Animator playerAnimator;
    float gravityScaleAtStart;
    bool doubleJump = false;
    float whichSide = 0;

    bool rollLockOut = true;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerCharacter = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();

        gravityScaleAtStart = playerCharacter.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive)
        {
            return;
        }

        Run();
        FlipSprite();
        if (normalJumpEnabled)
        {
            Jump();
        }
        
        if(doubleJumpEnabled)
        {
            Jump2();
        }
        Die();
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
            whichSide = (Mathf.Sign(playerCharacter.velocity.x));

        }
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                return;
            }
            
            Vector2 jumpVelocity = new Vector2(0.0f, jumpSpeed);
            playerCharacter.velocity += jumpVelocity;
            AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position, 0.5f);
            
        }
    }
    private void Jump2()
    {

        //Refreshes double jump once contact with ground is made
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            doubleJump = true;
        }

        

        if(Input.GetButtonDown("Jump"))
        {
            if(!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            //will stop this function if not true
            
        
                if (doubleJump)
                {
                    //Function continues, allowing a mid-air jump. Velocity reset so there's no varied double jump
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

    private void Climb()
    {
        //will stop this function if true
        if(!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            
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

        public void Die()
        {
            if(playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
            {
                isAlive = false;
                playerAnimator.SetTrigger("die");
                GetComponent<Rigidbody2D>().velocity = deathSeq;

                FindObjectOfType<GameSession>().ProcessPlayerDeath();
            }
        }


}
