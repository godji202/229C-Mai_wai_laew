using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public string attackAnimation = "golem attack";
    public float attackTime = 0.5f; 
    public Transform attackPoint;    
    public float attackRange = 0.5f; 
    
    // 1. เพิ่มตัวแปรสำหรับเก็บไฟล์เสียง
    public AudioSource attackAudio; 

    private Animator anim;
    private PlayerMove moveScript;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        moveScript = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();

        // 2. ถ้าลืมลากใส่ใน Inspector ให้มันพยายามหา AudioSource ในตัวมันเองก่อน
        if (attackAudio == null) attackAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !moveScript.isAttacking)
        {
            StartCoroutine(PlayAttack());
        }
    }

    IEnumerator PlayAttack()
    {
        moveScript.isAttacking = true; 
        rb.linearVelocity = Vector2.zero; 
        anim.Play(attackAnimation);    

        // 3. สั่งเล่นเสียงทันทีที่กดตี
        if (attackAudio != null) 
        {
            attackAudio.Play(); 
        }

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        
        foreach (Collider2D hit in hitObjects)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyHealth>()?.TakeDamage();
            }
            else if (hit.CompareTag("Breakable"))
            {
                hit.GetComponent<BreakableWall>()?.TakeDamage();
            }
        }
        
        yield return new WaitForSeconds(attackTime); 
        moveScript.isAttacking = false; 
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}