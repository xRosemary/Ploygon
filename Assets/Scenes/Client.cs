using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class Client : MonoBehaviour
{
    public Text InputFiled;
    public Message message;

    private string ipAddress;
    private Socket socket;
    private byte[] buffer = new byte[1024];

    public void ConnectedToServer()
    {
        this.ipAddress = InputFiled.text;
        if (ipAddress == null)
        {
            return;
        }
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(ipAddress, 6666);
        StartReceive();
        Send("Connected");
    }

    void StartReceive()
    {
        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
    }

    void ReceiveCallback(IAsyncResult asyncResult)
    {
        int len = socket.EndReceive(asyncResult);
        if (len == 0) return;

        message.Msg = Encoding.UTF8.GetString(buffer, 0, len);
        StartReceive();
    }
    public void Send(string msg)
    {
        socket.Send(Encoding.UTF8.GetBytes(msg));
    }
    private void OnDestroy()
    {
        if(socket != null)
        {
            socket.Close();
        }
    }
}
