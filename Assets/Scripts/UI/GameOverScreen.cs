using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    #region Vars

    [SerializeField] private Button _replayButton;

    [SerializeField] private GameEventsScriptableObject _gameEventsSO;

    #endregion

    #region Initialization

    private void OnEnable ( )
    {
        _replayButton.onClick.AddListener(ReplayButtonClickedEvent);
    }

    #endregion

    #region Events

    private void ReplayButtonClickedEvent()
    {
        _gameEventsSO.OnReplayButtonPressed?.Invoke();
    }

    #endregion

    #region

    private void OnDisable ( )
    {
        _replayButton.onClick.RemoveListener(ReplayButtonClickedEvent);
    }

    #endregion

}
