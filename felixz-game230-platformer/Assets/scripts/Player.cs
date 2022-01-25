using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 26.0f;
    Rigidbody2D playerCharacter; 
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    private void Run()
    {
        //Value between -1 to +1
        float horizontalMove = Input.GetAxis("Horizontal");
        Vector2 runVeloctiy = new Vector2(horizontalMove*runSpeed, playerCharacter.velocity.y);
        playerCharacter.velocity = runVeloctiy;

        playerAnimator.SetBool("run", true);

        bool moving = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;

        playerAnimator.SetBool("run", moving);
        

    }

    private void FlipSprite()
    {
        bool hMovement = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;
        if(hMovement)
        {
            transform.localScale =  new Vector2(Mathf.Sign(playerCharacter.velocity.x), 1.0f);
        }
    }


}
