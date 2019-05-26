using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvent
{
    public const int MY_PLAYER = 0;
    public const int LEFT_PLAYER = 1;
    public const int RIGHT_PLAYER = 2;
    /// <summary>
    /// 自身玩家增加卡牌
    /// </summary>
    public const int MY_PLAYER_ADD_CARD = 3;
    public const int LEFT_PLAYER_ADD_CARD = 4;
    public const int RIGHT_PLAYER_ADD_CARD = 5;

    public const int DEAL_CARD = 6;

    public const int REMOVE_MY_CARD = 7;
    public const int REMOVE_LEFT_CARD = 8;
    public const int REMOVE_RIGHT_CARD = 9;

    public const int UPDATE_SHOW_DESK = 10;
}
