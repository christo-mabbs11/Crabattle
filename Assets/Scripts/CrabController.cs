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

    private float turnSpeed = 40.0f;
    private float moveSpeed = 1.0f;

    /////////////////////////////
    // Fight related variables //
    /////////////////////////////

    private float crabAttack = 5.0f;
    private float crabDefence = 20.0f;

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
        Rigidbody2DRef.MoveRotation(Rigidbody2DRef.rotation + turnSpeed * speedDirect * Time.deltaTime);


    }

    public void moveCrab(bool moveDirection)
    {

        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, Rigidbody2DRef.rotation) * Vector2.right);

        // Apply crab direction
        float moveDirect = 1.0f;
        if (moveDirection)
        {
            moveDirect *= -1.0f;
        }

        // Apply rotation
        Rigidbody2DRef.MovePosition(Rigidbody2DRef.position + dir * moveSpeed * moveDirect * Time.fixedDeltaTime);

    }

}
