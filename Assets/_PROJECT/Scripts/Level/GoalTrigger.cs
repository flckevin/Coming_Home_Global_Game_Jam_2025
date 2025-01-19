using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        if(LevelManager.Instance._outOfTime == true)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            return;
        }
        Time.timeScale = 0.5f;
        GameManager.Instance.currentLevel.OnReachLevel();
        LevelManager.Instance._countDown.enabled = false;
        LevelManager.Instance.winMenu.transform.localScale = Vector3.zero;
        LevelManager.Instance.winMenu.SetActive(true);
        LevelManager.Instance.water.DOColor(new Color(LevelManager.Instance.water.color.r,LevelManager.Instance.water.color.g,LevelManager.Instance.water.color.b,0), 1.5f).OnComplete(() => 
        {
            LevelManager.Instance.winMenu.transform.DOScale(Vector3.one,0.5f).OnComplete(() => 
            {
                for(int i = 0 ;i < LevelManager.Instance._countDown.bubble.Length; i++)
                {
                    if(LevelManager.Instance._countDown.bubble[i].sprite != LevelManager.Instance._countDown.explodedBubble)
                    {
                        LevelManager.Instance.winMenuBalls[i].transform.localScale = Vector3.zero;
                        LevelManager.Instance.winMenuBalls[i].SetActive(true);
                        LevelManager.Instance.winMenuBalls[i].transform.DOScale(Vector3.one,0.3f);
                    }
                    
                }
                this.GetComponent<BoxCollider2D>().enabled = false;
                GameManager.Instance.PLR_playerController.enabled =false;
            }).SetUpdate(true);

        }).SetUpdate(true);
        

        
    }
}
