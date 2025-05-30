using UnityEngine;

public class Shot : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float speed;
    private Vector2 direction;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }


    void Update()
    {
        rb2D.linearVelocity = direction * speed;
    }
    public void setDirection(Vector2 dir)
    {
        direction = dir;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        enemies enemie = collision.GetComponent<enemies>();
        if (enemie != null)
        {
            enemie.tomarDaño(30);
        }
        Destroy(gameObject);
    }
    // clase para instanciar los proyectiles de los personajes 
}
