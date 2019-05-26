using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : UIBase
{

    private Text nameTxt;
    private Text lvTxt;
    private Text expTxt;
    private Text beanTxt;
    private Slider expSlider;


    private void Awake()
    {
        Bind(UIEvent.REFRESH_INFO_PANEL);
    }


    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.REFRESH_INFO_PANEL:
                {
                    UserDto userDto = message as UserDto;
                    RefreshPanel(userDto.Name, userDto.Lv, userDto.Exp, userDto.Been);
                }
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        nameTxt = transform.Find("NameTxt").GetComponent<Text>();
        lvTxt = transform.Find("LvTxt").GetComponent<Text>();
        expTxt = transform.Find("ExpTxt").GetComponent<Text>();
        beanTxt = transform.Find("BeanTxt").GetComponent<Text>();
        expSlider = transform.Find("ExpSlider").GetComponent<Slider>();
    }

    /// <summary>
    /// 刷新面板
    /// </summary>
    /// <param name="name"></param>
    /// <param name="lv"></param>
    /// <param name="exp"></param>
    /// <param name="bean"></param>
    private void RefreshPanel(string name, int lv, int exp, int bean)
    {
        this.nameTxt.text = name;
        this.lvTxt.text = "Lv." + lv;
        this.expTxt.text = exp + "/" + lv * 100;
        this.beanTxt.text = "X" + bean;
        this.expSlider.value = (float)exp / (lv * 100);
    }

}
