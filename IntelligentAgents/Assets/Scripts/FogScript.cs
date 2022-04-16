using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogScript : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile[] tiles;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeTileTexture(Vector3Int coord)
    {
        tilemap.SetTile(coord, tiles[0]);
        // Debug.Log(tiles[0].color);
        // Make transparent
        // Color color = tilemap.GetColor(coord);
        // color.a = 0;
        // tilemap.SetColor(coord, color);
        return;
    }
}
        
