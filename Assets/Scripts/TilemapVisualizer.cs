using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap;

    [SerializeField] private TileBase floorTile, wallTop;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPostions)
    {
        PaintTiles(floorPostions, floorTilemap, floorTile);
    }


    // 벽타일 칠함
    internal void PaintSingleBasicWall(Vector2Int pos)
    {
        PaintSingleTile(wallTilemap, wallTop, pos);
    }
    // 생성된 랜덤맵의 위치 HeshSet을 가져와 타일을 칠해줌
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }
    // 타일맵에 타일을 칠함
    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePos, tile);
    }
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
