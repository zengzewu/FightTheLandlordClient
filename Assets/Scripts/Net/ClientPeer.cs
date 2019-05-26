using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class ClientPeer
{

    private Socket socket;
    private byte[] receiveCache;
    private List<byte> dataCache;
    private bool isProcess;
    private string ip;
    private int port;
    public Queue<SocketMsg> MsgQueue { get; set; }

    public ClientPeer()
    {
        
    }

    public ClientPeer(string ip, int port)
    {
        MsgQueue = new Queue<SocketMsg>();
        isProcess = false;
        receiveCache = new byte[1024];
        dataCache = new List<byte>();
        this.ip = ip;
        this.port = port;
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    public void Connect()
    {
        try
        {
            socket.Connect(ip, port);
            Debug.Log("连接服务器成功！");
            //开始异步接受数据
            StartReceive();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }



    private void StartReceive()
    {
        if (socket == null && !socket.Connected)
        {
            Debug.Log("服务器没有连接成功，无法发送数据...");
            return;
        }
           

        socket.BeginReceive(receiveCache, 0, 1024, SocketFlags.None, ReceiveCallback, socket);
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            int lenth = socket.EndReceive(ar);
            byte[] temByteArray = new byte[lenth];
            Buffer.BlockCopy(receiveCache, 0, temByteArray, 0, lenth);
            dataCache.AddRange(temByteArray);
            if (!isProcess)
                ProcessReceive();

            StartReceive();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        
    }

    private void ProcessReceive()
    {
        isProcess = true;
        byte[] data = EncodeTool.DecodeMessage(dataCache);
        if (data == null)
        {
            isProcess = false;
            return;
        }
        SocketMsg msg = EncodeTool.DecodeMsg(data);

        MsgQueue.Enqueue(msg);


        ProcessReceive();
    }

    public void Send(int opCode, int subCode, object value)
    {
        SocketMsg msg = new SocketMsg(opCode, subCode, value);
        byte[] packet = EncodeTool.EncodeMessage((EncodeTool.EncodeMsg(msg)));
        try
        {
            socket.Send(packet);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void Send(SocketMsg msg)
    {
        Send(msg.OpCode, msg.SubCode, msg.Value);
    }
}
