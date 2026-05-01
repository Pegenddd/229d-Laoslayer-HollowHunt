using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if (!hasHit && rb.linearVelocity.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // --- FIX BUG 2: Ignore Tower and Player ---
        
        if (collision.gameObject.CompareTag("Tower") || collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (hasHit) return; // ป้องกันการชนซ้ำซ้อน

        hasHit = true;
        
        // --- FIX BUG 1 Sub-point: Destroy arrow when hit enemy ---
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else // ถ้าชนพื้น ให้ปักคาไว้แบบเดิม
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            
            rb.bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Collider2D>().enabled = false; 
        }
    }
}