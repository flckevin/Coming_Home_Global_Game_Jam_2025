using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    [Header("General Level Info")]
    [HorizontalLine(padding = 20, thickness =4)]
    public Transform levelStart;        //start position
    public Rigidbody2D[] levelObjects;  //all level objects
    public GameObject player;           //player object
    
    [Header("Cam Info")]
    [HorizontalLine(padding = 20, thickness = 4)]
    public float[] allCamPosX;     //all position of camera in level
    public float camDist;    //all cameara distance to move to other position

    //=========================== PROPERTIES ===========================
    public int CurrentPosID
    {
        get => _currentPosID;

        set
        {
            if(_currentPosID > allCamPosX.Length - 1)
            {
                _currentPosID = allCamPosX.Length-1;
            }
            else if(_currentPosID < 0)
            {
                _currentPosID = 0;
            }
            else
            {
                _currentPosID = value;
            }
        }
    }
    
    //===============================================================

    //=========================== PRIVATE ===========================
    public int _currentPosID;      //current position id
    //===============================================================

    // Start is called before the first frame update
    void Awake()
    {
        LevelSetup();
    }


    /// <summary>
    /// function to setup level
    /// </summary>
    private void LevelSetup()
    {
        //assigning current level to gamemanager
        GameManager.Instance.currentLevel = this;

        //spawn the player
        GameObject _spawnedPlayer = Instantiate(player);
        //get the rigibody
        Rigidbody2D _spawnedPlayerRigi = _spawnedPlayer.GetComponent<Rigidbody2D>();
        //set postiion of the player at start position
        _spawnedPlayer.transform.position = levelStart.position;

        
        //give the controller the ball rigibody
        GameManager.Instance.PLR_playerController.objectRb = _spawnedPlayerRigi;
        

    }
}

