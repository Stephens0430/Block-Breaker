using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region Var

    [SerializeField] private GameEventsScriptableObject _gameEventsSO;
    [SerializeField] private BlockTypeCatalogScriptableObject _blockTypeCatalogSO;

    private int _totalHitPoints;
    private int _currentHitCount;
    private Color _currentBlockColor;
    private Material material;

    public bool _isIndestructable;
    
    #endregion

    #region Initialize

    public void Initialize ( Action<Block> OnKill, int totalHitPoints, bool isIndestructable )
    {
        //Setting up the block and its characteristics based on variable values
        _gameEventsSO.OnBlockDestroyed = OnKill;
        
        if (material == null)
        {
            material = this.GetComponent<MeshRenderer>().material;
        }

        if (!isIndestructable)
        {
            _totalHitPoints = totalHitPoints;
            _currentBlockColor = _blockTypeCatalogSO.GetColorBasedOnBlockCurrentHitPoints(_totalHitPoints);
        }
        else
        {
            // Pass in 0 to get a grey block to represent a block that can't be broken
            _currentBlockColor = _blockTypeCatalogSO.GetColorBasedOnBlockCurrentHitPoints(0);
        }

        material.color = _currentBlockColor;
        _isIndestructable = isIndestructable;        
    }

    private void OnEnable ( )
    {
        Reset();
    }

    #endregion

    #region Block Handling

    private void CheckBlockHealth()
    {
        if (_currentHitCount >= _totalHitPoints)
        {
            DestroyedEvent();
        }
    }

    private void MakeBlockIndestructable()
    {
        _isIndestructable = true;
    }

    #endregion

    #region Events

    private void DestroyedEvent()
    {        
        _gameEventsSO.CheckGameOver?.Invoke();
        _gameEventsSO.OnBlockDestroyed?.Invoke(this);
        _gameEventsSO.OnBlockDestroyedLog?.Invoke("A block was destroyed");
    }

    #endregion

    #region Collision

    private void OnCollisionEnter ( Collision collision )
    {
        if(_isIndestructable)
        {
            return;
        }

        if (collision.gameObject.layer == 6)
        {
            _currentHitCount++;
            CheckBlockHealth();

            if(_totalHitPoints - _currentHitCount > 0)
            {
                //Update block color when hit value changes
                _currentBlockColor = _blockTypeCatalogSO.GetColorBasedOnBlockCurrentHitPoints(_totalHitPoints - _currentHitCount);
                material.color = _currentBlockColor;
            }

            _gameEventsSO.OnBallHitBlock?.Invoke();
            _gameEventsSO.OnBallHitBlockLog?.Invoke("A projectile has hit a block");
        }               
    }

    #endregion

    #region Clean Up

    private void Reset()
    {
        _totalHitPoints = 1;
        _currentHitCount = 0;
        _isIndestructable = false;
    }       

    #endregion
}
