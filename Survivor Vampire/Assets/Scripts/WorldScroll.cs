using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScroll : MonoBehaviour
{
    Transform playerTransfrom;
    Vector2Int currentTilePosition = new Vector2Int(0,0);
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    [SerializeField] float tileSize = 20f;
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;
    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    private void Awake(){
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Start(){
        UpdateTilesOnScreen();
        playerTransfrom = GameManager.instance.playerTransform;
    }

    private void Update(){
        playerTilePosition.x = (int)(playerTransfrom.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransfrom.position.y / tileSize);

        //Fancy if negative -1
        playerTilePosition.x -= playerTransfrom.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTransfrom.position.y < 0 ? 1 : 0;

        if(currentTilePosition != playerTilePosition){
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false);
            UpdateTilesOnScreen();
        }
    }

    private void UpdateTilesOnScreen()
    {
        for(int pov_x = -(fieldOfVisionWidth/2); pov_x <= fieldOfVisionWidth/2; pov_x++){
            for(int pov_y = -(fieldOfVisionHeight/2); pov_y <= fieldOfVisionHeight/2; pov_y++){
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + pov_y, false);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                Vector3 newPosition = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
                if(newPosition != tile.transform.position){
                    tile.transform.position = newPosition;
                    terrainTiles[tileToUpdate_x,tileToUpdate_y].GetComponent<TerrainTile>().Spawn();
                }
            }
        }
    }

    //TODO when first extending the map it jumps for some reason, round to the next 10 was not the solution, problem for future me otherwise a small feature
    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if(horizontal){
            if(currentValue >= 0){
                currentValue = currentValue % terrainTileHorizontalCount;
            }else{
                currentValue += 1;
                currentValue = terrainTileHorizontalCount - 1
                + currentValue % terrainTileHorizontalCount;
            }
        }else{
            if(currentValue >= 0){
                currentValue = currentValue % terrainTileVerticalCount;
            }else{
                currentValue += 1;
                currentValue = terrainTileVerticalCount - 1
                + currentValue % terrainTileVerticalCount;
            }
        }
        return (int)currentValue;
    }

    public void Add(GameObject tileGameObject, Vector2Int tilePositon)
    {   
        terrainTiles[tilePositon.x, tilePositon.y] = tileGameObject;
    }
}
