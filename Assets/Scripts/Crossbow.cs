using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;
    public float shootPower = 15f;
    public float launchAngle = 45f; // มุมยิง (องศา)

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        
        float angleInRadians = launchAngle * Mathf.Deg2Rad;

        
        float velocityX = shootPower * Mathf.Cos(angleInRadians);
        float velocityY = shootPower * Mathf.Sin(angleInRadians);

        Vector2 initialVelocity = new Vector2(velocityX, velocityY);

        
        rb.linearVelocity = initialVelocity; // หมายเหตุ: Unity เวอร์ชันเก่าอาจใช้ rb.velocity
    }
}