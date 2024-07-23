using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowHighScore : MonoBehaviour
{
    [SerializeField] string scoreName;
    int theScore;
    TextMeshProUGUI theText;
    // Start is called before the first frame update
    void Start()
    {
        theText = GetComponent<TextMeshProUGUI>();
        theScore = PlayerPrefs.GetInt(scoreName);
    }

    // Update is called once per frame
    void Update()
    {
        theText.text =""+theScore;
    }
}
