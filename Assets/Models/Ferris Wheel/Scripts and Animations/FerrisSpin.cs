using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisSpin : MonoBehaviour {
    public float Speed = 10f;
    public bool RotateOnStart = false;

    public bool IsRotating
    {
        get { return rotating; }
    }

    private bool rotating = false;

    // Use this for initialization
    void Start () {
        if (RotateOnStart)
            StartRotating();
	}
	
	// Update is called once per frame
	void Update () {
        if(rotating)
            transform.Rotate( new Vector3(0, 0, -1f), Speed * Time.deltaTime);

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Ride_FerrisWheel")//only control the ferris wheel while riding it
        {
            if (Input.GetButtonDown("Fire1") || Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") >= 0.9f || Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") >= 0.9f)
            {
                if (!rotating)
                    StartRotating();
                else
                    StopRotating();

                Debug.Log("Fire1 TRIGGER PRESS DETECTED");
            }
        }
    }

    public void StopRotating()
    {
        rotating = false;
    }

    public void StartRotating()
    {
        opencloseDoor doorControls = gameObject.GetComponentInChildren<opencloseDoor>();
        doorControls.OpenDoor();

        rotating = true;
    }
}
