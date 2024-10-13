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
        //ĳ������ ���� ��ġ�� �ǽð����� characterData�� �����ϸ� ����
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

            //���ξ����� ĳ���� ��ȯ �� ĳ���� ����
            if ( currentPlayerObject != null )
            {
                //���� ĳ���� ��ġ ����
                currentPosition = currentPlayerObject.transform.position;
                Destroy( currentPlayerObject );
            }

            int prefabIndex = Mathf.Clamp(characterData.selectedCharacterID, 0, characterPrefabs.Length - 1); 
            currentPlayerObject = Instantiate(characterPrefabs[prefabIndex], currentPosition, Quaternion.identity);

            //�÷��̾� �̸� ����
            PlayerInfo playerInfo = currentPlayerObject.GetComponent<PlayerInfo>();
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
                        cameraFollow.target = currentPlayerObject.transform;
                    }
                }

            //NameUI�ȷο� ����
            NameUIFollow nameUiFollow = currentPlayerObject.GetComponentInChildren<NameUIFollow>();
            if(nameUiFollow != null)
            {
                nameUiFollow.target = currentPlayerObject.transform;
            }
            // ĳ���� ���� ��ġ CharacterData�� ����
            characterData.characterPosition = currentPosition;

        }
    }

    public void UpdateCharacter()
    {
        SpawnPlayer();
    }
}
