using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    // Private Variables
    [Header("Interval of the projectiles")]
    [SerializeField] private float spawnInterval;

    // Variable for timer
    private float timer;

    // Private Components
    [SerializeField] private List<GameObject> projectilesToSpawn; // collection for objects to spawn against player

    private void Start()
    {
        timer = spawnInterval;
    }
    private void Update()
    {
        // Calculate the interval between spawns
        IntervalTimer();
    }

    // When the counter reaches zero, spawn a projectile
    private void IntervalTimer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = spawnInterval;
            SpawnObj();
        }
    }

    // Spawn method
    private void SpawnObj()
    {
        // Random object of the array and then generate it
        int randomObj = Random.Range(0, projectilesToSpawn.Count);
        Instantiate(projectilesToSpawn[randomObj], transform.position, transform.transform.rotation);
    }
}

