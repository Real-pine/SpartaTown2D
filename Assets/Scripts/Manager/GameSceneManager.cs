using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    public CharacterData characterData;

    //UI Manager관련 변수
    [Header("Character and Name Change UI")]
    [SerializeField] private GameObject nameInputPanel; // 이름변경 버튼 후 팝업 패널
    [SerializeField] private Button openNameChangeButton; // 이름 변경 버튼
    [SerializeField] private Button openCharacterSelectButton; // 캐릭터 선택 버튼

    public bool isMainSceneLoaded = false; //메인씬이 이미 로드 되었는지 여부확인을 위한 변수

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 완료 후 이벤트
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeUIListeners();       
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            isMainSceneLoaded=true;
            Debug.Log("MainScene has been loaded, isMainSceneLoaded set to true");

            // 씬 로드 후 UI오브젝트 새로 찾기
            nameInputPanel = FindInactiveObject("NameInputPanel");
            openNameChangeButton = GameObject.Find("ChangeNameBtn")?.GetComponent<Button>();
            openCharacterSelectButton = GameObject.Find("SwitchCharBtn")?.GetComponent<Button>();

            //씬 로드 후 UI 리스너 재등록
            InitializeUIListeners();
            // Name Input Panel 상태를 다시 설정
            if(nameInputPanel != null)
            {
                nameInputPanel.SetActive(false); //초기에는 비활성화 상태
                Debug.Log("name input panel set to inactive after mainscene loaded");
            }
        }

        //씬 로드 완료 후 CharacterSpawner에서 캐릭터 생성
        CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
        if (characterSpawner != null)
        {
            characterSpawner.SpawnPlayerInMainScene();
        }
    }

    private void OnEnable()
    {
        //씬이 전환된 이후 다시 UI리스너를 초기화 
        InitializeUIListeners();
    }

    //리스너 관리..
    private void InitializeUIListeners()
    {
        if (openNameChangeButton != null)
        {
            //기존 리스너 제거 후 다시 추가(중복 방지)
            openNameChangeButton.onClick.RemoveAllListeners();
            openNameChangeButton.onClick.AddListener(OpenNameInputPanel);
        }      
    }
    public void OpenNameInputPanel()
    {
        if (nameInputPanel != null)
        {
            nameInputPanel.SetActive(true);
            Debug.Log("Name Input Panel Opened");
        }
    }

    public void OnConfirmNameChange()
    {
        if(characterData != null )
        {
            string newName = characterData.name; //새로운 이름은 외부에서 설정된 후 호출되도록 변경
        }

        //이름 변경 후 캐릭터 재스폰
        RespawnCharacter();

        CloseAllPanel();
    }

    private void RespawnCharacter()
    {
        CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
        if (characterSpawner != null)
        {
            characterSpawner.SpawnPlayerInMainScene();
        }
    }

    //로드씬
    public void LoadMainScene()
    {
        if (!isMainSceneLoaded)
        {
            SceneManager.LoadScene("MainScene");
            CloseAllPanel();
        }
    }

    public void CloseAllPanel()
    {
        if (nameInputPanel != null)
        {
            nameInputPanel.SetActive(false);
            Debug.Log("Name input panel closed. curren states: " + nameInputPanel.activeSelf);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
    private GameObject FindInactiveObject(string objectName)
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject rootObject in rootObjects)
        {
            Transform[] allTransforms = rootObject.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in allTransforms)
            {
                if (t.name == objectName)
                {
                    return t.gameObject;
                }
            }
        }
        return null;
    }

}