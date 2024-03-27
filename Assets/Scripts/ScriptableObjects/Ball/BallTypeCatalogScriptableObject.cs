using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BallTypeCatalog")]
public class BallTypeCatalogScriptableObject : ScriptableObject
{
    #region Vars

    [SerializeField] private Ball _ballPrefab;
    private MeshFilter _ballMeshFilter;
    private Material _ballMaterial;
    [SerializeField] private Dictionary<BallType, Color> _ballTypeColorDictionary = new Dictionary<BallType, Color>();

    #endregion
        
    #region BulletDescription

    public MeshFilter GetMeshFilterBasedOnType()
    {
        return _ballMeshFilter;
    }

    public Color GetBallColorBasedOnType ( BallType bulletType )
    {
        return _ballTypeColorDictionary[bulletType];
    }

    public Ball GetBallPrefabBasedOnType ( BallType bulletType = BallType.NORMAL)
    {
        //Only one type of prefab now could extend this later to support different prefabs
        return _ballPrefab;
    }

    public Material GetBallMaterial ( )
    {
        return _ballMaterial;
    }

    #endregion
}

//A way to implement different ball types
public enum BallType
{
    NORMAL,
    EXPLODING,
    MULTI,
}