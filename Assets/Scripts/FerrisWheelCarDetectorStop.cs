using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FerrisWheelCarDetectorStop : MonoBehaviour
{
    public FerrisSpin FerrisSpinReference;
    public int NumberOfRotations = 1;
    public BoxCollider CarDetectorStart;
    public GameObject SpawnPoint;

    private int rotationCounter = 0;
    private BoxCollider thisBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        thisBoxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayersCar")//stop the car
        {
            rotationCounter++;

            if (rotationCounter >= NumberOfRotations && NumberOfRotations != -1)
            {
                opencloseDoor doorControls = other.gameObject.GetComponentInChildren<opencloseDoor>();
                doorControls.OpenDoor();
                FerrisSpinReference.StopRotating();
                rotationCounter = 0;
                CarDetectorStart.enabled = true;
                thisBoxCollider.enabled = false;

                PlayerPrefs.SetString("SpawnPoint", SpawnPoint.transform.position.ToString());

                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "AmusementPark")//only reload the scene if we aren't in the amusement park scene
                    SceneManager.LoadSceneAsync(0);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    public void StopOnNextPass()
    {
        //this will cause the car to stop the next time it triggers the OnTriggerEnter event
        NumberOfRotations = 0;
    }
}
