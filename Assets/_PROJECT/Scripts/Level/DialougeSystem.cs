using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialougeSystem : MonoBehaviour
{
    [Header("Dialouge general info")]
    [HorizontalLine(padding = 20, thickness = 4)]
    public Image characterIMG;                          //image of the character
    public TextMeshProUGUI characterName;               //character name display on screen
    public TextMeshProUGUI characterConversation;       //character conversation display on screen
    public float typeSpeed;                             //type speed of the dialouge

    [Header("Dialouge UI")]
    [HorizontalLine(padding = 20, thickness = 4)]
    public GameObject introGroup;                       //image of background


    //========================== PRIVATE / HIDDEN ==========================
    private string[] _conversation;              //array of conversation
    private string[] _characterNames;            //array of character names
    private Sprite[] _characterSprites;          //array of character sprites
    private int _currentConversationID = 0;      //current conversation that character having
    //======================================================================
    
    /// <summary>
    /// initialization of the dialouge system
    /// </summary>
    /// <param name="_conversationINI"> conversation in level </param>
    /// <param name="_characterNamesINI"> character names iun level </param>
    /// <param name="_characterSpritesINI"> character sprites in level </param>
    public void Initalizer(string[] _conversationINI, string[] _characterNamesINI, Sprite[] _characterSpritesINI)
    {
        //get conversation
        _conversation = _conversationINI;
        //get all character names in level
        _characterNames = _characterNamesINI;
        //get all character sprites inlevel
        _characterSprites = _characterSpritesINI;
        //set conversation id to be at start
        _currentConversationID = -1;
        //call function of starting converstation
        NextLine();
    }

    // Update is called once per frame
    void Update()
    {
        //if player touch
        if(Input.touchCount > 0)
        {
            //get touch phase to check if it first touch
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //if character converstation text reached to the end
                if(characterConversation.text == _conversation[_currentConversationID])
                {
                    //start next line
                    NextLine();
                }
                else // character conversation not finished
                {
                    //stop typing
                    StopAllCoroutines();
                    //set conversation to be at the end
                    characterConversation.text = _conversation[_currentConversationID];
                }
                
            }
            
        }
    }

    /// <summary>
    /// function to move onto next line
    /// </summary>
    private void NextLine()
    {
        //if conversation not exceed length
        if(_currentConversationID < _conversation.Length - 1)
        {
            //move onto next line
            _currentConversationID++;
            //clear out text
            characterConversation.text = string.Empty;
            //set new character names
            characterName.text = _characterNames[_currentConversationID];
            //set new character sprite
            characterIMG.sprite = _characterSprites[_currentConversationID];
            //start typing new dialouge
            StartCoroutine(TypeDialouge());
        }
        else // not exceed length
        {
            //StopAllCoroutines();
            GameManager.Instance.PLR_playerController.enabled = true;

            //scale down dialouge
            introGroup.gameObject.transform.DOScale(Vector3.zero,0.2f).OnComplete(() => 
            {
                //deactivate game object
                introGroup.gameObject.SetActive(false);
            });

            
        }
    }


    /// <summary>
    /// function to type dialouge
    /// </summary>
    /// <returns></returns>
    IEnumerator TypeDialouge()
    {
        //loop all charcter in conversation
        foreach(char c in _conversation[_currentConversationID].ToCharArray())
        {
            //set that character into text
            characterConversation.text += c;
            //declay within given speed of typing
            yield return new WaitForSeconds(typeSpeed);

        }
        
    }
}
