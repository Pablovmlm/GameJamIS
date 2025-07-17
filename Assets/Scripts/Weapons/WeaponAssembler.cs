using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAssembler : MonoBehaviour
{

    [Header("GunParts")]
    public WeaponData butt;
    public WeaponData barrel;

    /// <summary>Intenta equipar la pieza. Devuelve TRUE si se colocó.</summary>
    public bool EquipPart(WeaponData nuevaParte)
    {
        switch (nuevaParte.partType)
        {
            case PartType.Butt:
                butt = nuevaParte;
                return true;

            case PartType.Barrel:
                barrel = nuevaParte;
                return true;

            default:
                return false;
        }
    }


    /// <summary>Devuelve un WeaponData fusionado con las piezas actuales.</summary>
    public WeaponData Build(WeaponData butt, WeaponData barrel)
    {
        WeaponData w = ScriptableObject.CreateInstance<WeaponData>();
        w.hideFlags = HideFlags.HideAndDontSave;

        if (barrel != null)
        {
            w.damage = butt.damage + barrel.damage;
            w.fireRate = butt.fireRate + barrel.fireRate;
            w.clipSize = butt.clipSize + barrel.clipSize;
            w.reloadTime = butt.reloadTime + barrel.reloadTime;

            w.sprite = CombineSprites(butt.sprite, barrel.sprite);


            //Esto sólo lo tiene barrel
            w.bulletPrefab = barrel.bulletPrefab;
            w.bulletSpeed = barrel.bulletSpeed;
            w.shotSFX = barrel.shotSFX;
            w.reloadSFX = barrel.reloadSFX;
            w.muzzleFlashPrefab = barrel.muzzleFlashPrefab;
        }
        else
        {
            w.damage = butt.damage;
            w.fireRate = butt.fireRate;
            w.clipSize = butt.clipSize;
            w.reloadTime = butt.reloadTime;

            w.sprite = butt.sprite;

            //Esto sólo lo tiene barrel
            w.bulletPrefab = butt.bulletPrefab;
            w.bulletSpeed = butt.bulletSpeed;
            w.shotSFX = butt.shotSFX;
            w.reloadSFX = butt.reloadSFX;
            w.muzzleFlashPrefab = butt.muzzleFlashPrefab;
        }


        // // === Aplica bonus de la culata ===
        // if (butt)
        // {
        //     w.damage += butt.damage;
        //     w.fireRate += butt.fireRate;
        //     w.clipSize += butt.clipSize;
        //     w.reloadTime += butt.reloadTime;
        //     w.sprite = CombineSprites(butt.sprite, baseData.sprite);
        //     // sprite: combinar si lo necesitas
        // }

        // // === Aplica bonus del cañón ===
        // if (barrel)
        // {
        //     w.damage += barrel.damage;
        //     w.fireRate += barrel.fireRate;
        //     w.clipSize += barrel.clipSize;
        //     w.reloadTime += barrel.reloadTime;
        //     w.sprite = CombineSprites(baseData.sprite, barrel.sprite);

        //     w.bulletPrefab = barrel.bulletPrefab;
        //     w.bulletSpeed = barrel.bulletSpeed;
        //     w.shotSFX = barrel.shotSFX;
        //     w.muzzleFlashPrefab = barrel.muzzleFlashPrefab;
        // }

        return w;
    }


    public static Sprite CombineSprites(Sprite stock, Sprite barrel, float overlapPx = 4f)
    {
        // 1. Convierte a texturas legibles
        Texture2D texA = stock.texture;
        Texture2D texB = barrel.texture;

        int ppu = (int)stock.pixelsPerUnit;
        int width = (int)(stock.rect.width + barrel.rect.width - overlapPx);
        int height = Mathf.Max((int)stock.rect.height, (int)barrel.rect.height);

        Texture2D result = new Texture2D(width, height, TextureFormat.RGBA32, false);
        result.filterMode = FilterMode.Point;
        result.wrapMode = TextureWrapMode.Clamp;

        // Copia píxeles de la culata
        Color[] pixelsA = texA.GetPixels((int)stock.rect.x, (int)stock.rect.y,
                                         (int)stock.rect.width, (int)stock.rect.height);
        result.SetPixels(0, 0, (int)stock.rect.width, (int)stock.rect.height, pixelsA);

        // Copia píxeles del cañón desplazados
        int offsetX = (int)(stock.rect.width - overlapPx);
        Color[] pixelsB = texB.GetPixels((int)barrel.rect.x, (int)barrel.rect.y,
                                         (int)barrel.rect.width, (int)barrel.rect.height);
        result.SetPixels(offsetX, 0, (int)barrel.rect.width, (int)barrel.rect.height, pixelsB);

        result.Apply();

        return Sprite.Create(result,
                             new Rect(0, 0, width, height),
                             new Vector2(0.5f, 0.5f),      // pivot centrado
                             ppu);
    }
}
