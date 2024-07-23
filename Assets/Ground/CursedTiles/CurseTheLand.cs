using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class CurseTheLand : MonoBehaviour
{
    [SerializeField] RuleTile cursedTile;
    
    Tilemap cursedTileMap;
   public  Tilemap decorationTilemap;
    [SerializeField] TheLists lists;
    List<GameObject> undead;
    [SerializeField] bool clear = false; //developer input eather this tilemap should optimize it's view by cutting "tails" or not
    int largestX = 0;
    float timeToCurse = 3;
    float timeSinceCursed=0;
    List<Vector3Int> circles2Draw = new List<Vector3Int>();

    // Start is called before the first frame update
    void Start()
    {
        undead = lists.allies;
        cursedTileMap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()   //will spread the curses land under the undead units that crost the half-batllefield 
    {
        if (RoundStatus.currentgameStatus != RoundStatus.CurrrentGameStatus.Planning) // if "charge" was pressed and planning mode is over
        {

      
        for (int i = 0; i < undead.Count; i++) 
        {
          if (undead[i].transform.position.x >= 0) // x= 0 is a middle of the battleground
            {
                //creating a 3x3 matrix of tiles around the unit and changes the tile appearnce into cursed 

                Vector3Int StartingtileToCurse = new Vector3Int((int)undead[i].transform.position.x - 1, (int)undead[i].transform.position.z -2, 0);
                for (int j = 0; j < 3; j++) 
                    for (int k = 0; k < 3; k++)
                    {
                        Vector3Int CurrenttileToCurse = new Vector3Int(StartingtileToCurse.x  + j, StartingtileToCurse.y + k, 0);
                        if (cursedTile != null)
                            cursedTileMap.SetTile(CurrenttileToCurse, cursedTile);
                        else
                            {
                                // the case of cursedTile = null means its the upper grass llayer of tiles. It willl just remove the proper tiles and do some other visual ipdates below
                                cursedTileMap.SetTile(CurrenttileToCurse, null);
                                findAndCorruptDeco(CurrenttileToCurse);                          //any "curseble decorations in the same area will switch their sprites into "cursed" ones
                                if (decorationTilemap != null)                                   //decoration tilemap is the most upper tile made of decoration tilees made proceduarlly. they should be removed upoun the underneath grass tile is removed
                                    decorationTilemap.SetTile(CurrenttileToCurse, cursedTile);
                                
                            }
                           
                        if (CurrenttileToCurse.x > largestX)
                            largestX = CurrenttileToCurse.x;
                    }

                // cursedTileMap.SetTile(tileToCurse, cursedTile);
                // cursedTileMap.SetTilesBlock()

            }
        }
        }

        if (clear)
            tailCut();

        if (RoundStatus.currentgameStatus == RoundStatus.CurrrentGameStatus.Aftermath)
            if (largestX > 0)
        {
            timeSinceCursed += Time.deltaTime;
            if (timeSinceCursed >= timeToCurse)
            {
                curseSpread();
                timeSinceCursed = 0;
            }
        }
      

    }

 /*   private void OnDrawGizmos()
    {
        for (int i = 0; i < circles2Draw.Count; i++)
        {
            Gizmos.DrawWireSphere(circles2Draw[i], 1.5f);


        }
    }*/


    void curseSpread()
    {
        List<Vector3Int> tilesToCurse = new List<Vector3Int>();
        Vector3Int Ystep = new Vector3Int(0, 1, 0);
        for (int x = -1; x <= largestX; x++)
        {
            for (int y = -6; y <= 5; y++)
            {
                Vector3Int CurrenttileToCheck = new Vector3Int(x, y, 0);
                if (cursedTileMap.GetTile(CurrenttileToCheck) == cursedTile)
                {
                    if (cursedTileMap.GetTile(CurrenttileToCheck + Vector3Int.right) != cursedTile)
                        tilesToCurse.Add(CurrenttileToCheck + Ystep);
                    if (cursedTileMap.GetTile(CurrenttileToCheck-Vector3Int.right) != cursedTile)
                        tilesToCurse.Add(CurrenttileToCheck + Ystep);
                    if (cursedTileMap.GetTile(CurrenttileToCheck + Ystep) != cursedTile)
                        tilesToCurse.Add(CurrenttileToCheck + Ystep);
                    if (cursedTileMap.GetTile(CurrenttileToCheck - Ystep) != cursedTile)
                        tilesToCurse.Add(CurrenttileToCheck - Ystep);
                }
            }
        }

        for (int j = 0; j < tilesToCurse.Count; j++)
        {
            cursedTileMap.SetTile(tilesToCurse[j], cursedTile);
            findAndCorruptDeco(tilesToCurse[j]);
            if (decorationTilemap!=null)
              decorationTilemap.SetTile(tilesToCurse[j], cursedTile);
        }
            
    }

    void tailCut() // due not every RuleTile case is covered, this function will clear any cases that are not currently supported by simplyfying the map form
    {
        

        Vector3Int Ystep = new Vector3Int(0, 1, 0);
        for (int x = -1; x <= largestX; x++)
        {
            for (int y = -6; y <= 5; y++)
            {
                Vector3Int CurrenttileToCheck = new Vector3Int(x, y, 0);
                if (cursedTileMap.GetTile(CurrenttileToCheck) != null)
                {
                    int notNullCounter = 0;
                    if (cursedTileMap.GetTile(CurrenttileToCheck + Vector3Int.right) != null)
                        notNullCounter++;
                    if (cursedTileMap.GetTile(CurrenttileToCheck - Vector3Int.right) != null)
                        notNullCounter++;
                    if (cursedTileMap.GetTile(CurrenttileToCheck + Ystep) != null)
                        notNullCounter++;
                    if (cursedTileMap.GetTile(CurrenttileToCheck - Ystep) != null)
                        notNullCounter++;

                    if (notNullCounter <= 1)// if the tile is connected to only one tile of its type or neather , it will be destroyed 
                    {
                        cursedTileMap.SetTile(CurrenttileToCheck, null);
                        findAndCorruptDeco(CurrenttileToCheck);     //any "curseble decorations in the same area will switch their sprites into "cursed" ones
                        if (decorationTilemap != null)    //decoration tilemap is the most upper tile made of decoration tilees made proceduarlly. they should be removed upoun the underneath grass tile is removed
                            decorationTilemap.SetTile(CurrenttileToCheck, null);
                    }
                       
                }
            }
        }
    }

    void findAndCorruptDeco(Vector3Int here)  // the function finds any decoration on a tile and sitches its sprite into corrupted one
    {
        Vector3Int realHere = new Vector3Int(here.x, 0, here.y);
        Collider[] intersecting = Physics.OverlapSphere(realHere, 0.5f);
     //   circles2Draw.Add(realHere);


        for (int i = 0; i < intersecting.Length; i++)
        {

            if (intersecting[i].gameObject.tag == "Decoration" )
            {
                GameObject currentDeco = intersecting[i].gameObject;
              
                LookRandomizer currentLook = currentDeco.GetComponent <LookRandomizer>();

                if (currentLook.corruptble)
                {
                    currentLook.CorruptTheDeco();
                   
                    
                }
            

            }

        }



    }
}
