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
        MovePlayer();
    }

    private void MovePlayer()
    {
        var ground = Physics2D.Raycast(transform.position, Vector2.down, 0.7f);
        Debug.Log(_playerRigidbody.velocity.y);
        if (_playerRigidbody.velocity.y == 0)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }
        if (_playerRigidbody.velocity.y > 0)
        {
            animator.SetBool("isJumping", true);
        }
        if (_playerRigidbody.velocity.y < 100)
        {
            animator.SetBool("isFalling", true);
        }
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
                _playerRigidbody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            }
        }
    }
}