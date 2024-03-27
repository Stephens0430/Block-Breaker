using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BlockTypeCatalog")]
public class BlockTypeCatalogScriptableObject : ScriptableObject
{
    [SerializeField] private Block _blockPrefab;

    [SerializeField] private List<Color> _blockHealthColorList = new List<Color>();

    public Block GetBlockPrefab ( )
    {
        //Only one type of prefab now could extend this later to support different prefabs
        return _blockPrefab;
    }

    public int RandomizeBlockHealth ( )
    {
        //0 is reserved for indestructable blocks
        return Random.Range(1, 4);
    }

    //Could possibly add random indestructible blocks in the grid
    public bool RandomizeIfBlockIsIndestructable()
    {        
        return Random.Range(0f, 100f) > 75f;
    } 
    
    //Assign colors based on how many hits the block has left before destroyed
    public Color GetColorBasedOnBlockCurrentHitPoints(int hitPoints)
    {
        return _blockHealthColorList[hitPoints];
    }
}
