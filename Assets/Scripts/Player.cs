using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public Camera cam;
    public Vector2 movement;
    public Vector2 mousePos;

    [Header("Salud")]
    public float maxHealth = 200f;
    public float Health;

    [Header("Dash")]
    public float dashSpeed = 16f;        // Multiplicador de velocidad durante el impulso
    public float dashDuration = 0.15f;   // Tiempo en segundos que dura el impulso
    public float dashCooldown = 1f;      // Tiempo en segundos de recarga antes de volver a usarlo
    private bool canDash = true;         // Controla si el cooldown terminó
    private bool isDashing = false;      // Estado activo del impulso
    private Vector2 dashDir;             // Dirección calculada para el dash

    void Start()
    {
        Health = maxHealth;

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        // Durante el dash bloqueamos la lectura de movimiento para no cortar la trayectoria
        if (isDashing) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Detección de la tecla espaciadora para iniciar el dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            // Movimiento forzado a alta velocidad en la dirección del dash
            rb.MovePosition(rb.position + dashDir * dashSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Movimiento estándar del personaje
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        // Rotación orientada hacia la posición del cursor en pantalla
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private IEnumerator PerformDash()
    {
        canDash = false;
        isDashing = true;

        // Si el jugador se está moviendo con WASD, dashea en esa dirección; si está quieto, hacia el cursor
        if (movement != Vector2.zero)
        {
            dashDir = movement.normalized;
        }
        else
        {
            dashDir = (mousePos - rb.position).normalized;
        }

        // Espera activa durante la fase de impulso
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        // Espera del tiempo de recarga antes de permitir otro dash
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    // Método para recibir daño desde enemigos o proyectiles
    public void TakeDamage(float amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}