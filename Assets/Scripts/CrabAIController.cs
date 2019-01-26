using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAIController : MonoBehaviour
{

    //[HideInInspector]
    public bool CrabAIEnabled = true;
    private CrabController crabController;
    enum AI_STATE { STATE_MOVE = 0, STATE_FIGHT = 1, STATE_DEAD = 2 };
    private AI_STATE aiState = AI_STATE.STATE_MOVE;
    private GameObject[] crabsToFight;        
    public float fightThreshold = 3;

        // Awake function gets a refernce to crab controller for controling the crab
        void Awake()
    {
        crabController = gameObject.GetComponent<CrabController>();
        GameObject[] tempCrabsArray = GameObject.FindGameObjectsWithTag("crab");
        this.crabsToFight = new GameObject[tempCrabsArray.Length - 1];
        bool foundPlayerCrab = false;
        for (int i = 0; i < tempCrabsArray.Length; i++)
        {
            GameObject tempGameObject = tempCrabsArray[i];            
            if (tempGameObject != this.gameObject)
            {
                if (foundPlayerCrab)
                {
                    this.crabsToFight[i-1] = tempGameObject;
                }
                else
                {
                    this.crabsToFight[i] = tempGameObject;
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
                            Debug.Log("Crabs are now attacking!");
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
                            Debug.Log("No crabs nearby!");
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
                //Get the closest crab and move towards it
                float closestCrabDistance = 10000000000;
                GameObject closestCrab = null;
                foreach (GameObject crabObject in crabsToFight)
                {
                    if (crabObject.GetComponent<CrabAIController>().aiState != AI_STATE.STATE_DEAD)
                    {
                        if (Vector3.Distance(crabObject.transform.position, gameObject.transform.position) < closestCrabDistance)
                        {                            
                            //Debug.Log("New close crab!");
                            closestCrabDistance = Vector3.Distance(crabObject.transform.position, gameObject.transform.position);
                            closestCrab = crabObject;
                        }
                        break;
                    }
                }
                if (closestCrab != null)
                {                   

                    Debug.Log(transform.rotation.eulerAngles.z);
                    Vector3 myVector = closestCrab.transform.position - transform.position;
                    myVector = Vector3.Normalize(myVector);
                    float newAngle = Mathf.Atan2(myVector.y, myVector.x);
                    newAngle = newAngle + 220;
                    
                    Debug.Log("New angle = "+newAngle);
                    Debug.Log("Final angle = "+(transform.rotation.eulerAngles.z-newAngle));
                    float finalAngle = transform.rotation.eulerAngles.z - newAngle;                    
                    if (finalAngle < 0)
                    {
                        Debug.Log("Turning Left");
                        crabController.turnCrab(false);
                    }
                    else if (finalAngle > 0)
                    {
                        Debug.Log("Turning Right");
                        crabController.turnCrab(true);                        
                    }
                    else
                    {
                        crabController.moveCrab(true);
                    }                    
                }
            }
            else if (aiState == AI_STATE.STATE_FIGHT)
            {

            }
        }

    }

}
