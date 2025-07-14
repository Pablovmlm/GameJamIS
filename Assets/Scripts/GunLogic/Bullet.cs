using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public int damage; // lo rellena Weapon.Shoot
    public float lifeTime = 2f;
    bool hasHit;

    void Start() => Destroy(gameObject, lifeTime);

    void OnTriggerEnter2D(Collider2D col)
    {
        if (hasHit) return;

        // Detectamos si el objeto colisionado tiene un componente Enemy
        Enemy enemy = col.GetComponent<Enemy>();
        if (enemy != null)
        {
            hasHit = true;
            enemy.TakeDamage(damage); // Le restamos vida al enemigo
            Destroy(gameObject);       // Destruye la bala
        }
        else
        {
            // Opcional: destruye la bala si choca con otra cosa, por ejemplo paredes
            Destroy(gameObject);
        }
    }
}

