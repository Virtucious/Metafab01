using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;

    private Rigidbody2D _playerRigidbody;
    public Animator animator;

    private void Start()
    { 
        _playerRigidbody = GetComponent<Rigidbody2D>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }

    private void Update()
    {

        if (_playerRigidbody.velocity.y == 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
        else if (_playerRigidbody.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
        else if (_playerRigidbody.velocity.y >0)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }
        MovePlayer();
    }

    private void MovePlayer()
    {
        var ground = Physics2D.Raycast(transform.position, Vector2.down, 0.7f);
        
        
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
        else if (Input.GetKey(KeyCode.Space))
        {
            if (ground.collider != null)
            {
                _playerRigidbody.AddForce(new Vector2(0, 0.1f), ForceMode2D.Impulse);
            }
        }
    }
}