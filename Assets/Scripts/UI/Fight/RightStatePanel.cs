using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightStatePanel : StatePanel
{

    protected override void Awake()
    {
        base.Awake();

        Bind(UIEvent.SET_RIGHT_PLAYER_DATA, UIEvent.SET_CHART_TEXT_RIGHT);
    }

    public override void Execute(int eventCode, object message)
    {
        base.Execute(eventCode, message);

        switch (eventCode)
        {
            case UIEvent.SET_RIGHT_PLAYER_DATA:
                SetPlayerData(message as UserDto);
                break;
            case UIEvent.SET_CHART_TEXT_RIGHT:
                SetChartText((int)message);
                break;
        }
    }
}
