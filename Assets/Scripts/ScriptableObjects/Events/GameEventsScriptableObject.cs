using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameEvents")]
public class GameEventsScriptableObject : ScriptableObject
{
    /// <summary>
    /// All events come through here 
    /// </summary>
    /// 
    #region Game Events

    public Action OnGameStarted;
    public Action OnGameOver;

    public Action<State> NextGameState;

    #endregion

    #region Game Over Screen Events

    public Action OnReplayButtonPressed;
    
    #endregion

    #region Turret Events

    public Action OnTurretShot;
    public Action<string> OnTurretShotLog;

    #endregion

    #region Ball Events

    public Action<Ball> OnBallKill;
    
    #endregion

    #region Block Events

    public Action OnBallHitBlock;
    public Action<string> OnBallHitBlockLog;
    public Action<Block> OnBlockDestroyed;
    public Action<string> OnBlockDestroyedLog;
    public Action CheckGameOver;

    #endregion
        
}
