using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class InfoPosition : MonoBehaviour
{
    private Camera cam;
    [SerializeField] GameObject Sprite;
    SpriteRenderer Renderer;
    RectTransform m_RectTransform;
    // Start is called before the first frame update
    void Start()
    {
     
        // Renderer = Sprite.GetComponent<SpriteRenderer>();
        m_RectTransform = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
       //Rect pointsRect = EditorGUIUtility.PointsToPixels(m_RectTransform.rect);
          Renderer = Sprite.GetComponent<SpriteRenderer>();
        if (Renderer.flipX)
            m_RectTransform.position = new Vector3(Sprite.transform.position.x - 2.5f, Sprite.transform.position.y, Sprite.transform.position.z);

        else
            m_RectTransform.position = new Vector3(Sprite.transform.position.x + 2.5f, Sprite.transform.position.y, Sprite.transform.position.z);

        if (Sprite.transform.position.z > 2)
        {
            m_RectTransform.position = new Vector3(m_RectTransform.position.x, Sprite.transform.position.y- (m_RectTransform.rect.height /20), Sprite.transform.position.z );
        }

        else if (Sprite.transform.position.z < -3)
        {
            m_RectTransform.position = new Vector3(m_RectTransform.position.x, Sprite.transform.position.y + (m_RectTransform.rect.height/20), Sprite.transform.position.z );
        }
    }



}
