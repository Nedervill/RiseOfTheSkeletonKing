using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeVolume : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string volumeType;
    public int  volume;
    [SerializeField] GameObject[] volumebars= new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        volume =  PlayerPrefs.GetInt(volumeType);
        float volumeToLog;
        if (volume != 0)
            volumeToLog = volume / 100f;
        else volumeToLog = 0.0001f; // log10 can never me 0
        audioMixer.SetFloat(volumeType, Mathf.Log10(volumeToLog) * 20f);
        updatevolumeBars();



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void decreaseVolume()
    {
        if (volume > 0)
        {
            volume -= 20;
            float volumeToLog;
                if (volume != 0) 
                    volumeToLog = volume/100f;
                   else volumeToLog = 0.0001f; // log10 can never me 0
            audioMixer.SetFloat(volumeType, Mathf.Log10(volumeToLog)*20f);
            PlayerPrefs.SetInt(volumeType, volume);
            updatevolumeBars();

        }
            

       
    }

    public void increaseVolume()
    {
        if (volume < 100)
            volume += 20;
        audioMixer.SetFloat(volumeType, Mathf.Log10(volume/100f) * 20f);
        PlayerPrefs.SetInt(volumeType, volume);
        updatevolumeBars();
    }

     void updatevolumeBars()
    {
        float barsToShow = volume/20;

        for (int i = 0; i < 5; i++)
        {
            if (i < barsToShow )
                volumebars[i].SetActive(true);
            else
                volumebars[i].SetActive(false);
        }
    }
}
