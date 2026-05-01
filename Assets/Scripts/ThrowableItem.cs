using UnityEngine;

public class ThrowableItem : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private bool isLanded = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        FreezeItem(); // เริ่มเกมมาให้หยุดนิ่งไว้ก่อน
    }

    public void PickUp(Transform holdPoint)
    {
        isLanded = false; 
        rb.simulated = false; 
        if (col != null) col.enabled = false; 
        transform.SetParent(holdPoint); 
        transform.localPosition = Vector3.zero;
    }

    public void Drop() { ReleaseItem(); }

    public void Throw(Vector2 direction, float force)
    {
        ReleaseItem();
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void ReleaseItem()
    {
        transform.SetParent(null); 
        rb.simulated = true;       
        if (col != null) col.enabled = true; 
        rb.constraints = RigidbodyConstraints2D.None; 
    }

    private void FreezeItem()
    {
        rb.velocity = Vector2.zero;      // หยุดความเร็วเคลื่อนที่
        rb.angularVelocity = 0f;         // หยุดการหมุน
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // ล็อคทุกอย่างให้นิ่งสนิท
        isLanded = true;
    }

    // ฟังก์ชันหยุดนิ่งเมื่อชนพื้น
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ถ้าสิ่งที่ชนมี Tag ว่า Ground ให้หยุดกึกทันที
        if (!isLanded && collision.gameObject.CompareTag("Ground"))
        {
            FreezeItem();
        }
    }
}