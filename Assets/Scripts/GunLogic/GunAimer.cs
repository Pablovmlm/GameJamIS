using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GunAimer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bodySprite;    // arrástrale el sprite del cuerpo
    private Vector3 rightHandOffset;                       // posición local cuando mira a la derecha

    void Awake()
    {
        rightHandOffset = transform.localPosition;         // Gun está en la mano DERECHA
    }

    void Update()
    {
        // 1. Dirección al ratón
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir        = mouseWorld - (Vector2)transform.position;
        float   angle      = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // 2. ¿Miramos a la izquierda?
        bool lookingLeft = dir.x < 0f;

        // 3. Voltear SOLO el eje Y del arma
        Vector3 sc = transform.localScale;
        sc.y = lookingLeft ? -Mathf.Abs(sc.y) :  Mathf.Abs(sc.y);
        transform.localScale = sc;

        // 4. Colocar el arma al otro costado
        Vector3 pos = rightHandOffset;
        pos.x = lookingLeft ? -rightHandOffset.x : rightHandOffset.x;
        transform.localPosition = pos;

        // 5. Voltear cuerpo
        bodySprite.flipX = lookingLeft;
    }
}

