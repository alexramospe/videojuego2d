using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarNivel : MonoBehaviour
{

    public bool ultimoNivel = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && ultimoNivel==false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(ultimoNivel == true)
        {
            SceneManager.LoadScene(0);
        }
    }
}
