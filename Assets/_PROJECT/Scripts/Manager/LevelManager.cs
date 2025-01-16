using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    /// <summary>
    /// function to load level
    /// </summary>
    private void LoadLevel()
    {
        //get the level
        GameObject levelToLoad = Resources.Load<GameObject>($"Levels/Level{GameData.levelToload}");
        //spawn out level
        GameObject _spawnedLevel = Instantiate(levelToLoad);
        //set position of the level to be at origin
        _spawnedLevel.transform.position = Vector3.zero;
    }
}
