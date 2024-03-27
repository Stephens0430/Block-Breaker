using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State
{
    /// <summary>
    /// This is the State that handles the main game state
    /// </summary>
    /// 
    #region Vars

    [SerializeField] private BlockManager _blockManager;

    #endregion

    #region Initialization

    private void OnEnable ( )
    {
        _gameEventsSO.OnGameOver += EndState;

        if (_blockManager == null)
        {
            _blockManager = Transform.FindObjectOfType<BlockManager>();
        }
    }

    #endregion

    #region 

    public override void StageState ( )
    {
        base.StageState();

        _gameEventsSO.OnGameStarted?.Invoke();
        _blockManager.CreateGrid();
    }

    public override void EndState ( )
    {
        _gameEventsSO.NextGameState(nextState);
        this.gameObject.SetActive(false);
    }

    #endregion

    #region Clean Up

    private void OnDisable ( )
    {
        _gameEventsSO.OnGameOver -= EndState;
    }

    #endregion
}
