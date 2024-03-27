using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    #region Vars
    [SerializeField] private GameEventsScriptableObject _gameEventsSO;
    
    private InputAction _fire;

    [SerializeField] private BallTypeCatalogScriptableObject _bulletTypeCatalogSO;
    [SerializeField] private BallPoolScriptableObject _ballPoolSO;

    [SerializeField] private Transform _shootPoint;

    private bool _isGameOver;

    #endregion

    #region Initialization

    private void Awake ( )
    {
        Initialize();
    }
        
    private void Initialize ( )
    {
        _isGameOver = false;

        _fire = GameManager.Instance.playerControls.Player.Fire;
        _fire.Enable();
        _fire.performed += ShootTurret;

        _gameEventsSO.OnGameOver += DontAllowShootingOnGameOver;
        _gameEventsSO.OnGameStarted += AllowShootingOnGameStart;
    }

    #endregion

    #region Shoot

    private void ShootTurret(InputAction.CallbackContext context)
    {
        if(_isGameOver)
        {
            return;
        }

        //Getting Ball object from pool and setting its transform parameters before sending it off
        Ball ball = _ballPoolSO.GetObjectFromPool();
        ball.transform.rotation = _shootPoint.rotation;
        ball.transform.position = _shootPoint.position;

        ball.MoveBall();

        _gameEventsSO.OnTurretShotLog?.Invoke("A projectile was shot");
    }

    private void AllowShootingOnGameStart ( )
    {
        _isGameOver = false;
    }

    private void DontAllowShootingOnGameOver()
    {
        _isGameOver = true;
    }

    #endregion

    #region Clean Up

    private void OnDisable ( )
    {
        _fire.Disable();
        _gameEventsSO.OnGameStarted -= AllowShootingOnGameStart;
        _gameEventsSO.OnGameOver -= DontAllowShootingOnGameOver;
    }

    #endregion
}
