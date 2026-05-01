using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator anim;

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    
    [Header("Charge Mechanics")]
    public float minForce = 5f;        
    public float maxForce = 25f;       
    public float chargeSpeed = 10f;    
    
    private float currentForce;
    private bool isCharging = false;

    void Start()
    {
        if (anim == null) 
        {
            anim = GetComponent<Animator>();
        }
        currentForce = minForce;
    }

    void Update()
    {
        // 1. On left mouse button press (Start charging)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            isCharging = true;
            currentForce = minForce; 
            // Removed anim.Play("attack") from here so it doesn't play prematurely
        }

        // 2. While holding left mouse button (Charging force)
        if (isCharging && Mouse.current.leftButton.isPressed)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, minForce, maxForce); 
        }

        // 3. On left mouse button release (Shoot!)
        if (isCharging && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            // Play attack animation exactly when the arrow is fired
            if (anim != null) 
            {
                anim.Play("attack"); 
            }
            
            Shoot();
            isCharging = false;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0f; 
            
            Vector2 aimDirection = (mousePos - firePoint.position).normalized;
            float angleInRadians = Mathf.Atan2(aimDirection.y, aimDirection.x);

            float velocityX = currentForce * Mathf.Cos(angleInRadians);
            float velocityY = currentForce * Mathf.Sin(angleInRadians);

            Vector2 projectileVelocity = new Vector2(velocityX, velocityY);
            rb.linearVelocity = projectileVelocity; 
        }
    }
}