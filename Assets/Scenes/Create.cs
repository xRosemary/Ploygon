using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Create : MonoBehaviour
{
    static public List<GameObject> circleArr = new List<GameObject>();
    static public List<GameObject> edgeArr = new List<GameObject>();
    static public List<GameObject> operatorArr = new List<GameObject>();
    static public string[] operator_4 = new string[] { "+", "*"};

    static public int edgeId = -1;
    static public bool isDeleted = false;
    static public int n;
    static public int clickTime = 0;

    public GameObject circle;
    public GameObject edge;
    public GameObject calcSign;
    public float R;

    static public float width = Screen.width / 2;
    static public float height = Screen.height / 2;

    private float delta = 2 * Mathf.PI;

    public Text nodeNum;

    static public bool createDone = false;

    public Canvas startUI;
    public void CreateGraph()
    {
        n = int.Parse(nodeNum.text);
        delta /= n;
        float length = new Vector3(Mathf.Cos(delta) * R - R, Mathf.Sin(delta) * R, 0).magnitude;
        edge.transform.localScale = new Vector3(edge.transform.localScale.x, length, 0);
        //ʵ�����е�
        for (int i = n; i >= 1; i--)
        {
            Vector3 circlePos = new Vector3(Mathf.Cos(delta * i) * R, Mathf.Sin(delta * i) * R, 0);
            GameObject circleNode = Instantiate(circle, circlePos, Quaternion.identity);
            GameObject nodeText = circleNode.transform.Find("Canvas/nodeV").gameObject;

            //��ý����Text���
            Text nodeValue = nodeText.GetComponent<Text>();
            int value = (int)(Random.Range(-10f, 10f));
            //�ڽ��Բ����ʾÿ������ֵ
            nodeValue.text = value.ToString();
            nodeText.transform.position = circlePos;
    
            circleArr.Add(circleNode);
        }
        createDone = true;

        //ʵ�����б�
        for(int i = n-1; i>=0; i--)
        {
            Vector3 circlePos = new Vector3(Mathf.Cos(delta * i) * R, Mathf.Sin(delta * i) * R, 0);
            Vector3 priorPos = new Vector3(Mathf.Cos(delta * (i + 1)) * R, Mathf.Sin(delta * (i + 1)) * R, 0);

            GameObject edgeNode = Instantiate(edge, (circlePos + priorPos) / 2,
                Quaternion.Euler(new Vector3(0, 0, 360 * Mathf.Atan(-(circlePos.x - priorPos.x) / (circlePos.y - priorPos.y)) / (2 * Mathf.PI))));

            //���Gameobject��Text���
            GameObject calcOperator = Instantiate(calcSign, (circlePos + priorPos) / 2, Quaternion.identity);
            GameObject operatorText = calcOperator.transform.Find("Canvas/Operator0").gameObject;
            Text operatorValue = operatorText.GetComponent<Text>();
            //�漴��ֵ�����������λ���ڱ��м�
            operatorValue.text =operator_4[(int)Random.Range(0, 2)];
            operatorText.transform.position = edgeNode.transform.position;
            
            edgeArr.Add(edgeNode);
            operatorArr.Add(calcOperator);
        }
        startUI.gameObject.SetActive(false);
    }
}
