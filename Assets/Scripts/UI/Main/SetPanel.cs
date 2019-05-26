using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SetPanel : UIBase
{
    private Button btnSet;
    private Button btnClose;
    private Toggle togAudio;
    private Slider sdlVolume;
    private Button btnQuit;
    private Image imgBg;
    private Text txtVolumSwitch;
    private Text txtVolumSize;

    private void Start()
    {
        //取得引用
        btnSet = transform.Find("btnSet").GetComponent<Button>();
        btnClose = transform.Find("btnClose").GetComponent<Button>();
        togAudio = transform.Find("Toggle").GetComponent<Toggle>();
        sdlVolume = transform.Find("Slider").GetComponent<Slider>();
        btnQuit = transform.Find("btnExit").GetComponent<Button>();
        imgBg = transform.Find("imgBg").GetComponent<Image>();
        txtVolumSwitch = transform.Find("txtVolumSwitch").GetComponent<Text>();
        txtVolumSize = transform.Find("txtVolumSize").GetComponent<Text>();

        btnSet.onClick.AddListener(onbtnSetClick);
        btnClose.onClick.AddListener(onbtnCloseClick);
        togAudio.onValueChanged.AddListener(ontogAudioClick);
        btnQuit.onClick.AddListener(onbtnQuitClick);
        sdlVolume.onValueChanged.AddListener(onValueChange);

        //默认状态
        setObjectActive(false);
    }

    /// <summary>
    /// 当滑动条的值改变时执行
    /// </summary>
    /// <param name="arg0"></param>
    private void onValueChange(float arg0)
    {
        //操作音量
        //TODO
    }

    /// <summary>
    /// 退出按钮被点击时执行
    /// </summary>
    private void onbtnQuitClick()
    {
        Application.Quit();
    }

    /// <summary>
    /// 声音开关按钮被点击时执行
    /// </summary>
    /// <param name="arg0"></param>
    private void ontogAudioClick(bool arg0)
    {

        //操作声音
        //TODO
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        btnSet.onClick.RemoveListener(onbtnSetClick);
        btnClose.onClick.RemoveListener(onbtnCloseClick);
        togAudio.onValueChanged.RemoveListener(ontogAudioClick);
        btnQuit.onClick.RemoveListener(onbtnQuitClick);
        sdlVolume.onValueChanged.RemoveListener(onValueChange);
    }

    /// <summary>
    /// 关闭按钮被点击时执行
    /// </summary>
    private void onbtnCloseClick()
    {
        setObjectActive(false);
    }

    private void setObjectActive(bool active)
    {
        btnClose.gameObject.SetActive(active);
        togAudio.gameObject.SetActive(active);
        sdlVolume.gameObject.SetActive(active);
        btnQuit.gameObject.SetActive(active);
        imgBg.gameObject.SetActive(active);
        txtVolumSize.gameObject.SetActive(active);
        txtVolumSwitch.gameObject.SetActive(active);
    }

    /// <summary>
    /// 设置按钮被点击时执行
    /// </summary>
    private void onbtnSetClick()
    {
        setObjectActive(true);
    }
}
