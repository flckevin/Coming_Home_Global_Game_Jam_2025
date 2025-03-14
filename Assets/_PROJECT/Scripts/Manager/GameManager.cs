using System.Collections;
using System.Collections.Generic;
using Quocanh.pattern;
using UnityEngine;

public class GameManager : QuocAnhSingleton<GameManager>
{
    [Header("Player_PLR"),Space(10)]
    [HorizontalLine(padding = 20, thickness = 4)]
    public PlayerController PLR_playerController;
    [Header("LEVEL"),Space(10)]
    [HorizontalLine(padding = 20, thickness = 4)]
    [HideInInspector]public LevelBehaviour currentLevel;
    [Header("AUDIO"),Space(10)]
    [HorizontalLine(padding = 20, thickness = 4)]
    public AudioSource src;
}
