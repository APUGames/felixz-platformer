using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gull : MonoBehaviour
{
        [SerializeField] float gullDistance = 5.0f;
        Rigidbody2D enemyCharacter;

        CapsuleCollider2D bodyCollider;
        BoxCollider2D frontCollider;
        [SerializeField] float moveSpeed = 2.0f;

    void Start()
        {

            enemyCharacter = GetComponent<Rigidbody2D>();
            StartCoroutine(processTask());
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

    IEnumerator processTask()
    {
        yield return new WaitForSecondsRealtime(gullDistance);

         transform.localScale = new Vector2(-(Mathf.Sign(enemyCharacter.velocity.x)), 1.0f);

        StartCoroutine(processTask());
    }
   
}
