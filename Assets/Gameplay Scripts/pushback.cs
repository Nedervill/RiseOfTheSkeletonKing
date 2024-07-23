using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 dir = ( transform.position- collision.gameObject.transform.position).normalized;
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3 (dir.x,1f,dir.y) * 100);
    }
}
