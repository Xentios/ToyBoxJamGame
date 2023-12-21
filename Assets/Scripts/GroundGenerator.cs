using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundGenerator : MonoBehaviour
{

    [SerializeField]
    private Tilemap ground_1;

    [SerializeField]
    private Tilemap ground_2;

    [SerializeField]
    private Tilemap ground_3;

    [SerializeField]
    private RuleTile topGrassyTiles;

    [SerializeField]
    private RuleTile bottomRockyTiles;

    [SerializeField]
    private TileBase test;
    // Start is called before the first frame update
    void Start()
    {
        //bottomRockyTiles.GetType();
        GetRandomTileFromRuleTile();
        //Vector3Int endOfTileMap = tileMap.origin + tileMap.size;
        //endOfTileMap.y -= tileMap.size.y;
        //endOfTileMap = Vector3Int.zero;
        SetPreview(Vector3Int.zero);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 topRight = tileMap.origin + tileMap.size;
        //var result=Camera.main.WorldToViewportPoint(topRight);
        //if (result.x < 2)
        //{
        //    GetRandomTileFromRuleTile();
        //    //RandomWalkTopSmoothed()

        //}


    }

    public int[,] RandomWalkTopSmoothed(int[,] map, float seed, int minSectionWidth)
    {
        //Seed our random
        System.Random rand = new System.Random(seed.GetHashCode());

        //Determine the start position
        int lastHeight = Random.Range(0, map.GetUpperBound(1));

        //Used to determine which direction to go
        int nextMove = 0;
        //Used to keep track of the current sections width
        int sectionWidth = 0;

        //Work through the array width
        for (int x = 0; x <= map.GetUpperBound(0); x++)
        {
            //Determine the next move
            nextMove = rand.Next(2);

            //Only change the height if we have used the current height more than the minimum required section width
            if (nextMove == 0 && lastHeight > 0 && sectionWidth > minSectionWidth)
            {
                lastHeight--;
                sectionWidth = 0;
            }
            else if (nextMove == 1 && lastHeight < map.GetUpperBound(1) && sectionWidth > minSectionWidth)
            {
                lastHeight++;
                sectionWidth = 0;
            }
            //Increment the section width
            sectionWidth++;

            //Work our way from the height down to 0
            for (int y = lastHeight; y >= 0; y--)
            {
                map[x, y] = 1;
            }
        }

        //Return the modified map
        return map;
    }

    private void GetRandomTileFromRuleTile()
    {
        //if (topGrassyTiles != null)
        //{
        //    // Get a random rule from the RuleTile
        //    RuleTile.TilingRule randomRule = topGrassyTiles.m_TilingRules[Random.Range(0, topGrassyTiles.m_TilingRules.Count)];

        //    // Get a random output tile from the selected rule
        //    //TileBase randomOutputTile = randomRule.m_Output.

        //    // Do something with the randomOutputTile (e.g., print its name)
        //    if (randomOutputTile != null)
        //    {
        //        Debug.Log("Random Output Tile: " + randomOutputTile.name);
        //    }
        //}
        //else
        //{
        //    Debug.LogError("No Rule Tile assigned to topGrassyTiles.");
        //}
    }

    public void SetPreview(Vector3Int location)
    {

        Vector3Int minPos = new Vector3Int(-555, -555, 0);
        Vector3Int maxPos = new Vector3Int(4, 4, 0);
        // tileMap.InsertCells(location, new Vector3Int(100, 6, 0));
        //tileMap.SetTile(minPos, test);
        //tileMap.SetTile(maxPos, test);
        //tileMap.BoxFill(new Vector3Int(0, 0, 0), test, -555, -555, 4, 4);

        ground_1.BoxFill(ground_1.origin, null, 1, 1, 3, 3);
        ground_1.SetTile(ground_1.origin+Vector3Int.right,null);
        ground_2.BoxFill(ground_2.origin, null, 1, 1, 3, 3);
        ground_2.SetTile(ground_3.origin + Vector3Int.right, null);
        ground_3.BoxFill(ground_3.origin, null, 1, 1, 3, 3);
        ground_3.SetTile(ground_3.origin + Vector3Int.right, null);

        var copy = location;
        var endx = 100;
        var endy = 6;
        copy.x += endx;
        copy.y += endy;

   
       

        //BoundsInt b = new BoundsInt(location,Vector3Int.one*100);
        //tileMap.SetTilesBlock(b, test);
        // tileMap.BoxFill(location, test, 0, 0, 100, 6);
        //tileMap.SetTile(copy, test);
        //tileMap.ResizeBounds();
        //tileMap.BoxFill(location, test, 0,0,endx,endy);
        //tileMap.BoxFill(copy, test, -1000, -6, endx, endy);

        //tileMap.CompressBounds();  
        //tileMap.RefreshAllTiles();
    }

}



