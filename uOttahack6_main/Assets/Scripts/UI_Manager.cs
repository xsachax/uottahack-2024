using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    [SerializeField] private GameManager _gameManager;
    private string _name;
    private string _ip;
    
    [SerializeField] private TextMeshProUGUI _nameText;
    
    [SerializeField] private TextMeshProUGUI _ipText;

    [SerializeField] private List<GameObject> _pages;
    
    public void Awake()
    {
        GoToPage(0);
    }
    
    public void GoToPage(int index)
    {
        for (int i = 0; i < _pages.Count; i++)
        {
            if (i == index)
            {
                _pages[i].SetActive(true);
            }
            else
            {
                _pages[i].SetActive(false);
            }
        }
    }
    
    public void typeLetter(string letter)
    {
        
        _name += letter;
        _nameText.text = _name;
    }
    
    public void backSpaceLetter()
    {
        _name = _name.Substring(0, _name.Length - 1);
        _nameText.text = _name;
    }
    
    public void typeIP(string ip)
    {
        _ip += ip;
        _ipText.text = _ip;
    }
    
    public void backSpaceIP()
    {
        _ip = _ip.Substring(0, _ip.Length - 1);
        _ipText.text = _ip;
    }
    
    public void onCreateRoomClick()
    {
        Debug.Log("Create Room Clicked");
        _gameManager.StartAsHost(_name, _ip);
        GoToPage(2);
    }
   
    public void onJoinRoomClick()
    {
        Debug.Log("Join Room Clicked");
        _gameManager.GetComponent<GameManager>().JoinAsClient(_name, _ip);
        GoToPage(2);
    }

    public void onExitClick()
    {
        GoToPage(0);
        _gameManager.ResetGame();
    }
    
}