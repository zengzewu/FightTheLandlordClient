using Protocol.Code;
using Protocol.Constant;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverPanel : UIBase
{

    private Text txtWinIdentity;
    private Text txtBeenNum;
    private Button btnReMenu;


    private void Awake()
    {
        Bind(UIEvent.OVER_PANEL_ACTIVE, UIEvent.REFRESH_OVER_PANEL);
    }

    public override void Execute(int eventCode, object message)
    {
        switch(eventCode)
        {
            case UIEvent.OVER_PANEL_ACTIVE:
                gameObject.SetActive((bool)message);
                break;
            case UIEvent.REFRESH_OVER_PANEL:
                RefreSh(message as OverDto);
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        //获取组件
        txtWinIdentity = transform.Find("TxtWinIdentity").GetComponent<Text>();
        txtBeenNum = transform.Find("TxtBeenNum").GetComponent<Text>();
        btnReMenu = transform.Find("BtnReMenu").GetComponent<Button>();

        //给按钮绑定事件
        btnReMenu.onClick.AddListener(OnReMenuClick);


        //隐藏面板
        gameObject.SetActive(false);
    }



    /// <summary>
    /// 返回主菜单按钮被点击
    /// </summary>
    private void OnReMenuClick()
    {
        SceneMsg sceneMsg = new SceneMsg(1, "1.mian", () =>
        {
            SocketMsg socketMsg = new SocketMsg(OpCode.USER, UserSubCode.GET_USER_INFO_CREQ, null);
            Dispatch(AreaCode.NET, NetEvent.SENDMSG, socketMsg);
        });
        Dispatch(AreaCode.SCENE, SceneEvent.LOAD_SCENE, sceneMsg);
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreSh(OverDto overDto)
    {
        string identity = Identity.GetString(overDto.WinIdentity);
        int been = overDto.BeenCount;
        List<int> winList = overDto.WinUidList;


        //判断自己是否胜利
        if(winList.Contains(Caches.UserDto.Id))
        {
            txtWinIdentity.text = identity + "胜利";

            if(overDto.WinIdentity==Identity.LANDLORD)
            {
                txtBeenNum.text = "+" + overDto.BeenCount * 2;
            }
            else
            {
                txtBeenNum.text = "+" + overDto.BeenCount;
            }

        }
        else
        {
            txtWinIdentity.text = Identity.GetOpposite(overDto.WinIdentity) + "失败";
            if (overDto.WinIdentity == Identity.LANDLORD)
            {
                txtBeenNum.text = "-" + overDto.BeenCount ;
            }
            else
            {
                txtBeenNum.text = "-" + overDto.BeenCount*2;
            }
        }
    }

}
