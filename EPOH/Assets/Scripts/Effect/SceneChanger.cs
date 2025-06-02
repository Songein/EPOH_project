using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger _instance;
    public CanvasGroup _fadeImg;
    float fadeDuration = 1.5f;

    public static SceneChanger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneChanger>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(SceneChanger).Name);
                    _instance = singletonObject.AddComponent<SceneChanger>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지됨
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _fadeImg.DOFade(0, fadeDuration)
            .OnComplete(() =>
            {
                _fadeImg.blocksRaycasts = false;
                if (scene.name == "CutScene1" || scene.name == "CutScene2" || scene.name == "CutScene3" ||
                    scene.name == "CutScene4")
                {
                    PlayerController.Instance.canMove = false;
                    PlayerInteract.Instance.canInteract = false;
                }
                else
                {
                    PlayerController.Instance.canMove = true;
                    PlayerInteract.Instance.canInteract = true;
                }
                
                EffectManager.Instance.OnEffectEnd?.Invoke();
            });
    }

    public async UniTaskVoid ChangeScene(string sceneName)
    {
        await _fadeImg.DOFade(1, fadeDuration)
            .OnStart(() => {
                _fadeImg.blocksRaycasts = true;
                if (PlayerController.Instance != null)
                {
                    PlayerController.Instance.canMove = false;
                    PlayerInteract.Instance.canInteract = false;
                }
                //SoundManager.Instance.StopBGM();
            })
            .AsyncWaitForCompletion();

        await LoadScene(sceneName);
    }
    
    private async UniTask LoadScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
       
        while (!async.isDone)
        {
            await UniTask.Yield();
        }
    }
}
