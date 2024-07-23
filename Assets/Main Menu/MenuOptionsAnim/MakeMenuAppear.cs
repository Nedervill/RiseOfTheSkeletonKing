using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMenuAppear : MonoBehaviour
{
    // makes the "retro style" appeance and disapeance of menus 

    [SerializeField] CanvasGroup CanvasGrp;
    public bool appearing = true;
       public float appearFrames = 0.2f;
       public float appearSteps = 0.2f;
    float dealay;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (appearing)
        {
            if (CanvasGrp.alpha < 1)
            {
                dealay += Time.unscaledDeltaTime;
                if (dealay > appearFrames)
                {
                    dealay = 0;
                    CanvasGrp.alpha += appearSteps;
                }

            }
        }
        else
        if (CanvasGrp.alpha > 0)
        {
            dealay += Time.unscaledDeltaTime * 3;
            if (dealay > appearFrames)
            {
                dealay = 0;
                CanvasGrp.alpha -= appearSteps;
            }

        }

        if ((!appearing) && (CanvasGrp.alpha <=0))
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        CanvasGrp.alpha = 0;
    }
}
