using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    public GameObject Mob;
    float contadorTempo = 0;
    public float tempoGerarMob = 1;

    // Update is called once per frame
    void Update()
    {
        contadorTempo += Time.deltaTime;

        if (contadorTempo >= tempoGerarMob)
        {
            Instantiate(Mob, transform.position, transform.rotation);
            contadorTempo = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
