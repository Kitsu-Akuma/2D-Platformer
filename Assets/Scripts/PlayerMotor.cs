using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    Vector2 direction;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public float speed = 10;
    public float jump = 10;
    public float maxspeed = 5;
    public int maxJumps = 2;
    public float dashSpeed = 20;
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.5f;
    public float stoppingForce = 5;
    public Animator animator;

    private int jumpsRemaining;
    private float facingDirection = 1;
    private bool isDashing;
    private bool canDash = true;
    private float dashTimer;
    private float dashCooldownTimer;
    private int lastJumpFrame = -1;
    private bool isRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpsRemaining = maxJumps;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleKeyboardInput();
    }

    private void FixedUpdate()
    {
        HandleDash();
        HandlePlayerXMovement();
    }

    private void HandleKeyboardInput()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
        {
            return;
        }

        direction.x = 0;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            direction.x -= 1;
        }

        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            direction.x += 1;
        }

        if (keyboard.spaceKey.wasPressedThisFrame || keyboard.wKey.wasPressedThisFrame || keyboard.upArrowKey.wasPressedThisFrame)
        {
            Jump();
        }

        if (keyboard.fKey.wasPressedThisFrame)
        {
            Dash();
        }
    }

    private void HandlePlayerXMovement()
    {
        if (isDashing)
        {
            return;
        }

        float horizontalSpeed = direction.x * maxspeed;
        rb.linearVelocity = new Vector2(horizontalSpeed, rb.linearVelocity.y);

        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
            facingDirection = -1;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
            facingDirection = 1;
        }
        animator.SetBool("IsRunning", direction.x != 0);
    }

    public void OnMove(InputValue value)
    {
        //Debug.Log("Moving");
        //Debug.Log(value.Get<Vector2>());
        direction = value.Get<Vector2>();
    }
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (lastJumpFrame == Time.frameCount)
        {
            return;
        }

        if (jumpsRemaining <= 0)
        {
            return;
        }

        lastJumpFrame = Time.frameCount;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        jumpsRemaining--;
    }

    private void Dash()
    {
        if (!canDash)
        {
            return;
        }

        isDashing = true;
        canDash = false;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
        rb.linearVelocity = new Vector2(facingDirection * dashSpeed, 0);
    }

    private void HandleDash()
    {
        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;
            rb.linearVelocity = new Vector2(facingDirection * dashSpeed, 0);

            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }

        if (!canDash)
        {
            dashCooldownTimer -= Time.fixedDeltaTime;

            if (dashCooldownTimer <= 0)
            {
                canDash = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpsRemaining = maxJumps;
    }
}