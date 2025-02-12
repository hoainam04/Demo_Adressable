using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển
    public float jumpForce = 5f; // Lực nhảy
    private bool isGrounded; // Kiểm tra xem nhân vật có đang đứng trên mặt đất không
    private Rigidbody2D rb; // Rigidbody2D để xử lý vật lý
    private Animator animator; // Animator để điều khiển animation

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy Rigidbody2D từ GameObject
        animator = GetComponent<Animator>(); // Lấy Animator từ GameObject
    }

    void Update()
    {
        Move(); // Gọi hàm di chuyển
        Jump(); // Gọi hàm nhảy
        PlayAnimation(); // Gọi hàm phát animation
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Lấy input từ bàn phím (A/D hoặc mũi tên trái/phải)
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y); // Cập nhật vận tốc

        // Cập nhật hướng nhìn của nhân vật
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1); // Nhân vật nhìn sang phải
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Nhân vật nhìn sang trái
    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump")) // Kiểm tra nếu đang đứng trên mặt đất và nhấn phím nhảy
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // Thêm lực nhảy
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Kiểm tra va chạm với đối tượng có tag "Ground"
        {
            isGrounded = true; // Đặt isGrounded thành true
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Kiểm tra khi rời khỏi mặt đất
        {
            isGrounded = false; // Đặt isGrounded thành false
        }
    }

    void PlayAnimation()
    {
        if (isGrounded)
        {
            if (Mathf.Abs(rb.linearVelocity.x) > 0.1f) // Nếu đang di chuyển
            {
                animator.Play("Run"); // Phát animation chạy
            }
            else // Nếu không di chuyển
            {
                animator.Play("Idle"); // Phát animation đứng yên
            }
        }
        else
        {
            if (rb.linearVelocity.y > 0) // Nếu đang nhảy
            {
                animator.Play("Jump"); // Phát animation nhảy
            }
            else // Nếu đang rơi
            {
                animator.Play("Fall"); // Phát animation rơi
            }
        }
    }
}