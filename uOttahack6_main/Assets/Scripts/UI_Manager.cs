using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
   
    private int _roomNumber;
    
    [SerializeField] private TextMeshProUGUI _roomNumberText;

    private int lastInputNumber;

    [SerializeField] private GameObject _keyboard;
    
    public void onCreateRoomClick()
    {
        Debug.Log("Create Room Clicked");
    }
   
    public void onNumberClick(int number)
    {
        if (_roomNumber > 999999)
        {
            return;
        }
        
        _roomNumber = _roomNumber * 10 + number;
        lastInputNumber = number;
        _roomNumberText.text = _roomNumber.ToString();
    }
    
    public void onBackspaceClick()
    {
        //remove last digit and set text
        _roomNumber = _roomNumber / 10;
        _roomNumberText.text = _roomNumber.ToString();
        
    }
   
    public void onJoinRoomClick()
    {
        Debug.Log("Join Room Clicked with room number: " + _roomNumber);
      
    }
    
    public void onInputFocus()
    {
        _keyboard.SetActive(true);
    }
}