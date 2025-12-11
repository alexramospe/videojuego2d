using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int valor = 1;
    public AudioClip audioTomarMoneda;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.SumarPuntos(valor);
            Destroy(this.gameObject);

            AudioManager.Instance.ReproducirSonido(audioTomarMoneda);
        }

    }
}
