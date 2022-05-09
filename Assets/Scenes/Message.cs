using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Message : MonoBehaviour
{
    public Text connection;
    public Text chatText;
    public MsgControl msgControl;
    public Create create;

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

        if (msg.Contains("Connected"))
        {
            if(msgControl.IsConnect == false)
            {
                Debug.Log("True");
                msgControl.IsConnect = true;
                connected = true;
            }

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
            if (msg.Contains("Create Game:"))
            {

                string subStr = msg.Substring(msg.IndexOf(':') + 1);
                string vertexStr = subStr.Substring(0, subStr.IndexOf('|') - 1);
                string edgeStr = subStr.Substring(subStr.IndexOf('|') + 1, subStr.LastIndexOf(',') - subStr.IndexOf('|') - 1);

                string[] vertex = vertexStr.Split(',');
                int[] vertexVal = Array.ConvertAll<string, int>(vertex, int.Parse);

                string[] edgeVal = edgeStr.Split(',');

                List<int> vertexList = new List<int>(vertexVal);
                List<string> edgeList = new List<string>(edgeVal);

                Create.vertexVal = vertexList;
                Create.edgeVal = edgeList;

                Create.n = vertexVal.Length;
                Create.m = edgeVal.Length;
                create.CreateGraph();
            }

            if(msg.Contains("Delete:"))
            {
                string subStr = msg.Substring(msg.IndexOf(':') + 1);
                int edgeID = int.Parse(subStr);
                Delete.DeleteEdge(edgeID);
            }

            if (msg.Contains("Backtrack:"))
            {

                string subStr = msg.Substring(msg.IndexOf(':') + 1);
                string vertexStr = subStr.Substring(0, subStr.IndexOf('|') - 1);
                string edgeStr = subStr.Substring(subStr.IndexOf('|') + 1, subStr.LastIndexOf(',') - subStr.IndexOf('|') - 1);

                string[] vertex = vertexStr.Split(',');
                int[] vertexVal = Array.ConvertAll<string, int>(vertex, int.Parse);

                string[] edgeVal = edgeStr.Split(',');

                List<int> vertexList = new List<int>(vertexVal);
                List<string> edgeList = new List<string>(edgeVal);

                Create.vertexVal = vertexList;
                Create.edgeVal = edgeList;

                Create.n = vertexVal.Length;
                Create.m = edgeVal.Length;
                create.CreateGraph();
            }
        }
    }
}
