using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SocketMsg
{
    public int OpCode { get; set; }

    public int SubCode { get; set; }

    public object Value { get; set; }

    public SocketMsg()
    {

    }

    public SocketMsg(int opCode, int subCode, object value)
    {
        OpCode = opCode;
        SubCode = subCode;
        Value = value;
    }

    public void Change(int opCode, int subCode, object value)
    {
        OpCode = opCode;
        SubCode = subCode;
        Value = value;
    }
}
