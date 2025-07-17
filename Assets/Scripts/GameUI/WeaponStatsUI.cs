using TMPro; // Solo si usas TextMeshPro
using UnityEngine;

public class WeaponStatsUI : MonoBehaviour
{
    [SerializeField] Weapon weapon;               // arrástralo desde la escena
    [SerializeField] TextMeshProUGUI statsText;   // arrastra aquí el Text TMP

    void Update()
    {
        if (weapon == null || weapon.CurrentData == null) return;

        var data = weapon.CurrentData;

        statsText.text =
                         $"Damage: {data.damage}\n" +
                         $"FireRate: {data.fireRate:0.00}\n" +
                         $"ClipSize: {data.clipSize}\n" +
                         $"ReloadTime: {data.reloadTime:0.00}s\n" +
                         $"BulletSpeed: {data.bulletSpeed}";
    }
}
