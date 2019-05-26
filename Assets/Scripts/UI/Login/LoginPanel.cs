using Protocol.Code;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : UIBase
{

    private InputField accountTxt;
    private InputField pwdTxt;
    private Button loginBtn;
    private Button cancelBtn;

    private PromptMsg promptMsg=new PromptMsg();

    private SocketMsg msg = new SocketMsg();

    private void Awake()
    {
        Bind(UIEvent.START_PANEL_ACTIVE);
    }

    private void Start()
    {

        accountTxt = transform.Find("AccountInput").GetComponent<InputField>();
        pwdTxt = transform.Find("PwdInput").GetComponent<InputField>();
        loginBtn = transform.Find("LoginBtn").GetComponent<Button>();
        cancelBtn = transform.Find("CancelBtn").GetComponent<Button>();


        loginBtn.onClick.AddListener(LoginBtnOnClick);
        cancelBtn.onClick.AddListener(CancelBtnOnClick);

        SetPanelActive(false);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.START_PANEL_ACTIVE:
                SetPanelActive((bool)message);
                break;
            default:
                break;
                
        }
    }

    private void CancelBtnOnClick()
    {
        SetPanelActive(false);
    }

    private void LoginBtnOnClick()
    {
        string acc = accountTxt.text;
        string pwd = pwdTxt.text;

        //输入校验
        if(string.IsNullOrEmpty(acc))
        {
            promptMsg.Change("账号不能为空", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            return;
        }
        if(string.IsNullOrEmpty(pwd))
        {
            promptMsg.Change("密码不能为空", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            return;
        }
        if(pwd.Length<4||pwd.Length>16)
        {
            promptMsg.Change("密码长度不合法", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            return;
        }

        AccountDto accountDto = new AccountDto(acc, pwd);
        msg.Change(OpCode.ACCOUNT, AccountSubCode.LOGIN_CREQ, accountDto);
        Dispatch(AreaCode.NET, NetEvent.SENDMSG, msg);
    }

}
