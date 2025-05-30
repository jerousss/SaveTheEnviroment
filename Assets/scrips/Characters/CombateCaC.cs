using UnityEngine;

public class CombateCaC : MonoBehaviour

{
    public Controls controles;

    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    private Animator animator;
    [SerializeField] private float tiempoEntreGolpes = 0.8f;
    private float tiempoUltimoGolpe;
    private bool puedeAtacar = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Awake()
    {
        controles = new Controls();
    }
    void OnEnable()
    {
        // activa los controles
        controles.Enable();
        controles.ataque.ataque1.started += _ => golpe();
        controles.ataque.ataque2.started += _ => golpe();

    }
    void OnDisable()
    {
        // desactiva los controles
        controles.Disable();
        controles.ataque.ataque1.started -= _ => golpe();
        controles.ataque.ataque2.started -= _ => golpe();

    }

    private void golpe()
    {
        if (!puedeAtacar) return;

        puedeAtacar = false;
        tiempoUltimoGolpe = Time.time;

        animator.SetTrigger("atacando");

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("enemies"))
            {
                colisionador.transform.GetComponent<enemies>().tomarDaño(dañoGolpe);
            }
        }

        Invoke(nameof(ResetearAtaque), tiempoEntreGolpes);
    }
    private void ResetearAtaque()
    {
        puedeAtacar = true;
    }
    private void OnDrawGizmos()
    {
        //controlador del ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);

    }


}
