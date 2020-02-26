using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    private TextMeshProUGUI texto;
    private int tamanho;
    private int contador;
    public float tempo = 0.02f;

    void Start()
    {
        texto   = gameObject.GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();
        texto.maxVisibleCharacters = 0;
        tamanho = texto.text.Length;


        StartCoroutine(letraPorLetra());
    }

    IEnumerator letraPorLetra()
    {
        yield return new WaitForSeconds(tempo);

        while (contador <= tamanho)
        {
            texto.maxVisibleCharacters = contador++;

            yield return new WaitForSeconds(tempo);
        }

        contador = 0;
    }
}
