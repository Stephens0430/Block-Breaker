using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region Vars

    public static GameManager Instance;

    [SerializeField] private GameEventsScriptableObject _gameEventsSO;

    public PlayerInputActions playerControls;

    public State currentGameState;

    public State gameState;
    public State gameOverState;

    #endregion

    #region Initialization

    private void Awake ( )
    {
        if (Instance == null)
        {
            Instance = this;
        }

        playerControls = new PlayerInputActions();
        playerControls.Enable();
    }

    void Start ()
    {
        //Assign Event and start off in the game state
        _gameEventsSO.NextGameState += NextGameState;
        NextGameState(gameState);
    }

    #endregion

    #region Game States

    private void NextGameState(State nextState)
    {        
        //Cycling through game states done here after called from event
        currentGameState = nextState;
        currentGameState.gameObject.SetActive(true);
        currentGameState.StageState();
    }

    #endregion

    #region Clean Up

    private void OnDisable ( )
    {
        playerControls.Disable();
        _gameEventsSO.NextGameState -= NextGameState;
    }

    #endregion

}
