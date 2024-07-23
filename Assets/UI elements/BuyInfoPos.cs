using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyInfoPos : MonoBehaviour
{
    RectTransform m_RectTransform;
    // Start is called before the first frame update
    void Start()
    {
        // Renderer = Sprite.GetComponent<SpriteRenderer>();
        m_RectTransform = GetComponent<RectTransform>();

        if (m_RectTransform.position.z > 0)
        {
            m_RectTransform.position -= new Vector3(0, 0, 2);
        }

        else if (m_RectTransform.position.z < -2)
        {
            m_RectTransform.position += new Vector3(0,0, 4);
        }
    }

    // Update is called once per frame
    void Update()
    {
      

    }


}
