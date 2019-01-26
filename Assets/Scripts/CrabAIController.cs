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
    private float fightThreshold = 1.0f;

    public int CrabID = -1;

    // Awake function gets a refernce to crab controller for controling the crab
    void Awake()
    {
        crabController = gameObject.GetComponent<CrabController>();
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

            //If we're dead then we're dead
            //If we are close to another crab then we're fighting
            //If we are set to move then look for the closest crab and move
            if (aiState == AI_STATE.STATE_DEAD)
            {
                //Do nothing because we're dead
            }

            else if (aiState == AI_STATE.STATE_MOVE)
            {
                //If we're close to a crab then change the state to fight
                foreach (GameObject crabObject in crabsToFight)
                {
                    if (crabObject.GetComponent<CrabAIController>().aiState != AI_STATE.STATE_DEAD)
                    {
                        if (Vector3.Distance(crabObject.transform.position, gameObject.transform.position) <= this.fightThreshold)
                        {
                            // Debug.Log("Crabs are now attacking!");
                            aiState = AI_STATE.STATE_FIGHT;
                        }
                        break;
                    }
                }
            }


            else if (aiState == AI_STATE.STATE_FIGHT)
            {
                //If we're no longer close to a crab then change the state to move
                //If we're close to a crab then change the state to fight
                foreach (GameObject crabObject in crabsToFight)
                {
                    if (crabObject.GetComponent<CrabAIController>().aiState != AI_STATE.STATE_DEAD)
                    {
                        if (Vector3.Distance(crabObject.transform.position, gameObject.transform.position) > this.fightThreshold)
                        {
                            // Debug.Log("No crabs nearby!");
                            aiState = AI_STATE.STATE_MOVE;
                        }
                        break;
                    }
                }
            }

        }

    }

    // Update function runs crab decision on eveyr frame, can:
    // Move & Turn
    // Fight
    void Update()
    {

        // Complete actions if AI is running
        if (CrabAIEnabled)
        {

            //Debug.Log("Crab ai enabled YES");
            if (aiState == AI_STATE.STATE_MOVE)
            {

                // Get the closest crab and move towards it
                float closestCrabDistance = float.MaxValue;
                GameObject closestCrab = null;

                // Loop through the crabs
                foreach (GameObject crabObject in crabsToFight)
                {

                    // If the crab is not dead
                    if (crabObject.GetComponent<CrabAIController>().aiState != AI_STATE.STATE_DEAD)
                    {

                        // Get the distance to this crab
                        if (Vector3.Distance(crabObject.transform.position, gameObject.transform.position) < closestCrabDistance)
                        {
                            closestCrabDistance = Vector3.Distance(crabObject.transform.position, gameObject.transform.position);
                            closestCrab = crabObject;
                        }
                    }
                }

                // If there is not a closest crab
                if (closestCrab != null)
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
                    float CrabLeftAngle = convertAngle360(currentCrabAngle - closestCrabAngle);
                    float CrabRightAngle = convertAngle360(closestCrabAngle - currentCrabAngle);

                    // Debug info for just one crab
                    // if (CrabID == 0)
                    // {
                    //     Debug.Log(CrabID + ", currentCrabAngle: " + currentCrabAngle);
                    //     Debug.Log(CrabID + ", closestCrabAngle: " + closestCrabAngle);
                    //     Debug.Log(CrabID + ", CrabLeftAngle: " + CrabLeftAngle);
                    //     Debug.Log(CrabID + ", CrabRightAngle: " + CrabRightAngle);
                    // }

                    // If there is a great enough difference between the angles
                    // if (convertAngle360(CrabRightAngle - CrabLeftAngle) > 5.0f)
                    // {
                    // Determine if it is faster to go left or right
                    if (CrabLeftAngle > CrabRightAngle)
                    {
                        if (CrabRightAngle > 90.0f)
                        {
                            crabController.turnCrab(false);
                        }
                        else
                        {
                            crabController.turnCrab(true);
                        }
                        Debug.Log("hit a");
                        // crabController.moveCrab(false);
                    }
                    else if (CrabLeftAngle < CrabRightAngle)
                    {
                        // crabController.turnCrab(true);
                        // Debug.Log("hit b");
                        // crabController.moveCrab(false);
                    }
                    // }
                }
            }
            else if (aiState == AI_STATE.STATE_FIGHT)
            {

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

}
