using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnstuckButton : MonoBehaviour
{
    Button butt;
    // Start is called before the first frame update
    void Start()
    {
        butt = GetComponent<Button>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reEnable()
    {
        butt.interactable = false;
        butt.interactable = true;

    }
}
