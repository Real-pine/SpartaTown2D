using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public CharacterData characterData;
    public GameObject[] characterPrefabs;
    public CameraFollow cameraFollow;

    private GameObject currentPlayerObject;

    private void Start()
    {
        LoadSelectedCharacterID();
        SpawnPlayer();
    }

    private void LoadSelectedCharacterID()
    {
        if(PlayerPrefs.HasKey("SelectedCharacterID"))
        {
            characterData.selectedCharacterID = PlayerPrefs.GetInt("SelectedCharacterID");
            Debug.Log($"Loaded selectedCharacterID: {characterData.selectedCharacterID}");
        }
    }

    private void Update()
    {
        //캐릭터의 현재 위치를 실시간으로 characterData에 저장하며 유지
        if(currentPlayerObject != null && characterData != null)
        {
            characterData.characterPosition = currentPlayerObject.transform.position;
        }
    }

    private void SpawnPlayer()
    {
        if( characterData != null )
        {
            Vector2 currentPosition = characterData.characterPosition;

            //메인씬에서 캐릭터 전환 시 캐릭터 제거
            if ( currentPlayerObject != null )
            {
                //현재 캐릭터 위치 저장
                currentPosition = currentPlayerObject.transform.position;
                Destroy( currentPlayerObject );
            }

            int prefabIndex = Mathf.Clamp(characterData.selectedCharacterID, 0, characterPrefabs.Length - 1); 
            currentPlayerObject = Instantiate(characterPrefabs[prefabIndex], currentPosition, Quaternion.identity);

            //플레이어 이름 설정
            PlayerInfo playerInfo = currentPlayerObject.GetComponent<PlayerInfo>();
            if (playerInfo != null)
            {
                playerInfo.SetPlayerName(characterData.playerName);
            }

            //카메라 타겟 설정
            Camera mainCamera = Camera.main;
            if(mainCamera != null ) 
                {
                    CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
                    if(cameraFollow != null)
                    {
                        cameraFollow.target = currentPlayerObject.transform;
                    }
                }

            //NameUI팔로우 설정
            NameUIFollow nameUiFollow = currentPlayerObject.GetComponentInChildren<NameUIFollow>();
            if(nameUiFollow != null)
            {
                nameUiFollow.target = currentPlayerObject.transform;
            }
            // 캐릭터 현재 위치 CharacterData에 저장
            characterData.characterPosition = currentPosition;

        }
    }

    public void UpdateCharacter()
    {
        SpawnPlayer();
    }
}
