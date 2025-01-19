using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
public class LevelData_Scriptable : ScriptableObject
{
    [Header("Game"),Space(10)]
    [HorizontalLine(padding =20, thickness = 4)]
    public float time;
    public float[] timeMeasurement;
    [Header("Dialouge"),Space(10)]
    [HorizontalLine(padding =20, thickness = 4)]
    public string title;
    public string[] characterName;
    public string[] dialogs;
    public Sprite[] dialogCharacterSprite;

    [Header("Hint"),Space(10)]
    [HorizontalLine(padding =20, thickness = 4)]
    public string hint;   

    [Header("Player"),Space(10)]
    [HorizontalLine(padding =20, thickness = 4)] 
    public Sprite playerItem;             
}
