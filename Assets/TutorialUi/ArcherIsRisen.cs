
using UnityEngine;

public class ArcherIsRisen : MonoBehaviour
{
    //the script allows to go forward once a new unit is reanimated
    [SerializeField] TheLists theLists;
    [SerializeField] GameObject nextSlide;
    [SerializeField] GameObject seplls;
    [SerializeField] GameObject Arrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (theLists.allies.Count > 2)
        {
            nextSlide.SetActive(true);
            seplls.SetActive(true);
            Arrow.SetActive(false);
        }
        
    }
}
