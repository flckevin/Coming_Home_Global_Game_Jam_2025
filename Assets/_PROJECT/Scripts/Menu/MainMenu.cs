using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [Header("ANIMATION"),Space(10)]
    [HorizontalLine(thickness =4, padding =20)]
    public AnimationClip playAnim;      //animation of play menu
    
    [Header("UI_Game"),Space(10)]
    [HorizontalLine(thickness =4, padding =20)]
    public GameObject MainMenuGroup;    //main menu
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
        //deactivate main menu
        MainMenuGroup.SetActive(false);
        //play animation
        _animancer.Play(playAnim);
        //transition sprite
        StartCoroutine(TransitionTo(playAnim.length - 0.5f,new Color(1,1,1,1),3,null,LevelMenuGroup));

    }

    IEnumerator TransitionTo(float _delay, Color _color, float _speed ,GameObject _origin = null, GameObject _target = null)
    {
        //wait for given second of delay
        yield return new WaitForSeconds(_delay);
        //do tween transition screen
        transitionScreen.DOColor(_color,_speed);
        yield return new WaitForSeconds(_speed + 0.5f);
        //deactivate original
        if(_origin != null) _origin.SetActive(false);
        //activate target transitioning to
        if(_target != null) _target.SetActive(true);
        //do tween transition screen
        transitionScreen.DOColor(new Color(_color.r,_color.g,_color.b,0),_speed);
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
