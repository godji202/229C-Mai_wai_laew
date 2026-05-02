using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSenes1 : MonoBehaviour
{
    void Update()
    {
        // เช็คว่ากดเมาส์ซ้ายไหม
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GamePlay"); // ชื่อฉากต้องตรงกับใน Build Settings
        }
    }
}