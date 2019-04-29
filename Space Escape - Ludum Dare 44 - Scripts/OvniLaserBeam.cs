using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvniLaserBeam : MonoBehaviour
{
    // Public variables
    public float intervalShot;

    // Public components
    public AudioClip laserSound;

    // Public components
    public GameObject laserBeam;

    private float fixedIntervalShot;

    private AudioSource ovniAudio;

    private void Awake()
    {
        ovniAudio = GetComponent<AudioSource>();
    }
    void Start()
    {
        fixedIntervalShot = intervalShot;
        laserBeam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        intervalShot -= Time.deltaTime;

        if (intervalShot <= 0)
        {
            intervalShot = fixedIntervalShot;
            ovniAudio.PlayOneShot(laserSound);
            StartCoroutine(LaserTime());           
        }
    }

    private IEnumerator LaserTime()
    {
        AudioManager.instance.PlaySFX(6);
        yield return new WaitForSeconds(1f);
        laserBeam.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        laserBeam.SetActive(false);
    }
}
