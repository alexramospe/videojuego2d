using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 10f;
    public float distanciaDeteccion = 15;
    private Transform player;

    [Header("Ataque")]
    public float cooldownAtaque = 1.5f;
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Buscar al player por Tag
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Calcula distancia al jugador
        float distancia = Vector2.Distance(transform.position, player.position);

        //Debug.Log("Detectando al jugador a distancia = " + distancia);
        // Si está dentro del rango, lo sigue
        if (distancia < distanciaDeteccion)
        {
            SeguirJugador();
        }
    }

    void SeguirJugador()
    {
        // Mover hacia el jugador
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            velocidad * Time.deltaTime
        );

        // Voltear sprite según la posición del jugador
        if (player.position.x < transform.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!puedeAtacar) return;

            puedeAtacar = false;

            // Efecto visual al atacar
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            // Quitar vida por GameManager
            GameManager.Instance.PerderVida();

            // Golpe al personaje
            other.gameObject.GetComponent<CharacterController>().AplicarGolpe();

            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    void ReactivarAtaque()
    {
        puedeAtacar = true;

        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }
}
