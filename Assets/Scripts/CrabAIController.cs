using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAIController : MonoBehaviour
{

    //[HideInInspector]
    private bool CrabAIEnabled = false;
    private CrabController crabController;
    enum AI_STATE { STATE_MOVE = 0, STATE_FIGHT = 1, STATE_DEAD = 2 };
    private AI_STATE aiState = AI_STATE.STATE_MOVE;
    private GameObject[] crabsToFight;
    private float fightThreshold = 1.2f;
    private battleAnimationScript battleCloudSprite;
    private DeathAnimationScript deathCloudSprite;

    GameObject closestCrab = null;

    public int CrabID = -1;

    // Awake function gets a refernce to crab controller for controling the crab
    void Awake()
    {
        crabController = gameObject.GetComponent<CrabController>();
        battleCloudSprite = gameObject.GetComponentInChildren<battleAnimationScript>();
        deathCloudSprite = gameObject.GetComponentInChildren<DeathAnimationScript>();
        
    }

    public void PostAllCrabsCreateInit()
    {
        GameObject[] tempCrabsArray = GameObject.FindGameObjectsWithTag("crab");
        crabsToFight = new GameObject[tempCrabsArray.Length - 1];

        bool foundPlayerCrab = false;
        for (int i = 0; i < tempCrabsArray.Length; i++)
        {
            GameObject tempGameObject = tempCrabsArray[i];

            if (tempGameObject != this.gameObject)
            {
                if (foundPlayerCrab)
                {
                    crabsToFight[i - 1] = tempGameObject;
                }
                else
                {
                    crabsToFight[i] = tempGameObject;
                }
            }
            else
            {
                foundPlayerCrab = true;
            }
        }
    }

    // Fixed update function for crab decision making
    void FixedUpdate()
    {

        // Make decisions if AI is running
        if (CrabAIEnabled)
        {

            if (aiState == AI_STATE.STATE_MOVE)
            {

                // Get the closest crab and move towards it
                float closestCrabDistance = float.MaxValue;
                foreach (GameObject crabObject in crabsToFight)
                {

                    // If the crab is not dead
                    if (crabObject.GetComponent<CrabController>().GetCrabMode() != 2)
                    {

                        // Get the distance to this crab
                        if (Vector3.Distance(crabObject.transform.position, gameObject.transform.position) < closestCrabDistance)
                        {
                            closestCrabDistance = Vector3.Distance(crabObject.transform.position, gameObject.transform.position);
                            closestCrab = crabObject;
                        }
                    }
                }

                // If we're close to a crab then change the state to fight
                if (Vector3.Distance(closestCrab.transform.position, gameObject.transform.position) <= fightThreshold)
                {
                    // Indicate we're in fight mode
                    if (closestCrab.GetComponentInChildren<CrabAIController>().aiState != AI_STATE.STATE_DEAD)
                    {
                        aiState = AI_STATE.STATE_FIGHT;
                        // Put crab into fight mode
                        crabController.EnableFightMode(closestCrab.GetComponent<CrabController>());
                    }
                }
            }


            else if (aiState == AI_STATE.STATE_FIGHT)
            {

                // If there is no closest crab (debug checker, should not occur)
                if (closestCrab == null)
                {
                    // Go into move mode
                    aiState = AI_STATE.STATE_MOVE;
                }

                // Otheriwise..
                else
                {

                    // If we're no longer close to a crab or the closest crab is dead then change the state to move
                    if (closestCrab.GetComponent<CrabController>().GetCrabMode() == 2 || Vector3.Distance(closestCrab.transform.position, gameObject.transform.position) > fightThreshold)
                    {
                        // Indicate we're in move mode
                        aiState = AI_STATE.STATE_MOVE;

                        // Get crab out of fight mode
                        crabController.DisableFightMode();

                    }
                }
            }

        }

    }

    void Update()
    {

        if (aiState == AI_STATE.STATE_FIGHT)
        {
            battleCloudSprite.enableSprite();
        }
        else
        {
            battleCloudSprite.disableSprite();
        }

        if (aiState == AI_STATE.STATE_DEAD)
        {
            deathCloudSprite.enableSprite();
        }
        else
        {
            deathCloudSprite.disableSprite();
        }

        // Complete actions if AI is running
        if (CrabAIEnabled)
        {

            if (aiState == AI_STATE.STATE_MOVE)
            {

                // If there is a closest crab
                if (closestCrab != null)
                {

                    // If the closest crab is not dead
                    if (closestCrab.GetComponent<CrabController>().GetCrabMode() != 2)
                    {

                        // Find the current angle of the crab
                        float currentCrabAngle = transform.rotation.eulerAngles.z;

                        // find the angle to the clostest crab
                        Vector3 myVector = closestCrab.transform.position - transform.position;
                        float closestCrabAngle = Mathf.Atan2(myVector.y, myVector.x) * 180 / Mathf.PI;

                        // Adjust the angles to be within the realms of 360 degrees
                        currentCrabAngle = convertAngle360(currentCrabAngle);
                        closestCrabAngle = convertAngle360(closestCrabAngle);

                        // Calculate the angles of the crab going left and right
                        float CrabTurnInfo = convertAngle360(currentCrabAngle - closestCrabAngle);

                        // Turn the crab based on what size is closer
                        if (CrabTurnInfo > 90.0f && CrabTurnInfo <= 270.0f)
                        {

                            // Move the crab
                            crabController.moveCrab(true);

                            // Turn the crab if they need to
                            // This is potentially buggy
                            // if ((CrabTurnInfo >= 178.0f && CrabTurnInfo <= 182.0f))
                            // {

                            if (CrabTurnInfo >= 180.0f)
                            {
                                crabController.turnCrab(true);

                            }
                            else
                            {
                                crabController.turnCrab(false);

                            }
                            // }


                        }
                        else
                        {

                            // Move the crab regardless
                            crabController.moveCrab(false);

                            // Turn the crab if they need to
                            // This is potentially buggy
                            // if (CrabTurnInfo <= 2.0f || CrabTurnInfo <= 358.0f)
                            // {

                            if (CrabTurnInfo >= 180.0f)
                            {
                                crabController.turnCrab(false);

                            }
                            else
                            {
                                crabController.turnCrab(true);

                            }
                            // }

                        }

                    }

                }

            }
        }

    }

    private float convertAngle360(float argAngle)
    {

        if (argAngle > 360.0f)
        {
            argAngle -= 360.0f;

        }
        else if (argAngle < 0.0f)
        {
            argAngle += 360.0f;

        }

        return argAngle;

    }

    /////////////////////////
    // Getters and setters //
    /////////////////////////

    public bool GetCrabAIEnabled()
    {

        return CrabAIEnabled;

    }

    public void SetCrabAIEnabled(bool argCrabEnabled)
    {

        CrabAIEnabled = argCrabEnabled;

    }

    public void KillCrabAI()
    {
        aiState = AI_STATE.STATE_DEAD;
        CrabAIEnabled = false;
    }

}
