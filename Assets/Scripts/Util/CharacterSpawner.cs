using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public CharacterData characterData;
    public GameObject[] characterPrefabs;
    public CameraFollow cameraFollow; 

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

            //�÷��̾� �̸� ����
            PlayerInfo playerInfo = playerObject.GetComponent<PlayerInfo>();
            if (playerInfo != null)
            {
                playerInfo.SetPlayerName(characterData.playerName);
            }

            //ī�޶� Ÿ�� ����
            Camera mainCamera = Camera.main;
            if(mainCamera != null ) 
                {
                    CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
                    if(cameraFollow != null)
                    {
                        cameraFollow.target = playerObject.transform;
                    }
                }

            //NameUI�ȷο� ����
            NameUIFollow nameUiFollow = playerObject.GetComponentInChildren<NameUIFollow>();
            if(nameUiFollow != null)
            {
                nameUiFollow.target = playerObject.transform;
            }

        }
    }
}
