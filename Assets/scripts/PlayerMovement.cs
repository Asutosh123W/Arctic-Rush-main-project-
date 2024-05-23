using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;
    public float speed = 5f;
    public Rigidbody rb;


    public int maxHealth = 3;
    public int currentHealth;
    float horizontalInput;
    public float horizontalMultiplier = 2f;

    public float speedIncreasePerPoint = 0.0f;

    public float jumpforce = 400f;
    public LayerMask groundMask;

    private int _jumpInput = 0;
    private int _slideInput;
    private int _Xmovement;

    private Touch _sTouch;
    private bool _hasSwiped = false;

    // Assuming you have an Animator component attached to the player
    private Animator _animator;

    void Start()
    {
        currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
    }

    void TouchHandling()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                _sTouch = t;
            }
            else if (t.phase == TouchPhase.Moved && !_hasSwiped)
            {
                float Xswipe = _sTouch.position.x - t.position.x;
                float Yswipe = _sTouch.position.y - t.position.y;
                float Distance = Mathf.Sqrt((Xswipe * Xswipe) + Mathf.Pow(Yswipe, 2f));
                bool IsVertical = false;

                if (Mathf.Abs(Xswipe) < Mathf.Abs(Yswipe))
                {
                    IsVertical = true;
                }

                if (Distance > 5f)
                {
                    if (IsVertical)
                    {
                        if (Yswipe < 0)
                        {
                            _jumpInput = 1;
                        }
                        else if (Yswipe > 0)
                        {
                            _slideInput = 1;
                        }
                    }
                    else if (!IsVertical)
                    {
                        // Detect left or right swipe
                        if (Xswipe < 0)
                        {
                            horizontalInput = -1f; // Set horizontal input for left swipe
                        }
                        else if (Xswipe > 0)
                        {
                            horizontalInput = 1f; // Set horizontal input for right swipe
                        }
                    }
                    _hasSwiped = true;
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                _sTouch = new Touch();
                _hasSwiped = false;
            }
        }
    }


    private void FixedUpdate()
    {
        if (!alive) return;
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);

        TouchHandling();

        // Assuming you want to use the swipe inputs for jumping and sliding
        if (_jumpInput == 1)
        {
            _jumpInput = 0;
            Jump();
        }
        else if (_slideInput == 1)
        {
            _slideInput = 0;
            // Call a method for sliding if needed
        }
    }

    public void ApplySpeedBoost(float boostAmount)
    {
        speed *= boostAmount;
    }

    public void RemoveSpeedBoost(float boostAmount)
    {
        Debug.Log("Removing speed boost. Current speed: " + speed);
        speed -= boostAmount;
        Debug.Log("Speed after removing boost: " + speed);
    }

    public void ApplySpeedReduction(float reductionAmount)
    {
        // Apply speed reduction effect
        speed -= reductionAmount; // You can adjust this according to your game's logic
    }

    public void RemoveSpeedReduction(float reductionAmount)
    {
        // Revert the speed reduction effect
        speed += reductionAmount; // Add the reduction amount to revert back to original speed
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<AudioManager>().PlaySound("jump");
            Jump();
        }

        if (transform.position.y < -5 || transform.position.y > 7)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }

    public void Die()
    {
        alive = false;
        Invoke("Restart", 1);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpforce);
        }
    }
}