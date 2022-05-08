using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgControl : MonoBehaviour
{
    public static bool isServer = true;
    public static bool isConnect;
    public Text inputText;
    public static Client client;
    public static Server server;

    public bool IsConnect { 
        get => isConnect;
        set
        {
            isConnect = value;
            Send("Connected");
        }
    }

    public void Send()
    {
        
        if(isServer == true)
        {
            server.Send(inputText.text);
        }
        else
        {
            client.Send(inputText.text);
        }
    }
    public static void Send(string str)  // 重载
    {
        
        if (isServer == true)
        {
            server.Send(str);
        }
        else
        {
            client.Send(str);
        }
    }

}
