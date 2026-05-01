using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 8f;
    
    [Header("Animation Names")]
    public string idleAnimation = "golem idle"; // ใส่ชื่อไฟล์แอนิเมชันท่ายืน
    public string walkAnimation = "golem walk"; // ใส่ชื่อไฟล์แอนิเมชันท่าเดิน

    private Rigidbody2D rb;
    private Vector3 initialScale;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        initialScale = transform.localScale;
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(moveInput * initialScale.x, initialScale.y, initialScale.z);
            
            // ถ้าเดินอยู่ ให้เล่นท่าเดิน
            anim.Play(walkAnimation);
        }
        else
        {
            // ถ้าหยุดเดิน ให้เล่นท่ายืน
            anim.Play(idleAnimation);
        }
    }
}