
using UnityEngine;
using static RoundStatus;


public class Reanimation : MonoBehaviour
{
    // Start is called before the first frame update
    // public RoundStatus roundStatus;
    public Animator cameraAnim;
    [SerializeField] Animator crystalAnim;
    // public GameObject GraveyardText;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter() //checks if mouse hovers over the "graves" area and makes it appear or dissapear
    {
        if (RoundStatus.currentgameStatus == CurrrentGameStatus.Planning)
        {

         //   GraveyardText.SetActive(false);
            cameraAnim.SetBool("Reanimation", true);
            crystalAnim.SetBool("Reanimation", true);

        }
    }
        private void OnMouseExit()
    {
        //   GraveyardText.SetActive(true);
        cameraAnim.SetBool("Reanimation", false);
        crystalAnim.SetBool("Reanimation", false);
    }

}