using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BubblePowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        collision.transform.parent.GetComponent<CharacterBehaviourRoot>().OnPowerUp();
        this.transform.DOScale(Vector3.zero,0.5f);
    }
}
