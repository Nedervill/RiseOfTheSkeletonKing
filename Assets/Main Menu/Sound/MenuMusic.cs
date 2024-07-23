using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float volumeChange = 0.01f;
    public bool levelEnd = false;

    AudioSource BGM;
    void Start()
    {
        BGM = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (levelEnd)
            BGM.volume -= volumeChange;

    }

    public void fadeMusic()
    {
        levelEnd = true;
    }
}
