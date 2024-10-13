using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RosterManager : MonoBehaviour
{
    [SerializeField] private GameObject rosterPanel; // 참석자 패널
    [SerializeField] private Button openRosterButton; // 로스터 UI 열기 버튼(참석자 버튼)
    [SerializeField] private Button closeRosterButton; // 로스터 UI 닫기 버튼
    [SerializeField] private TextMeshProUGUI rosterText; // 참석자 목록 텍스트

    private void Start()
    {
        // 패널 초기 비활성화
        if(rosterPanel != null)
        {
            rosterPanel.SetActive(false);
        }

        // 참석자 버튼 리스너 추가
        if(openRosterButton != null)
        {
            openRosterButton.onClick.AddListener(OpenRosterPanel);
        }
        
        // 닫기 버튼 리스너 추가
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
            UpdateRosterList(); // 패널을 열 때 참석자 목록 업데이트
        }
    }

    public void UpdateRosterList()
    {
        rosterText.text = ""; //이전 텍스트 초기화

        // 현재 씬의 모든 게임 오브젝트 찾기
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allGameObjects)
        {
            // CharacterHolder가 있는 오브젝트만 처리
            CharacterHolder holder = obj.GetComponent<CharacterHolder>();

            if (holder != null && holder.characterData != null)
            {
                CharacterData data = holder.characterData;

                // 플레이어일 경우 playerName 출력
                if (data.characterType == CharacterType.Player)
                {
                    rosterText.text += $"{data.playerName}\n";
                }
                // NPC일 경우 "npcName" 출력
                else if (data.characterType == CharacterType.NPC)
                {
                    rosterText.text += $"{data.npcName}\n";
                }
            }
        }
    }
}
