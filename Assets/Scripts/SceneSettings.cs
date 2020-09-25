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
        string spawnPointString = PlayerPrefs.GetString("SpawnPoint","");

        if (spawnPointString.Contains(","))
        {
            string[] vectors = spawnPointString.Split(',');
            SpawnPoint = new Vector3(int.Parse(vectors[0]), int.Parse(vectors[1]), int.Parse(vectors[2]));

            //place player
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = SpawnPoint;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
