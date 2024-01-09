using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float currentSpeed;

    [SerializeField] private float mudSpeed = 1f;
    [SerializeField] public float jumpForce = 3f;

    public GameObject winText;
    public GameObject clickButton;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * currentSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;


        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

        public void SetJumpForce(float newJumpForce)
    {
        jumpForce = newJumpForce;
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mud"))
        {
            currentSpeed = mudSpeed;
        }

        if (other.CompareTag("jumpArea"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (other.CompareTag("end"))
    {
        winText.SetActive(true);
        clickButton.SetActive(true);
        Time.timeScale = 0;
    }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Mud"))
        {
            currentSpeed = moveSpeed;
        }
    }

    public void RestartGame()
{
    Time.timeScale = 1;
    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
}
}