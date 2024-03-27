using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(menuName = "ScriptableObject/BlockPool")]
public class BlockPoolScriptableObject : ScriptableObject
{
    #region Vars

    [SerializeField] private BlockTypeCatalogScriptableObject _blockTypeCatalogSO;

    [SerializeField] private Block _blockPrefab;

    public int initialPoolSize = 210;
    public int maxPoolSize = 240;

    public ObjectPool<Block> _blockPool;

    #endregion

    #region Initialization

    private void OnEnable ( )
    {
        InitializePool();
    }

    private void InitializePool ( )
    {
        _blockPrefab = _blockTypeCatalogSO.GetBlockPrefab();

        //Creating Object pool of Blocks each step tells what happens at each state of the pooled objects life for the most part
        _blockPool = new ObjectPool<Block>(( ) =>
        {
            return Instantiate(_blockPrefab);
        }, block =>
        {
            block.gameObject.SetActive(true);
            block.Initialize(ReturnObjectToPool, _blockTypeCatalogSO.RandomizeBlockHealth(), false);
        }, block =>
        {
            block.gameObject.SetActive(false);
        }, block =>
        {
            Destroy(block.gameObject);
        }, false, initialPoolSize, maxPoolSize);                
    }

    #endregion

    #region Pooling

    public Block GetObjectFromPool ( )
    {
        return _blockPool.Get();
    }

    public void ReturnObjectToPool ( Block block )
    {
        _blockPool.Release(block);
    }       

    #endregion
}
