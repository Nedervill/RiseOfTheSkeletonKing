
using UnityEngine;

public class DissapearOnWin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (winOrLose.victory)
            gameObject.SetActive(false);
    }
}
