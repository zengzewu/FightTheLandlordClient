using Protocol.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottonPanel : UIBase
{
    /// <summary>
    /// 豆子数量
    /// </summary>
    private Text txtbeen;
    /// <summary>
    /// 倍数
    /// </summary>
    private Text txtmutiply;
    /// <summary>
    /// 快捷喊话按钮
    /// </summary>
    private Button btnChart;
    /// <summary>
    /// 快捷喊话面板
    /// </summary>
    private Image imgBg;
    /// <summary>
    /// 聊天文字按钮
    /// </summary>
    private Button[] chatxtBtns;
    /// <summary>
    /// 聊天内容对象
    /// </summary>
    private SocketMsg socketMsg;

    private void Start()
    {
        txtbeen = transform.Find("txtbeennum").GetComponent<Text>();
        txtmutiply = transform.Find("txtmutiply").GetComponent<Text>();
        btnChart = transform.Find("btnChart").GetComponent<Button>();
        imgBg = transform.Find("imgBg").GetComponent<Image>();
        chatxtBtns = new Button[7];
        socketMsg = new SocketMsg();

        for (int i = 0; i < 7; i++)
        {
            chatxtBtns[i] = imgBg.transform.GetChild(i).GetComponent<Button>();
            int x = i + 1;
            chatxtBtns[i].onClick.AddListener(() =>
            {
                btnChatxtClick(x);
            });

        }



        btnChart.onClick.AddListener(btnChartClick);

        imgBg.gameObject.SetActive(false);

        FlushUserInfo();
        changeMutiply(1);

    }
    /// <summary>
    /// 刷新本地角色豆子数量
    /// </summary>
    private void FlushUserInfo()
    {
        txtbeen.text = "x" + Caches.UserDto.Been;
    }
    /// <summary>
    /// 改变倍数
    /// </summary>
    private void changeMutiply(int mutiply)
    {
        txtmutiply.text = "倍数 x" + mutiply;
    }

    /// <summary>
    /// 快捷喊话按钮被点击
    /// </summary>
    private void btnChartClick()
    {
        imgBg.gameObject.SetActive(!imgBg.IsActive());
    }
    /// <summary>
    /// 聊天文字按钮被点击
    /// </summary>
    /// <param name="index"></param>
    private void btnChatxtClick(int index)
    {
        socketMsg.Change(OpCode.CHAT, ChatCode.DEFAULT, index);
        Dispatch(AreaCode.NET, NetEvent.SENDMSG, socketMsg);
        Dispatch(AreaCode.UI, UIEvent.SHOW_CHAT_TEXT_SELF, index);
    }
}
