using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;        // ลากตัว Golem มาใส่ในช่องนี้
    
    [Header("Follow Settings")]
    public float smoothSpeed = 0.125f; // ค่าความนุ่มนวล (0.1 - 0.5 กำลังดี)
    public Vector2 offset = new Vector2(0, 1); // ระยะห่างจากตัวละคร (x, y)

    void LateUpdate()
    {
        if (target != null)
        {
            // คำนวณตำแหน่งที่กล้องควรจะไป โดยรักษาค่า z ของกล้องไว้ที่ -10 เสมอ
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, -10f);
            
            // ใช้ Lerp เพื่อให้กล้องเคลื่อนที่ตามแบบนุ่มๆ ไม่กระชาก
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            transform.position = smoothedPosition;
        }
    }
}