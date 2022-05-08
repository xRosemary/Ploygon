using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPStart : MonoBehaviour
{
    public Canvas startUI;
    public Canvas connectUI;
    public Canvas chatUI;
    public void StartPvp()
    {
        startUI.gameObject.SetActive(true);
        connectUI.gameObject.SetActive(false);
        chatUI.gameObject.SetActive(true);
    }
}
