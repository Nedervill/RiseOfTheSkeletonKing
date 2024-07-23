
using UnityEngine;
using UnityEngine.UI;
public class TwoCirclesAreFilled : MonoBehaviour
{
    //the detector of that both tutorial circles are filled with the needed units 
    [SerializeField] TargetIntheCircle TargetIntheCircle;
    [SerializeField] TargetIntheCircle TargetIntheCircle2;
    Button butt;
    // Start is called before the first frame update
    void Start()
    {
        butt = GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {

        butt.interactable = (TargetIntheCircle.targetInTheCircle && TargetIntheCircle2.targetInTheCircle);
    }
}
