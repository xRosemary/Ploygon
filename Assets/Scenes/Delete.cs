using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delete : MonoBehaviour
{
    SpriteRenderer edgeRender;
    private static float x_scale=10;
    private int edgeId;
    // Start is called before the first frame update
    void Start()
    {
        edgeRender = gameObject.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        edgeId = Create.edgeArr.FindIndex(a => a == this.gameObject);
    }

    private void OnMouseOver()
    {
        edgeRender.color = Color.white;
    }

    private void OnMouseExit()
    {
        edgeRender.color = Color.black;
    }
    public static void DeleteEdge(int index)
    {
        Create.clickTime++;
        if (Create.clickTime == 1)
        {
            Create.edgeArr[index].SetActive(false);
            Create.edgeArr.RemoveAt(index);

            Create.operatorArr[index].SetActive(false);
            Create.operatorArr.RemoveAt(index);
            Create.edgeVal.RemoveAt(index);
            for (int i = 0; i < index + 1; i++)
            {
                Create.circleArr.Add(Create.circleArr[0]);
                Create.circleArr.RemoveAt(0);
                Create.vertexVal.Add(Create.vertexVal[0]);
                Create.vertexVal.RemoveAt(0);
            }
            for (int i = 0; i < index; i++)
            {
                Create.edgeArr.Add(Create.edgeArr[0]);
                Create.edgeArr.RemoveAt(0);
                Create.operatorArr.Add(Create.operatorArr[0]);
                Create.operatorArr.RemoveAt(0);
                Create.edgeVal.Add(Create.edgeVal[0]);
                Create.edgeVal.RemoveAt(0);
            }
        }
        else
        {
            Create.edgeArr[index].SetActive(false);
            Create.edgeArr.RemoveAt(index);

            Create.circleArr[index + 1].SetActive(false);
            Create.circleArr[index].transform.position = (Create.circleArr[index + 1].transform.position + Create.circleArr[index].transform.position) / 2;
            GameObject nodeText1 = Create.circleArr[index].transform.Find("Canvas/nodeV").gameObject;
            GameObject nodeText2 = Create.circleArr[index + 1].transform.Find("Canvas/nodeV").gameObject;
            Text nodeValue1 = nodeText1.GetComponent<Text>();
            Text nodeValue2 = nodeText2.GetComponent<Text>();

            int m_operator = 0;
            GameObject operText = Create.operatorArr[index].transform.Find("Canvas/Operator0").gameObject;
            Text operValue = operText.GetComponent<Text>();

            for (int i = 0; i < Create.operator_4.Length; i++)
            {
                if (Create.operator_4[i] == operValue.text)
                {
                    m_operator = i;
                }
            }

            switch (m_operator)
            {
                case 0:
                    nodeValue1.text = (int.Parse(nodeValue1.text) + int.Parse(nodeValue2.text)).ToString();
                    Create.vertexVal[index] = Create.vertexVal[index] + Create.vertexVal[index + 1];
                    break;

                case 1:
                    nodeValue1.text = (int.Parse(nodeValue1.text) * int.Parse(nodeValue2.text)).ToString();
                    Create.vertexVal[index] = Create.vertexVal[index] * Create.vertexVal[index + 1];
                    break;

                default:
                    break;
            }

            Create.operatorArr[index].SetActive(false);
            Create.operatorArr.RemoveAt(index);
            Create.edgeVal.RemoveAt(index);
            nodeText1.transform.position = Create.circleArr[index].transform.position;
            Create.circleArr.RemoveAt(index + 1);
            Create.vertexVal.RemoveAt(index + 1);
            float length;
            float length2;

            if (index == 0)
            {
                if (Create.clickTime != Create.n)
                {
                    GameObject operText1 = Create.operatorArr[index].transform.Find("Canvas/Operator0").gameObject;
                    length = (Create.circleArr[index].transform.position - Create.circleArr[index + 1].transform.position).magnitude;
                    Create.edgeArr[index].transform.position = (Create.circleArr[index + 1].transform.position + Create.circleArr[index].transform.position) / 2;
                    Create.edgeArr[index].transform.localScale = new Vector3(x_scale, length, 0);
                    Create.edgeArr[index].transform.rotation = Quaternion.Euler(0, 0,
                        360 / (2 * Mathf.PI) * -Mathf.Atan(
                            (Create.circleArr[index + 1].transform.position.x - Create.circleArr[index].transform.position.x) /
                            (Create.circleArr[index + 1].transform.position.y - Create.circleArr[index].transform.position.y)));
                    operText1.transform.position = Create.edgeArr[index].transform.position;
                }

            }
            else
            {

                length = (Create.circleArr[index].transform.position - Create.circleArr[index - 1].transform.position).magnitude;
                if (index < Create.edgeArr.Count)
                {
                    length2 = (Create.circleArr[index + 1].transform.position - Create.circleArr[index].transform.position).magnitude;

                    GameObject operText1 = Create.operatorArr[index].transform.Find("Canvas/Operator0").gameObject;

                    Create.edgeArr[index].transform.position = (Create.circleArr[index + 1].transform.position + Create.circleArr[index].transform.position) / 2;

                    Create.edgeArr[index].transform.localScale = new Vector3(x_scale, length2, 0);
                    Create.edgeArr[index].transform.rotation = Quaternion.Euler(0, 0,
                        360 / (2 * Mathf.PI) * -Mathf.Atan(
                            (Create.circleArr[index + 1].transform.position.x - Create.circleArr[index].transform.position.x) /
                            (Create.circleArr[index + 1].transform.position.y - Create.circleArr[index].transform.position.y)));
                    operText1.transform.position = Create.edgeArr[index].transform.position;
                }

                GameObject operText2 = Create.operatorArr[index - 1].transform.Find("Canvas/Operator0").gameObject;

                Create.edgeArr[index - 1].transform.position = (Create.circleArr[index].transform.position + Create.circleArr[index - 1].transform.position) / 2;
                Create.edgeArr[index - 1].transform.localScale = new Vector3(x_scale, length, 0);
                Create.edgeArr[index - 1].transform.rotation = Quaternion.Euler(0, 0,
                    360 / (2 * Mathf.PI) * -Mathf.Atan(
                        (Create.circleArr[index].transform.position.x - Create.circleArr[index - 1].transform.position.x) /
                        (Create.circleArr[index].transform.position.y - Create.circleArr[index - 1].transform.position.y)));
                operText2.transform.position = Create.edgeArr[index - 1].transform.position;
            }



        }
    }
    private void OnMouseDown()
    {
        Backtrack.Push(Create.vertexVal, Create.edgeVal);

        if(MsgControl.isConnect == true)
        {
            MsgControl.Send("Delete:" + edgeId);
        }
        DeleteEdge(edgeId);
    }

}
