using Unity.VisualScripting;
using UnityEngine;

public class enemies : MonoBehaviour
{
    public Transform player;
    public float detectionRadius;
    public float speed;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool caminando;
    private Animator animator;
    [SerializeField] private float vida;
    private Movimiento2D character;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        character = player.GetComponent<Movimiento2D>();

    }
    void Update()
    {
        // movimiento enemigo
        if (player == null) return;
        Vector3 dir = player.transform.position - transform.position;
        Vector3 localScale = transform.localScale;
        if (dir.x >= 0.0f)
            localScale.x = Mathf.Abs(localScale.x); 
        else
            localScale.x = -Mathf.Abs(localScale.x); 
        transform.localScale = localScale;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0);
            caminando = true;
        }
        else
        {
            movement = Vector2.zero;
            caminando = false;
        }
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        animator.SetBool("caminando", caminando);

    }


    public void tomarDaño(float daño)
    {
        //daño a los enemigos
        animator.SetTrigger("hurt");
        vida -= daño;
        if (vida <= 0)
        {
            muerte();
        }
    }
    private void muerte()
    {
        rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("muerte");
        Destroy(gameObject, 1f);
    }
}
