using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public abstract class Enemy : MonoBehaviour
{
    public EnemyData data;            // Se arrastra un asset aquí

    public Animator animator;

    protected Rigidbody2D rb;       // Cachés de componentes
    protected SpriteRenderer sr;
    protected int currentHealth;

    /* ——— Ciclo de vida ——— */
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = data.sprite;     // Usa el sprite del asset
        currentHealth = data.maxHealth;  // Vida inicial
    }

    protected virtual void Update()
    {
        Move();                           // Delegamos el movimiento concreto
    }

    /* ——— Lógica genérica ——— */
    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        animator.SetTrigger("Hit");
        if (currentHealth <= 0) Die();
    }


    public event System.Action OnDeath;
    
    protected virtual void Die()
    {
        OnDeath?.Invoke();
        // animator.SetInteger("HP", currentHealth);
        Destroy(gameObject);              // Aquí pondrías partículas, drop, etc.
    }



    /* ——— Para que cada sub‑tipo haga lo suyo ——— */
    protected abstract void Move();       // Obligatorio sobrescribir
}
