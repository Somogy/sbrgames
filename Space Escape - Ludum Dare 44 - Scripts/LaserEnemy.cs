using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    // Public variables
    public float intervalShot;

    // Public components
    public GameObject laserShot;
    public GameObject spawnLaserPoint;

    private float fixedIntervalShot;

    void Start()
    {
        fixedIntervalShot = intervalShot;
    }

    // Update is called once per frame
    void Update()
    {
        intervalShot -= Time.deltaTime;

        if (intervalShot <= 0)
        {
            intervalShot = fixedIntervalShot;
            StartCoroutine(LaserTime());
        }
    }

    private IEnumerator LaserTime()
    {
        Instantiate(laserShot, spawnLaserPoint.transform.position, spawnLaserPoint.transform.rotation);

        yield return new WaitForSeconds(intervalShot);
    }
}
