using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{

    public int shellsAllowed = 4;
    private ArrayList shellsArray;
    public GameObject shellObject1;
    public GameObject shellObject2;
    public GameObject shellObject3;
    public GameObject shellObject4;

    private float shellGetCooldown = 1;

    // Start is called before the first frame update
    void Start()
    {
        shellsArray = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Cooldown = "+shellGetCooldown);
        shellGetCooldown = shellGetCooldown - Time.deltaTime;
        if (shellGetCooldown <= 0)
        {
            Debug.Log("Setting shell pickup!");
            shellGetCooldown = 4;
            if (shellsArray.Count == 0)
            {
                Debug.Log("Adding shell 1");
                shellPickedUp(shellObject1);
            }
            else if (shellsArray.Count == 1)
            {
                Debug.Log("Adding shell 2");
                shellPickedUp(shellObject2);
            }
            else if (shellsArray.Count == 2)
            {
                Debug.Log("Adding shell 3");
                shellPickedUp(shellObject3);
            }
            else if (shellsArray.Count == 3)
            {
                Debug.Log("Adding shell 4");
                shellPickedUp(shellObject4);
            }
        }
    }

    public void shellPickedUp(GameObject shell)
    {
        Debug.Log("Picking up shell");        
        if (shellsArray.Count == 0)
        {
            //We don't have a shell yet so position it in a random rotation
            shell.transform.rotation = Quaternion.AngleAxis(Random.Range(0,360), Vector3.forward);
            //GameObject newShell = Instantiate(shell, new Vector3(0, 0, 0), shell.transform.rotation);
            //Destroy the old shell
            //Destroy(shell);
            //Add the new shell
            shellsArray.Add(shell); 
        }
        else if(shellsArray.Count < shellsAllowed)
        {
            //We don't have a shell yet so position it in a random rotation
            GameObject previousObject = (GameObject)shellsArray[shellsArray.Count - 1];
            shell.transform.rotation = previousObject.transform.rotation *= Quaternion.Euler(0, 0, 90); ;
            //GameObject newShell = Instantiate(shell, new Vector3(0, 0, 0), shell.transform.rotation);
            //Destroy the old shell
            //Destroy(shell);
            //Add the new shell
            shellsArray.Add(shell);
        }
    }
}
