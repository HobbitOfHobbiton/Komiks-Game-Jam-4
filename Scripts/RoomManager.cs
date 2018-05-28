using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject[] roomSwitchers;

    public static bool inConversation;
    int curentRoom;

    void Awake()
    {
        FixRooms();
    }

    public void SwitchRoom(int roomIndex)
    {
        curentRoom = roomIndex;
        FixRooms();
    }

    void FixRooms()
    {
        for(int i = 0; i < rooms.Length; i++)
        {
            if(i != curentRoom)
            {
                rooms[i].SetActive(false);
            }
            else
            {
                rooms[i].SetActive(true);
            }
        }
    }

    void Update()
    {
        foreach(var button in roomSwitchers)
        {
            if (inConversation)
                button.SetActive(false);
            else
                button.SetActive(true);
        }
    }
}
