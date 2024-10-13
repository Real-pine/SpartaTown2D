using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameInputManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button confirmButton;
    [SerializeField] private int minNameLength = 2;
    [SerializeField] private int maxNameLength = 10;
    [SerializeField] private CharacterData characterData; //ScriptableObject 참조(캐릭터 데이터 저장용)

    //에러팝업관련변수
    [SerializeField] private GameObject errorPopup;
    [SerializeField] private TMP_Text errorMessageText;
    [SerializeField] private Button errorPopupCloseButton;

    // 캐릭터 선택 팝업관련 변수
    [SerializeField] private Button characterSelectButton;
    [SerializeField] private Image selectedCharacterImage;
    [SerializeField] private GameObject characterSelectionPopup;
    [SerializeField] private CharacterOption[] characterOptions;


    private void Start()
    {
        //InputField 입력제한
        nameInputField.characterLimit = maxNameLength;
        //컨펌 버튼 연결
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        //초기 버튼 연결 상태
        UpdateConfirmButtonState();
        //에러 팝업 확인 버튼에 이벤트 리스너 추가
        errorPopupCloseButton.onClick.AddListener(CloseErrorPopup);
        //캐릭터선택 팝업 버튼에 리스너 추가
        characterSelectButton.onClick.AddListener(OpenCharacterSelectionPopup);

        //화면에 띄어지는 기본 캐릭터 설정
        if (characterOptions.Length > 0)
        {
            SetSelectedCharacter(characterOptions[0]);
        }

        //캐릭터 선택 버튼들 초기화
        InitializeCharacterSelectionButtons();
    }

    private void InitializeCharacterSelectionButtons()
    {
        foreach (var option in characterOptions)
        {
            {
                Button button = Instantiate(option.buttonPrefab, characterSelectionPopup.transform);
                button.onClick.AddListener(()=> OnCharacterSelected(option));
                Image characterImage = button.transform.GetChild(0).GetComponent<Image>();
                if(characterImage != null)
                {
                    characterImage.sprite = option.characterSprite;
                    characterImage.preserveAspect = true;
                }

                button.onClick.AddListener(() => OnCharacterSelected(option));
            }
        }
    }

    private void OnCharacterSelected(CharacterOption option)
    {
        SetSelectedCharacter(option);
        characterSelectionPopup.SetActive(false);
    }

    private void SetSelectedCharacter(CharacterOption option)
    {
        selectedCharacterImage.sprite = option.characterSprite;
        characterData.selectedCharacterID = option.characterID;
    }

    private void OpenCharacterSelectionPopup()
    {
        characterSelectionPopup.SetActive(true);
    }

    private void CloseErrorPopup()
    {
        errorPopup.SetActive(false);
    }

    private void OnConfirmButtonClicked()
    {
        string inputName = nameInputField.text.Trim(); // 앞뒤 공백을 제거

        if (IsValidname(inputName))
        {
            SaveName(inputName);
            //데이터관리를 위해 씬매니저를 만들어서 메인씬전환
            GameSceneManager.Instance.LoadMainScene();
        }
        else
        {
            DisplayErrorMessage($"이름은 {minNameLength}에서 {maxNameLength}사이여야 합니다.");
        }
    }

    private bool IsValidname(string name)
    {
        return name.Length >= minNameLength && name.Length <= maxNameLength;
    }

    private void SaveName(string name)
    {
        characterData.playerName = name;
        // ScriptableObject에 변경사항 저장
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(characterData);
#endif
    }

    private void DisplayErrorMessage(string message)
    {
        //UI를 통한 에러 메세지 표시 로직 추가
        errorMessageText.text = message;
        errorPopup.SetActive(true);
    }

    private void UpdateConfirmButtonState()
    {
        confirmButton.interactable = !string.IsNullOrEmpty(nameInputField.text.Trim());
    }

    //InputField의 onValueChanged이벤트에 연결할 메서드
    public void OnNameInputChanged(string value)
    {
        UpdateConfirmButtonState();
    }
}
