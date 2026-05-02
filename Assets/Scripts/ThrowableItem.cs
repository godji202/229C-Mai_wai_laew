using UnityEngine;

public class ThrowableItem : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private bool isLanded = true; 
    
    [HideInInspector] public bool isThrown = false; 

    [Header("Item Settings")]
    public bool isFinalStone = false; // ตัวแปรเช็คว่าเป็นหินพิเศษไหม

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // ถ้าเราตั้ง Tag หินก้อนนี้ว่า FinalStone มันจะเซตค่าเป็น true ให้เอง
        if (gameObject.CompareTag("FinalStone"))
        {
            isFinalStone = true;
        }

        FreezeItem(); 
    }

    public void PickUp(Transform holdPoint)
    {
        isLanded = false; 
        isThrown = false; 
        rb.simulated = false; 
        if (col != null) col.enabled = false; 
        transform.SetParent(holdPoint); 
        transform.localPosition = Vector3.zero;
    }

    public void Drop() 
    { 
        isThrown = false; 
        ReleaseItem(); 
    }

    public void Throw(Vector2 direction, float force)
    {
        isThrown = true; 
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
        // ใช้ linearVelocity สำหรับ Unity เวอร์ชั่นใหม่
        rb.linearVelocity = Vector2.zero; 
        rb.angularVelocity = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll; 
        isLanded = true;
        isThrown = false; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isLanded && collision.gameObject.CompareTag("Ground"))
        {
            FreezeItem();
        }
    }
}