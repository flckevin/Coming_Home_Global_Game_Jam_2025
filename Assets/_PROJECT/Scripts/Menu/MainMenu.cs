using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    
    [Header("UI_Game"),Space(10)]
    [HorizontalLine(thickness =4, padding =20)]

    [Header("UI_MainMenu"),Space(5)]
    public GameObject MainMenuGroup;        //main menu
    public TextMeshProUGUI playText;        //play text
    public TextMeshProUGUI titleText;       //title text
    public Transform[] bubbleBalls;         //all bubble in main menu
    [Header("UI_LevelMenu"),Space(10)]
    public GameObject LevelMenuGroup;       //level menu
    public RawImage backGround_InGame;      //in game background

    [Header("UI_Transition"),Space(10)]
    [HorizontalLine(thickness =4, padding =20)]
    public Image transitionScreen;      //transition screen on menu

    [Header("UI_Dialouge"),Space(10)]
    [HorizontalLine(thickness =4, padding =20)]
    public DialougeSystem _dialouge;
    public string[] dialougeINTRO;
    public string[] nameCharac;
    public Sprite[] characSprite;

    [Header("WORLD"),Space(10)]
    [HorizontalLine(thickness =4, padding =20)]
    public GameObject world_mainMenu;            //the main menu world

    //====================== PRIVATE ====================== 
    private AnimancerComponent _animancer;  //animation component to play animation menuw
    //=====================================================

    // Start is called before the first frame update
    void Start()
    {
        //==================== GET ====================
        _animancer = GetComponent<AnimancerComponent>();
        //=============================================

        if(GameData.enteredToGame == true)
        {
            MainMenuGroup.SetActive(false);
            LevelMenuGroup.SetActive(true);
            transitionScreen.gameObject.SetActive(false);
            world_mainMenu.SetActive(false);
            backGround_InGame.gameObject.SetActive(true);
            GetComponent<PlayTouch>().enabled = false;
        }

        for(int i = 0 ; i < bubbleBalls.Length;i++)
        {
            Sequence _ballSeq = DOTween.Sequence();
            _ballSeq
            .Append(bubbleBalls[i].DOMoveY(bubbleBalls[i].transform.position.y + 5,1.5f))
            .Append(bubbleBalls[i].DOMoveY(bubbleBalls[i].transform.position.y - 5,1.5f))
            .Append(bubbleBalls[i].DOMoveY(bubbleBalls[i].transform.position.y + 5,1.5f))
            .Append(bubbleBalls[i].DOMoveY(bubbleBalls[i].transform.position.y - 5,1.5f))
            .SetLoops(-1);
            
        }

       
    }

    public void Play()
    {
        //activate transition screen
        transitionScreen.gameObject.SetActive(true);
        //lerp text color
        playText.DOColor(new Color(playText.color.r,playText.color.g,playText.color.b,0),0.2f);
        titleText.DOColor(new Color(playText.color.r,playText.color.g,playText.color.b,0),0.2f);
        //transition sprite
        StartCoroutine(TransitionTo(0.5f,new Color(1,1,1,1),3f,MainMenuGroup,world_mainMenu,LevelMenuGroup));

    }

    /// <summary>
    /// function to load level
    /// </summary>
    /// <param name="_levelIndex"> level to load </param>
    public void EnterLevel(int _levelIndex)
    {
        //set level to load
        GameData.LeveToLoad = _levelIndex;
        //load into a level
        SceneManager.LoadScene(1);
        
    }

    public void ActiveMenuWZoom(GameObject _target)
    {
        _target.SetActive(true);
        _target.transform.DOScale(Vector3.one,0.5f);
    }

    public void DeactiveMenuWZoom(GameObject _target)
    {
        _target.transform.DOScale(Vector3.zero,0.5f).OnComplete(() => 
        {
            _target.SetActive(false);
        });
    }

    IEnumerator TransitionTo(float _delay, Color _color, float _sceneTransitionSpeed ,GameObject _origin = null, GameObject _extra = null ,GameObject _target = null)
    {
        //wait for given second of delay
        yield return new WaitForSeconds(_delay);

        //do tween transition screen
        transitionScreen.DOColor(_color,_sceneTransitionSpeed);
        
        yield return new WaitForSeconds(_sceneTransitionSpeed + 0.5f);

        //deactivate original
        if(_origin != null) _origin.SetActive(false);
        //deactivate main menu world
        if(_extra != null) _extra.SetActive(false);
        //activate target transitioning to
        if(_target != null) _target.SetActive(true);
        
        if(PlayerPrefs.GetInt("newplayer",0) == 0)
        {
            _dialouge.gameObject.SetActive(true);
            _dialouge.Initalizer(dialougeINTRO,nameCharac,characSprite);
            PlayerPrefs.SetInt("newplayer",1);
        }

        //do tween transition screen
        transitionScreen.DOColor(new Color(_color.r,_color.g,_color.b,0),_sceneTransitionSpeed - 1).OnComplete(() => 
        {
            //deactivate transition screen
            transitionScreen.gameObject.SetActive(false);
        });

        //activate in game background
        backGround_InGame.gameObject.SetActive(true);
    }

    

    

}
