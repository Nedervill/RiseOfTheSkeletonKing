using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    Animator blackScreenAnimator;
    public bool blacknow = false;
    // Start is called before the first frame update
    void Start()
    {
        blackScreenAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoOut()
    {
            Time.timeScale = 1;
            blackScreenAnimator.Play("Out");
    }

    public void disableMe()
    {
        gameObject.SetActive(false);
    }

    public void reportBlack()
    {
        blacknow = true;
    }

}
