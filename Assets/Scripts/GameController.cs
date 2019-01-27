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
    private float CountDownTotalTime = 30.0f;

    // Vairables related to the fight round
    private int DeadCrabs = 0;

    // Final game counter vars
    private bool FinalGameCountDownEnabled = false;
    private float FinalGameCountDownTimer = 0.0f;
    private float FinalGameCountDownTime = 4.0f;

    //Music
    public AudioClip clipPeaceful;
    public AudioClip clipBattle;

    [HideInInspector]
    public AudioSource audioPeaceful;
    [HideInInspector]
    public AudioSource audioBattle;

    // GUI biz
    GUIStyle GeneralGUIStyle, GeneralGUIStyle_medium, GeneralGUIStyle_smaller, GeneralGUIStyle_xsmaller, GeneralGUIStyle_xxsmaller, GeneralGUIStyle_outline, GeneralGUIStyle_medium_outline, GeneralGUIStyle_smaller_outline;
    Color TextColor;
    public Texture SplashImageTex;

    ////////////////
    // References //
    ////////////////

    InputController InputControllerRef;

    CrabController[] Crabs;
    public Object CrabPrefab;

    void Awake()
    {
        //Audio
        audioPeaceful = addAudio(clipPeaceful, true, true, 1.0f);
        audioBattle = addAudio(clipBattle, true, false, 1.0f);

        // Grab refs
        InputControllerRef = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();

        // Setup intial game state on game start
        UpdateGameState();

        TextColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        GeneralGUIStyle = new GUIStyle();
        GeneralGUIStyle.fontSize = (int)(Screen.width * 0.09f);
        GeneralGUIStyle.normal.textColor = TextColor;
        GeneralGUIStyle.font = (Font)Resources.Load<Font>("Fonts/Playtime");

        GeneralGUIStyle_medium = new GUIStyle();
        GeneralGUIStyle_medium.fontSize = (int)(Screen.width * 0.04f);
        GeneralGUIStyle_medium.normal.textColor = TextColor;
        GeneralGUIStyle_medium.font = (Font)Resources.Load<Font>("Fonts/Playtime");

        GeneralGUIStyle_smaller = new GUIStyle();
        GeneralGUIStyle_smaller.fontSize = (int)(Screen.width * 0.03f);
        GeneralGUIStyle_smaller.normal.textColor = TextColor;
        GeneralGUIStyle_smaller.font = (Font)Resources.Load<Font>("Fonts/Playtime");

        GeneralGUIStyle_xsmaller = new GUIStyle();
        GeneralGUIStyle_xsmaller.fontSize = (int)(Screen.width * 0.02f);
        GeneralGUIStyle_xsmaller.normal.textColor = TextColor;
        GeneralGUIStyle_xsmaller.font = (Font)Resources.Load<Font>("Fonts/Playtime");

        GeneralGUIStyle_xxsmaller = new GUIStyle();
        GeneralGUIStyle_xxsmaller.fontSize = (int)(Screen.width * 0.015f);
        GeneralGUIStyle_xxsmaller.normal.textColor = TextColor;
        GeneralGUIStyle_xxsmaller.font = (Font)Resources.Load<Font>("Fonts/Playtime");
    }

    void Update()
    {

        // If we're in meu mode
        if (GameState == (int)GAME_STATE.STATE_MENU)
        {
            if (!audioPeaceful.isPlaying)
            {
                audioPeaceful.Play();
                audioBattle.Stop();
            }

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
            if (!audioPeaceful.isPlaying)
            {
                audioPeaceful.Play();
                audioBattle.Stop();
            }
            // Count up the timer
            CountDownTimer += Time.deltaTime;

            // If the timer is completed
            if (CountDownTimer >= CountDownTotalTime)
            {

                // Update the game state (start the fight!!)
                UpdateGameState();

            }

        }
        else if (GameState == (int)GAME_STATE.STATE_FIGHT)
        {
            if (!audioBattle.isPlaying)
            {
                audioPeaceful.Stop();
                audioBattle.Play();
            }
            if (FinalGameCountDownEnabled)
            {
                FinalGameCountDownTimer += Time.deltaTime;

                if (FinalGameCountDownTimer > FinalGameCountDownTime)
                {

                    FinalGameCountDownEnabled = false;

                    // End the round
                    EndFightRound();

                }
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
            Crabs[i1].GetComponent<CrabAIController>().CrabID = i1;
        }

        // Init all crabs
        for (int i1 = 0; i1 < CrabSpawns.Length; i1++)
        {
            Crabs[i1].GetComponent<CrabAIController>().PostAllCrabsCreateInit();
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

        // Reset the no@ of dead crabs this round
        DeadCrabs = 0;

        // Stop the player from controlling the crabs
        InputControllerRef.PlayerCanControlCrabs = false;

        // Enable the AI controller on each crab to begin
        for (int i1 = 0; i1 < Crabs.Length; i1++)
        {

            Crabs[i1].GetComponent<CrabAIController>().SetCrabAIEnabled(true);

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

        // If all but one (or more) crabs die the game state is updated (and all crabs are cleaned up etc)
        if (DeadCrabs >= (Crabs.Length - 1))
        {

            FinalGameCountDownEnabled = true;

            // Reset the timers
            FinalGameCountDownTimer = 0.0f;
        }

    }

    private void EndFightRound()
    {

        // Delete all the crabs
        for (int i1 = 0; i1 < Crabs.Length; i1++)
        {

            Destroy(Crabs[i1].gameObject);

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
                if (FinalGameCountDownEnabled)
                {
                    WinnerUI();
                }
                else
                {
                    FightUI();
                }
                break;
        }

    }

    private void MenuUI()
    {

        float imgWidth = Screen.width * 0.33f;
        float imgHeight = imgWidth * 375.0f / 477.0f;

        GUI.DrawTexture(new Rect(Screen.width * 0.33f, Screen.height * 0.1f, imgWidth, imgHeight), SplashImageTex);
        GUI.Label(new Rect(Screen.width * 0.43f, Screen.height * 0.65f, 0.0f, 0.0f), "BASED ON TRUE EVENTS", GeneralGUIStyle_xsmaller);
        GUI.Label(new Rect(Screen.width * 0.33f, Screen.height * 0.9f, 0.0f, 0.0f), "(PRESS SPACE TO START)", GeneralGUIStyle_smaller);

    }

    private void CountDownUI()
    {

        // Print the timer
        int timeInt = (int)Mathf.Round(CountDownTotalTime - CountDownTimer);    // Get the remaining time as an int
        string timeString = timeInt.ToString(); // Convert remaning time to string

        GUI.Label(new Rect(Screen.width * 0.41f, Screen.height * 0.015f, 0.0f, 0.0f), "COLLECT THE SHELLS", GeneralGUIStyle_xsmaller);
        GUI.Label(new Rect(Screen.width * 0.44f, Screen.height * 0.067f, 0.0f, 0.0f), "BUILD YOUR HOME", GeneralGUIStyle_xxsmaller);
        if (timeInt > 9)
        {
            GUI.Label(new Rect(Screen.width * 0.477f, Screen.height * 0.1f, 0.0f, 0.0f), timeString, GeneralGUIStyle_medium);
        }
        else
        {
            GUI.Label(new Rect(Screen.width * 0.4855f, Screen.height * 0.1f, 0.0f, 0.0f), timeString, GeneralGUIStyle_medium);
        }

    }

    private void FightUI()
    {
        GUI.Label(new Rect(Screen.width * 0.385f, Screen.height * 0.0f, 0.0f, 0.0f), "FIGHT", GeneralGUIStyle);
    }

    private void WinnerUI()
    {
        GUI.Label(new Rect(Screen.width * 0.28f, Screen.height * 0.1f, 0.0f, 0.0f), "YOU HAVE THE MOST FABULOUS HOME!", GeneralGUIStyle_smaller);
    }

    //clip:AudioClip, loop: boolean, playAwake: boolean, vol: float): AudioSource
    AudioSource addAudio(AudioClip clip, bool loop, bool playAwake, float volume)
    {
        AudioSource newAudio = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = volume;
        return newAudio;
    }

}
