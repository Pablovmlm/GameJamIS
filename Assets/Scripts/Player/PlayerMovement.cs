using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ajustes")]
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    public Animator animator;

    public float padding = 0.5f; // espacio de margen opcional

    private float minX, maxX, minY, maxY;

    void Start()
    {
        // Convertimos los bordes de la cámara a coordenadas del mundo
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minX = bottomLeft.x + padding;
        maxX = topRight.x - padding;
        minY = bottomLeft.y + padding;
        maxY = topRight.y - padding;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
    //Guardamos la el rigidbody en la variable rb
    void Awake() => rb = GetComponent<Rigidbody2D>();

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        //Hay que normalizar el vector para moverte a la misma velocidad al ir en diagonal
        movementInput = movementInput.normalized;


        float speed = movementInput.sqrMagnitude;
        animator.SetFloat("Speed", speed);
    }

    void FixedUpdate()
    {
        rb.velocity = movementInput * moveSpeed;
    }

}
