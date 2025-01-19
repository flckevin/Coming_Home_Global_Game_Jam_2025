using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData 
{
    private static int _levelToload = 0;
    private static int _maxLevel = 6;
    public static int LeveToLoad
    {
        get{return _levelToload;}
        set
        {
            if(_levelToload > _maxLevel)
            {
                _levelToload = _maxLevel;
            }
            else if(_levelToload < 0 )
            {
                _levelToload = 0;
            }
            else
            {
                _levelToload = value;
            }
        }
    }

    public static bool newToGame;
    public static int levelUnlocked;
    public static bool enteredToGame;
}
