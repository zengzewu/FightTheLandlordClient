using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HandlerBase
{
    public abstract void OnReceive(int subCode, object value);

    public void Dispatch(int areaCode, int eventCode, object msg)
    {
        MsgCenter.Instance.Dispatch(areaCode, eventCode, msg);
    }
}
