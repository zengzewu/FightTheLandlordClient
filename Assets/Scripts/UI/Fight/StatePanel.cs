using Protocol.Constant;
using Protocol.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePanel : UIBase
{
    /// <summary>
    /// 用户数据
    /// </summary>
    protected UserDto userDto;
    /// <summary>
    /// 头像图片
    /// </summary>
    protected Image imgHead;
    /// <summary>
    /// 准备文字
    /// </summary>
    protected Text txtReady;
    /// <summary>
    /// 聊天文字
    /// </summary>
    protected Text txtChart;
    /// <summary>
    /// 聊天背景动画
    /// </summary>
    protected Animation aniBg;
    /// <summary>
    /// 聊天文字动画
    /// </summary>
    protected Animation aniChart;

    protected virtual void Awake()
    {
        Bind(UIEvent.PLAYER_READY, UIEvent.PLAYER_LEAVE, UIEvent.HIDE_READY_TEXT, UIEvent.CHANGE_IDENTITY);
    }

    

    public override void Execute(int eventCode, object message)
    {
        

        switch (eventCode)
        {
            case UIEvent.PLAYER_READY:
                SetPlayerReady((int)message);
                break;
            case UIEvent.PLAYER_LEAVE:
                PlayerLeave((int)message);
                break;
            case UIEvent.HIDE_READY_TEXT:
                txtReady.gameObject.SetActive(false);
                break;
            case UIEvent.CHANGE_IDENTITY:
                ChangeIdentity((int)message);
                break;
        }


    }

    /// <summary>
    /// 改变玩家身份
    /// </summary>
    /// <param name="message"></param>
    private void ChangeIdentity(int lanUid)
    {
        if (this.userDto.Id == lanUid)
        {
            this.SetHeadImg(Identity.LANDLORD);
        }
        else
        {
            this.SetHeadImg(Identity.FARMER);
        }
    }




    protected virtual void Start()
    {
        imgHead = transform.Find("ImgHead").GetComponent<Image>();
        txtReady = transform.Find("TxtReady").GetComponent<Text>();
        txtChart = transform.Find("TxtChat").GetComponent<Text>();
        aniBg = transform.Find("ImgtxtBg").GetComponent<Animation>();
        aniChart = transform.Find("TxtChat").GetComponent<Animation>();
        SetDefaultState();
    }
    /// <summary>
    /// 设置默认状态,隐藏物体
    /// </summary>
    protected void SetDefaultState()
    {
        imgHead.gameObject.SetActive(false);
        txtReady.gameObject.SetActive(false);
    }
    /// <summary>
    /// 设置头像
    /// </summary>
    /// <param name="indentity"></param>
    protected void SetHeadImg(int indentity)
    {
        if (indentity == 1)//农民
        {
            imgHead.sprite = Resources.Load<Sprite>("Identity/Farmer");
        }
        else//地主
        {
            imgHead.sprite = Resources.Load<Sprite>("Identity/Landlord");
        }
    }
    /// <summary>
    /// 设置聊天文字
    /// </summary>
    /// <param name="id"></param>
    protected void SetChartText(int id)
    {
        switch (id)
        {
            case 1:
                txtChart.text = "大家好,很高兴见到各位";
                break;
            case 2:
                txtChart.text = "和你合作真是太愉快了";
                break;
            case 3:
                txtChart.text = "快点吧,我等到花儿都谢了";
                break;
            case 4:
                txtChart.text = "你的牌打的太好了";
                break;
            case 5:
                txtChart.text = "不要吵了,有什么好吵的,专心玩游戏吧";
                break;
            case 6:
                txtChart.text = "不要走,决战到天亮";
                break;
            case 7:
                txtChart.text = "再见了,我会想大家的";
                break;
            default:
                break;
        }
        aniBg.Stop();
        aniBg.Play();
        aniChart.Stop();
        aniChart.Play();
    }
    /// <summary>
    /// 设置玩家数据
    /// </summary>
    /// <param name="userDto"></param>
    protected void SetPlayerData(UserDto userDto)
    {
        this.userDto = userDto;
        this.imgHead.gameObject.SetActive(true);
    }
    /// <summary>
    /// 设置玩家准备
    /// </summary>
    /// <param name="uid"></param>
    protected void SetPlayerReady(int uid)
    {
        if (userDto == null)
            return;

        if (userDto.Id == uid)
        {
            this.txtReady.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// 玩家离开
    /// </summary>
    /// <param name="uid"></param>
    protected void PlayerLeave(int uid)
    {
        if (userDto == null)
            return;
        if (userDto.Id == (int)uid)
        {
            userDto = null;
            SetDefaultState();
        }
    }
}
