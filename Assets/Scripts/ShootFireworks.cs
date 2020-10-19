using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShootFireworks : MonoBehaviour
{
    public GameObject TheArrow;
    public GameObject[] Fireworks; //prefabs to clone
    public GameObject LauncherLocationObject;
    public int FireworksCount = 5;//how many fireworks get set off
    public int FireworksScale = 4;
    public float FireworksDelayMin = .2f;
    public float FireworksDelayMax = 1f;
    int howManyFlashes = 6;
    int flashCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //turn on the arrow
        if (TheArrow)
        {
            flashArrow();

            //shoot the fireworks
            StartFireworks();
        }
    }

    private void flashArrow()
    {
        TheArrow.SetActive(true);

        Invoke("turnOffArrow", 1);
    }

    private void turnOffArrow()
    {
        TheArrow.SetActive(false);

        flashCount++;

        if (flashCount < howManyFlashes)
            Invoke("flashArrow", 1);
        else
            flashCount = 0;
    }

    public void StartFireworks()
    {
        for(int x = 0; x < FireworksCount; x++)
        {
            Invoke("launchFireworks", Random.Range(FireworksDelayMin, FireworksDelayMax));
        }
    }

    private void launchFireworks()
    {
        if(Fireworks.Length > 0)
        {
            GameObject newObject = Instantiate(Fireworks[Random.Range(0, Fireworks.Length - 1)],LauncherLocationObject.transform.position, Quaternion.identity);
            newObject.transform.localScale = new Vector3(FireworksScale, FireworksScale, FireworksScale);
        }
    }
}
