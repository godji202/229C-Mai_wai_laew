using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // ฟังก์ชันสำหรับปุ่ม Restart
    public void RestartGame()
    {
        // โหลดฉากปัจจุบันใหม่
        SceneManager.LoadScene("GamePlay");
        Time.timeScale = 1f; // กลับมาให้เกมเดินปกติ
    }

    // ฟังก์ชันสำหรับกลับหน้าเมนู
    public void GoToMainMenu()
    {
        // เปลี่ยน "MainMenu" เป็นชื่อ Scene หน้าเมนูของคุณ
        SceneManager.LoadScene("MainMenu"); 
        Time.timeScale = 1f;
    }
}