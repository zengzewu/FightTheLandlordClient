using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : UIBase {

    private Button startBtn;
    private Button registeBtn;

    private void Start()
    {
        startBtn = transform.Find("StartBtn").GetComponent<Button>();
        registeBtn = transform.Find("RegisteBtn").GetComponent<Button>();

        startBtn.onClick.AddListener(StartBtnOnClick);
        registeBtn.onClick.AddListener(RegisteBtnOnClick);
    }

    private void StartBtnOnClick()
    {
        Dispatch(AreaCode.UI, UIEvent.START_PANEL_ACTIVE, true);
    }

    private void RegisteBtnOnClick()
    {
        Dispatch(AreaCode.UI, UIEvent.REGISTE_PANEL_ACTIVE, true);
    }
}
