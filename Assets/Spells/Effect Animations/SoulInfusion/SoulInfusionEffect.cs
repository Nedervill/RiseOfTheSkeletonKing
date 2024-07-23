using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulInfusionEffect : MonoBehaviour
{
    public GameObject target;
    float livingTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        livingTime += Time.deltaTime;
        transform.position = target.transform.position;
        // transform.position = new Vector3(targetPos.x, targetPos.y+ targetColidrSize.y/2, targetPos.z);

        if (livingTime >= 0.5f)
            Destroy(gameObject);
    }
}
