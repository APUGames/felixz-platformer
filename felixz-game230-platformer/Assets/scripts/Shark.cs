using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [SerializeField] float sharkDistance = 5.0f;
    Rigidbody2D enemyCharacter;

    CapsuleCollider2D bodyCollider;
    [SerializeField] float moveSpeed = 2.0f;

    void Start()
    {

        enemyCharacter = GetComponent<Rigidbody2D>();
        enemyCharacter.gravityScale = 0;
        StartCoroutine(processTask());
    }

    void Update()
    {
        if (IsUp())
        {
            enemyCharacter.velocity = new Vector2(0.0f, moveSpeed);
        }
        else
        {
            enemyCharacter.velocity = new Vector2(0.0f, -moveSpeed);
        }


    }

    bool IsUp()
    {
        return transform.localScale.y > 0;
    }

    IEnumerator processTask()
    {
        yield return new WaitForSecondsRealtime(sharkDistance);

        transform.localScale = new Vector2(1.0f, -(Mathf.Sign(enemyCharacter.velocity.y)));

        StartCoroutine(processTask());
    }

}
