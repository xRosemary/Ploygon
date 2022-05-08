using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas startUI;
    public Canvas pvpUI;
    public Canvas autoUI;
    public Canvas mainUI;
    public Canvas chatUI;
    public void Solo()
    {
        startUI.gameObject.SetActive(true);

        mainUI.gameObject.SetActive(false);
    }
    public void PVP()
    {
        pvpUI.gameObject.SetActive(true);
        mainUI.gameObject.SetActive(false);
        chatUI.gameObject.SetActive(true);
    }

    public void AutoShow()
    {
        startUI.gameObject.SetActive(true);
        autoUI.gameObject.SetActive(true);
        mainUI.gameObject.SetActive(false);
    }
}
