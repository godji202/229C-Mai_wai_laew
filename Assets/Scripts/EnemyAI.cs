using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("Settings")]
    public Transform player;
    public float moveSpeed = 3f;
    public float chaseRange = 5f;
    public float attackRange = 1.2f;
    public float attackCooldown = 2f;
    public int damage = 100;

    [Header("Animations")]
    public string walkAnim = "sk walk"; 
    public string idleAnim = "sk idle";
    public string attackAnim = "sk attack";

    [Header("Audio")]
    // เพิ่มตัวแปรสำหรับใส่เสียงตีของมอนสเตอร์
    public AudioSource attackAudio; 

    private Animator anim;
    private Rigidbody2D rb;
    private bool isAttacking = false;
    private float lastAttackTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        // ค้นหาผู้เล่นอัตโนมัติ
        if (player == null) player = GameObject.Find("Golem_Player")?.transform;

        // ถ้าลืมลาก AudioSource ใส่ช่อง ให้มันหาในตัวเองก่อน
        if (attackAudio == null) attackAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player == null || isAttacking) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(AttackPlayer());
            }
            else
            {
                StopMoving();
            }
        }
        else if (distance <= chaseRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMoving();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        if (direction.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        anim.Play(walkAnim);
    }

    void StopMoving()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        anim.Play(idleAnim);
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;
        
        anim.Play(attackAnim);
        lastAttackTime = Time.time;

        // --- เพิ่มคำสั่งเล่นเสียงตรงนี้ ---
        if (attackAudio != null)
        {
            attackAudio.Play();
        }
        // -----------------------------

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }

        yield return new WaitForSeconds(1.0f); 
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}