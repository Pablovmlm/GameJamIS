using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Weapons/Weapon Data", fileName = "New Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("General")]
    public string weaponName = "Pistol";
    public Sprite sprite;
    public int damage = 1;
    public float fireRate = 5f;      // disparos por segundo
    public int clipSize = 8;
    public float reloadTime = 1.2f;
    public PartType partType;

    [Header("Projectile")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;

    [Header("Effects")]
    public AudioClip shotSFX;
    public AudioClip reloadSFX;
    public GameObject muzzleFlashPrefab;
}

public enum PartType
{
    Butt,
    Barrel
}

