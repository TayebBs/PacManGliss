using UnityEngine;
using UnityEngine.SceneManagement;

public class PacManController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which Pac-Man moves
    private Rigidbody2D rb; // Rigidbody component
    private Vector2 moveDirection = Vector2.zero; // Current movement direction
    private SpriteRenderer spriteRenderer; // SpriteRenderer component
    private Vector2 touchStartPos, swipeDelta; // Stores touch start and delta
    private bool isDragging = false; // Flag to track swipe
    private bool isHorizontalSwipe, isVerticalSwipe; // Flags for swipe direction
    bool isSent=false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            isDragging = true;
            touchStartPos = Input.GetTouch(0).position;
            swipeDelta = Vector2.zero;
        }

        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
        {
            isDragging = false;
            if (swipeDelta.magnitude > 100) // Minimum swipe distance threshold
            {
                swipeDelta = (Vector2)Input.GetTouch(0).position - touchStartPos;
                float swipeAngle = Mathf.Atan2(swipeDelta.y, swipeDelta.x) * Mathf.Rad2Deg; // Convert radians to degrees

                // Restrict movement based on swipe angle (90-degree increments)
                int angleDivision = Mathf.RoundToInt(swipeAngle / 90); // Divide by 90 and round to nearest integer
                moveDirection = Vector2.zero;

                switch (angleDivision)
                {
                    case 1:
                        moveDirection = Vector2.up; // Up swipe
                        transform.rotation = Quaternion.Euler(0, 0, 0);

                        transform.rotation = Quaternion.Euler(0, 0, 90);
                        spriteRenderer.flipX = false;
                        break;
                    case -1:
                        moveDirection = Vector2.down; // Down swipe
                        transform.rotation = Quaternion.Euler(0, 0, 0);

                        transform.rotation = Quaternion.Euler(0, 0, -90);
                        spriteRenderer.flipX = false;
                        break;
                    case 0:
                        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                            moveDirection = Vector2.right; // Right swipe (consider horizontal priority)
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        spriteRenderer.flipX = false;

                        break;
                    case 2:
                        moveDirection = Vector2.left; // Left swipe
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        spriteRenderer.flipX = true;

                        break;
                }
            }
        }
    }

    public void Die()
    {
      
        if (Application.platform == RuntimePlatform.Android&&!isSent)
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.azesmwayreactnativeunity.ReactNativeUnityViewManager"))
            {
                jc.CallStatic("sendMessageToMobileApp", ScoreManager.Score);
                Debug.Log("sendMessageToMobileApp " + ScoreManager.Score);
                isSent = true;
            }
        }
        RestartScene(); 
    }
    public void RestartScene()
    {
        // Get the current scene's build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        ScoreManager.Score = 0;
        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
    void FixedUpdate()
    {
        if (isDragging)
        {
            swipeDelta = (Vector2)Input.GetTouch(0).position - touchStartPos;
        }

        // Set the velocity of the Rigidbody2D to move Pac-Man at constant speed
        rb.velocity = moveDirection * moveSpeed;
    }
}
