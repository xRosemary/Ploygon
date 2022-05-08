using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChatField : MonoBehaviour
{
    public Text inputText;
    public Client client;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendMsg()
    {
        client.Send(inputText.text);
    }
}
