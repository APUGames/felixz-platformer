using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
    {
       
        Rigidbody2D enemyCharacter;

        CapsuleCollider2D bodyCollider;
        BoxCollider2D frontCollider;
        [SerializeField] float moveSpeed = 2.0f;

    void Start()
        {

            enemyCharacter = GetComponent<Rigidbody2D>();
        }

    void Update()
        {
            if(IsRight())
            {
                enemyCharacter.velocity = new Vector2(moveSpeed, 0.0f);
            }
            else
            {
                enemyCharacter.velocity = new Vector2(-moveSpeed, 0.0f);
            }
        }

    bool IsRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D()
    {
        
            transform.localScale = new Vector2(-(Mathf.Sign(enemyCharacter.velocity.x)), 1.0f);

            
        
    }

}

    
