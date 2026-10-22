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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
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