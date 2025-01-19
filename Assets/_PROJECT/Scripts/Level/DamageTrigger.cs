using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DamageTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        CharacterBehaviourRoot _charac = collision.transform.parent.GetComponent<CharacterBehaviourRoot>();
        if(_charac.poweringUp == false)
        {
            _charac.OnDamageReceive();
            StartCoroutine(DelayBeforeMenu());
        }
        else
        {
            _charac.shield.SetActive(false);
            _charac.poweringUp = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(RenableBox());
        }
        
    }

    IEnumerator RenableBox()
    {
        yield return new WaitForSeconds(3);
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator DelayBeforeMenu()
    {
        yield return new WaitForSeconds(2f);
         LevelManager.Instance.looseMenu.transform.localScale = Vector3.zero;
        LevelManager.Instance.looseMenu.gameObject.SetActive(true);
        LevelManager.Instance.looseMenu.transform.DOScale(Vector3.one,0.5f);
        GameManager.Instance.PLR_playerController.enabled = false;
    }
}
