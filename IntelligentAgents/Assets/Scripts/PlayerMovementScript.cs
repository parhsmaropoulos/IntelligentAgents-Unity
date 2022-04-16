﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2   moveDirection;

    // Update is called once per frame
    void Update()
    {
        // ProcessInputs
        ProcessInputs();
        
    }

    void FixedUpdate()
    {
        // Psysics Calculations
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;  // TODO come back to this
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed,  moveDirection.y * moveSpeed);
    }
}
