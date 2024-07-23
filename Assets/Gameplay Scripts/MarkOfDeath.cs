using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class MarkOfDeathEffect : MonoBehaviour
{
    public GameObject target;
    // colidrSize = colidr.bounds.size
  
  
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
       // transform.position = new Vector3(targetPos.x, targetPos.y+ targetColidrSize.y/2, targetPos.z);
      
        if (!target.GetComponent<Defence>().alive)
            Destroy(gameObject);
    }
}
