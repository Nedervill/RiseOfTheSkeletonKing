using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI theText;
    // public GameStatus gameStatus;
    int score;
    int currentShownScore;
    void Start()
    {
        currentShownScore = GameStatus.currentScore;
        theText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        score = GameStatus.currentScore;
        if (currentShownScore > score)
            currentShownScore--;

        else if (currentShownScore < score)
            currentShownScore++;

        theText.text = "score: "+ currentShownScore.ToString();
    }
}
