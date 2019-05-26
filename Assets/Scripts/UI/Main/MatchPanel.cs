using Protocol.Code;
using Protocol.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPanel : UIBase
{
    /// <summary>
    /// 开始匹配按钮
    /// </summary>
    private Button btnStart;
    private Image imgChara;
    private Text txtLoading;
    /// <summary>
    /// 取消匹配按钮
    /// </summary>
    private Button btnCancel;
    /// <summary>
    /// 进入房间按钮
    /// </summary>
    private Button btnEnter;

    private void Awake()
    {
        Bind(UIEvent.SHOW_ENTER_ROOM_BUTTOM);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SHOW_ENTER_ROOM_BUTTOM:
                {
                    btnEnter.gameObject.SetActive((bool)message);
                }
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        //取得引用
        btnStart = transform.Find("btnStart").GetComponent<Button>();
        imgChara = transform.Find("imgChara").GetComponent<Image>();
        txtLoading = transform.Find("txtLoading").GetComponent<Text>();
        btnCancel = transform.Find("btnCancel").GetComponent<Button>();
        btnEnter = transform.Find("btnEnter").GetComponent<Button>();

        //给按钮添加点击事件
        btnStart.onClick.AddListener(onbtnStartClick);
        btnCancel.onClick.AddListener(onbtnCancelClick);
        btnEnter.onClick.AddListener(onbtnEnterClick);

        //设置默认状态，隐藏
        setObjectActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        //移除事件
        btnStart.onClick.RemoveListener(onbtnStartClick);
        btnCancel.onClick.RemoveListener(onbtnCancelClick);
        btnEnter.onClick.RemoveListener(onbtnEnterClick);

    }

    private void setObjectActive(bool active)
    {
        imgChara.gameObject.SetActive(active);
        txtLoading.gameObject.SetActive(active);
        btnCancel.gameObject.SetActive(active);
        btnEnter.gameObject.SetActive(active);
    }

    /// <summary>
    /// 当进入房间按钮被点击
    /// </summary>
    private void onbtnEnterClick()
    {
        Dispatch(AreaCode.SCENE, SceneEvent.LOAD_SCENE, new SceneMsg(2, "2.fight", () =>
        {
            ////重置玩家位置
            //Caches.RoomDto.ResetPosition(Caches.UserDto.Id);
            //RoomDto roomDto = Caches.RoomDto;
            ////设置左边玩家的数据并显示头像
            //if (roomDto.LeftId != -1)
            //{
            //    Dispatch(AreaCode.UI, UIEvent.SET_LEFT_PLAYER_DATA, roomDto.uidlistDict[roomDto.LeftId]);
            //    Dispatch(AreaCode.UI, UIEvent.SET_HEAD_IMG_LEFT, true);
            //    //判断左边玩家是否已准备
            //    if (roomDto.readyuidList.Contains(roomDto.LeftId))
            //    {
            //        //显示准备按钮
            //        Dispatch(AreaCode.UI, UIEvent.PLAYER_READY, roomDto.LeftId);
            //    }
            //}
            ////设置右边玩家的数据并显示头像
            //if (roomDto.RightId != -1)
            //{
            //    Dispatch(AreaCode.UI, UIEvent.SET_RIGHT_PLAYER_DATA, roomDto.uidlistDict[roomDto.RightId]);
            //    Dispatch(AreaCode.UI, UIEvent.SET_HEAD_IMG_RGIHT, true);
            //    //判断右边玩家是否已准备
            //    if (roomDto.readyuidList.Contains(roomDto.RightId))
            //    {
            //        //显示准备按钮
            //        Dispatch(AreaCode.UI, UIEvent.PLAYER_READY, roomDto.RightId);
            //    }
            //}
        }));
    }

    /// <summary>
    /// 当取消按钮被点击
    /// </summary>
    private void onbtnCancelClick()
    {
        //发送离开房间的请求
        Dispatch(AreaCode.NET, NetEvent.SENDMSG, new SocketMsg(OpCode.MATCH, MatchCode.LEAVE_ROOM_REQ, null));
        //清空本地房间数据
        Caches.RoomDto = null;
        //隐藏面板信息
        setObjectActive(false);
    }

    /// <summary>
    /// 当开始按钮被点击
    /// </summary>
    private void onbtnStartClick()
    {
        //发送匹配请求
        Dispatch(AreaCode.NET, NetEvent.SENDMSG, new SocketMsg(OpCode.MATCH, MatchCode.MATCH_REQ, null));
        //显示面板信息
        imgChara.gameObject.SetActive(true);
        txtLoading.gameObject.SetActive(true);
        btnCancel.gameObject.SetActive(true);
    }
}
