using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    Transform player;

    protected override void Awake()
    {
        base.Awake();                                // no olvides el base
        player = GameObject.FindWithTag("Player").transform;
    }

    protected override void Move()
    {
        if (!player) return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = dir * data.moveSpeed;          // perseguir
    }
}

