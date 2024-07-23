using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    [SerializeField] float yStep = 0.2f;
    public Vector3 startingPos;
    bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = this.GetComponent<RectTransform>().localPosition;
        firstTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, this.GetComponent<RectTransform>().localPosition.y + yStep * Time.deltaTime, this.GetComponent<RectTransform>().localPosition.z);

    }
    private void OnEnable()
    {
        if (!firstTime) 
        this.GetComponent<RectTransform>().localPosition = startingPos;
    }
}
