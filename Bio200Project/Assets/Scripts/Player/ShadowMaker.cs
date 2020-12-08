using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShadowMaker : MonoBehaviour
{
    [SerializeField] private GameObject shadows = null;
    [SerializeField] private Vector3 offset = new Vector3(-2.5f, -2.5f, 0f);

    private Tilemap tileMap = null;
    void Start()
    {
        tileMap = GetComponent<Tilemap>();

        if (tileMap == null || shadows == null)
            Debug.Log("ShadowMaker objects are null.");
        else
        {
            TileBase[] tiles = tileMap.GetTilesBlock(tileMap.cellBounds);

            for(int x = 0; x < tileMap.cellBounds.size.x; x++)
            {
                for (int y = 0; y < tileMap.cellBounds.size.y ; y++)
                {
                    if (tiles[x+y* tileMap.cellBounds.size.x] != null)
                        Instantiate(shadows, tileMap.CellToWorld(new Vector3Int(x, y, 0)) + offset, this.transform.rotation, this.transform);
                }
            }
        }
        
    }
}
