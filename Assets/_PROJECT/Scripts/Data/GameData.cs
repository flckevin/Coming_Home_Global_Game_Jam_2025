using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData 
{
    private static int _levelToload = 1;
    private static int _maxLevel = 6;
    public static int LeveToLoad
    {
        get{return _levelToload;}
        set
        {
            if(_levelToload > _maxLevel)
            {
                PlayerPrefs.SetInt("Unlocked",_maxLevel);
                _levelToload = _maxLevel;
            }
            else if(_levelToload < 1 )
            {
                PlayerPrefs.SetInt("Unlocked",1);
                _levelToload = 1;
            }
            else
            {
                if(value > PlayerPrefs.GetInt("Unlocked"))
                {
                    PlayerPrefs.SetInt("Unlocked",value);
                }
                
                _levelToload = value;
            }
        }
    }

    public static bool newToGame;
    public static bool enteredToGame;
}
