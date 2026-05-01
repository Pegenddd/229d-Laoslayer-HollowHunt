using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int maxHealth = 1;
    public float moveSpeed = 2f;
    
    [Header("Jump Physics (F=ma)")]
    public float jumpAcceleration = 7f; // The "a" (Acceleration) in F=ma
    
    private int currentHealth;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D for physics calculations
    }

    void Update()
    {
        // Continuously move left
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }

    // Detect when the enemy enters the Jump Point trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("JumpPoint"))
        {
            Jump();
        }
    }

    // Detect arrow hits
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            TakeDamage(1); 
        }

        else if (collision.gameObject.CompareTag("Tower"))
        {
            // Deal 1 damage to the tower
            GameManager.instance.TakeTowerDamage(1);
            
            // Destroy the enemy without adding a kill
            Destroy(gameObject); 
        }
    }

    void Jump()
    {
        if (rb != null)
        {
            // --- Physics Calculation: Newton's Second Law ---[cite: 2]
            float m = rb.mass;                 // Mass (m)
            float a = jumpAcceleration;        // Acceleration (a)
            float forceY = m * a;              // Force (F = m * a)[cite: 2]

            // Create an upward force vector
            Vector2 upwardForce = new Vector2(0, forceY);

            // Apply the calculated force to make the enemy jump[cite: 2]
            // Using Impulse mode because a jump is a sudden burst of force
            rb.AddForce(upwardForce, ForceMode2D.Impulse);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

void Die()
    {
        // Add 1 kill score only when killed by the player
        if(GameManager.instance != null)
        {
            GameManager.instance.AddKill(1);
        }
        
        Destroy(gameObject);
    }
}