using UnityEngine;

// Controla la rotación del arma y muestra la orientación correspondiente.
public class GunRotate : MonoBehaviour
{
    // Referencia al asset del jugador mirando a las distintas direcciones
    public GameObject front;

    public GameObject back;

    public GameObject left;

    public GameObject right;

    // Guarda la escala original para poder restaurarla cuando sea necesario.
    private Vector3 initialScale;

    // Se ejecuta una vez cuando se activa este objeto.
    private void Awake()
    {
        initialScale = transform.localScale;
    }

    // Se ejecuta de forma periódica para actualizar la dirección del jugador y su arma.
    private void FixedUpdate()
    {
        // Calcula la posición del ratón en el mundo y la distancia hasta el arma.
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // Convierte la distancia en un vector con longitud uno.
        difference.Normalize();
        // Calcula el ángulo entre el arma y el ratón en grados.
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // Rota el arma sobre el eje Z para que apunte hacia el ratón.
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        // Comprueba a que direccion apunta el arma
        if (transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 44)
        {
            transform.localScale = initialScale;
            right.SetActive(true);
            front.SetActive(false);
            left.SetActive(false);
            back.SetActive(false);
        }
        if (transform.eulerAngles.z >= 44 && transform.eulerAngles.z <= 130)
        {
            transform.localScale = initialScale;
            back.SetActive(true);
            right.SetActive(false);
            front.SetActive(false);
            left.SetActive(false);
        }
        // Cuando mira a la izquierda o abajo se invierte el eje Y del arma
        if (transform.eulerAngles.z >= 130 && transform.eulerAngles.z <= 180)
        {
            transform.localScale = new Vector3(initialScale.x, -initialScale.y, initialScale.z);
            left.SetActive(true);
            right.SetActive(false);
            front.SetActive(false);
            back.SetActive(false);
        }
        if (transform.eulerAngles.z >= 220 && transform.eulerAngles.z <= 340)
        {
            transform.localScale = new Vector3(initialScale.x, -initialScale.y, initialScale.z);
            front.SetActive(true);
            right.SetActive(false);
            left.SetActive(false);
            back.SetActive(false);
        }
    }
}
