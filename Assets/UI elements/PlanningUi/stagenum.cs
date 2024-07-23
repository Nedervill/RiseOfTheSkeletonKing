using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class stagenum : MonoBehaviour
{
    //shows te round number at the start of the round and makes it go away after

    TextMeshProUGUI theText;
    [SerializeField] float TimeToExist = 3;
    [SerializeField] float disspearTime = 0.2f;
    [SerializeField] float dissaperaAmount = 0.2f;
    float dissapearDelta = 0;
    float currentAlfa = 1;
    // Start is called before the first frame update
    void Start()
    {
        theText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        theText.text = "round " + GameStatus.RoundNum;
        if (Time.timeSinceLevelLoad >= TimeToExist)
        {
            dissapearDelta += Time.deltaTime;
            if (dissapearDelta >= disspearTime)
            {
                dissapearDelta = 0;
                currentAlfa -= dissaperaAmount;
                theText.color = new Color(1, 1, 1, currentAlfa);
            }
            if (currentAlfa<=0)
                Destroy(gameObject);
        }
    }
}
