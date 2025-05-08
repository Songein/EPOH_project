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
                PlayerController.Instance.canMove =true;
                EffectManager.Instance.OnEffectEnd?.Invoke();

                if ((EventManager.Instance.currentEventID == "Event_021" ||
                    EventManager.Instance.currentEventID == "Event_032" ||
                    EventManager.Instance.currentEventID == "Event_043" ||
                    EventManager.Instance.currentEventID == "Event_053")
                    && scene.name == "MainRoomTest")
                {
                    Debug.LogWarning("플레이어 위치 강제 변경");
                    Vector2 playerPos = new Vector2(GameManager.instance.playerPosTemp.x,
                        GameManager.instance.playerPosTemp.y + 1f);
                    PlayerController.Instance.GetComponent<Rigidbody2D>().isKinematic = true;
                    PlayerController.Instance.GetComponent<Rigidbody2D>().MovePosition(playerPos);
                    PlayerController.Instance.GetComponent<Rigidbody2D>().isKinematic = false;
                }
            });
    }

    public async UniTaskVoid ChangeScene(string sceneName)
    {
        await _fadeImg.DOFade(1, fadeDuration)
            .OnStart(() => {
                _fadeImg.blocksRaycasts = true;
                if (PlayerController.Instance!=null)
                {
                    PlayerController.Instance.canMove = false;
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
