using Protocol.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 缓存类
/// </summary>
public static class Caches
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public static UserDto UserDto { get; set; }
    /// <summary>
    /// 房间信息
    /// </summary>
    public static RoomDto RoomDto { get; set; }
    /// <summary>
    /// 静态构造函数
    /// </summary>
    static Caches()
    {
        UserDto = new UserDto();
        RoomDto = new RoomDto();
    }
}

