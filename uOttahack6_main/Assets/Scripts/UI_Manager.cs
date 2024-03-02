using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
   
    private int _roomNumber;
   
    [SerializeField] private TextMeshProUGUI _inputText;
    public void onCreateRoomClick()
    {
        Debug.Log("Create Room Clicked");
    }
   
    public void onInputFieldChange()
    {
        _roomNumber = int.Parse(_inputText.text);
        Debug.Log(_roomNumber);
    }
   
    public void onJoinRoomClick()
    {
        Debug.Log("Join Room Clicked with room number: " + _roomNumber);
      
    }
}