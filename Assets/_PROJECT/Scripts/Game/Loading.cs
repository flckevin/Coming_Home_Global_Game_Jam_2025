using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI loadingTitle;
    public TextMeshProUGUI loadingFact;
    public Image ballIMG;
    public LoadObJ[] loadContent;
    private bool ableToInteract;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load());
        Random();
    }

    private void Random()
    {
        int rand = UnityEngine.Random.Range(0,loadContent.Length);
        
        loadingTitle.text = loadContent[rand].title;
        loadingFact.text = loadContent[rand].fact;
        ballIMG.sprite = loadContent[rand].ball;


    }

    // Update is called once per frame
    void Update()
    {
        if(ableToInteract == false) return;
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(3f);
        ableToInteract= true;
        loadingText.text = "Tap to continue";
    }
}

[Serializable]
public class LoadObJ
{
    public Sprite ball;
    public string title;
    public string fact;
}