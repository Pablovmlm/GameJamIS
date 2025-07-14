using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    [Header("Referencias")]
    Transform player;
    [SerializeField] Collider2D axeHitbox;


    [Header("AI")]
    [SerializeField] float stopRange = 0.8f;
    [SerializeField] float attackRate = 1f;

    float nextAttackTime; //cooldown

    protected override void Awake()
    {
        base.Awake();                                // no olvides el base
        player = GameObject.FindWithTag("Player").transform;
    }

    protected override void Move()
    {
        if (!player) return;

        /* 1. Calcular vector y distancia */
        Vector2 delta = player.position - transform.position;
        float dist = delta.magnitude;

        /* 2. Siempre mira al jugador */
        sr.flipX = delta.x < 0;

        /* 3. Dentro / fuera del radio de parada */
        if (dist > stopRange)
        {
            // --- A) Todavía lejos: avanzar ---
            rb.velocity = delta.normalized * data.moveSpeed;
        }
        else
        {
            // --- B) Ya cerca: quedarse quieto y atacar ---
            rb.velocity = Vector2.zero;

            if (Time.time >= nextAttackTime)
            {
                animator.SetTrigger("Attack");        // reinicia el clip “Attack”
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }


    public override void TakeDamage(int amount)
    {
        if (animator.GetBool("Dead")) return;    // ya muerto

        base.TakeDamage(amount);                    // resta vida, chequea muerte

        if (!animator.GetBool("Dead"))           // si sigue vivo, reproduce Hit
            animator.Play("GoblinHit", 0, 0f);         // reinicia Hit incluso en bucle
    }


    /* ─────────────────── EVENTOS DE ANIMACIÓN ─────────────────── */
    // *Estos métodos se llaman desde el clip Attack mediante Animation Events*

    public void EnableHitbox() => axeHitbox.enabled = true;   // frame de impacto
    public void DisableHitbox() => axeHitbox.enabled = false;  // fin del swing

    // *Llamar desde el último frame del clip Death*
    public void DieOver()       => Destroy(gameObject, 0.05f);
}

