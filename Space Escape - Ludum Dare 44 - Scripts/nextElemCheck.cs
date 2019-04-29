using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextElemCheck : MonoBehaviour
{
    public float time;

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            SpawnPoint.instance.spawnReady = true;
            Debug.Log("Respawn");
        }
    }
}
