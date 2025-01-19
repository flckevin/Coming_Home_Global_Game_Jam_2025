using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public Transform hint;

    public void ActivateMenuZoom(GameObject _target)
    {
        _target.transform.localScale = Vector3.zero;
        _target.SetActive(true);
        _target.transform.DOScale(Vector3.one,0.5f).SetUpdate(true);
    }
    
    public void DeactivateMenuZoom(GameObject _target)
    {
        _target.transform.DOScale(Vector3.zero,0.5f).OnComplete(() => 
        {
            _target.SetActive(false);
            
        }).SetUpdate(true);
    }

    public void Pause(bool _pause)
    {
        switch(_pause)
        {
            case true:
            Time.timeScale = 0;
            break;

            case false:
            Time.timeScale = 1;
            break;
        }
    }

    public void LoadScene(int _sceneIndex)
    {
        Pause(false);
        SceneManager.LoadScene(_sceneIndex);
    }

    public void Restart()
    {
        Pause(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MoveToNextLevel()
    {
        GameData.LeveToLoad ++;
        SceneManager.LoadScene(1);
    }

    public void Hint()
    {
        Sequence _hintSeq = DOTween.Sequence();

        _hintSeq
        .Append(hint.DOScale(Vector3.one,0.5f))
        .AppendInterval(4.5f)
        .Append(hint.DOScale(Vector3.zero,0.5f));
    }
}
