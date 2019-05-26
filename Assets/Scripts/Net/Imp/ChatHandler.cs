using Protocol.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol.Dto;


public class ChatHandler : HandlerBase
{
    public override void OnReceive(int subCode, object value)
    {
        switch (subCode)
        {
            case ChatCode.DEFAULT:
                chatBrocast(value);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 收到聊天响应的处理
    /// </summary>
    /// <param name="value"></param>
    private void chatBrocast(object value)
    {
        ChatDto chatDto = value as ChatDto;
        //判断左右玩家显示文字
        if (Caches.RoomDto.LeftId == chatDto.Uid)
        {
            Dispatch(AreaCode.UI, UIEvent.SET_CHART_TEXT_LEFT, chatDto.ChatType);
        }
        else
        {
            Dispatch(AreaCode.UI, UIEvent.SET_CHART_TEXT_RIGHT, chatDto.ChatType);
        }
        //播放声音
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_EFFECTAUDIO, "Chat/Chat_" + chatDto.ChatType.ToString());
    }
}