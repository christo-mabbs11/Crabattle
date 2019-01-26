using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAIController : MonoBehaviour
{

    [HideInInspector]
    public bool CrabAIEnabled = false;

    // Awake function gets a refernce to crab controller for controling the crab
    void Awake()
    {

    }

    // Fixed update function for crab decision making
    void FixedUpdate()
    {

        // Make decisions if AI is running
        if (CrabAIEnabled)
        {

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

        }

    }

}
