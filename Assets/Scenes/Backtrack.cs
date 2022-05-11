using System;
using System.Collections.Generic;
using UnityEngine;

public class Backtrack : MonoBehaviour
{
    // Start is called before the first frame update
    public static Stack<Tuple<List<int>, List<string>>> stack;
    public static Create create;
    private void Start()
    {
        stack = new Stack<Tuple<List<int>,List<string>>>();
    }
    public static void Rollback()
    {
        if(stack.Count > 0)
        {
            Tuple<List<int>, List<string>> t = stack.Pop();
            
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Graph");

            Create.circleArr = new List<GameObject>();
            Create.edgeArr = new List<GameObject>();
            Create.operatorArr = new List<GameObject>();

            Create.vertexVal = t.Item1;
            Create.edgeVal = t.Item2;
            Create.n = Create.vertexVal.Count;
            Create.m = Create.edgeVal.Count;

            foreach (GameObject go in obj)
            {
                Destroy(go);
            }
            
            if(stack.Count == 0)
            {
                Create.clickTime = 0;
            }
            create.CreateGraph();
        }
    }

    public static void Push(List<int> vertex, List<string> edge)
    {
        List<int> vCopy = new List<int>(vertex);
        List<string> eCopy = new List<string>(edge);
        string str = "";
        foreach(int i in vCopy)
        {
            str += i.ToString() + ",";
        }
        Debug.Log(str);
        str = "";
        foreach (string i in eCopy)
        {
            str += i.ToString() + ",";
        }
        Debug.Log(str);


        Tuple<List<int>, List<string>> t = Tuple.Create(vCopy, eCopy);

        stack.Push(t);
        
    }
}
