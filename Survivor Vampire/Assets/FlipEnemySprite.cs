using UnityEngine;

public class FlipEnemySprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D parentRb;
    private float lastFlipTime;
    public float flipCooldown = 0.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentRb = GetComponentInParent<Rigidbody2D>();
        lastFlipTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastFlipTime >= flipCooldown)
        {
            if (parentRb.velocity.x > 0 && spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false; // Facing right
                lastFlipTime = Time.time;
            }
            else if (parentRb.velocity.x < 0 && !spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true; // Facing left
                lastFlipTime = Time.time;
            }
        }
    }
}
