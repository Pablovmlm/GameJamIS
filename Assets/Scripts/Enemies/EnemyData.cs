using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemies/Enemy Data", fileName = "New Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;   // “Slime Azul”, “Goblin”, “Boss1”
    public int    maxHealth;   // Vida máxima
    public float  moveSpeed;   // Velocidad básica en unidades/segundo
    public int    damage;      // Cuánto quita al golpear
    public Sprite sprite;      // Sprite por defecto que mostrará el enemigo
}

