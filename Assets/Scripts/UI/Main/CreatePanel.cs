using Protocol.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : UIBase
{

    private InputField iptName;
    private Button btnButton;
    private PromptMsg promptMsg;

    private void Awake()
    {
        Bind(UIEvent.CREATE_PANEL_ACTIVE);
    }

    private void Start()
    {
        iptName = transform.Find("iptName").GetComponent<InputField>();
        btnButton = transform.Find("btnCreate").GetComponent<Button>();
        promptMsg = new PromptMsg();

        btnButton.onClick.AddListener(onbtnCreateClick);

        //设置默认显示状态
        SetPanelActive(false);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.CREATE_PANEL_ACTIVE:
                SetPanelActive((bool)message);
                break;
            default:
                break;
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        btnButton.onClick.RemoveListener(onbtnCreateClick);
    }

    private void onbtnCreateClick()
    {
        if (string.IsNullOrEmpty(iptName.text))
        {
            promptMsg.Change("请输入正确的用户名", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            return;
        }
        //向服务器发送创建用户请求
        //TODO
        Dispatch(AreaCode.NET, NetEvent.SENDMSG, new SocketMsg(OpCode.USER, UserSubCode.CREATE_USER_CREQ, iptName.text));
    }
}
