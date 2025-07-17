using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealth : MonoBehaviour
{
    [Header("Ajustes de salud")]
    [SerializeField] int maxHP       = 5;
    [SerializeField] float iFrames   = 0.5f;   // invencibilidad tras recibir daño

    public int  hp;
    bool invulnerable;
    SpriteRenderer sr;

    void Awake()
    {
        hp = maxHP;
        sr = GetComponent<SpriteRenderer>();
    }

    /* ——— método que otros scripts llamarán ——— */
    public void TakeDamage(int dmg)
    {
        Debug.Log(invulnerable);
        if (invulnerable) return;

        hp -= dmg;
        if (hp <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(FlashAndInvulnerable());
    }

    /* ——— feedback visual + invencibilidad temporal ——— */
    System.Collections.IEnumerator FlashAndInvulnerable()
    {
        invulnerable = true;

        const int flashCount = 4;
        for (int i = 0; i < flashCount; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(iFrames / (flashCount * 2));
            sr.color = Color.white;
            yield return new WaitForSeconds(iFrames / (flashCount * 2));
        }

        invulnerable = false;
    }

    /* ——— qué pasa al morir ——— */
    void Die()
    {
        // Ejemplo: recargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // O mostrar menú de Game Over, desactivar controles, etc.
    }
}

