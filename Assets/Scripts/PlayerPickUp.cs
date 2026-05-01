using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public Transform holdPoint;
    public float throwForce = 15f;
    private ThrowableItem itemInRange;
    private ThrowableItem currentItem;
    private bool isHolding = false;

    void Update()
    {
        if (holdPoint != null)
        {
        // 1. รับตำแหน่งเม้าส์จากหน้าจอ
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // ล็อคแกน Z ไว้ไม่ให้ทะลุเข้าออกฉาก

        // 2. คำนวณระยะห่าง (ให้มันลอยรอบๆ ตัว Golem ไม่ไกลเกินไป)
        float distance = 1.5f; // ปรับความไกลของจุดแดงได้ตรงนี้
        Vector2 direction = (mousePos - transform.position).normalized;
        holdPoint.position = (Vector2)transform.position + (direction * distance);
        }
        
        // 1. กด E เพื่อหยิบของ หรือ วางของ
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHolding && itemInRange != null) PickUpItem();
            else if (isHolding) DropItem();
        }

        // 2. คลิกซ้ายเพื่อปา (ถ้าถือของอยู่)
        if (isHolding && Input.GetMouseButtonDown(0))
        {
            ThrowItem();
        }
    }

    void PickUpItem()
    {
        currentItem = itemInRange;
        currentItem.PickUp(holdPoint);
        isHolding = true;
    }

    void DropItem()
    {
        currentItem.Drop();
        currentItem = null;
        isHolding = false;
    }

    void ThrowItem()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 throwDir = (mousePos - transform.position).normalized;

        currentItem.Throw(throwDir, throwForce);
        currentItem = null;
        isHolding = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ThrowableItem")) itemInRange = other.GetComponent<ThrowableItem>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ThrowableItem")) itemInRange = null;
    }
}