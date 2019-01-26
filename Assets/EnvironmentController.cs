using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public GameObject wave;
    public GameObject shell;

    private ArrayList shellsArray;
    private GameObject[] shellsToSpawn;

    public int minShellSpawn = 3;
    public int maxShellSpawn = 30;

    // Start is called before the first frame update
    void Start()
    {
        shellsArray = new ArrayList();        
        /*
        foreach (Transform child in shell.transform)
        {
            //child is your child transform
            shellsToSpawn.Add(transform.gameObject);
        } 
        */
        this.shellsToSpawn = GameObject.FindGameObjectsWithTag("Shells");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Spawn shells inside the wave location
    public void spawnShells()
    {        
        Vector3 bottomRightCorner = new Vector3(wave.transform.position.x - (wave.GetComponent<SpriteRenderer>().bounds.size.x / 2), wave.transform.position.y - (wave.GetComponent<SpriteRenderer>().bounds.size.y / 2), wave.transform.position.z);
        Vector3 topLeftCorner = new Vector3(wave.transform.position.x + (wave.GetComponent<SpriteRenderer>().bounds.size.x / 2), wave.transform.position.y + (wave.GetComponent<SpriteRenderer>().bounds.size.y / 2), wave.transform.position.z);
        Debug.Log("Spawn at location = "+topLeftCorner.ToString() + " and " + bottomRightCorner.ToString());
        //Remove all of the old shells
        foreach (GameObject oldShell in shellsArray)
        {
            Debug.Log("Destroying Shell!");
            if (oldShell.tag.Equals("Shells"))
            {
                Destroy(oldShell);
            }
        }

        //Clear the shells array
        shellsArray = new ArrayList();
        int shellsToSpawn = getShellsToSpawn();
        Debug.Log("Shells to spawn = " +shellsToSpawn);
        //Spawn new shells
        
        for (int i = 0; i < shellsToSpawn; i++)
        {
            //Choose a random shell                                          
            //Random rotation
            //GameObject shellClone = Instantiate((GameObject)this.shellsToSpawn[Random.Range(0, this.shellsToSpawn.Length)], new Vector3(Random.Range(bottomRightCorner.x, topLeftCorner.x), Random.Range(bottomRightCorner.y, topLeftCorner.y), 0), Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
            GameObject shellClone = Instantiate((GameObject)this.shellsToSpawn[Random.Range(0, this.shellsToSpawn.Length)], new Vector3(Random.Range(bottomRightCorner.x, topLeftCorner.x), Random.Range(bottomRightCorner.y, topLeftCorner.y), 0), Quaternion.identity);
            //Change scale randomlly
            shellClone.transform.localScale = new Vector3(Random.Range(0.8f,1.2f), Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f));
            shellsArray.Add(shellClone);
        }
    }

    int getShellsToSpawn()
    {        
        return Random.Range(minShellSpawn,maxShellSpawn);         
    }
}
