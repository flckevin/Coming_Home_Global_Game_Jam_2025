using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountDown : MonoBehaviour
{
    public TextMeshProUGUI timerText;       //timer text
    public float remainingTime;             //time left
    public Slider slider;                   //slider value
    public Image[] bubble;                  //all bubble
    public Sprite explodedBubble;           //exploded bubble
    private float _defaultTime;             //default time
    public List<float> _timeMeasurement = new List<float>();       //all time measuerment
    private int _currentMesurementID = 0;   //current time that we are measuring
    
    private void Start()
    {
        //store default time

        //IDK HOW DID I THINK OF THIS FORMULA WITHOUT SLEEP BUT IT WORKED
        _defaultTime = remainingTime;
        float _dividedTime = _defaultTime / bubble.Length;
        float _measuerUnit = _defaultTime;
        for(int i = 0; i < bubble.Length; i ++)
        {
            _measuerUnit -= _dividedTime;
            _timeMeasurement.Add(_measuerUnit);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            slider.value = remainingTime/_defaultTime;

            if(remainingTime < _timeMeasurement[_currentMesurementID])
            {
                bubble[_currentMesurementID].sprite = explodedBubble;
                _currentMesurementID++;
            }
        }
        else
        {
            bubble[0].sprite = explodedBubble;
            timerText.text = "0:00";
            LevelManager.Instance.looseMenu.transform.localScale = Vector3.zero;
            LevelManager.Instance.looseMenu.gameObject.SetActive(true);
            LevelManager.Instance.looseMenu.transform.DOScale(Vector3.one,0.5f);
            LevelManager.Instance._outOfTime = true;
            GameManager.Instance.PLR_playerController.enabled = false;
            //loose screen
            this.enabled = false;
        }

        int minutes = Mathf.FloorToInt(remainingTime/60);
        int seconds = Mathf.FloorToInt(remainingTime%60);

        timerText.text = string.Format("{0:00} : {1:00}",minutes,seconds);
    }
}
