using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    float contadorTempo = 0;
    public float tempoGerarInimigo = 1;

    // Update is called once per frame
    void Update()
    {
        contadorTempo += Time.deltaTime;

        if (contadorTempo >= tempoGerarInimigo)
        {
            Instantiate(Enemy, transform.position, transform.rotation);
            contadorTempo = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
