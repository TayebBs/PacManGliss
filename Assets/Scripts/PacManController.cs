using UnityEngine;

public class PacManController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which Pac-Man moves
    private Rigidbody2D rb; // Rigidbody component
    private Vector2 moveDirection = Vector2.zero; // Current movement direction
    private SpriteRenderer spriteRenderer; // SpriteRenderer component

    void Start()
    {
        // Get the Rigidbody2D component attached to the GameObject
        rb = GetComponent<Rigidbody2D>();

        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Calculate movement direction based on arrow key inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Update movement direction only if a key is pressed
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            // Normalize the movement direction to ensure constant speed
            moveDirection = new Vector2(moveHorizontal, moveVertical).normalized;

            // Calculate the angle of rotation based on the movement direction
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            // Flip the sprite renderer on the X-axis if moving left
           

            // Adjust the angle and flip the sprite renderer on the Y-axis for top and bottom movement
            if (moveVertical > 0)
            {
                spriteRenderer.flipX = false;

            }
            else if (moveVertical < 0)
            {
                spriteRenderer.flipX = true;
            }
          

            // Rotate Pac-Man to face the direction of movement
      //      transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // Set the velocity of the Rigidbody2D to move Pac-Man at constant speed
        rb.velocity = moveDirection * moveSpeed;

        // Debug statements to check movement values
        Debug.Log("Horizontal Movement: " + moveHorizontal);
        Debug.Log("Vertical Movement: " + moveVertical);
        Debug.Log("Pac-Man Velocity: " + rb.velocity.magnitude);
    }
}
