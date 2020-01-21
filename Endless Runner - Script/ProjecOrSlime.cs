using UnityEngine;

public class ProjecOrSlime : MonoBehaviour
{
    // Private Variables
    [Header("This game object is a Projectile or a Slime? True for Projectile")]
    [SerializeField] private bool projectile;

    [Header("Initial speed")]
    [SerializeField] private float speed;

    // Private Components
    [Header("Object to instantiate on collision with the player ")]
    [Tooltip("Put the collision objecto with same color")]
    [SerializeField] private GameObject collisionEffect;

    // Private Components
    private Rigidbody2D rB2D;

    private void Awake()
    {
        rB2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // When instantiated, the object captures the current speed of the game and updates its own
        speed += GameManager.instance.newSpeed;
    }

    private void FixedUpdate()
    {
        // Performing the movement
        rB2D.velocity = new Vector2(speed * -100, rB2D.velocity.y) * Time.deltaTime;
    }

    private void Update()
    {
        // If the object leaves the screen with especific X value
        if (transform.position.x <= -15f)
        {
            if (projectile)
            {
                GameManager.instance.score += 1;
            }

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // On collision with the player, the collision effect object will be created and the projectile destroyed
        if (collision.gameObject.tag == "Player")
        {
            CollisionEffects();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon" && !projectile)
        {
            Player.instance.playerAudio.PlayOneShot(Player.instance.hitSlime);
            CollisionEffects();
        }
    }

    // Effects after the collision with any anothers objects
    private void CollisionEffects()
    {
        Instantiate(collisionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
