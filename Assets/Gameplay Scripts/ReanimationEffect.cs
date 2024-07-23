using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReanimationEffect : MonoBehaviour
{
    public GameObject target;
    // colidrSize = colidr.bounds.size
    private Collider Targetcolidr;
    Vector3 targetPos;
    Vector3 targetColidrSize;
    [SerializeField] float addPos;
    float livingTime = 0;
    // Start is called before the first frame update
    void Start()
    {

        Targetcolidr = target.GetComponent<Collider>();
        targetColidrSize = Targetcolidr.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        livingTime += Time.deltaTime;
        targetPos = target.transform.position;
        transform.position = new Vector3(targetPos.x-0.1f, targetPos.y - targetColidrSize.y / 2 + addPos, targetPos.z);

       if (livingTime>=1f)
           Destroy(gameObject);
    }
}
