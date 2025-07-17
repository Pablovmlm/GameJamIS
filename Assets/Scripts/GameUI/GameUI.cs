using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] TextMeshProUGUI currentBullets;

    [SerializeField] PlayerHealth player;
    [SerializeField] TextMeshProUGUI hp;
    
    // Update is called once per frame
    void Update()
    {
        currentBullets.text = $"{weapon.ammoInClip}";
        hp.text = $"{player.hp}";
    }
}
