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
    private bool canJump = true;
    public float stoppingForce = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        HandleKeyboardInput();
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

        if (keyboard.spaceKey.isPressed || keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
        {
            Jump();
        }
    }

    private void HandlePlayerXMovement()
    {
        float horizontalSpeed = direction.x * maxspeed;
        rb.linearVelocity = new Vector2(horizontalSpeed, rb.linearVelocity.y);
        if (direction.x < 0)
    {
        spriteRenderer.flipX = true;
    }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
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
        if (!canJump)
        {
            return;
        }

        rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }
}