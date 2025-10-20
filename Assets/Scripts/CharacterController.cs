using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public LayerMask capaSuelo;
    public float saltosMaximos;

    private Rigidbody2D rigibody;
    private BoxCollider2D boxColider;
    private bool mirandoDerecha = true;
    private float saltosRestantes;
    private Animator animation;
    // Start is called before the first frame update
    void Start()
    {
        
        rigibody = GetComponent<Rigidbody2D>();
        boxColider = GetComponent<BoxCollider2D>();
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }
    void ProcesarMovimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");

        if (inputMovimiento != 0f)
        {
            animation.SetBool("isRunning", true);
        }
        else
        {
            animation.SetBool("isRunning", false);
        }
         rigibody.velocity = new Vector2(inputMovimiento * velocidad, rigibody.velocity.y);

        GestionarMovimiento(inputMovimiento);
    }
    
    bool enSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxColider.bounds.center, new Vector2(boxColider.bounds.size.x, boxColider.bounds.size.y),0f, Vector2.down,0.2f,capaSuelo);
        return raycastHit.collider != null;
    }
    void GestionarMovimiento(float inputMovimiento)
    {

        if ( (mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false &&inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);
        }
    }
    void ProcesarSalto()
    {
        if (enSuelo())
        {
            saltosRestantes = saltosMaximos;
        }
        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes>0)
        {
            saltosRestantes--;
            rigibody.velocity = new Vector2(rigibody.velocity.x, 0f);
            rigibody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);


        }
    }

}