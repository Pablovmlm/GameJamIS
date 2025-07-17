using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class MiniShop : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Weapon weapon;          // tu arma
    [SerializeField] WeaponAssembler assembler;       // ensamblador
    [SerializeField] public GameObject shopPanel;       // panel raíz
    [SerializeField] Transform grid;            // contenedor botones
    [SerializeField] Button partButtonPrefab;
    [SerializeField] WaveManager wave;
    [SerializeField] ShopButton shopButton;

    [Header("Todas las piezas posibles")]
    WeaponData[] allParts;           // lista completa

    List<WeaponData> usedParts = new List<WeaponData>(); //lista con las usadas ya

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
        // Filtrado según la oleada
        List<WeaponData> pool;
        if (wave.currentWave == 1)
        {
            pool = new List<WeaponData>();
            foreach (var part in allParts)
            {
                if (part.partType == PartType.Barrel && !usedParts.Contains(part))
                    pool.Add(part);
            }
        }
        else
        {
            pool = new List<WeaponData>();
            foreach (var part in allParts)
            {
                if (!usedParts.Contains(part))
                    pool.Add(part);
            }
        }


        for (int i = 0; i < OFFER_COUNT && pool.Count > 0; i++)
        {
            
            int idx = Random.Range(0, pool.Count);
            WeaponData part = pool[idx];
            pool.RemoveAt(idx);

            // 2) Crea botón
            Button b = Instantiate(partButtonPrefab, grid);
            ShopButton shopButton = b.GetComponentInChildren<ShopButton>();
            shopButton.Setup(part);

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
        //Añadimos la pieza a usadas
        usedParts.Add(part);
        WaveManager.Instance.StartNextWave();    // arranca siguiente oleada
    }

    void ClearGrid()
    {
        foreach (Transform c in grid)
            Destroy(c.gameObject);
    }
}
