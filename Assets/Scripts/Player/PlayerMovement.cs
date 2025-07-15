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
