using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 8f;
    public string idleAnimation = "golem idle";
    public string walkAnimation = "golem walk";
    
    [HideInInspector] public bool isAttacking = false; 

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
        // ถ้ากำลังตีอยู่ ไม่ต้องทำอะไรเพื่อให้แอนิเมชันตีเล่นจนจบ
        if (isAttacking) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(moveInput * initialScale.x, initialScale.y, initialScale.z);
            anim.Play(walkAnimation);
        }
        else
        {
            anim.Play(idleAnimation);
        }
    }
}