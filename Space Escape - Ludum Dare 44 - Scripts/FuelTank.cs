using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    // Private variables
    private int durability;

    // Private components
    private Collider2D collider;
    private MeshRenderer meshRend;
    private ParticleSystem particles;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        meshRend = GetComponent<MeshRenderer>();
        particles = GetComponent<ParticleSystem>();
        durability = 3;
    }

    void Update()
    {
        if (durability <= 0)
        {
            AudioManager.instance.PlaySFX(0);
            Destroy(this.gameObject);
        }       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collider.enabled = false;
            meshRend.enabled = false;
            AudioManager.instance.PlaySFX(4);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShot")
        {
            durability--;
        }
    }

    private IEnumerator Destroy()
    {
        particles.Play();
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlaySFX(4);
        Destroy(this.gameObject);
    }
}
