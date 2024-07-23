using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class GloryKill : MonoBehaviour
{
    [SerializeField] float gloryTime = 3;
    float endT;
    public bool GKHappening = false;
    public GameObject MainCam;
    public GameObject GloryCam;
    public float yRotationToGoTo = 0;
    public float currentMAinCameraY;
     float middleRoatationY = 0;
    float startingRotationY;
    [SerializeField] float maxSpeed = 5;
    [SerializeField] float turnSpeed = 0;
    [SerializeField] bool maxSpeedReached = false;
    quaternion startingRotation;
    quaternion finalRotation;

    float timeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        startingRotation = MainCam.transform.rotation;
        finalRotation = startingRotation;
        currentMAinCameraY = MainCam.transform.localRotation.y;
    }

    // Update is called once per frame
    void Update()
    {

        MainCam.transform.rotation = Quaternion.Slerp(startingRotation, finalRotation, timeCount); //rotates the regular camer by a tiny bit when a unit dies. the direction is depeends on the side of thee died unit
        timeCount = timeCount + Time.deltaTime;
        currentMAinCameraY = MainCam.transform.rotation.y;
     
        if (GKHappening && (Time.timeSinceLevelLoad > endT))  //disables the glory kill cam on time over and goes back to normal view
        {
            GloryKillShotEnd();
        }

    }

    public void GloryKillShotStart(GameObject Obj)
    {
        if (!GKHappening)  //checking if we are already somehow in a glorykill mode 
        {
            endT = Time.timeSinceLevelLoad + gloryTime;
            Time.timeScale = 0.4f;

            GKHappening = true;
            MainCam.SetActive(false);
            GloryCam.SetActive(true);

            GloryCam.transform.position = Obj.transform.position; //setting up the starting camera position
            GloryCam.transform.position += Vector3.back * 4;
            GloryCam.transform.position += Vector3.up * 1.5f;
            if (GloryCam.transform.position.z < -8.5) // not going back that far so the "rocks" of the bottom screen will gat into the frame and break the illusion
                GloryCam.transform.position = new Vector3(GloryCam.transform.position.x, GloryCam.transform.position.y, -8.5f);
            GloryCam.transform.LookAt(Obj.transform.position);
        }
    }

    void GloryKillShotEnd()
    {
        MainCam.SetActive(true);
        GloryCam.SetActive(false);
        Time.timeScale = 1f;
        GKHappening = false;
    }

    public void newDeath(string who,float addY) //called upon unit death, new needed camera nagle will be calculated
    {
        timeCount = 0;
      
        startingRotation = MainCam.transform.rotation;
       
        if (who == "Ally")
          finalRotation = startingRotation * Quaternion.Euler(0f, MainCam.transform.rotation.y- addY, 0f);
        else
            finalRotation = startingRotation * Quaternion.Euler(0f, MainCam.transform.rotation.y + addY, 0f);
    }

    public void cameraTurn()
    {
        MainCam.transform.rotation = Quaternion.Slerp(startingRotation, finalRotation, timeCount);
        timeCount = timeCount + Time.deltaTime;

    }
}
