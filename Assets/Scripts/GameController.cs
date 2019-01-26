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

    // Time for the countdown round, saved as seconds
    private float CountDownTimer = 0.0f;
    private float CountDownTotalTime = 10.0f;

    // Vairables related to the fight round
    private int DeadCrabs = 0;

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

        // If we're in meu mode
        if (GameState == (int)GAME_STATE.STATE_MENU)
        {

            // If the user hits spacebar start the game
            if (Input.GetKey(KeyCode.Space))
            {

                // Start that game!
                UpdateGameState();

            }

        }
        // If we're in the coundown mode
        else if (GameState == (int)GAME_STATE.STATE_COUNTDOWN)
        {

            // Count up the timer
            CountDownTimer += Time.deltaTime;

            // If the timer is completed
            if (CountDownTimer >= CountDownTotalTime)
            {

                // Update the game state (start the fight!!)
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

        // Setup the timer for the round
        CountDownTimer = 0.0f;

        // Allow the player to control the crabs
        InputControllerRef.PlayerCanControlCrabs = true;

    }

    private void SetupFight()
    {

        GameState = (int)GAME_STATE.STATE_FIGHT;

        // Stop the player from controlling the crabs
        InputControllerRef.PlayerCanControlCrabs = false;

        // Enable the AI controller on each crab to begin
        for (int i1 = 0; i1 < Crabs.Length; i1++)
        {

            Crabs[i1].GetComponent<CrabAIController>().CrabAIEnabled = true;

        }


    }

    ///////////////////////////////////
    // Fight state related variables //
    ///////////////////////////////////

    // Function for crabs to let the game controller know that they have died
    public void CrabHasDied()
    {
        // Indicate another crab has died
        DeadCrabs++;

        // If all crabs die the game state is updated (and all crabs are cleaned up etc)
        if (DeadCrabs >= Crabs.Length)
        {
            EndFightRound();
        }

    }

    private void EndFightRound()
    {

        // Room for embelishment, title screens, etc here..
        //

        // Delete all the crabs
        for (int i1 = 0; i1 < Crabs.Length; i1++)
        {

            Destroy(Crabs[i1]);

        }

        // Go back to the menu scene
        UpdateGameState();
    }

    //////////////////////////
    // UI related functions //
    //////////////////////////

    void OnGUI()
    {

        // Update the next game state based on this game's state
        switch (GameState)
        {
            case (int)GAME_STATE.STATE_MENU:
                MenuUI();
                break;

            case (int)GAME_STATE.STATE_COUNTDOWN:
                CountDownUI();
                break;

            case (int)GAME_STATE.STATE_FIGHT:
                FightUI();
                break;
        }

    }

    private void MenuUI()
    {

    }

    private void CountDownUI()
    {

        // Print the timer
        int timeInt = (int)Mathf.Round(CountDownTotalTime - CountDownTimer);    // Get the remaining time as an int
        string timeString = timeInt.ToString(); // Convert remaning time to string
        GUI.Label(new Rect(10, 10, 100, 20), timeString);   // Print string as GUI object


    }

    private void FightUI()
    {

    }

}
