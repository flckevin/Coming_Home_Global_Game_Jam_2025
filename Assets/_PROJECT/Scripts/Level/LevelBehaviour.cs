using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public Transform levelStart;    //start position
    public Transform camPos;        //cam position
    public GameObject player;       //player object
    
    // Start is called before the first frame update
    void Awake()
    {
        LevelSetup();
    }

    private void LevelSetup()
    {
        //setup cam position
        if(camPos != null) Camera.main.transform.position = camPos.position;
        //spawn the player
        GameObject _spawnedPlayer = Instantiate(player);
        //get the rigibody
        Rigidbody2D _spawnedPlayerRigi = _spawnedPlayer.GetComponent<Rigidbody2D>();
        //set the player to be kinimatic to move the player
        //_spawnedPlayerRigi.isKinematic = true;
        //set postiion of the player at start position
        _spawnedPlayer.transform.position = levelStart.position;
        //give the controller the ball rigibody
        GameManager.Instance.PLR_playerController.objectRb = _spawnedPlayerRigi;

    }
}

