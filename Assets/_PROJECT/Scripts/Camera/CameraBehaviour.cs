using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Camera _cam;                        //camera
    private LevelBehaviour _levelBehaviour;     //level behaviour
    private Transform _player;                  //player 
    
    // Start is called before the first frame update
    void Start()
    {
        CamInitializer();
    }

    // Update is called once per frame
    void Update()
    {
        PosUpdater();
    }

    /// <summary>
    /// function to initialize camera
    /// </summary>
    private void CamInitializer()
    {
        //getting player
        _player = GameManager.Instance.PLR_playerController.objectRb.transform;

        //getting current level
        _levelBehaviour = GameManager.Instance.currentLevel;

        //getting main camera
        _cam = Camera.main;
        
        //setup cam position
        _cam.transform.position = new Vector2(_levelBehaviour.allCamPosX[0],_cam.transform.position.y);
    } 

    /// <summary>
    /// function to update camera position based on the distance between ball and camera
    /// </summary>
    private void PosUpdater()
    {
        //distance between player and the camera
        float _dist = _player.transform.position.x - _cam.transform.position.x;
        //QuanhLogger.Log(_player.transform.position.x - _cam.transform.position.x);

        //if the diestance is larger than the level cam distance
        if( _dist > _levelBehaviour.camDist)
        {   
            QuanhLogger.Log("CHANIGN NEXT");
            //increase distance id to to go next position
            _levelBehaviour.CurrentPosID++;
        }
        //if distance smoler than minimum
        else if (_dist < _levelBehaviour.camDist * -1)
        {
            //QuanhLogger.Log("CHANIGN BACK");
            //decrease to go back to last position
            _levelBehaviour.CurrentPosID--;
        }

        //change cam position
        _cam.transform.position = new Vector2(_levelBehaviour.allCamPosX[_levelBehaviour.CurrentPosID],_cam.transform.position.y);
    }
}
