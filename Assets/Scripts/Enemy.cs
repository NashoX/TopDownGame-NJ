using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public Transform player;
    [Header("Salud")]
    public float maxHealth = 100;
    public float Health;
    [Header("Ataque")]
    public float damage = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        /*
- Unity busca un objeto con la etiqueta "Player". Si por alguna razón el jugador todavía no se cargó,
- no existe en la escena o te olvidaste de asignarle el Tag, 
- esa búsqueda devuelve null (nada). 
- Al intentar sacarle el .transform a algo que no existe,
- Unity lanza un error crítico y el script se rompe.
- esta anotacion se la pedi a mi gran amiga Gemini AI joaco, aunque no se que tan util sera en realidad.
- sentite libre de sacar ese if si no lo ves necesario
         */
        Health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. filtro primero por etiqueta
            if (collision.gameObject.CompareTag("Bullet"))
            {
                // 2. busco el componente (con G mayúscula)
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();

                // 3. verifico que exista
                if (bullet != null && !bullet.EstaEnElSuelo)
                {
                    //calculo del knockback
                    Vector2 direccionEmpuje = (transform.position - collision.transform.position).normalized;
                    rb.AddForce(direccionEmpuje * bullet.knockbackForce, ForceMode2D.Impulse);

                    Health -= bullet.damage;
                    bullet.CaerAlSuelo();
            }

                // 4. verifico si murió el enemigo
                if (Health <= 0)
                {
                    Destroy(gameObject);
                }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();

            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
            }
        }
    }
}