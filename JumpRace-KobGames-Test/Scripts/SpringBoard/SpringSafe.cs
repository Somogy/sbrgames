using System.Collections;
using UnityEngine;

public class SpringSafe : MonoBehaviour
{
    [Header("Spring Safe attributes")]
    [SerializeField]
    private int bounceForce = 750;

    private MeshCollider meshColl;
    private Rigidbody springRb;

    private void Awake() => InitializeCache();
    private void InitializeCache()
    {
        meshColl = GetComponent<MeshCollider>();
        springRb = GetComponent<Rigidbody>();
    }

    public void SpringUsed()
    {
        PlayerMovement.instance.BoostJump(bounceForce);
        StartCoroutine(TimeToDestroy());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpringUsed();
        }
    }

    private IEnumerator TimeToDestroy()
    {
        meshColl.enabled = false;
        springRb.isKinematic = false;
        springRb.useGravity = true;

        yield return new WaitForSeconds(0.25f);
        Destroy(this.gameObject);
    }
}
