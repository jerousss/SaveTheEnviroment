using UnityEngine;
using UnityEngine.UIElements;

public class Movimiento2D : MonoBehaviour
{
    public Controls controles;
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
    public GameObject arrow;
    private bool disparando = false;
    private float tiempoDisparo = 0f;
    private float duracionDisparo = 0.3f;
    public float vida;
    [SerializeField] private float tiempoEntreDisparos = 1f;
    private float tiempoSiguienteDisparo = 1f;




    void Awake()
    {
        controles = new Controls();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        controles.Enable();
        controles.Movimiento.Saltar.started += _ => saltar();
        controles.ataque.ataque3.started += _ => shoot();

    }
    void OnDisable()
    {
        controles.Disable();
        controles.Movimiento.Saltar.started -= _ => saltar();
        controles.ataque.ataque3.started -= _ => shoot();

    }
    void Update()
    {
        direccion = controles.Movimiento.Mover.ReadValue<Vector2>();
        ajustarRotacion(direccion.x);
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("run", direccion.x != 0);
        animator.SetBool("saltar", !enSuelo);


        if (disparando)
        {
            tiempoDisparo += Time.deltaTime;
            animator.SetBool("disparando", true);

            if (tiempoDisparo >= duracionDisparo)
            {
                disparando = false;
                animator.SetBool("disparando", false);
            }
        }
    }
    void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(direccion.x * velocidadMovimiento, rb2D.linearVelocity.y);
    }
    private void ajustarRotacion(float direccionX)
    {
        if (direccionX > 0 && !mirandoDerecha)
        {
            girar();
        }
        else if (direccionX < 0 && mirandoDerecha)
        {
            girar();
        }
    }
    private void saltar()
    {
        if (enSuelo)
        {
            rb2D.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
        }
    }
    public void tomarDaño(float daño)
    {
        animator.SetTrigger("hurt");
        vida -= daño;
        if (vida <= 0)
        {
            muerte();
        }
    }
    private void muerte()
    {
        rb2D.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("muerte");
        Destroy(gameObject, 1f);
    }
    private void girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.aliceBlue;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
    private void shoot()
{
    if (arrow == null) return;

    if (Time.time < tiempoSiguienteDisparo) return;

    Vector3 direction = mirandoDerecha ? Vector3.right : Vector3.left;

    GameObject flecha = Instantiate(arrow, transform.position + direction, Quaternion.identity);
    flecha.GetComponent<Shot>().setDirection(direction);
    disparando = true;
    tiempoDisparo = 0f;

    
    tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
}
    // en esta clase esta toda la logica del movimiento del personaje y se tiene tambien el disparo ya que es mas facil instanciarlo desde aca

}