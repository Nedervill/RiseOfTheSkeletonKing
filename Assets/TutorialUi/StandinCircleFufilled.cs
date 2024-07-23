
using UnityEngine;
using UnityEngine.UI;

public class StandinCircleFufilled : MonoBehaviour
{
    //the component ensures that the circle is full with a needed unit
    [SerializeField] TargetIntheCircle TargetIntheCircle;
    Button butt;
    // Start is called before the first frame update
    void Start()
    {
        butt = GetComponent<Button>();
        
    }

    // Update is called once per frame
    void Update()
    {

        butt.interactable = TargetIntheCircle.targetInTheCircle;
    }
}
