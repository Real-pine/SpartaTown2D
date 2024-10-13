using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public CharacterData characterData;
    public GameObject[] characterPrefabs;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if( characterData != null )
        {
            int prefabIndex = Mathf.Clamp(characterData.selectedCharacterID, 0, characterPrefabs.Length - 1); 
            GameObject playerObject = Instantiate(characterPrefabs[prefabIndex], transform.position, Quaternion.identity);

            //플레이어 이름 설정
            PlayerInfo playerInfo = playerObject.GetComponent<PlayerInfo>();
            if (playerInfo != null)
            {
                playerInfo.SetPlayerName(characterData.playerName);
            }
        }
    }
}
