using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol.Code;
using Protocol.Dto;

public class UserHandler : HandlerBase
{
    /// <summary>
    /// 收到请求
    /// </summary>
    /// <param name="subCode">子操作码</param>
    /// <param name="value">传输数据</param>
    public override void OnReceive(int subCode, object value)
    {
        switch(subCode)
        {
            case UserSubCode.CREATE_USER_SRES:
                createUserResponse(value as UserDto);
                break;
            case UserSubCode.GET_USER_INFO_SRES:
                getUserInfoResponse(value as UserDto);
                break;
        }
    }
    /// <summary>
    /// 获取角色的响应
    /// </summary>
    /// <param name="userDto">角色数据传输模型</param>
    private void getUserInfoResponse(UserDto userDto)
    {
        if(userDto==null)
        {
            //未获取到角色信息,显示创建面板
            Dispatch(AreaCode.UI, UIEvent.CREATE_PANEL_ACTIVE, true);
            return;
        }
        //获取到角色信息,将角色信息显示在客户端,保存角色信息
        Dispatch(AreaCode.UI, UIEvent.REFRESH_INFO_PANEL, userDto);
        Caches.UserDto = userDto;
    }
    /// <summary>
    /// 创建角色的响应
    /// </summary>
    /// <param name="userDto">角色数据传输模型</param>
    private void createUserResponse(UserDto userDto)
    {
        if (userDto == null)
            return;
        //将角色信息显示在客户端,保存角色信息,隐藏创建面板
        Dispatch(AreaCode.UI, UIEvent.REFRESH_INFO_PANEL, userDto);
        Caches.UserDto = userDto;
        Dispatch(AreaCode.UI, UIEvent.CREATE_PANEL_ACTIVE, false);
    }
}
