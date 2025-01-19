using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Quocanh.pattern;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class LevelManager : QuocAnhSingleton<LevelManager>
{

    [Header("UI")]
    [HorizontalLine(padding = 20, thickness =4)]
    public TextMeshProUGUI title;           //title of the level
    public DialougeSystem _dialougeSys;     //dialouge system
    public GameObject winMenu;              //win menu
    public GameObject looseMenu;            //loose menu
    public GameObject[] winMenuBalls;       //win menu stars
    public RawImage water;                 //water
    
    [Header("UI_END CUTSENBCE")]
    [HorizontalLine(padding = 20, thickness =4)]
    public GameObject dialougeGroup;
    public DialougeSystem _dialougeEND;
    public Image _cutScene;

    [Header("Game")]
    [HorizontalLine(padding = 20, thickness =4)]
    public TimeCountDown _countDown;
    public bool _outOfTime;
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
        GameObject levelToLoad = Resources.Load<GameObject>($"Levels/Level{GameData.LeveToLoad}");
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

        //========================= setup apearrance ========================= 
        if(GameManager.Instance.PLR_playerController._characterBehaviour._playerItemSpriteRend.sprite != null)
        {
            GameManager.Instance.PLR_playerController._characterBehaviour._playerItemSpriteRend.sprite = _levelBehave.lvlData.playerItem;
        }
        
    }

    private void StartSequence()
    {
         Sequence _introSeq;                //sequence intro
        _introSeq = DOTween.Sequence();     //create new sequence

        //setting intro sequence
        _introSeq
        .Append(title.transform.DOLocalMoveY(0,1))
        .AppendInterval(1.5f)
        .Append(title.DOColor(new Color(title.color.r,title.color.g,title.color.b,0),1));
        

        //play intro sequence
        _introSeq.Play().OnComplete(() => 
        {
            //on complete

            //activate dialouge
            _dialougeSys.gameObject.SetActive(true);
            //========================= setup Dialouges ========================= 
            _dialougeSys.Initalizer(_levelBehave.lvlData.dialogs,
                                _levelBehave.lvlData.characterName,
                                _levelBehave.lvlData.dialogCharacterSprite);
        });
        
        title.text = _levelBehave.lvlData.title;
    }
    
    public void StartTimer()
    {
        //========================= time countdown ========================= 
        _countDown.remainingTime = _levelBehave.lvlData.time;
        _countDown.enabled = true;
    }
    
}
