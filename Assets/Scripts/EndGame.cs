using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    void Update()
    {
        // เช็คว่ากดเมาส์ซ้ายไหม
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Credit"); // ชื่อฉากต้องตรงกับใน Build Settings
        }
    }
}