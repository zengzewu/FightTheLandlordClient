using Protocol.Code;
using Protocol.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MatchHandler : HandlerBase
{
    private PromptMsg promptMsg = new PromptMsg();

    public override void OnReceive(int subCode, object value)
    {
        switch (subCode)
        {
            case MatchCode.MATCH_SER://返回房间数据模型
                matchRes(value as RoomDto);
                break;
            case MatchCode.ENTER_ROOM_BRO:
                enterRoomRes(value as UserDto);
                break;
            case MatchCode.LEAVE_ROOM_BRO:
                leaveRoomRes((int)value);
                break;
            case MatchCode.UNREADY_BRO:
                break;
            case MatchCode.START_BRO:
                startBro();
                break;
            case MatchCode.READY_BRO:
                readyBro((int)value);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 玩家准备的广播处理
    /// </summary>
    private void readyBro(int readyUid)
    {
        //保存数据
        Caches.RoomDto.Ready(readyUid);
        //显示准备文字
        Dispatch(AreaCode.UI, UIEvent.PLAYER_READY, readyUid);
    }

    /// <summary>
    /// 开始游戏响应
    /// </summary>
    private void startBro()
    {
        promptMsg.Change("所有玩家都已准备，开始游戏", Color.green);
        Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
        Dispatch(AreaCode.UI, UIEvent.HIDE_READY_TEXT, null);
    }

    /// <summary>
    /// 有玩家离开房间响应
    /// </summary>
    /// <param name="value"></param>
    private void leaveRoomRes(int uid)
    {
        //提示消息
        promptMsg.Change(Caches.RoomDto.uidlistDict[uid].Name + "离开房间", UnityEngine.Color.yellow);
        Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
        //移除玩家
        Caches.RoomDto.Leave(uid);
        //刷新面板
        Dispatch(AreaCode.UI, UIEvent.PLAYER_LEAVE, uid);
    }

    /// <summary>
    /// 匹配响应
    /// </summary>
    /// <param name="roomDto"></param>
    private void matchRes(RoomDto roomDto)
    {
        Caches.RoomDto = roomDto;//保存房间数据模型
        Dispatch(AreaCode.UI, UIEvent.SHOW_ENTER_ROOM_BUTTOM, true);//显示进入房间按钮
    }

    /// <summary>
    /// 有用户进入房间的响应
    /// </summary>
    /// <param name="userDto"></param>
    private void enterRoomRes(UserDto userDto)
    {
        Caches.RoomDto.Add(userDto);
        promptMsg.Change(userDto.Name + "加入房间", UnityEngine.Color.green);
        Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
        //重置玩家位置
        Caches.RoomDto.ResetPosition(Caches.UserDto.Id);
        RoomDto roomDto = Caches.RoomDto;
        //设置左边玩家的数据并显示头像
        if (roomDto.LeftId != -1)
        {
            Dispatch(AreaCode.UI, UIEvent.SET_LEFT_PLAYER_DATA, roomDto.uidlistDict[roomDto.LeftId]);
        }
        //设置右边玩家的数据并显示头像
        if (roomDto.RightId != -1)
        {
            Dispatch(AreaCode.UI, UIEvent.SET_RIGHT_PLAYER_DATA, roomDto.uidlistDict[roomDto.RightId]);
        }
    }
}