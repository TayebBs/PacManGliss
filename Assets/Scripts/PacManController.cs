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
    bool isSent = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (isDragging)
                {
                    Vector2 touchEndPos = touch.position;
                    swipeDelta = touchEndPos - touchStartPos;

                    if (swipeDelta.magnitude > 200) // Minimum swipe distance threshold
                    {
                        float angle = Mathf.Atan2(swipeDelta.y, swipeDelta.x) * Mathf.Rad2Deg;
                        angle = (angle + 360) % 360; // Ensure angle is within [0, 360)

                        // Determine the swipe direction based on the angle
                        if (angle >= 45 && angle < 135)
                        {
                            moveDirection = Vector2.up;
                            transform.rotation = Quaternion.Euler(0, 0, 0);

                            transform.rotation = Quaternion.Euler(0, 0, 90);
                            spriteRenderer.flipX = false;
                        }// Up swipe
                       
                        else if (angle >= 135 && angle < 225)
                        {
                            // Left swipe
                            moveDirection = Vector2.left;
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            spriteRenderer.flipX = true;
                        } 
                        else if (angle >= 225 && angle < 315)
                        {
                            moveDirection = Vector2.down;
                            transform.rotation = Quaternion.Euler(0, 0, 0);

                            transform.rotation = Quaternion.Euler(0, 0, -90);
                            spriteRenderer.flipX = false;
                        } // Down swipe
                           
                        else
                        {
                            // Right swipe

                            moveDirection = Vector2.right;
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            spriteRenderer.flipX = false;
                        }
                    

                        // Reset the swipe state
                        isDragging = false;
                    }
                }
            }
        }
    }


    public void Die()
    {
        Debug.Log("die !");
        if (Application.platform == RuntimePlatform.Android && !isSent)
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.azesmwayreactnativeunity.ReactNativeUnityViewManager"))
            {
                string jsonString = "{\"score\":" + ScoreManager.Score + ",\"glissNodes\":" + ScoreManager.TotalGlissNodes + "}";
                jc.CallStatic("sendMessageToMobileApp", jsonString);
                Debug.Log("sendMessageToMobileApp " + ScoreManager.Score);
                isSent = true;
            }
        }
        RestartScene();
    }
    public void Win()
    {
        Debug.Log("win !");
        if (Application.platform == RuntimePlatform.Android && !isSent)
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.azesmwayreactnativeunity.ReactNativeUnityViewManager"))
            {
                string jsonString = "{\"score\":" + ScoreManager.Score + ",\"glissNodes\":" + ScoreManager.TotalGlissNodes + "}";
                jc.CallStatic("sendMessageToMobileApp", jsonString);
                Debug.Log("sendMessageToMobileApp " + ScoreManager.Score);
                isSent = true;
            }
        }
        RestartScene();
    }
    public void RestartScene()
    {
        Debug.Log("Restarting Scene...");
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
