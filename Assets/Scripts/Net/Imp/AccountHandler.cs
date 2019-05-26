using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol.Code;

public class AccountHandler : HandlerBase
{
    private PromptMsg promptMsg = new PromptMsg();

    public override void OnReceive(int subCode, object value)
    {
        switch (subCode)
        {
            case AccountSubCode.REGISTE_SRES:
                {
                    Register(value.ToString());
                }
                break;
            case AccountSubCode.LOGIN_SRES:
                {
                    Login(value.ToString());
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 注册响应
    /// </summary>
    /// <param name="value"></param>
    private void Register(string value)
    {
        if (value == "账号已经存在")
        {
            promptMsg.Change("账号已经存在", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            return;
        }
        if (value == "输入的账号不合法")
        {
            promptMsg.Change("输入的账号不合法", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            return;
        }
        if (value == "密码长度不合法")
        {
            promptMsg.Change("密码长度不合法", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            return;
        }
        if (value == "注册成功")
        {
            promptMsg.Change("注册成功", Color.green);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            return;
        }
    }
    /// <summary>
    /// 登录响应
    /// </summary>
    /// <param name="value"></param>
    private void Login(string value)
    {
        if (value == "账号不存在")
        {
            promptMsg.Change("账号不存在", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            return;
        }
        if (value == "账号在线")
        {
            promptMsg.Change("账号在线", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            return;
        }
        if (value == "密码错误")
        {
            promptMsg.Change("密码错误", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            return;
        }
        if (value == "登录成功")
        {
            promptMsg.Change("登录成功", Color.green);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            //TODO 跳转场景
            SceneMsg sceneMsg = new SceneMsg(1, "1.mian", () =>
            {
                SocketMsg socketMsg = new SocketMsg(OpCode.USER, UserSubCode.GET_USER_INFO_CREQ, null);
                Dispatch(AreaCode.NET, NetEvent.SENDMSG, socketMsg);
            });

            Dispatch(AreaCode.SCENE, SceneEvent.LOAD_SCENE, sceneMsg);

            return;
        }
    }
}
