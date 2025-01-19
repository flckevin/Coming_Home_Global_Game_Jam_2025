using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBubble : MonoBehaviour
{
    public int LevelToLoad;
    public Image locker;
    public TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {

        if(LevelToLoad <= PlayerPrefs.GetInt("Unlocked",1))
        {
            GetComponent<Button>().enabled = true;
            locker.gameObject.SetActive(false);
            levelText.gameObject.SetActive(true);
        }
        else
        {
            GetComponent<Button>().enabled = false;
            locker.gameObject.SetActive(true);
            levelText.gameObject.SetActive(false);
        }
        levelText.text = LevelToLoad.ToString();
    }

    public void LoadLevel()
    {
        GameData.LeveToLoad = LevelToLoad;
        SceneManager.LoadScene(1);
    }

}
