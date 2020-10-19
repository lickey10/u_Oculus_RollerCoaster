using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheelPlayerDetector : MonoBehaviour
{
    public FerrisWheelCarDetectorStop ferrisWheelCarDetectorStop;
    public FerrisSpin ferrisSpin;

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
            ferrisWheelCarDetectorStop.StopOnNextPass();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")//
        {
            PlayerPrefs.SetString("SpawnPoint", "");
            ferrisSpin.StartRotating();
        }
    }
}
