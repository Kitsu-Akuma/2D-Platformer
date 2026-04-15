using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    Vector2 direction;
    Rigidbody2D rigidbody2D;
    public float speed = 10;
    public float jump = 10;
    public float maxspeed = 5;
    private bool canJump = true;
    public float stoppingForce = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        HandlePlayerXMovement();
        MaxSpeedLimiting();
    }

    private void HandlePlayerXMovement()
    {
        if (direction.x != 0)
        {
            rigidbody2D.AddForce(new Vector2(direction.x * speed, 0));
        }
        else if (rigidbody2D.linearVelocity.x != 0)
        {
            rigidbody2D.AddForce(new Vector2(-rigidbody2D.linearVelocityX * stoppingForce, 0));
        }
    }

    private void MaxSpeedLimiting()
    {
        if (rigidbody2D.linearVelocityX >= maxspeed)
        {
            rigidbody2D.linearVelocityX = maxspeed;
        }
        else if (rigidbody2D.linearVelocityX <= -maxspeed)
        {
            rigidbody2D.linearVelocityX = -maxspeed;
        }
    }

    private void OnMove(InputValue value)
    {
        //Debug.Log("Moving");
        //Debug.Log(value.Get<Vector2>());
        direction = value.Get<Vector2>();

    }
    private void OnJump()
    {
        if (canJump)
        {
            rigidbody2D.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            canJump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }
}
