using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    #region Vars
    [SerializeField] private GameEventsScriptableObject _gameEventsSO;
    [SerializeField] private BlockTypeCatalogScriptableObject _blockTypeCatalogSO;
    [SerializeField] private BlockPoolScriptableObject _blockPoolSO;

    [SerializeField] private Transform _blockParent;

    private static int _width = 13;
    private static int _height = 8;

    [SerializeField] private Vector2Int _gridStartPosition = new Vector2Int(-22, 21);

    [Range(0f, 1f)]
    [SerializeField] private float percentageOfEmptySpaceToGenerate = 0.2f;

    private Block[,] _blockGrid = new Block[_width, _height];

    private int liveBlockCount;

    #endregion

    #region Initialization
    
    private void OnEnable ( )
    {
        _gameEventsSO.CheckGameOver += GameOverCheck;        
    }

    #endregion

    #region Create Grid

    public void CreateGrid()
    {
        //Grid creation starting with the top left grid position

        //Temp position used because this is setting a new position after each new block
        Vector2Int tempBlockPosition = _gridStartPosition;
        liveBlockCount = 0;

        for (int y = _height - 1 ; y > 0; y--)
        {
            for (int x = 0 ; x < _width - 1; x++)
            {
                //Randomizing a block skip so that every new grid creation is unique and not every position is filled
                if(RandomizeBlockSkip(percentageOfEmptySpaceToGenerate))
                {
                    _blockGrid[x, y] = null;
                    tempBlockPosition.x += 4;
                    continue;
                }

                Block block =  _blockPoolSO.GetObjectFromPool();
                block.transform.position = new Vector3(tempBlockPosition.x, tempBlockPosition.y, 0f);
                block.transform.SetParent(_blockParent);

                _blockGrid[x, y] = block;

                tempBlockPosition.x += 4;
            }
            tempBlockPosition = new Vector2Int(_gridStartPosition.x, tempBlockPosition.y - 2);
        }

        GetAllActiveBlocks();
    }

    private bool RandomizeBlockSkip(float percent)
    {
        return percent >= UnityEngine.Random.Range(0 , 1f);
    }

    private void GetAllActiveBlocks()
    {
        //Have to count myself because the built in methods to get Unity's objectpool count are bugged
        Block[] activeBlocks = FindObjectsOfType<Block>();

        for (int i = 0 ; i < activeBlocks.Length ; i++)
        {
            if(!activeBlocks[i]._isIndestructable)
            {
                liveBlockCount++;
            }
        }
    }

    #endregion

    #region Checks
       
    private void GameOverCheck()
    {
        --liveBlockCount;
        if (liveBlockCount <= 0)
        {
            _gameEventsSO.OnGameOver?.Invoke();
        }
    }

    #endregion

    #region Clean Up

    private void OnDisable ( )
    {
        _gameEventsSO.CheckGameOver -= GameOverCheck;
    }

    #endregion
}
