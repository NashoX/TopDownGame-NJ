using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float knockbackForce = 5f;
    public float range=10;
    private Vector2 initialPosition;
    public float damage=10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(initialPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    
}
