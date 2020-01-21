using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Instance this script
    public static Player instance;

    // Public Variables
    public AudioClip attackSound;
    public AudioClip hitSlime;
    public AudioClip jump;

    // Public Components
    [HideInInspector] public Animator playerAnim;
    [HideInInspector] public AudioSource playerAudio;

    // Private Components
    [SerializeField] private AudioClip hitPlayer;
    [SerializeField] private AudioClip slimeContact;

    private PlayerMove playerMoveScript;

    private void Awake()
    {
        instance = this;

        GettingComponents();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            playerAudio.PlayOneShot(hitPlayer);
            playerMoveScript.enabled = false;
            playerAnim.SetBool("dead", true);
            StartCoroutine(GameManager.instance.GameOver()); // Call game over method
        }

        if (collision.gameObject.tag == "Slime")
        {
            StartCoroutine(SlimeCollision());
        }
    }

    // Getting Components to use later
    private void GettingComponents()
    {
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMoveScript = GetComponent<PlayerMove>();
    }

    // Collision with a slime causes temporary paralysis
    private IEnumerator SlimeCollision()
    {
        playerAudio.PlayOneShot(slimeContact);
        playerAnim.SetBool("paralyzed", true);
        playerMoveScript.enabled = false;

        yield return new WaitForSeconds(1.1f);

        playerMoveScript.enabled = true;
        playerAnim.SetBool("paralyzed", false);
    }
}
