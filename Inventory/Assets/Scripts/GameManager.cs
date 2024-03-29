﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player myPlayer;
    [SerializeField] ItemManager itemManager;
    void Awake()
    {
        myPlayer = FindObjectOfType<Player>();
        myPlayer = LoadInfo();
    }

    private void OnDisable()
    {
        SaveInfo(myPlayer);
    }
    void SaveInfo(Player myPlayer)
    {
        string json = JsonUtility.ToJson(myPlayer);
        PlayerPrefs.SetString("PlayerInfo",json);
    }

    Player LoadInfo()
    {        
        Player myPlayer = FindObjectOfType<Player>();
        string jsonData = PlayerPrefs.GetString("PlayerInfo");
        JsonUtility.FromJsonOverwrite(jsonData, myPlayer);
        if (myPlayer.itemManager == null) { myPlayer.itemManager = itemManager; }
        return myPlayer;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            SaveInfo(myPlayer);
        }
        if (Input.GetKey(KeyCode.L))
        {
            myPlayer = LoadInfo();
            myPlayer.inventoryChanged?.Invoke();
        }
    }
}
