using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public string attackAnimation = "golem attack";
    public float attackTime = 0.5f; 
    public Transform attackPoint;    
    public float attackRange = 0.5f; 

    private Animator anim;
    private PlayerMove moveScript;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        moveScript = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();
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

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        
        foreach (Collider2D hit in hitObjects)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyHealth>()?.TakeDamage();
            }
            else if (hit.CompareTag("Breakable"))
            {
                // แก้ Error: ลบเลข 1 ในวงเล็บออก
                hit.GetComponent<BreakableWall>()?.TakeDamage();
            }
            // ไม่ต้องใส่เช็ค Finalwall ในนี้ ดาบเลยจะฟันไม่เข้าโดยปริยาย
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