using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{

    ////////////////
    // References //
    ////////////////

    Rigidbody2D Rigidbody2DRef;
    GameController GameControllerRef;

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
    private CrabController CurrentFightCrab;
    private float CrabFightTimer = 0.0f;
    private float CrabFightTime = 1.0f;

    ///////////
    // Setup //
    ///////////

    void Awake()
    {

        // Grab useful references
        Rigidbody2DRef = this.GetComponent<Rigidbody2D>();
        GameControllerRef = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

    }

    void Update()
    {

        // If the crab is fighting, run the the fight functionality
        if (CrabState == (int)CRAB_STATE.STATE_FIGHT)
        {
            RunCrabFight();
        }

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

    public void EnableFightMode(CrabController FightCrab)
    {

        // If the crab is not dead
        if (crabDefence > 0)
        {

            // Update the crab state
            CrabState = (int)CRAB_STATE.STATE_FIGHT;

            // Reset the fight timer
            CrabFightTimer = 0.0f;

            // Inidcate which crab is being fought
            CurrentFightCrab = FightCrab;

        }

    }

    public void DisableFightMode()
    {

        // If the crab has no HP
        if (crabDefence <= 0)
        {
            CrabState = (int)CRAB_STATE.STATE_DEAD;
        }
        else
        {
            CrabState = (int)CRAB_STATE.STATE_MOVE;
        }

    }

    // Function to take damage
    private void takeDamage(float argDMG)
    {

        // Take this much damage 
        crabDefence -= argDMG;

        // If the health goes below 0
        if (crabDefence <= 0)
        {

            // Indicate the crab is dead
            CrabState = (int)CRAB_STATE.STATE_DEAD;

            // Tell the game controller the crab has died
            GameControllerRef.CrabHasDied();

        }

    }

    private void RunCrabFight()
    {

        // Update the fight timer
        CrabFightTimer += Time.deltaTime;

        // If the crab is ready to arrack again
        if (CrabFightTimer >= CrabFightTime)
        {

            // Reset the fight timer
            CrabFightTimer = 0.0f;

            // Apply this much damage to the other crab
            CurrentFightCrab.takeDamage(crabAttack);

        }

    }

    // Create function to let the AI know what the state of the Crab is
    public int GetCrabMode()
    {
        return CrabState;
    }

}
