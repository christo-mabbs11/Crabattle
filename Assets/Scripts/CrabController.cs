using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{

    ////////////////
    // References //
    ////////////////

    Rigidbody2D Rigidbody2DRef;

    ////////////////////////////////
    // Movement related variables //
    ////////////////////////////////

    public float turnSpeed = 1.0f;
    public float moveSpeed = 1.0f;

    ///////////
    // Setup //
    ///////////

    void Awake()
    {

        // Grab useful references
        Rigidbody2DRef = this.GetComponent<Rigidbody2D>();

    }

    ///////////////////
    // Crab movement //
    ///////////////////

    public void turnCrab(bool turnDirection)
    {

        // Apply crab direction
        float speedDirect = 1.0f;
        if (turnDirection)
        {
            speedDirect *= -1.0f;
        }

        // Apply rotation
        Rigidbody2DRef.MoveRotation(turnSpeed * speedDirect);


    }

    public void moveCrab(bool moveDirection)
    {

        // Apply crab direction
        float moveDirect = 1.0f;
        if (moveDirection)
        {
            moveDirect *= -1.0f;
        }

        // Apply rotation
        Rigidbody2DRef.MoveRotation(moveSpeed * moveDirect);

    }

}
