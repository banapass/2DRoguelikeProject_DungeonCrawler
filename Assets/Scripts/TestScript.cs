using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tileBase;
    // Start is called before the first frame update
    void Start()
    {
        SetTilemap();
    }
    void SetTilemap()
    {

        var tilePos = tilemap.WorldToCell(Vector3Int.zero);
        tilemap.SetTile(tilePos, tileBase);
    }

}
