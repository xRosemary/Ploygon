using UnityEngine.UI;
using UnityEngine;

public class Message : MonoBehaviour
{
    public Text connection;
    public Text chatText;

    private string msg;
    private bool connected;
    private bool hasMsg;
    public string Msg 
    { 
        get => msg;
        set
        {
            msg = value;
            ProcessMessage();
        }
    }

    private void ProcessMessage()
    {
        if (msg == null) return;

        if (msg.Equals("Connected"))
        {
            connected = true;
        }
        else
        {
            hasMsg = true;
        }
    }
    private void Update()
    {
        if (connected)
        {
            connection.text = "Connected";
            connected = false;
        }
        if (hasMsg)
        {
            chatText.text += "\n" + msg;
            hasMsg = false;
        }
    }
}
