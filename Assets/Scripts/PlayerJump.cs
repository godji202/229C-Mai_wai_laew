using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private bool isGrounded; // เช็คว่าแตะพื้นอยู่ไหม

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ถ้ากด Space และอยู่บนพื้น (isGrounded เป็น true) ให้กระโดด
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false; // พอกระโดดแล้วให้เป็น false ทันที กันกระโดดรัวกลางอากาศ
        }
    }

    // ฟังก์ชันเช็คการชนกับพื้น
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ถ้าวัตถุที่ชนมี Tag ชื่อว่า "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}