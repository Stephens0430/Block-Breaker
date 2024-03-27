using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public GameEventsScriptableObject _gameEventsSO;

    public State nextState;

    public virtual void StageState ( )
    {

    }

    public virtual void EndState ( )
    {

    }
}
