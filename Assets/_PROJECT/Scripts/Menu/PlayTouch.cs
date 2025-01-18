using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayTouch : MonoBehaviour
{
    public MainMenu mainMenu;           // main menu script
    public TextMeshProUGUI playText;    //play text to lerp color

    // Start is called before the first frame update
    void Start()
    {
        //if mainmenu does not exist then get the main menu
        if(mainMenu == null) mainMenu = GetComponent<MainMenu>();

        //if there is play text
        if(playText != null)
        {
            Sequence _seq = DOTween.Sequence();
            _seq
            .Append(playText.DOColor(new Color(playText.color.r,playText.color.g,playText.color.b,20/255f),1f))
            .AppendInterval(0.1f)
            .Append(playText.DOColor(new Color(playText.color.r,playText.color.g,playText.color.b,1),0.8f))
            .AppendInterval(0.1f)
            .SetLoops(-1);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //if there is touch
        if(Input.touchCount > 0)
        {
            //play
            mainMenu.Play();
            //disable this script
            this.enabled =false;
        }
    }
}
