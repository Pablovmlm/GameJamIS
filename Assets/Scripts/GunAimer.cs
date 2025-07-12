using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GunAimer : MonoBehaviour
{
    [SerializeField] private Transform      spritePivot;   // ← GunPivot, pivote del arma
    [SerializeField] private SpriteRenderer bodySprite;    // ← SpriteRenderer del cuerpo

    private Vector3 rightHandOffset;                       // desplazamiento local original

    void Awake()
    {
        if (spritePivot == null) spritePivot = transform;

        // Guarda la posición local que tiene el pivote cuando el personaje mira a la DERECHA
        rightHandOffset = spritePivot.localPosition;       
    }

    void Update()
    {
        /* 1. Calcular dirección al ratón y rotar el arma */
        Vector2  mouseWorld  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2  direction   = mouseWorld - (Vector2)spritePivot.position;
        float    angle       = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        spritePivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        /* 2. ¿Estamos mirando a la izquierda? */
        bool lookingLeft = direction.x < 0f;

        /* 3. Voltear SOLO la Y del arma (para que no quede boca abajo) */
        Vector3 scale = spritePivot.localScale;
        scale.y = lookingLeft ? -Mathf.Abs(scale.y) :  Mathf.Abs(scale.y);
        spritePivot.localScale = scale;

        /* 4. Mover el pivote al lado correcto del cuerpo */
        //  Si el cuerpo se voltea, ponemos la X negativa.
        Vector3 newOffset = rightHandOffset;
        newOffset.x = lookingLeft ? -rightHandOffset.x :  rightHandOffset.x;
        spritePivot.localPosition = newOffset;

        /* 5. Voltear el cuerpo */
        bodySprite.flipX = lookingLeft;
    }
}
