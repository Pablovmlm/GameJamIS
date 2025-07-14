using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] int damage = 1;          // cuánto quita este golpe

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))       // requiere que tu jugador tenga Tag "Player"
        {
            // other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
}
