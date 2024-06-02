using UnityEngine;

public class FlipEnemySprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D parentRb;
    private float flipThreshold = 0.1f; // Minimum velocity to consider for flipping

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentRb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        if (Mathf.Abs(parentRb.velocity.x) > flipThreshold)
        {
            if (parentRb.velocity.x > 0 && spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false; // Facing right
            }
            else if (parentRb.velocity.x < 0 && !spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true; // Facing left
            }
        }
    }
}
