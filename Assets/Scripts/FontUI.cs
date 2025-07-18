using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontUI : MonoBehaviour
{
    public TMP_FontAsset newFont;

    void Start()
    {
        TMP_Text[] allTexts = FindObjectsOfType<TMP_Text>(true); // true = incluye objetos desactivados

        foreach (var txt in allTexts)
        {
            txt.font = newFont;
        }
    }
}
