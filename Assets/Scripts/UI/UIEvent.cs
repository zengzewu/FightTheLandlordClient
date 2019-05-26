using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvent
{
    /// <summary>
    /// 显示登陆面板
    /// </summary>
    public const int START_PANEL_ACTIVE = 0;
    /// <summary>
    /// 显示注册面板
    /// </summary>
    public const int REGISTE_PANEL_ACTIVE = 1;
    /// <summary>
    /// 刷新信息面板
    /// </summary>
    public const int REFRESH_INFO_PANEL = 2;
    /// <summary>
    /// 显示进入房间按钮
    /// </summary>
    public const int SHOW_ENTER_ROOM_BUTTOM = 3;
    /// <summary>
    /// 显示创建面板
    /// </summary>
    public const int CREATE_PANEL_ACTIVE = 4;
    /// <summary>
    /// 设置底牌
    /// </summary>
    public const int SET_BOTTOM_CARD = 5;
    /// <summary>
    /// 设置聊天文字
    /// </summary>
    public const int SET_CHART_TEXT_LEFT = 7;
    /// <summary>
    /// 设置左边角色数据
    /// </summary>
    public const int SET_LEFT_PLAYER_DATA = 8;
    /// <summary>
    /// 设置右边角色数据
    /// </summary>
    public const int SET_RIGHT_PLAYER_DATA = 9;
    /// <summary>
    /// 角色准备
    /// </summary>
    public const int PLAYER_READY = 10;
    /// <summary>
    /// 角色进入房间
    /// </summary>
    public const int PLAYER_ENTER = 11;
    /// <summary>
    /// 角色离开房间
    /// </summary>
    public const int PLAYER_LEAVE = 12;
    /// <summary>
    /// 设置角色身份
    /// </summary>
    public const int CHANGE_IDENTITY = 13;
    /// <summary>
    /// 开始游戏,隐藏准备文字
    /// </summary>
    public const int HIDE_READY_TEXT = 14;
    /// <summary>
    /// 设置玩家数据
    /// </summary>
    public const int SET_PLAYER_DATA = 15;
    /// <summary>
    /// 提示消息显示
    /// </summary>
    public const int PROMPT_PANEL_SHOW = int.MaxValue;
    /// <summary>
    /// 设置右边玩家聊天文字显示
    /// </summary>
    public const int SET_CHART_TEXT_RIGHT = 17;
    /// <summary>
    /// 自身聊天文字显示
    /// </summary>
    public const int SHOW_CHAT_TEXT_SELF = 18;
    /// <summary>
    /// 显示抢地主按钮
    /// </summary>
    public const int SHOW_GRAB_LAND_BTN = 19;
    /// <summary>
    /// 隐藏抢地主按钮
    /// </summary>
    public const int HIDE_GRAB_LAND_BTN = 20;
    /// <summary>
    /// 显示或者隐藏出牌按钮
    /// </summary>
    public const int SHOW_OR_HIDE_DEAL_BTN = 21;

    /// <summary>
    /// 显示结束面板
    /// </summary>
    public const int OVER_PANEL_ACTIVE = 22;

    /// <summary>
    /// 刷新结束面板
    /// </summary>
    public const int REFRESH_OVER_PANEL = 23;
}
