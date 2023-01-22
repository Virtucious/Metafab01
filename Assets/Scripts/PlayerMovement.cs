using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float buttonTime = 0.3f;
    [SerializeField] private float cancelRate = 100f;
    private float jumptime;
    bool jumping;
    bool jumpCancelled;

    private Rigidbody2D _playerRigidbody;
    private Animator animator;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }

    private void Update()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);
        if (_playerRigidbody.velocity.x < 0.5 || _playerRigidbody.velocity.x > -0.5)
        {
            animator.SetBool("Move", false);
        }

        if (_playerRigidbody.velocity.y > 0.05)
        {
            animator.SetBool("isJumping", true);
        }
        if (_playerRigidbody.velocity.y < -0.05)
        {
            animator.SetBool("isFalling", true);
        }
        MovePlayer();
        Jump();
    }

    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x = -1;
            gameObject.transform.localScale = currentScale;
            _playerRigidbody.velocity = new Vector2(-1 * playerSpeed, _playerRigidbody.velocity.y);
            animator.SetBool("Move", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x = 1;
            gameObject.transform.localScale = currentScale;
            _playerRigidbody.velocity = new Vector2(1 * playerSpeed, _playerRigidbody.velocity.y);
            animator.SetBool("Move", true);
        }
    }

    private void Jump()
    {
        var ground = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * _playerRigidbody.gravityScale));
            _playerRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumping = true;
            jumpCancelled = false;
            jumptime = 0f;
        }

        if (jumping)
        {
            jumptime += Time.deltaTime;
            if(Input.GetKeyUp(KeyCode.Space))
            {
                jumpCancelled = true;
            }
            if (jumptime > buttonTime)
            {
                jumping = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (jumpCancelled && jumping && _playerRigidbody.velocity.y > 0)
        {
            _playerRigidbody.AddForce(Vector2.down * cancelRate);
        }
    }
}