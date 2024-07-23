using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillcamSwitch : MonoBehaviour
{
    public bool KillCamActive;
    TextMeshProUGUI buttText;
    Button butt;

    // Start is called before the first frame update
    void Start()
    {
        butt = GetComponent<Button>();
        if (PlayerPrefs.GetInt("killCamOn") == 0)
            KillCamActive = false;
        else
            KillCamActive = true;

        buttText = GetComponent<TextMeshProUGUI>();
        if (KillCamActive)
            buttText.text = "On";
        else 
            buttText.text = "Off";


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchTheCam()
    {
        if (KillCamActive)
        {
            KillCamActive = false;
            buttText.text = "Off";
            PlayerPrefs.SetInt("killCamOn", 0);
            butt.interactable = false;
            butt.interactable = true;
        }
        else
        {
            KillCamActive = true;
            buttText.text = "On";
            PlayerPrefs.SetInt("killCamOn", 1);
            butt.interactable = false;
            butt.interactable = true;

        }

    }
}
