
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotateCam : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()  //alows the sprite will allways be turned towards the camera, nake it appear solidlly in the 3d world
    {
      
        transform.rotation = Camera.main.transform.rotation;
        
    }
}
