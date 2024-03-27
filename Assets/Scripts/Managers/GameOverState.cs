using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    /// <summary>
    /// This State handles what to do when the game is over
    /// </summary>

    #region Vars

    [SerializeField] Transform _gameOverScreenTransform;

    #endregion

    #region Initialization

    private void OnEnable ( )
    {
        _gameEventsSO.OnReplayButtonPressed += EndState;        
    }

    #endregion

    #region 

    public override void StageState ( )
    {
        base.StageState();
        _gameOverScreenTransform.gameObject.SetActive(true);
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
        _gameOverScreenTransform.gameObject.SetActive(false);
        _gameEventsSO.OnReplayButtonPressed -= EndState;
    }

    #endregion
}
