using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData data;
    [SerializeField] private Transform  firePoint;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SpriteRenderer gunSprite;

    private float nextShotTime;
    private int ammoInClip;
    bool isReloading;


    void Awake()
    {
        if (gunSprite) gunSprite.sprite = data.sprite;
        ammoInClip = data.clipSize;
    }

    void Update()
    {
        if (Input.GetButton("Fire1")) TryShoot();
        if (Input.GetKeyDown(KeyCode.R)) StartCoroutine(ReloadRoutine());
    }

    void TryShoot()
    {
        if (isReloading) return;
        if (Time.time < nextShotTime) return;  // cadencia
        if (ammoInClip <= 0) { StartCoroutine(ReloadRoutine()); return; }

        Shoot();
        ammoInClip--;
        nextShotTime = Time.time + 1f / data.fireRate;
    }

    void Shoot()
    {
        
        // proyectil
        var bullet = Instantiate(data.bulletPrefab, firePoint.position, firePoint.rotation);
        var rb     = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * data.bulletSpeed;

        // inyecta daño
        bullet.GetComponent<Bullet>().damage = data.damage;

        // efectos
        if (data.muzzleFlashPrefab)
            Instantiate(data.muzzleFlashPrefab, firePoint.position, firePoint.rotation, firePoint);

        if (audioSource && data.shotSFX)
            audioSource.PlayOneShot(data.shotSFX);
    }

    IEnumerator ReloadRoutine()
    {
        isReloading = true;
        if (audioSource && data.reloadSFX)
            audioSource.PlayOneShot(data.reloadSFX);

        yield return new WaitForSeconds(data.reloadTime);
        ammoInClip = data.clipSize;
        isReloading = false;
    }

    /* —— getters para UI ——
       public float ClipPercent => (float)ammoInClip / data.clipSize;
       public string WeaponName => data.weaponName;
    */
}

