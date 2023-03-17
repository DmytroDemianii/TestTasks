using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountingAllChildObjects : MonoBehaviour
{   
    public WinMenu winMenu;
    public Slider healthSlider;
    public GameObject[] cubes;
    List<GameObject> allChildren = new List<GameObject>();


    void Update()
    {
        GetAllChildren(transform);
    }
    
    void GetAllChildren(Transform parent)
    {
        allChildren.Clear();
        allChildren.AddRange(cubes);

        int allCount = 0;
        foreach (GameObject child in allChildren)
        {
            allCount += child.transform.childCount + 1;
        }

        healthSlider.minValue = 600;
        healthSlider.maxValue = 1400;
        healthSlider.value = allCount;
        if(healthSlider.value == 600)
        {
            Debug.Log("win");
            winMenu.OpenWinMenu();
            InputSystem.isPlaying = false;
        }
    }
}
