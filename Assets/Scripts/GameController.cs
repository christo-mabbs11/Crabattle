using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //////////////////////////////////
    // Game state relates varibales //
    //////////////////////////////////

    enum GAME_STATE { STATE_STARTUP = -1, STATE_MENU = 0, STATE_COUNTDOWN = 1, STATE_FIGHT = 2 };
    private int GameState = (int)GAME_STATE.STATE_STARTUP;

    ////////////////
    // References //
    ////////////////

    InputController InputControllerRef;

    CrabController[] Crabs;
    public Object CrabPrefab;

    void Awake()
    {

        // Grab refs
        InputControllerRef = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();

        // Setup intial game state on game start
        UpdateGameState();
    }

    void Update()
    {

        // If the user hits spacebar start the game
        if (Input.GetKey(KeyCode.Space))
        {

            // If we're in meu mode
            if (GameState == (int)GAME_STATE.STATE_MENU)
            {

                // Start that game!
                UpdateGameState();

            }

        }

    }

    void UpdateGameState()
    {

        // Update the next game state based on this game's state
        switch (GameState)
        {
            case (int)GAME_STATE.STATE_STARTUP:
                SetupMenu();
                break;

            case (int)GAME_STATE.STATE_MENU:
                SetupCountDown();
                break;

            case (int)GAME_STATE.STATE_COUNTDOWN:
                SetupFight();
                break;

            case (int)GAME_STATE.STATE_FIGHT:
                SetupMenu();
                break;
        }

    }

    ///////////////////////////////
    // Setup various game states //
    ///////////////////////////////

    private void SetupMenu()
    {

        GameState = (int)GAME_STATE.STATE_MENU;

    }

    private void SetupCountDown()
    {

        GameState = (int)GAME_STATE.STATE_COUNTDOWN;

        // Reference the crab spawns
        GameObject[] CrabSpawns = GameObject.FindGameObjectsWithTag("crabspawn");
        Crabs = new CrabController[CrabSpawns.Length];

        // Add all the crabs to the game (one for each spawn)
        for (int i1 = 0; i1 < CrabSpawns.Length; i1++)
        {

            // Add that crab to the game world and starts the game
            GameObject tempCrab = (GameObject)Instantiate(CrabPrefab, CrabSpawns[i1].transform.position, CrabSpawns[i1].transform.rotation);
            Crabs[i1] = tempCrab.GetComponent<CrabController>();

        }

        // Set this up with the inital game controller
        InputControllerRef.SetupCrabs(Crabs);

    }

    private void SetupFight()
    {

        GameState = (int)GAME_STATE.STATE_FIGHT;

    }

}
