using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public static FinishLine instance;

    private ParticleSystem confettiPS;

    private void Awake() => InitializeCache();

    private void InitializeCache()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
        }

        confettiPS = GetComponent<ParticleSystem>();
    }

    public void ThrowConfetti() => confettiPS.Play();
}
