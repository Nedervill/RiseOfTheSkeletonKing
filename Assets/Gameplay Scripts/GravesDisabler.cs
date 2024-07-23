using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravesDisabler : MonoBehaviour
{
    [SerializeField] GameObject PlanningModeUI;
    [SerializeField] List<GameObject> graves = new List<GameObject>();
    [SerializeField] Animator cameraAnim;
    [SerializeField] Animator crystalAnim;
    [SerializeField] GameObject GraveyardText;
    int totalCount;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        // if there is no corpses to reanimate, dsiable anything reanimation-related
        totalCount = 0;
       
        for (int i = 0; i < graves.Count; i++)
        {
            totalCount += graves[i].GetComponent<animationButton>().getCurrentCorpseNum();
            if (graves[i].GetComponent<animationButton>().getCurrentCorpseNum() == 0)
            {
                graves[i].SetActive(false);
            }
        }
        if (totalCount == 0)
        {
            GraveyardText.SetActive(false);
            PlanningModeUI.GetComponent<Reanimation>().enabled = false;
            cameraAnim.SetBool("Reanimation", false);
            crystalAnim.SetBool("Reanimation", false);
            

        }
    }
}
