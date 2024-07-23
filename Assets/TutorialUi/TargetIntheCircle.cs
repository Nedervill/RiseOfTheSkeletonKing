
using UnityEngine;

public class TargetIntheCircle : MonoBehaviour
{
    //the detector of a tutorial circle that a target unit is within it
    [SerializeField] GameObject target;
    public bool targetInTheCircle = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
   
        

   
    private void OnTriggerEnter(Collider collision)
    {
            if (collision.gameObject == target) { targetInTheCircle = true; }
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == target) { targetInTheCircle = false; }
    }

}
