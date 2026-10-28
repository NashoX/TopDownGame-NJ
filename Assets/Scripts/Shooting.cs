using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce;

    private bool puedeDisparar = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (puedeDisparar && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();

        }
    }

    void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = firePoint.position.z;

        Vector2 direction = (mousePosition - firePoint.position).normalized;
        Quaternion bulletRotation = Quaternion.FromToRotation(Vector2.up, direction);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
        puedeDisparar = false;
    }

    public void RecuperarBala()
    {
        puedeDisparar = true;
    }
}
