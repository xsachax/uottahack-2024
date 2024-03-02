using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject setupCanvas;
    [SerializeField] private GameObject[] setupPages;
    
    
    public void GoToPage(int index)
    {
        for(int i = 0; i < setupPages.Length; i++)
        {
            if(i == index)
            {
                setupPages[i].SetActive(true);
            }
            else
            {
                setupPages[i].SetActive(false);
            }
        }
    }


}
