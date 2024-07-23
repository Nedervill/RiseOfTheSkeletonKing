using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargeOnKillcam : MonoBehaviour
{
    Vector3 ogScale;
    // Start is called before the first frame update
    void Start()
    {
        ogScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {  // make the decoration bigger if killcam is playing, to prevent wierd size differences between the decorations and the units 
        if (Camera.main.name == "GloryKillCam")
            transform.localScale = ogScale*1.5f;
        else 
            transform.localScale = ogScale;


    }
}
