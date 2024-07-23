using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DecorateTiles : MonoBehaviour  //made for the upper grass tile set - it spreads random decoration tiles upon the grass field 
{
    [SerializeField] RuleTile tile;
    [SerializeField] int StartX;
    [SerializeField] int EndX;
    Tilemap tileMap;
    [SerializeField] int appearChance;
    // Start is called before the first frame update
    void Start()
    {

        tileMap = GetComponent<Tilemap>();

        for (int x = StartX; x <= EndX; x++)
        {
            for (int y = -5; y <= 5; y++)
            {
                
                if (Random.Range(0,100) <= appearChance)
                {
                    Vector3Int CurrenttileToCheck = new Vector3Int(x, y, 0);
                    tileMap.SetTile(CurrenttileToCheck, tile);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
