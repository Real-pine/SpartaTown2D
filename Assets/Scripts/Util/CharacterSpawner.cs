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

            //���ξ����� ĳ���� ��ȯ �� ĳ���� ����
            if (currentPlayerObject != null)
            {
                //���� ĳ���� ��ġ ����
                currentPosition = currentPlayerObject.transform.position;
                Destroy(currentPlayerObject);
            }

            int prefabIndex = Mathf.Clamp(characterData.selectedCharacterID, 0, characterPrefabs.Length - 1);
            currentPlayerObject = Instantiate(characterPrefabs[prefabIndex], currentPosition, Quaternion.identity);

            SetupPlayer(currentPlayerObject, characterData);

            //�÷��̾� �̸� ����
            PlayerInfo playerInfo = currentPlayerObject.GetComponent<PlayerInfo>();
            if (playerInfo != null)
            {
                playerInfo.SetPlayerName(characterData.playerName);
            }

            //ī�޶� Ÿ�� ����
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
                if (cameraFollow != null)
                {
                    cameraFollow.target = currentPlayerObject.transform;
                }
            }

            //NameUI�ȷο� ����
            NameUIFollow nameUiFollow = currentPlayerObject.GetComponentInChildren<NameUIFollow>();
            if (nameUiFollow != null)
            {
                nameUiFollow.target = currentPlayerObject.transform;
            }
            // ĳ���� ���� ��ġ CharacterData�� ����
            characterData.characterPosition = currentPosition;

        }
    }

    private void SetupPlayer(GameObject currentPlayerObject, CharacterData characterData)
    {
        //�÷��̾� �̸� ����
        PlayerInfo playerInfo = currentPlayerObject.GetComponent<PlayerInfo>();
        if (playerInfo != null)
        {
            if (playerInfo.nameText == null)
            {
                // �ʿ��� ��� �����Ҵ�
                playerInfo.nameText = currentPlayerObject.GetComponentInChildren<TextMeshProUGUI>();
            }

            playerInfo.SetPlayerName(characterData.playerName);
        }

        //ī�޶� Ÿ�� ����
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.target = currentPlayerObject.transform;
            }
        }

        //NameUI�ȷο� ����
        NameUIFollow nameUiFollow = currentPlayerObject.GetComponentInChildren<NameUIFollow>();
        if (nameUiFollow != null)
        {
            nameUiFollow.target = currentPlayerObject.transform;
        }
        // ĳ���� ���� ��ġ CharacterData�� ����
        characterData.characterPosition = currentPlayerObject.transform.position;
    }

    private void Update()
    {
        //ĳ������ ���� ��ġ�� �ǽð����� characterData�� �����ϸ� ����
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
