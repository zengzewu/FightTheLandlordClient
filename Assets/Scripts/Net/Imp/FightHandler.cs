using Protocol.Code;
using Protocol.Constant;
using Protocol.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightHandler : HandlerBase
{

    public override void OnReceive(int subCode, object value)
    {
        switch (subCode)
        {
            case FightCode.GET_CARD_SRES:
                GetCard(value as List<CardDto>);
                break;
            case FightCode.GRAB_LANDLORD_BRO:
                GrabLandBro(value as GrabDto);
                break;
            case FightCode.LEAVE_BRO:
                break;
            case FightCode.TURN_DEAL_BRO:
                TurnDealBro((int)value);
                break;
            case FightCode.TURN_GRAB_BRO:
                TurnGrabBro((int)value);
                break;
            case FightCode.PASS_SRES:
                break;
            case FightCode.DEAL_SRES:
                DealResponse((int)value);
                break;
            case FightCode.DEAL_BRO:
                DealBro(value as DealDto);
                break;
            case FightCode.OVER:
                GameOver(value as OverDto);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    /// <param name="indentity"></param>
    private void GameOver(OverDto overDto)
    {
        //显示结束面板
        Dispatch(AreaCode.UI, UIEvent.OVER_PANEL_ACTIVE, true);

        //刷新信息
        Dispatch(AreaCode.UI, UIEvent.REFRESH_OVER_PANEL, overDto);
    }

    private void DealResponse(int value)
    {
        if(value==-1)
        {
            //管不上上一家出的牌
            PromptMsg promptMsg = new PromptMsg("玩家出的牌管不上上一个玩家出的牌", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            Dispatch(AreaCode.UI, UIEvent.SHOW_OR_HIDE_DEAL_BTN, true);
        }
    }

    /// <summary>
    /// 同步出牌
    /// </summary>
    /// <param name="dealDto"></param>
    private void DealBro(DealDto dealDto)
    {        
        //移除出完的手牌
        int eventCode = -1;
        if (dealDto.Uid == Caches.RoomDto.LeftId)
        {
            eventCode = CharacterEvent.REMOVE_LEFT_CARD;
        }
        else if (dealDto.Uid == Caches.RoomDto.RightId)
        {
            eventCode = CharacterEvent.REMOVE_RIGHT_CARD;
        }
        else if (dealDto.Uid == Caches.UserDto.Id)
        {
            eventCode = CharacterEvent.REMOVE_MY_CARD;
        }
        Dispatch(AreaCode.CHARACTER, eventCode, dealDto);

        //显示到桌面上
        Dispatch(AreaCode.CHARACTER, CharacterEvent.UPDATE_SHOW_DESK, dealDto.Cards);
        //播放出牌音效
        PlayDealAudio(dealDto.Type, dealDto.Weight);
    }

    private void PlayDealAudio(int cardType, int weight)
    {
        string audioName = "Fight/";

        switch (cardType)
        {
            case CardType.SINGLE:
                audioName += "Woman_" + weight;
                break;
            case CardType.DOUBLE:
                audioName += "Woman_dui" + weight/2;
                break;
            case CardType.STRAIGHT:
                audioName += "Woman_shunzi";
                break;
            case CardType.LIANDUI:
                audioName += "Woman_liandui";
                break;
            case CardType.THREEWITHSINGLE:
                audioName += "Woman_sandaiyi";
                break;
            case CardType.THREEWITHDOUBLE:
                audioName += "Woman_sandaiyidui";
                break;
            case CardType.THREEWITHNOTHING:
                audioName += "Woman_tuple"+weight/3;
                break;
            case CardType.BOOM:
                audioName += "Woman_zhadan";
                break;
            case CardType.JOKERBOOM:
                audioName += "Woman_wangzha";
                break;
            default:
                break;
        }

        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_EFFECTAUDIO, audioName);
    }

    /// <summary>
    /// 收到服务器的出牌命令
    /// </summary>
    /// <param name="uid"></param>
    private void TurnDealBro(int uid)
    {
        Dispatch(AreaCode.UI, UIEvent.SHOW_OR_HIDE_DEAL_BTN, false);
        if (Caches.UserDto.Id == uid)
        {
            Dispatch(AreaCode.UI, UIEvent.SHOW_OR_HIDE_DEAL_BTN, true);
        }
    }

    /// <summary>
    /// 有人抢地主的响应
    /// </summary>
    /// <param name="grabDto"></param>
    private void GrabLandBro(GrabDto grabDto)
    {
        int landUid = grabDto.Uid;
        List<CardDto> tableCards = grabDto.TableCards;
        //通知UI设置地主头像
        Dispatch(AreaCode.UI, UIEvent.CHANGE_IDENTITY, landUid);
        //显示底牌
        Dispatch(AreaCode.UI, UIEvent.SET_BOTTOM_CARD, grabDto.TableCards.ToArray());
        //播放抢地主的声音
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_EFFECTAUDIO, "Fight/Woman_Order");
        //给对应玩家发送底牌
        int eventCode = -1;
        if (grabDto.Uid == Caches.RoomDto.LeftId)
        {
            eventCode = CharacterEvent.LEFT_PLAYER_ADD_CARD;
        }
        else if (grabDto.Uid == Caches.RoomDto.RightId)
        {
            eventCode = CharacterEvent.RIGHT_PLAYER_ADD_CARD;
        }
        else
        {
            eventCode = CharacterEvent.MY_PLAYER_ADD_CARD;
        }
        Dispatch(AreaCode.CHARACTER, eventCode, grabDto);
    }


    /// <summary>
    /// 服务器通知客户端开始抢地主
    /// </summary>
    /// <param name="uid"></param>
    private void TurnGrabBro(int uid)
    {
        if (Caches.UserDto.Id == uid)
        {
            Dispatch(AreaCode.UI, UIEvent.SHOW_GRAB_LAND_BTN, true);
        }
    }

    /// <summary>
    /// 开始游戏获取手牌
    /// </summary>
    /// <param name="list"></param>
    private void GetCard(List<CardDto> cardList)
    {
        Dispatch(AreaCode.CHARACTER, CharacterEvent.MY_PLAYER, cardList);
        Dispatch(AreaCode.CHARACTER, CharacterEvent.LEFT_PLAYER, null);
        Dispatch(AreaCode.CHARACTER, CharacterEvent.RIGHT_PLAYER, null);
    }
}
