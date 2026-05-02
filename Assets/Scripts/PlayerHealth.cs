using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 500; 
    public int currentHealth;
    public HealthUI healthUIScript;
    public string dieAnimation = "golem die";
    
    // เพิ่มตัวแปรสำหรับใส่ชื่อฉากที่ต้องการให้วาร์ปไป
    public string gameOverSceneName = "DeathMenu"; 

    private bool isDead = false;
    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        if (healthUIScript != null) healthUIScript.UpdateHearts(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (healthUIScript != null) healthUIScript.UpdateHearts(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void Heal(int amount)
    {
        if (isDead) return;
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (healthUIScript != null) healthUIScript.UpdateHearts(currentHealth);
    }
    
    void Die()
    {
        if (isDead) return;
        isDead = true;
        if (anim != null) anim.Play(dieAnimation);
        
        // หยุดการเคลื่อนที่ของผู้เล่น
        if (GetComponent<PlayerMove>() != null) GetComponent<PlayerMove>().enabled = false;
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        // รอ 2 วินาที (ให้ดูท่าตายแป๊บนึง) แล้วค่อยวาร์ป
        Invoke("WarpToScreen", 2f);
    }

    void WarpToScreen()
    {
        // โหลดฉากตามชื่อที่เราตั้งไว้ใน Inspector
        SceneManager.LoadScene(gameOverSceneName);
    }
}