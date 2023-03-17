using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject WinMenuImage;

    public void OpenWinMenu()
    {
        WinMenuImage.SetActive(true);
    }
}
