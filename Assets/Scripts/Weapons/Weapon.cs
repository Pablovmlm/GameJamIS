using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Datos")]
    [SerializeField] WeaponAssembler assembler;   // ← arrastra aquí tu assembler
    [SerializeField] WeaponData fallbackData; // opcional (por si no hay assembler)

    // El WeaponData final que usa el arma en runtime
    WeaponData data;

    [Header("Referencias escena")]
    [SerializeField] Transform firePoint;
    [SerializeField] SpriteRenderer gunSprite;
    [SerializeField] AudioSource audioSource;

    float nextShotTime;
    public int ammoInClip;
    bool isReloading;

    /* ──────────────── INIT ──────────────── */
    void Start()
    {
        // 1) Genera el arma combinada o usa la de fallback
        data = assembler.Build(assembler.butt, null);

        // 2) Aplica sprite y stats
        if (gunSprite) gunSprite.sprite = data.sprite;
        ammoInClip = data.clipSize;
    }

    /* ──────────────── LOOP ──────────────── */
    void Update()
    {
        if (Input.GetButton("Fire1")) TryShoot();
        if (Input.GetKeyDown(KeyCode.R)) StartCoroutine(ReloadRoutine());
    }

    /* ─────────── DISPARO ─────────── */
    void TryShoot()
    {
        if (isReloading) return;
        if (Time.time < nextShotTime) return;
        if (ammoInClip <= 0) { StartCoroutine(ReloadRoutine()); return; }

        Shoot();
        ammoInClip--;
        nextShotTime = Time.time + 1f / data.fireRate;
    }

    void Shoot()
    {
        // proyectil
        var bullet = Instantiate(data.bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = firePoint.right * data.bulletSpeed;
        bullet.GetComponent<Bullet>().damage = data.damage;

        // efectos
        if (data.muzzleFlashPrefab)
            Instantiate(data.muzzleFlashPrefab, firePoint.position, firePoint.rotation, firePoint);

        if (audioSource && data.shotSFX)
            audioSource.PlayOneShot(data.shotSFX);
    }

    /* ─────────── RECARGA ─────────── */
    IEnumerator ReloadRoutine()
    {
        if (isReloading) yield break;
        isReloading = true;

        if (audioSource && data.reloadSFX)
            audioSource.PlayOneShot(data.reloadSFX);

        yield return new WaitForSeconds(data.reloadTime);
        ammoInClip = data.clipSize;
        isReloading = false;
    }

    public void RefreshWeapon()                  // <<— llamado tras la tienda
    {
        data = assembler.Build(assembler.butt, assembler.barrel);
        gunSprite.sprite = data.sprite;
        ammoInClip = Mathf.Min(ammoInClip, data.clipSize);
    }

    public WeaponData CurrentData => data;
    
}
