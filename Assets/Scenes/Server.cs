using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class Server : MonoBehaviour
{
    private Socket socket;
    private byte[] buffer = new byte[1024];

    public Text localhost;
    public Message message;
    void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind(new IPEndPoint(IPAddress.Any, 6666));
        socket.Listen(0);
        StartAccept();
        foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (_IPAddress.AddressFamily == AddressFamily.InterNetwork)
            {
                localhost.text = "本机IP：" + _IPAddress.ToString();
                break;
            }
        }
    }

    void StartAccept()
    {
        socket.BeginAccept(AcceptCallback, null);
    }

    void StartReceive(Socket client)
    {
        client.BeginReceive(buffer, 0, buffer.Length,SocketFlags.None, ReceiveCallback, client);
    }

    void AcceptCallback(IAsyncResult asyncResult)
    {
        Socket client = socket.EndAccept(asyncResult);
        StartReceive(client);
        StartAccept();
    }
    void ReceiveCallback(IAsyncResult asyncResult)
    {
        Socket client = asyncResult.AsyncState as Socket;
        int len = client.EndReceive(asyncResult);
        if (len == 0) return;
        message.Msg = Encoding.UTF8.GetString(buffer, 0, len);
        StartReceive(client);
    }

    private void OnDestroy()
    {
        if(socket != null)
        {
            socket.Close();
        }
    }
}
