using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float knockbackForce = 5f;
    public float range=10;
    private Vector2 initialPosition;
    public float damage=10;

    [SerializeField]
    private bool enElSuelo = false;
    private Rigidbody2D rb;
    private Collider2D cld;
    public bool EstaEnElSuelo => enElSuelo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        cld = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!enElSuelo && Vector2.Distance(initialPosition, transform.position) >= range)
        {
            CaerAlSuelo();
    }
    }
    public void CaerAlSuelo()
    {
        enElSuelo = true;

        // 1. Quitar atributos de ataque
        damage = 0f;
        knockbackForce = 0f;

        // 2. Apagar toda la física (se vuelve inmóvil e inmune a empujes)
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        // 3. Convertir en sensor para no bloquear paso
        cld.isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (enElSuelo && collision.CompareTag("Player"))
        {
            // Buscamos el componente Shooting en el jugador que pisó la bala
            Shooting arma = collision.GetComponent<Shooting>();
            if (arma != null)
            {
                arma.RecuperarBala();
                Destroy(gameObject);
            }
        }


    }



}
