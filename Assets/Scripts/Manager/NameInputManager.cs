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
    [SerializeField] private CharacterData characterData; //ScriptableObject ����(ĳ���� ������ �����)

    // �����˾����ú���
    [SerializeField] private GameObject errorPopup;
    [SerializeField] private TMP_Text errorMessageText;
    [SerializeField] private Button errorPopupCloseButton;

    // ĳ���� ���� �˾����� ����
    [SerializeField] private Button characterSelectButton;
    [SerializeField] private Image selectedCharacterImage;
    [SerializeField] private GameObject characterSelectionPopup;
    [SerializeField] private CharacterOption[] characterOptions;



    private void Start()
    {
        //InputField �Է�����
        nameInputField.characterLimit = maxNameLength;
        //���� ��ư ����
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        //���� ��ư Ȱ��ȭ
        nameInputField.onValueChanged.AddListener(OnNameInputChanged);
        UpdateConfirmButtonState();
        //���� �˾� Ȯ�� ��ư�� �̺�Ʈ ������ �߰�
        errorPopupCloseButton.onClick.AddListener(CloseErrorPopup);
        //ĳ���ͼ��� �˾� ��ư�� ������ �߰�
        characterSelectButton.onClick.AddListener(OpenCharacterSelectionPopup);

        //ȭ�鿡 ������� �⺻ ĳ���� ����
        if (characterOptions.Length > 0)
        {
            SetSelectedCharacter(characterOptions[0]);
        }

        //ĳ���� ���� ��ư�� �ʱ�ȭ
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
        SetSelectedCharacter(option); //ĳ���� ������ ������Ʈ
        characterSelectionPopup.SetActive(false); //�˾� �ݱ�

        //ĳ���� ������� ����
        CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
        if(characterSpawner != null)
        {
            characterSpawner.UpdateCharacter();
        }
    }

    private void SetSelectedCharacter(CharacterOption option)
    {
        selectedCharacterImage.sprite = option.characterSprite;
        characterData.selectedCharacterID = option.characterID;
    }

    private void OpenCharacterSelectionPopup()
    {
        //�˾� ���� ���� ��� �г� �ʱ�ȭ�Ͽ� �浹 ����
        CloseAllPanels();
        characterSelectionPopup.SetActive(true);
    }

    private void CloseAllPanels()
    {
        characterSelectionPopup.SetActive(false );
        errorPopup.SetActive(false );
    }

    private void CloseErrorPopup()
    {
        errorPopup.SetActive(false);
    }

    private void OnConfirmButtonClicked()
    {
        string inputName = nameInputField.text.Trim(); // �յ� ������ ����

        if (IsValidname(inputName))
        {
            SaveName(inputName);

            //���ξ��� �ε���� ���� ��쿡�� �� ��ȯ�� ����
            if(!GameSceneManager.Instance.isMainSceneLoaded)
            {
                //�����Ͱ����� ���� ���Ŵ����� ���� ���ξ���ȯ
                GameSceneManager.Instance.LoadMainScene();
                //���� ������ ��ȯ �� selectedCharacterImage�� ��Ȱ��ȭ
                selectedCharacterImage.gameObject.SetActive(false);
            }
            else
            {
                //���ξ������� �̸��� ����Ǵ��� �� ��ȯ�� ���� ����
                //�ܼ��� �г� UI�ݱ�
                GameSceneManager.Instance.CloseAllPanel(); 
                CloseAllPanels();
                //���ξ����� �̸� ���� �� ĳ���� ������Ʈ
                CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
                if (characterSpawner != null)
                {
                    characterSpawner.UpdateCharacter();
                }
            }
        }
        else
        {
            DisplayErrorMessage($"�̸��� {minNameLength}���� {maxNameLength}���̿��� �մϴ�.");
        }
    }

    private bool IsValidname(string name)
    {
        return name.Length >= minNameLength && name.Length <= maxNameLength;
    }

    private void SaveName(string name)
    {
        characterData.playerName = name;
        // ScriptableObject�� ������� ����
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(characterData);
#endif
    }

    private void DisplayErrorMessage(string message)
    {
        //UI�� ���� ���� �޼��� ǥ�� ���� �߰�
        errorMessageText.text = message;
        errorPopup.SetActive(true);
    }

    private void UpdateConfirmButtonState()
    {
        confirmButton.interactable = !string.IsNullOrEmpty(nameInputField.text.Trim());
    }

    //InputField�� onValueChanged�̺�Ʈ�� ������ �޼���
    public void OnNameInputChanged(string value)
    {
        UpdateConfirmButtonState();
    }
}
