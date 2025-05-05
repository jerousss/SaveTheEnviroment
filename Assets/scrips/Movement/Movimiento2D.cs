using UnityEngine;
using UnityEngine.UIElements;

public class Movimiento2D : MonoBehaviour
{
  public Move controles;
  public Vector2 direccion;
  public Rigidbody2D rb2D;
  public float velocidadMovimiento;
  public bool mirandoDerecha = true;
  public float fuerzaSalto;
  public LayerMask queEsSuelo;
  public Transform controladorSuelo;
  public Vector3 dimensionesCaja;
  public bool enSuelo;
  private Animator animator;

    void Awake()
    {
        controles = new();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        controles.Enable();
        controles.Movimiento.Saltar.started += _ => saltar();
    }
    void OnDisable()
    {
        controles.Disable();
        controles.Movimiento.Saltar.started -= _ => saltar();
    }
    void Update()
    {
        direccion = controles.Movimiento.Mover.ReadValue<Vector2>();
        ajustarRotacion(direccion.x);
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja,0f,queEsSuelo);
        animator.SetBool("run",direccion.x != 0);
        animator.SetBool("jump", !enSuelo);
    }
    void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(direccion.x*velocidadMovimiento, rb2D.linearVelocity.y);
    }
    private void ajustarRotacion(float direccionX){
        if(direccionX>0 && !mirandoDerecha){
           girar();
        }else if (direccionX<0 && mirandoDerecha){
        girar();
        }
    }
     private void saltar(){
        if(enSuelo){
        rb2D.AddForce(new Vector2(0,fuerzaSalto),ForceMode2D.Impulse);
        }
     }
    private void girar(){
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.aliceBlue;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
