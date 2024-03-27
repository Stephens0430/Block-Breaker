using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(menuName = "ScriptableObject/BallPool")]
public class BallPoolScriptableObject : ScriptableObject
{
    #region Vars

    [SerializeField] private BallTypeCatalogScriptableObject _ballTypeCatalogSO;
    private BallType _ballType;

    [SerializeField] private Ball _ballPrefab;

    public int initialPoolSize = 20;
    public int maxPoolSize = 40;

    private ObjectPool<Ball> _ballPool;

    #endregion

    #region Initialization

    private void OnEnable ( )
    {        
        InitializePool();
    }
    
    private void InitializePool ( )
    {
        _ballPrefab = _ballTypeCatalogSO.GetBallPrefabBasedOnType();

        //Creating Object pool of Balls each step tells what happens at each state of the pooled objects life for the most part
        _ballPool = new ObjectPool<Ball>(( ) =>
        {
            return Instantiate(_ballPrefab);
        }, ball =>
        {
            ball.gameObject.SetActive(true);
            ball.Initialize(ReturnObjectToPool);
        }, ball =>
        {
            ball.gameObject.SetActive(false);
        }, ball =>
        {
            Destroy(ball.gameObject);
        }, false, 20, 40);
    }

    #endregion

    #region Pooling

    public Ball GetObjectFromPool ( )
    {
        return _ballPool.Get();
    }

    public void ReturnObjectToPool( Ball ball )
    {
        _ballPool.Release(ball);
    }       

    #endregion
}
