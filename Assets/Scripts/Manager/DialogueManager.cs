using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;//대화 텍스트
    [SerializeField] private Button dialogueButton; //대화 시작 버튼
    [SerializeField, TextArea] private string[] dialogues; //NPC대사 배열

    private int currentDialogueIndex = 0;

    private void Start()
    {
        //대화 패널 비활성화
        if ( dialoguePanel != null )
        {
            dialoguePanel.SetActive( false );
        }

        // 대화 버튼 리스너 등록
        if ( dialogueButton != null )
        {
            dialogueButton.onClick.AddListener(StartDialogue);
        }
    }

    public void StartDialogue()
    {
        if( dialoguePanel != null )
        {
            dialoguePanel.SetActive( true );
            
            ShowNextDialogue();
        }
    }

    private void ShowNextDialogue()
    {
        if( dialogues.Length > 0 && currentDialogueIndex < dialogues.Length )
        {
            dialogueText.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            //대화가 끝나면 패널 비활성화
            dialoguePanel.SetActive ( false );
            currentDialogueIndex = 0; //대화를 처음부터 할 수 있게 초기화
        }
    }
}
