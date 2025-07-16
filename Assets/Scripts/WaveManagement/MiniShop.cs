using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class MiniShop : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Weapon weapon;          // tu arma
    [SerializeField] WeaponAssembler assembler;       // ensamblador
    [SerializeField] GameObject shopPanel;       // panel raíz
    [SerializeField] Transform grid;            // contenedor botones
    [SerializeField] Button partButtonPrefab;

    [Header("Todas las piezas posibles")]
    WeaponData[] allParts;           // lista completa
    
    const int OFFER_COUNT = 2;                        // nº de piezas que se ofrecen

    void Awake()
    {
        allParts = Resources.LoadAll<WeaponData>("Weapons");
    }

    /* ---- llamado por WaveManager cuando termina la oleada ---- */
    public void OpenShop()
    {
        ClearGrid();

        // 1) Elige 2 piezas distintas al azar
        List<WeaponData> pool = new List<WeaponData>(allParts);
        for (int i = 0; i < OFFER_COUNT && pool.Count > 0; i++)
        {
            int idx = Random.Range(0, pool.Count);
            WeaponData part = pool[idx];
            pool.RemoveAt(idx);

            // 2) Crea botón
            Button b = Instantiate(partButtonPrefab, grid);
            b.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = part.weaponName;

            // 3) Captura la referencia local para el listener
            b.onClick.AddListener(() => BuyPart(part));
        }

        shopPanel.SetActive(true);
    }

    void BuyPart(WeaponData part)
    {
        bool equipped = assembler.EquipPart(part);
        if (equipped)
        {
            weapon.RefreshWeapon();              // reconstruye stats + sprite
        }

        shopPanel.SetActive(false);              // oculta UI
        WaveManager.Instance.StartNextWave();    // arranca siguiente oleada
    }

    void ClearGrid()
    {
        foreach (Transform c in grid)
            Destroy(c.gameObject);
    }
}
