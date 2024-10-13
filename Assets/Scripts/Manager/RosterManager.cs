using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RosterManager : MonoBehaviour
{
    [SerializeField] private GameObject rosterPanel; // ������ �г�
    [SerializeField] private Button openRosterButton; // �ν��� UI ���� ��ư(������ ��ư)
    [SerializeField] private Button closeRosterButton; // �ν��� UI �ݱ� ��ư
    [SerializeField] private TextMeshProUGUI rosterText; // ������ ��� �ؽ�Ʈ

    private void Start()
    {
        // �г� �ʱ� ��Ȱ��ȭ
        if(rosterPanel != null)
        {
            rosterPanel.SetActive(false);
        }

        // ������ ��ư ������ �߰�
        if(openRosterButton != null)
        {
            openRosterButton.onClick.AddListener(OpenRosterPanel);
        }
        
        // �ݱ� ��ư ������ �߰�
        if( closeRosterButton != null)
        {
            closeRosterButton.onClick.AddListener(CloseRosterPanel);
        }
    }

    public void CloseRosterPanel()
    {
        if (rosterPanel != null)
        {
            rosterPanel.SetActive(false );
        }
    }

    public void OpenRosterPanel()
    {
        if (rosterPanel != null)
        {
            rosterPanel.SetActive(true);
            UpdateRosterList(); // �г��� �� �� ������ ��� ������Ʈ
        }
    }

    public void UpdateRosterList()
    {
        rosterText.text = ""; //���� �ؽ�Ʈ �ʱ�ȭ

        // ���� ���� ��� ���� ������Ʈ ã��
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allGameObjects)
        {
            // CharacterHolder�� �ִ� ������Ʈ�� ó��
            CharacterHolder holder = obj.GetComponent<CharacterHolder>();

            if (holder != null && holder.characterData != null)
            {
                CharacterData data = holder.characterData;

                // �÷��̾��� ��� playerName ���
                if (data.characterType == CharacterType.Player)
                {
                    rosterText.text += $"{data.playerName}\n";
                }
                // NPC�� ��� "npcName" ���
                else if (data.characterType == CharacterType.NPC)
                {
                    rosterText.text += $"{data.npcName}\n";
                }
            }
        }
    }
}
