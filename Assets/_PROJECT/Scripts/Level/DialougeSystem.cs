using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialougeSystem : MonoBehaviour
{
    [Header("Dialouge general info")]
    [HorizontalLine(padding = 20, thickness = 4)]
    public Image character;                             //image of the character
    public TextMeshProUGUI characterName;               //character name display on screen
    public TextMeshProUGUI characterConversation;       //character conversation display on screen
    public float typeSpeed;                             //type speed of the dialouge
    //========================== PRIVATE / HIDDEN ==========================
    private string[] _conversation;              //array of conversation
    private string[] _characterNames;            //array of character names
    private Sprite[] _characterSprites;          //array of character sprites
    private int _currentConversationID = 0;     //current conversation that character having
    //======================================================================
    
    /// <summary>
    /// initialization of the dialouge system
    /// </summary>
    /// <param name="_conversationINI"> conversation in level </param>
    /// <param name="_characterNamesINI"> character names iun level </param>
    /// <param name="_characterSpritesINI"> character sprites in level </param>
    public void Initalizer(string[] _conversationINI, string[] _characterNamesINI, Sprite[] _characterSpritesINI)
    {
        _conversation = _conversationINI;
        _characterNames = _characterNamesINI;
        _characterSprites = _characterSpritesINI;
        characterConversation.text = string.Empty;
        _currentConversationID = 0;

        StartCoroutine(TypeDialouge());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if(characterConversation.text == _conversation[_currentConversationID])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    characterConversation.text = _conversation[_currentConversationID];
                }
                
            }
            
        }
    }

    private void NextLine()
    {
        if(_currentConversationID < _conversation.Length - 1)
        {
            _currentConversationID++;
            characterConversation.text = string.Empty;
            StartCoroutine(TypeDialouge());
        }
        else
        {
            StopAllCoroutines();
            GameManager.Instance.PLR_playerController.enabled = true;
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator TypeDialouge()
    {
        foreach(char c in _conversation[_currentConversationID].ToCharArray())
        {
            characterConversation.text += c;
            yield return new WaitForSeconds(typeSpeed);

        }
        
    }
}
