using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    [HideInInspector]
    public Vector3 SpawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
//#if !UNITY_EDITOR
//        GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>().enabled = false;
//        Debug.Log("NOT Unity Editor");
//#endif

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "AmusementPark")//only move player to spawnPoint if it is the amusement park
        {
            string spawnPointString = PlayerPrefs.GetString("SpawnPoint", "").Replace("(", "").Replace(")", "");

            if (spawnPointString.Contains(","))
            {
                string[] vectors = spawnPointString.Split(',');
                SpawnPoint = new Vector3(float.Parse(vectors[0]), float.Parse(vectors[1]), float.Parse(vectors[2]));

                //place player
                GameObject player = GameObject.FindWithTag("Player");
                player.transform.position = SpawnPoint;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
