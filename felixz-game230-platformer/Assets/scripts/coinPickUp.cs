using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickSFX;
    [SerializeField] int coinValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().ProcssPlayerScore(coinValue);

        AudioSource.PlayClipAtPoint(coinPickSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
