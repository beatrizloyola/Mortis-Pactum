using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public variables
    public float speed = 5f; // The speed at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally

    // Private variables 
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Tracks if the player is moving horizontally
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

    void Start()
    {
        // Initialize the Rigidbody2D and SpriteRenderer components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get player input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Check if diagonal movement is allowed
        if (canMoveDiagonally)
        {
            movement = new Vector2(horizontalInput, verticalInput);

            // Flip sprite if moving horizontally
            if (horizontalInput > 0)
                spriteRenderer.flipX = true;
            else if (horizontalInput < 0)
                spriteRenderer.flipX = false;
        }
        else
        {
            // Determine priority of movement based on input
            if (horizontalInput != 0)
            {
                isMovingHorizontally = true;
                spriteRenderer.flipX = horizontalInput < 0;
            }
            else if (verticalInput != 0)
            {
                isMovingHorizontally = false;
            }

            movement = isMovingHorizontally
                ? new Vector2(horizontalInput, 0)
                : new Vector2(0, verticalInput);
        }

        // Normalize to maintain consistent speed when moving diagonally
        if (movement.magnitude > 1)
            movement.Normalize();
    }

    void FixedUpdate()
    {
        // Apply movement using new Unity 6 physics API
        rb.linearVelocity = movement * speed;
    }
}
