using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public bool playerHand;

    public AudioSource hitSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && playerHand)
        {
            hitSound.Play();
            Enemy.instance.PlayerHit();
        }

        if (collision.gameObject.tag == "Player" && !playerHand)
        {
            hitSound.Play();
            Player.instance.EnemyHit();
        }
    }
}
