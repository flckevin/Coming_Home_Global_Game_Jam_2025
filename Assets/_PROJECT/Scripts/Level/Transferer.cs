using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transferer : MonoBehaviour
{
    public Transform targetPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject _playerRigi = collision.transform.parent.gameObject;
            _playerRigi.SetActive(false);
            _playerRigi.transform.position = targetPos.position;
            _playerRigi.SetActive(true);
        }
        
    }
}
