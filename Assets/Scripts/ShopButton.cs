using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI stats;
    [SerializeField] public Image weaponPartImage;

    public void Setup(WeaponData data)
    {
        weaponPartImage.sprite = data.sprite;

        if (data.partType == PartType.Barrel)
        {
            stats.text = $"ClipSize {data.clipSize}\n" +
                         $"ReloadTime {data.reloadTime}\n" +
                         $"BulletSpeed {data.bulletSpeed}";
        }
        else
        {
            stats.text = $"Damage {data.damage}\n" +
                         $"FireRate {data.fireRate}";
        }
        
    }
}
