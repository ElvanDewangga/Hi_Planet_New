using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floortilemap, wallTileMap;
    [SerializeField]
    private TileBase floortile;

    [Header("Wall")]
    [SerializeField]
    private TileBase wallTop; 
    [SerializeField]
    private TileBase wallsideRight; 
    [SerializeField]
    private TileBase wallsideLeft;
    [SerializeField]
    private TileBase wallBottom;
    [SerializeField]
    private TileBase wallFull;

    [Header("Corner")]

    [SerializeField]
    private TileBase wallInnerCornerDownLeft;
    [SerializeField]
    private TileBase wallInnerCornerDownRight;

    [SerializeField]
    private TileBase WallDiagonalCornerDownLeft;
    [SerializeField]
    private TileBase WallDiagonalCornerDownRight;

    [SerializeField]
    private TileBase WallDiagonalCornerUpLeft;
    [SerializeField]
    private TileBase WallDiagonalCornerUpRight;
    
    public void Paintfloortile(IEnumerable<Vector2Int> position)
    {
        PaintTiles(position, floortilemap, floortile);
    }
    
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach(var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binarytype)
    {
        int typeAsInt = Convert.ToInt32(binarytype, 2);
        TileBase tile = null;

        if(WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }else if(WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallsideRight;
        }else if(WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallsideLeft;
        }
        else if(WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if(WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        if(tile != null)
        {
            PaintSingleTile(wallTileMap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tileposition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tileposition, tile);
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binarytype)
    {
        int typeAsInt = Convert.ToInt32(binarytype, 2);
        TileBase tile = null;

        if(WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }else if(WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        if(WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }else if(WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if(WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = WallDiagonalCornerDownLeft;
        }else if(WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = WallDiagonalCornerDownRight;
        }else if(WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = WallDiagonalCornerUpLeft;
        }else if(WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = WallDiagonalCornerUpRight;
        }else if(WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }else if(WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }

        if(tile != null)
        {
            PaintSingleTile(wallTileMap, tile, position);
        }
    }

    public void Clear()
    {
        floortilemap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }
}
