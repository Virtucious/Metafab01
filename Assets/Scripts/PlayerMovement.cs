using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 1f;

    private Rigidbody2D _playerRigidbody;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }
    private void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        animator.SetBool("isGrounded", true);
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
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            var ground = Physics2D.Raycast(transform.position, Vector2.down, 0.3f);
            _playerRigidbody.AddForce(transform.up * jumpPower);
            if (ground.collider != null)
            {
                animator.SetBool("isGrounded", false);

            }
            else
            {
                animator.SetBool("isGrounded", true);
            }


        }
        else
        { 
            animator.SetBool("Move", false);
        }
    }



}