using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateMissile : MonoBehaviour
{
    [SerializeField] GameObject spriteObject;
    GameObject target;
    quaternion rotateTW;
    float angle;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<missileDmg>().target;
       
        float a = transform.position.z - target.transform.position.z;
        float b = transform.position.x - target.transform.position.x;
         angle = math.atan2(a, b) * Mathf.Rad2Deg; //the ange needs to be added so the projectile willl point towards the target

        if (transform.position.x > target.transform.position.x)
            angle -= 180f;
        else if (transform.position.x < target.transform.position.x)
            angle += 180f;
        else
            angle -= 90f; //the case of missle falls form the sky
        // spriteObject.transform.localRotation = Quaternion.Euler(0,0,angle);
    }

    // Update is called once per frame
    void Update()
    {
       

        transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(0, 0, angle);
      


    }
}
