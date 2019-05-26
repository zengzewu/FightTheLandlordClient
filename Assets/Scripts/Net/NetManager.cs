using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol.Code;

public class NetManager : ManagerBase
{

    public static NetManager Instance;

    private ClientPeer clientPeer = new ClientPeer("127.0.0.1", 9999);

    private HandlerBase accountHandler = new AccountHandler();
    private HandlerBase userHandler = new UserHandler();
    private HandlerBase matchHandler = new MatchHandler();
    private HandlerBase chatHandler = new ChatHandler();
    private HandlerBase fightHandler = new FightHandler();

    private void Awake()
    {
        Instance = this;
        Add(NetEvent.SENDMSG, this);
    }

    private void Start()
    {
        clientPeer.Connect();
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case NetEvent.SENDMSG:
                clientPeer.Send((SocketMsg)message);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (clientPeer == null)
            return;

        while (clientPeer.MsgQueue.Count > 0)
        {
            SocketMsg msg = clientPeer.MsgQueue.Dequeue();

            ProcessMsg(msg);
        }
    }

    private void ProcessMsg(SocketMsg msg)
    {
        switch (msg.OpCode)
        {
            case OpCode.ACCOUNT:
                {
                    accountHandler.OnReceive(msg.SubCode, msg.Value);
                }
                break;
            case OpCode.USER:
                {
                    userHandler.OnReceive(msg.SubCode, msg.Value);
                }
                break;
            case OpCode.MATCH:
                {
                    matchHandler.OnReceive(msg.SubCode, msg.Value);
                }
                break;
            case OpCode.CHAT:
                {
                    chatHandler.OnReceive(msg.SubCode, msg.Value);
                }
                break;
            case OpCode.FIGHT:
                {
                    fightHandler.OnReceive(msg.SubCode, msg.Value);
                }
                break;
            default:
                break;
        }
    }
}
