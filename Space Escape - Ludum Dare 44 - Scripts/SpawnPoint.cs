using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static SpawnPoint instance;

    // Public variables
    public bool spawnReady;

    [Header("Variables to time")]
    public float minRandSpawn;
    public float maxRandSpawn;
    public float timer;

    // Public components
    [Header("Objects to Spawn")]
    [Tooltip("Indicate all objects that will be spawned by the right side of the screen")]
    public GameObject[] objToSpawn;

    // Private variables
    private int randomElement;

    // Private components

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        spawnReady = false;
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        if (GameManager.instance.gameStarted)
        {
            if (timer >= 9.1f)
            {
                spawnReady = true;
            }
        }        

        if (spawnReady)
        {
            timer = 0;
            randomElement = Random.Range(0, objToSpawn.Length);
            spawnReady = false;
            Instantiate(objToSpawn[randomElement]);
        }
    }
}
