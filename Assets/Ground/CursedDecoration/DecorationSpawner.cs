using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DecorationSpawner : MonoBehaviour
{
    [SerializeField] int minX;
    [SerializeField] int maxX;
    [SerializeField] int minZ;
    [SerializeField] int maxZ;
    [SerializeField] int objectsToSpawn;
    [SerializeField] GameObject decoObject;
    
    int orderInlayer;
    // Start is called before the first frame update
    void Start()
    {
        // on start run, the function will generate and put some provided decorations upon the entered field values
        Vector3 pos;
        orderInlayer = gameObject.GetComponent<TilemapRenderer>().sortingOrder+1;

        for (int i = 0; i < objectsToSpawn; i++)
        {
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            pos = new Vector3(x, 0, z);

         
          
           

            GameObject newDeco = Instantiate(decoObject, pos, Quaternion.identity);
           // newDeco.GetComponent<SpriteRenderer>().sortingOrder = orderInlayer;
            newDeco.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
