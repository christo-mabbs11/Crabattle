using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{

    ////////////////
    // References //
    ////////////////

    Rigidbody2D Rigidbody2DRef;

    /////////////////////////////
    // State related variables //
    /////////////////////////////

    enum CRAB_STATE { STATE_MOVE = 0, STATE_FIGHT = 1, STATE_DEAD = 2 };
    private int CrabState = (int)CRAB_STATE.STATE_MOVE;

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

    ///////////////////////////////
    // Fighting related variables /
    ///////////////////////////////

    // Function to take damage
    public void takeDamage(float argDMG)
    {

        // Take this much damage 
        crabDefence -= argDMG;

        // If the health goes below 0
        if (crabDefence <= 0)
        {

            // Indicate the crab is dead

            // Tell the game controller the crab has died


        }

    }

    // Function to give damage
    public void sendDamage(CrabController argCrab)
    {

        // Apply this much damage to the other crab
        argCrab.takeDamage(crabAttack);

    }

}
