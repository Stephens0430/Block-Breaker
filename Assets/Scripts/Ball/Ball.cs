using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Ball : MonoBehaviour
{
    #region Var

    [SerializeField] private GameEventsScriptableObject _gameEventsSO;
        
    [SerializeField] private Rigidbody _rigidbody;

    public float force = 200f;
    public float lifeExpectancy = 5f;
    private float elapsedLife;
        
    #endregion

    #region Initialization
      
    public void Initialize(Action<Ball> OnKill)
    {
        _gameEventsSO.OnBallKill = OnKill;
    }
    
    #endregion

    #region Update

    private void Update ( )
    {
        if(elapsedLife > lifeExpectancy)
        {
            KillEvent();
        }
        elapsedLife += Time.deltaTime;
    }

    #endregion

    #region Movement

    public void MoveBall()
    {
        //Chose to do a physics based movement where we just use an impulse force to push the ball
        _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    #endregion

    #region Event
        
    private void KillEvent()
    {
        CleanUp();

        _gameEventsSO.OnBallKill?.Invoke(this);
    }

    #endregion

    #region Clean Up

    private void CleanUp()
    {
        elapsedLife = 0f;
        _rigidbody.velocity = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
    }

    #endregion
}
