using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerPan : MonoBehaviour
{
    [SerializeField] float originalspeed = 1f;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
      //  speed = originalspeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
    }
    private void OnEnable() // desides the panning type of the gloryKill camera
    {
        float RND = Random.value;
        if (RND < 0.3f)
            speed = originalspeed * -1;
        else if (RND < 0.6f)
            speed = originalspeed;
        else
            speed = 0;
    }
}
