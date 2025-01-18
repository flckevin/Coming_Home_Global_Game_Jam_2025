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
    public GameObject MainMenuGroup;    //main menu
    public TextMeshProUGUI playText;    //play text
    public TextMeshProUGUI titleText;   //title text

    [Header("UI_LevelMenu"),Space(10)]
    public GameObject LevelMenuGroup;   //level menu

    [Header("UI_Transition"),Space(10)]
    [HorizontalLine(thickness =4, padding =20)]
    public Image transitionScreen;      //transition screen on menu

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
    }

    public void Play()
    {
       
        //transition sprite
        StartCoroutine(TransitionTo(0.5f,new Color(1,1,1,1),2.5f,3f,MainMenuGroup,LevelMenuGroup));

    }

    IEnumerator TransitionTo(float _delay, Color _color, float _textLerpSpeed, float _sceneTransitionSpeed ,GameObject _origin = null, GameObject _target = null)
    {
        playText.DOColor(new Color(playText.color.r,playText.color.g,playText.color.b,0),_textLerpSpeed);
        titleText.DOColor(new Color(playText.color.r,playText.color.g,playText.color.b,0),_textLerpSpeed);
        //wait for given second of delay
        yield return new WaitForSeconds(_textLerpSpeed + 0.5f);
        //do tween transition screen
        transitionScreen.DOColor(_color,_sceneTransitionSpeed);
        yield return new WaitForSeconds(_sceneTransitionSpeed + 0.5f);
        //deactivate original
        if(_origin != null) _origin.SetActive(false);
        //activate target transitioning to
        if(_target != null) _target.SetActive(true);
        //do tween transition screen
        transitionScreen.DOColor(new Color(_color.r,_color.g,_color.b,0),_sceneTransitionSpeed);
        //deactivate main menu world
        world_mainMenu.SetActive(false);
        
    }

    /// <summary>
    /// function to load level
    /// </summary>
    /// <param name="_levelIndex"> level to load </param>
    public void EnterLevel(int _levelIndex)
    {
        //set level to load
        GameData.levelToload = _levelIndex;
        //load into a level
        SceneManager.LoadScene(1);
        
    }

    public void ActiveMenu()
    {

    }

    public void DeactiveMenu()
    {

    }

}
