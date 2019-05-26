using Protocol.Code;
using Protocol.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class SelfPanel : StatePanel
{
    /// <summary>
    /// 准备按钮
    /// </summary>
    private Button btnReady;
    /// <summary>
    /// 出牌按钮
    /// </summary>
    private Button btnPlay;
    /// <summary>
    /// 不出牌按钮
    /// </summary>
    private Button btnPass;
    /// <summary>
    /// 叫地主按钮
    /// </summary>
    private Button btnCTL;
    /// <summary>
    /// 不叫地主按钮
    /// </summary>
    private Button btnNCTL;

    private void SetPosition()
    {
        SetPlayerData(Caches.UserDto);
        //重置玩家位置
        Caches.RoomDto.ResetPosition(Caches.UserDto.Id);
        RoomDto roomDto = Caches.RoomDto;
        //设置左边玩家的数据并显示头像
        if (roomDto.LeftId != -1)
        {
            Dispatch(AreaCode.UI, UIEvent.SET_LEFT_PLAYER_DATA, roomDto.uidlistDict[roomDto.LeftId]);
            //判断左边玩家是否已准备
            if (roomDto.readyuidList.Contains(roomDto.LeftId))
            {
                //显示准备按钮
                Dispatch(AreaCode.UI, UIEvent.PLAYER_READY, roomDto.LeftId);
            }
        }
        //设置右边玩家的数据并显示头像
        if (roomDto.RightId != -1)
        {
            Dispatch(AreaCode.UI, UIEvent.SET_RIGHT_PLAYER_DATA, roomDto.uidlistDict[roomDto.RightId]);
            //判断右边玩家是否已准备
            if (roomDto.readyuidList.Contains(roomDto.RightId))
            {
                //显示准备按钮
                Dispatch(AreaCode.UI, UIEvent.PLAYER_READY, roomDto.RightId);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Bind(UIEvent.SHOW_CHAT_TEXT_SELF, UIEvent.SHOW_GRAB_LAND_BTN,UIEvent.SHOW_OR_HIDE_DEAL_BTN);
    }

    public override void Execute(int eventCode, object message)
    {
        base.Execute(eventCode, message);

        switch (eventCode)
        {
            case UIEvent.SHOW_CHAT_TEXT_SELF:
                //显示文字
                SetChartText((int)message);
                //播放声音
                Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_EFFECTAUDIO, "Chat/Chat_" + message.ToString());
                break;
            case UIEvent.SHOW_GRAB_LAND_BTN:
                ShowGrabLandBtn((bool)message);
                break;
            case UIEvent.SHOW_OR_HIDE_DEAL_BTN:
                SetDealActive((bool)message);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置出牌按钮的状态
    /// </summary>
    /// <param name="active"></param>
    private void SetDealActive(bool active)
    {
        btnPlay.gameObject.SetActive(active);
        btnPass.gameObject.SetActive(active);
    }


    /// <summary>
    /// 显示叫地主按钮
    /// </summary>
    /// <param name="active"></param>
    private void ShowGrabLandBtn(bool active)
    {
        this.btnCTL.gameObject.SetActive(active);
        this.btnNCTL.gameObject.SetActive(active);
    }

    protected override void Start()
    {
        base.Start();
        btnReady = transform.Find("BtnReady").GetComponent<Button>();
        btnPlay = transform.Find("BtnPush").GetComponent<Button>();
        btnPass = transform.Find("BtnNoPush").GetComponent<Button>();
        btnCTL = transform.Find("BtnGet").GetComponent<Button>();
        btnNCTL = transform.Find("BtnNoGet").GetComponent<Button>();

        btnReady.onClick.AddListener(btnReadyClick);
        btnPlay.onClick.AddListener(btnPlayClick);
        btnPass.onClick.AddListener(btnPassClick);
        btnCTL.onClick.AddListener(btnCTLClick);
        btnNCTL.onClick.AddListener(btnNCTLClick);

        imgHead.gameObject.SetActive(true);

        SetDefault();
        SetPosition();


    }
    /// <summary>
    /// 不叫地主按钮被点击
    /// </summary>
    private void btnNCTLClick()
    {
        //通知网络模块发送叫地主请求
        Dispatch(AreaCode.NET, NetEvent.SENDMSG, new SocketMsg() { OpCode = OpCode.FIGHT, SubCode = FightCode.GRAB_LANDLORD_CREQ, Value = false });
        //隐藏按钮
        Dispatch(AreaCode.UI, UIEvent.SHOW_GRAB_LAND_BTN, false);
    }
    /// <summary>
    /// 叫地主按钮被点击
    /// </summary>
    private void btnCTLClick()
    {
        //通知网络模块发送叫地主请求
        Dispatch(AreaCode.NET, NetEvent.SENDMSG, new SocketMsg() {  OpCode=OpCode.FIGHT, SubCode=FightCode.GRAB_LANDLORD_CREQ, Value=true });
        //隐藏按钮
        Dispatch(AreaCode.UI, UIEvent.SHOW_GRAB_LAND_BTN, false);
    }
    /// <summary>
    /// 不出牌按钮被点击
    /// </summary>
    private void btnPassClick()
    {
        Dispatch(AreaCode.NET,0, new SocketMsg(OpCode.FIGHT, FightCode.PASS_CREQ, null));
    }
    /// <summary>
    /// 出牌按钮被点击
    /// </summary>
    private void btnPlayClick()
    {
        Dispatch(AreaCode.CHARACTER, CharacterEvent.DEAL_CARD, null);
    }
    /// <summary>
    /// 准备按钮被点击
    /// </summary>
    private void btnReadyClick()
    {
        Dispatch(AreaCode.NET, NetEvent.SENDMSG, new SocketMsg(OpCode.MATCH, MatchCode.READY_REQ, null));
        btnReady.gameObject.SetActive(false);
        txtReady.gameObject.SetActive(true);
    }
    /// <summary>
    /// 设置默认状态
    /// </summary>
    private void SetDefault()
    {
        btnPlay.gameObject.SetActive(false);
        btnPass.gameObject.SetActive(false);
        btnCTL.gameObject.SetActive(false);
        btnNCTL.gameObject.SetActive(false);
    }
}
