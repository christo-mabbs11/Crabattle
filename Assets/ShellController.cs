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
    public GameObject crabShellConnectionPoint;

    private float shellGetCooldown = 1;

    // Start is called before the first frame update
    void Start()
    {
        shellsArray = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Cooldown = "+shellGetCooldown);
        //shellGetCooldown = shellGetCooldown - Time.deltaTime;
        if (shellGetCooldown <= 0)
        {
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
            else
            {
                Debug.Log("Cannot add shell!");
            }
        }
    }

    public void shellPickedUp(GameObject shell)
    {
        if (this.shellsArray.Count == 0)
        {
            //shell.GetComponentInChildren<SpriteRenderer>().sortingOrder = 10 + this.shellsArray.Count;
            // Debug.Log("Picking up shell with count zero");
            //We don't have a shell yet so position it in a random rotation
            shell.transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
            shell.transform.position = crabShellConnectionPoint.transform.position;
            shell.transform.parent = crabShellConnectionPoint.transform;
            shell.GetComponentInChildren<SpriteRenderer>().sortingOrder = 101;
            CrabController tempCrabController = gameObject.GetComponent<CrabController>();
            ShellStatsController tempShellStatsController = shell.GetComponentInChildren<ShellStatsController>();
            tempCrabController.moveSpeed = tempCrabController.moveSpeed + tempShellStatsController.movementSpeed;
            tempCrabController.crabAttack = tempCrabController.crabAttack + tempShellStatsController.attackDamage;
            tempCrabController.crabDefence = tempCrabController.crabDefence + tempShellStatsController.defencePoints;
            //GameObject newShell = Instantiate(shell, new Vector3(0, 0, 0), shell.transform.rotation);
            //Destroy the old shell
            //Destroy(shell);
            //Add the new shell           
            this.shellsArray.Add(shell);
        }
        else if (shellsArray.Count < shellsAllowed)
        {
            // Debug.Log("Picking up shell with count non zero");
            //We don't have a shell yet so position it in a random rotation
            GameObject previousObject = (GameObject)shellsArray[shellsArray.Count - 1];
            Quaternion previousRotation = previousObject.transform.rotation;
            shell.transform.rotation = previousRotation *= Quaternion.Euler(0, 0, 90); ;
            shell.transform.position = crabShellConnectionPoint.transform.position;
            shell.transform.parent = crabShellConnectionPoint.transform;
            shell.GetComponentInChildren<SpriteRenderer>().sortingOrder = 101;
            //Apply the stats of this shell to the crab
            CrabController tempCrabController = gameObject.GetComponent<CrabController>();
            ShellStatsController tempShellStatsController = shell.GetComponentInChildren<ShellStatsController>();
            tempCrabController.moveSpeed = tempCrabController.moveSpeed + tempShellStatsController.movementSpeed;
            tempCrabController.crabAttack = tempCrabController.crabAttack + tempShellStatsController.attackDamage;
            tempCrabController.crabDefence = tempCrabController.crabDefence + tempShellStatsController.defencePoints;

            //GameObject newShell = Instantiate(shell, new Vector3(0, 0, 0), shell.transform.rotation);
            //Destroy the old shell
            //Destroy(shell);
            //Add the new shell
            this.shellsArray.Add(shell);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log("OnCollisionEnter2D");
        if (col.gameObject.tag.Equals("Shells"))
        {
            if (this.shellsArray.Count < shellsAllowed)
            {
                // Debug.Log("Touched a shell! ---------------------------------------------------------------");
                col.gameObject.tag = "Untagged";
                col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<ShellController>().shellPickedUp(col.gameObject);
            }
        }
    }
}
