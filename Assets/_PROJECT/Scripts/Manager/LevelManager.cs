using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [Header("UI")]
    [HorizontalLine(padding = 20, thickness =4)]
    public TextMeshProUGUI title;           //title of the level
    public DialougeSystem _dialougeSys;     //dialouge system

    //======================== PRIVATE ======================== 
    private LevelBehaviour _levelBehave;    //level behaviour
    //=========================================================

    // Start is called before the first frame update
    void Start()
    {
        //load level
        LoadLevel();
        //start intro sequence
        StartSequence();
    }

    /// <summary>
    /// function to load level
    /// </summary>
    private void LoadLevel()
    {
        //========================= get the level ========================= 
        GameObject levelToLoad = Resources.Load<GameObject>($"Levels/Level{GameData.levelToload}");
        //spawn out level
        GameObject _spawnedLevel = Instantiate(levelToLoad);
        //set position of the level to be at origin
        _spawnedLevel.transform.position = Vector3.zero;

        //=========================  setup level ========================= 
        _levelBehave = _spawnedLevel.GetComponent<LevelBehaviour>();
        _levelBehave.LevelSetup();

        //========================= setup cam ========================= 
        CameraBehaviour _camBehave = Camera.main.GetComponent<CameraBehaviour>();
        _camBehave.CamInitializer();
        //set cam size
        _camBehave.CamResSetter(_levelBehave.rink);

        //========================= setup controller ========================= 
        GameManager.Instance.PLR_playerController.ControllerInitialize();

        //========================= setup Dialouge ========================= 
        _dialougeSys.Initalizer(_levelBehave.lvlData.dialogs,
                                _levelBehave.lvlData.characterName,
                                _levelBehave.lvlData.dialogCharacterSprite);
    }

    private void StartSequence()
    {
         Sequence _introSeq;                //sequence intro
         Sequence _dialougeSeq;             //dialoge sequence
        _introSeq = DOTween.Sequence();     //create new sequence
        _dialougeSeq = DOTween.Sequence();  //createw new sequence

        //setting intro sequence
        _introSeq
        .Append(title.transform.DOLocalMoveY(0,1))
        .AppendInterval(1.5f)
        .Append(title.DOColor(new Color(title.color.r,title.color.g,title.color.b,0),1));
        
        //setting dialouge sequence
        _dialougeSeq
        .Append(_dialougeSys.transform.DOScale(new Vector3(1,1,1),1f));

        //play intro sequence
        _introSeq.Play().OnComplete(() => 
        {
            //on complete

            //activate dialouge
            _dialougeSys.gameObject.SetActive(true);
            //play dialouge sequence
            _dialougeSeq.Play();
        });
        
        title.text = _levelBehave.lvlData.title;
    }
    
}
