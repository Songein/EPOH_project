using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleportManager : MonoBehaviour
{
    private static PortalTeleportManager _instance;

    [SerializeField] private Vector3 mainroomPortal;
    [SerializeField] private Vector3 officeroomLeftPortal;
    [SerializeField] private Vector3 officeroomRightPortal;
    [SerializeField] private Vector3 bossroomPortal;

    public enum PortalState
    {
        Default,
        MainToOffice,
        OfficeToMain,
        OfficeToBoss,
        BossToOffice
    }

    struct PortalInfo
    {
        public PortalInfo(string sceneName, Vector3 landingPos)
        {
            MoveSceneName = sceneName;
            LandingPos = landingPos;
        }
        public string MoveSceneName { get; }
        public Vector3 LandingPos { get; }
    } 

    public PortalState portalState = PortalState.Default;
    private Dictionary<PortalState, PortalInfo> _portalInfos = new Dictionary<PortalState, PortalInfo>();

    public static PortalTeleportManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PortalTeleportManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(PortalTeleportManager).Name);
                    _instance = singletonObject.AddComponent<PortalTeleportManager>();
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
            
            // 씬 로드 시 이벤트 등록
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            // 포탈 정보 초기화
            _portalInfos.Add(PortalState.MainToOffice, new PortalInfo("OfficeRoom1", officeroomLeftPortal));
            _portalInfos.Add(PortalState.OfficeToMain, new PortalInfo("MainRoomTest", mainroomPortal));
            _portalInfos.Add(PortalState.BossToOffice, new PortalInfo("OfficeRoom1", officeroomRightPortal));
            _portalInfos.Add(PortalState.OfficeToBoss, new PortalInfo("BossRoomTest", bossroomPortal));
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }
    
    private void OnDestroy()
    {
        if (_instance == this)
        {
            // 씬 로드 이벤트 해제
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }


    public IEnumerator StartOperatePortal(PortalState state)
    {
        // 플레이어 움직임 막기
        PlayerController.Instance.canMove = false;
        PlayerInteract.Instance.canInteract = false;
        // 플레이어 포탈 시작 애니메이션 재생
        PlayerController.Instance.GetComponent<Animator>().SetTrigger("StartTeleport");
        
        // 애니메이션 종료 후 씬 이동
        yield return new WaitForSeconds(1f);
        portalState = state;
        SceneManager.LoadScene(_portalInfos[state].MoveSceneName);
    }

    public IEnumerator EndOperatePortal()
    {
        // 포탈 위치로 플레이어를 이동시킨 후, 도착 애니메이션을 실행 함.
        PlayerController.Instance.transform.position = _portalInfos[portalState].LandingPos;
        
        // 플레이어 움직임 막기
        PlayerController.Instance.canMove = false;
        PlayerInteract.Instance.canInteract = false;
        
        // 플레이어 포탈 도착 애니메이션 재생
        PlayerController.Instance.GetComponent<Animator>().SetTrigger("TeleportEnd");
        
        // 애니메이션 종료 후, 포탈 상태 디폴트로 변경
        yield return new WaitForSeconds(1f);
        
        // 플레이어 락 해제
        PlayerController.Instance.canMove = true;
        PlayerInteract.Instance.canInteract = true;
        
        // 실행되어야 할 이벤트가 있다면 이벤트 실행
        if (!String.IsNullOrEmpty(GetEventID(portalState)))
        {
            EventManager.Instance.ExecuteEvent(GetEventID(portalState)).Forget();
        }
        
        // 포탈 정보 디폴트로 변경
        portalState = PortalState.Default;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // portal의 상태가 Default가 아니라면, 포탈을 통해 이동을 했다는 것임.
        if (portalState != PortalState.Default)
        {
            StartCoroutine(EndOperatePortal());
        }
    }

    public string GetEventID(PortalState state)
    {
        if (state == PortalState.OfficeToMain)
        {
            switch (GameManager.instance.ProgressState)
            {
                case GameManager.ProgressId.Progress_Req1_Clear:
                    return "Event_016";
                case GameManager.ProgressId.Progress_Req2_Clear:
                    return "Event_027";
                case GameManager.ProgressId.Progress_Req3_Clear:
                    return "Event_038";
                case GameManager.ProgressId.Progress_Req4_Clear:
                    return "Event_049";
                default:
                    return null;
            }
        }
        if (state == PortalState.OfficeToBoss)
        {
            switch (GameManager.instance.ProgressState)
            {
                case GameManager.ProgressId.Progress_Req1_Start:
                    return "Event_013";
                case GameManager.ProgressId.Progress_Req2_Start:
                    return "Event_024";
                case GameManager.ProgressId.Progress_Req3_Start:
                    return "Event_035";
                case GameManager.ProgressId.Progress_Req4_Start:
                    return "Event_046";
                default:
                    return null;
            }
        }

        return null;
    }
}
