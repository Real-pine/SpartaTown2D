using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public CharacterData characterData;
    public GameObject[] characterPrefabs;
    public CameraFollow cameraFollow;

    private GameObject currentPlayerObject;

    public void SpawnPlayerInMainScene()
    {
        CharacterData characterData = GameSceneManager.Instance.characterData;

        if (characterData != null)
        {
            Vector2 currentPosition = characterData.characterPosition;

            //메인씬에서 캐릭터 전환 시 캐릭터 제거
            if (currentPlayerObject != null)
            {
                //현재 캐릭터 위치 저장
                currentPosition = currentPlayerObject.transform.position;
                Destroy(currentPlayerObject);
            }

            int prefabIndex = Mathf.Clamp(characterData.selectedCharacterID, 0, characterPrefabs.Length - 1);
            currentPlayerObject = Instantiate(characterPrefabs[prefabIndex], currentPosition, Quaternion.identity);

            SetupPlayer(currentPlayerObject, characterData);

            //플레이어 이름 설정
            PlayerInfo playerInfo = currentPlayerObject.GetComponent<PlayerInfo>();
            if (playerInfo != null)
            {
                playerInfo.SetPlayerName(characterData.playerName);
            }

            //카메라 타겟 설정
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
                if (cameraFollow != null)
                {
                    cameraFollow.target = currentPlayerObject.transform;
                }
            }

            //NameUI팔로우 설정
            NameUIFollow nameUiFollow = currentPlayerObject.GetComponentInChildren<NameUIFollow>();
            if (nameUiFollow != null)
            {
                nameUiFollow.target = currentPlayerObject.transform;
            }
            // 캐릭터 현재 위치 CharacterData에 저장
            characterData.characterPosition = currentPosition;

        }
    }

    private void SetupPlayer(GameObject currentPlayerObject, CharacterData characterData)
    {
        //플레이어 이름 설정
        PlayerInfo playerInfo = currentPlayerObject.GetComponent<PlayerInfo>();
        if (playerInfo != null)
        {
            if (playerInfo.nameText == null)
            {
                // 필요한 경우 동적할당
                playerInfo.nameText = currentPlayerObject.GetComponentInChildren<TextMeshProUGUI>();
            }

            playerInfo.SetPlayerName(characterData.playerName);
        }

        //카메라 타겟 설정
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.target = currentPlayerObject.transform;
            }
        }

        //NameUI팔로우 설정
        NameUIFollow nameUiFollow = currentPlayerObject.GetComponentInChildren<NameUIFollow>();
        if (nameUiFollow != null)
        {
            nameUiFollow.target = currentPlayerObject.transform;
        }
        // 캐릭터 현재 위치 CharacterData에 저장
        characterData.characterPosition = currentPlayerObject.transform.position;
    }

    private void Update()
    {
        //캐릭터의 현재 위치를 실시간으로 characterData에 저장하며 유지
        if(currentPlayerObject != null && GameSceneManager.Instance != null && GameSceneManager.Instance.characterData != null)
        {
            GameSceneManager.Instance.characterData.characterPosition = currentPlayerObject.transform.position;
        }
    }

    
    public void UpdateCharacter()
    {
        if(currentPlayerObject != null)
        {
            PlayerInfo playerInfo = currentPlayerObject.GetComponent<PlayerInfo>();
            if (playerInfo != null)
            {
                playerInfo.SetPlayerName(characterData.playerName);
            }
        }

        if (GameSceneManager.Instance.characterData != null && GameSceneManager.Instance.characterData.selectedCharacterID >= 0 && GameSceneManager.Instance.characterData.selectedCharacterID < characterPrefabs.Length)
        {
            Debug.Log($"Updating character with ID: {GameSceneManager.Instance.characterData.selectedCharacterID}");
            SpawnPlayerInMainScene();
        }
        else
        {
            Debug.LogWarning("Invalid selectedCharacterID. Cannot update character.");
        }
    }
}
