using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    [Header("General Level Info")]
    [HorizontalLine(padding = 20, thickness =4)]
    public Transform levelStart;            //start position
    public Rigidbody2D[] levelObjects;      //all level objects
    public GameObject player;               //player object
    public SpriteRenderer rink;             //rink size
    
    public LevelData_Scriptable lvlData;    //data of the level

    [Header("Cam Info")]
    [HorizontalLine(padding = 20, thickness = 4)]
    public float[] allCamPosX;      //all position of camera in level
    public float camDist;           //all cameara distance to move to other position

    
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
    private int _currentPosID;      //current position id
    private Vector2[] _allPos;      //all position of the object
    //===============================================================

    /// <summary>
    /// function to setup level
    /// </summary>
    public void LevelSetup()
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

        _allPos = new Vector2[levelObjects.Length];

        for(int i = 0 ; i < _allPos.Length;i++)
        {
            _allPos[i] = levelObjects[i].transform.localPosition;
        }
    }

    public void OnReachLevel()
    {
        for(int i = 0 ; i < levelObjects.Length;i++)
        {
            levelObjects[i].gravityScale = 1;
        }

        if(lvlData.useDialougeOnEnd_ENDD == true)
        {
            LevelManager.Instance._dialougeEND.gameObject.SetActive(false);
            LevelManager.Instance._cutScene.color = new Color(LevelManager.Instance._cutScene.color.r,LevelManager.Instance._cutScene.color.g,LevelManager.Instance._cutScene.color.b,0);
            LevelManager.Instance._cutScene.gameObject.SetActive(true);
            LevelManager.Instance.dialougeGroup.SetActive(true);

            LevelManager.Instance._cutScene.preserveAspect = true;
            LevelManager.Instance._cutScene.sprite = lvlData.cutsceneIMG_ENDD;
            LevelManager.Instance._cutScene.DOColor(new Color(LevelManager.Instance._cutScene.color.r,LevelManager.Instance._cutScene.color.g,LevelManager.Instance._cutScene.color.b,1),0.5f).OnComplete(() => 
            {
                LevelManager.Instance._dialougeEND.gameObject.SetActive(true);
                LevelManager.Instance._dialougeEND.Initalizer(lvlData.dialogs_ENDD,lvlData.characterName_ENDD,lvlData.dialogCharacterSprite_ENDD);
            });
            

        }
    }
}

