using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollerCoasterPlayerDetector : MonoBehaviour
{
    public int SceneIndexToLoad;
    public GameObject SpawnPoint;

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
        if (other.gameObject.tag == "Player")//stop ferris wheel on next pass
        {
            //ferrisWheelCarDetectorStop.StopOnNextPass();
            PlayerPrefs.SetString("SpawnPoint", SpawnPoint.transform.position.ToString());

            SceneManager.LoadSceneAsync(SceneIndexToLoad);
        }
    }
}
