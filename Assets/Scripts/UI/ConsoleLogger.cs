using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ConsoleLogger : MonoBehaviour
{
    #region Vars

    [SerializeField] GameEventsScriptableObject _gameEventsSO;

    [SerializeField] private int _maxLogs = 6;
    private Queue<string> _logQueue = new Queue<string>();
    private string _currentText = "";

    #endregion

    #region Initialize

    private void OnEnable ( )
    {
        _gameEventsSO.OnTurretShotLog += LogMessage;
        _gameEventsSO.OnBallHitBlockLog += LogMessage;
        _gameEventsSO.OnBlockDestroyedLog += LogMessage;
    }

    #endregion

    #region Log

    private void OnGUI ( )
    {
        //Create console box on the left side of the window
        GUI.Label(
           new Rect(
               25,                   
               Screen.height - 150, 
               300f,                
               150f                 
           ),
           _currentText,             
           GUI.skin.textArea        
        );
    }

    private void LogMessage(string message)
    {
        //Taking oldest message off the Q adding new message to the Q
        if (_logQueue.Count >= _maxLogs)
        {
            _logQueue.Dequeue();
        }

        _logQueue.Enqueue(message);

        var builder = new StringBuilder();
        foreach (string log in _logQueue)
        {
            builder.Append(log).Append("\n");
        }

        _currentText = builder.ToString();
    }

    #endregion

    #region CleanUp

    private void OnDisable( )
    {
        _gameEventsSO.OnTurretShotLog -= LogMessage;
        _gameEventsSO.OnBallHitBlockLog -= LogMessage;
        _gameEventsSO.OnBlockDestroyedLog -= LogMessage;
    }

    #endregion

}
