using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    // Public variables
    public int life;
    public int score;

    // Private components
    private ParticleSystem particules;
    private void Start()
    {
        particules = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (life <= 0)
        {
            PlayerPrefs.SetInt("ActualScore", PlayerPrefs.GetInt("ActualScore") + score);
            StartCoroutine(Die());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShot")
        {
            AudioManager.instance.PlaySFX(1);
            life--;
        }
    }

    private IEnumerator Die()
    {
        particules.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

}
