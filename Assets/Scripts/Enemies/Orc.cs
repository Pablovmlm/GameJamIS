using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Enemy
{
    [Header("Referencias")]
    Transform player;

    [SerializeField] EnemyHitbox axeHitbox;  // <-- Cambiar tipo a EnemyHitbox
    [SerializeField] Transform axeHit;
    Vector3 rightHandPos;

    [Header("AI")]
    [SerializeField] float stopRange = 0.8f;
    [SerializeField] float attackRate = 1f;

    float nextAttackTime; //cooldown
    bool isDead;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player").transform;
        rightHandPos = axeHit.localPosition;

        if (axeHitbox != null)
            axeHitbox.DisableHitbox();  // aseguramos que empieza desactivado
    }

    protected override void Move()
    {
        if (isDead) return;
        if (!player) return;

        Vector2 delta = player.position - transform.position;
        float dist = delta.magnitude;

        sr.flipX = delta.x < 0;

        Vector3 pos = rightHandPos;
        pos.x = sr.flipX ? -Mathf.Abs(pos.x) : Mathf.Abs(pos.x);
        axeHit.localPosition = pos;

        if (dist > stopRange)
        {
            rb.velocity = delta.normalized * data.moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;

            if (Time.time >= nextAttackTime)
            {
                animator.SetTrigger("Attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    public override void TakeDamage(int amount)
    {
        if (isDead) return;

        base.TakeDamage(amount);

        if (isDead) return;

        animator.Play("OrcHit", 0, 0f);
    }

    public override void Die()
    {
        if (isDead) return;
        isDead = true;

        base.Die();

        rb.velocity = Vector2.zero;
        rb.simulated = false;
        GetComponent<Collider2D>().enabled = false;

        animator.CrossFade("OrcDeath", 0f, 0, 0f);
    }

    // Este método se llama desde Animation Event
    public void EnableHitbox()
    {
        axeHitbox.EnableHitbox();
    }

    public void DisableHitbox()
    {
        axeHitbox.DisableHitbox();
    }

    public void DieOver() => Destroy(gameObject, 0.05f);
}
