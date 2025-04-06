using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = EPOH.Debug;
using Random = UnityEngine.Random;

public class PictureSkill : MonoBehaviour, BossSkillInterface
{
    [SerializeField] private float _duration;
    [SerializeField] private float _damage;
    [SerializeField] private Camera _captureCamera; // 캡처용 카메라
    [SerializeField] private int _captureWidth = 500; // 캡처 영역의 가로 크기 //정사각형으로 해야 찌부가 안돼서 나옴 원래는 500이었음
    [SerializeField] private int _captureHeight = 300; // 캡처 영역의 세로 크기
    [SerializeField] private float _cameraZPosition = -11f; // 카메라 Z 위치 (2D 환경)
    
    [SerializeField] private GameObject _pictureCameraPrefab;
    [SerializeField] private GameObject _picturePrefab;
    
    private PlayerInteract _player;
    private GameObject _camera;
    
    private void Start()
    {
        _player = FindObjectOfType<PlayerInteract>();
    }

    public void Activate()
    {
        BossData bossData = BossManagerNew.Current.bossData;
        float _x = Random.Range(bossData._leftBottom.x, bossData._rightTop.x);
        float _y = Random.Range(bossData._leftBottom.y, bossData._rightTop.y);
        Vector3 randomPos = new Vector3(_x, _y, 0);
        
        //카메라 오브젝트 랜덤한 위치에 생성하기
        _camera = Instantiate(_pictureCameraPrefab, randomPos, Quaternion.identity);
        
        //해당 위치에 대해 사진 찍고 picture 프리팹의 sprite 변경하기
        StartCoroutine(GeneratePicture(_x, _y));
    }

    IEnumerator GeneratePicture(float x, float y)
    {
        yield return new WaitForSeconds(_duration);
        Destroy(_camera);
        CaptureScreenshot(x,y);
    }
    
    public void CaptureScreenshot(float x, float y)
    {
        // 랜덤 위치 생성
        Vector3 randomPosition = new Vector3(x, y, _cameraZPosition);
        Debug.Log($"랜덤 캡처 위치: {randomPosition}");

        // 캡처 카메라 위치 설정
        _captureCamera.transform.position = randomPosition;
       

        // 캡처 카메라 크기 설정 (2D 환경에서는 Orthographic Size 사용)
        Debug.Log($"캡처 카메라 설정 완료: Size {_captureCamera.orthographicSize}, Position {_captureCamera.transform.position}");

        // RenderTexture 생성
        RenderTexture rt = new RenderTexture(_captureWidth, _captureHeight, 24);
        _captureCamera.targetTexture = rt;

        // 카메라 렌더링
        _captureCamera.Render();

        // RenderTexture 활성화
        RenderTexture.active = rt;

        // ReadPixels로 캡처
        Texture2D screenshot = new Texture2D(_captureWidth, _captureHeight, TextureFormat.RGBA32, false);
        screenshot.ReadPixels(new Rect(0, 0, _captureWidth, _captureHeight), 0, 0);
        screenshot.Apply();
        Debug.Log("랜덤 스크린샷 캡처 완료!");

        //찰칵 소리
        SoundManager2.instance.PlaySFX((int)SoundManager2.SfXSound.FMN_Picture);
        //해당 위치 내에 플레이어가 있으면 Damage 입히기
        if (IsPlayerInCamera())
        {
            Debug.LogWarning($"#플레이어가 카메라 내에 포착됨. {_damage}만큼 체력 감소");
            _player.GetComponent<PlayerHealth>().Damage(_damage);
        }
        else
        {
            Debug.LogWarning($"#플레이어가 카메라 내에 포착되지 않음.");
        }

        // RenderTexture 해제 및 삭제
        _captureCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Texture2D를 Sprite로 변환
        Sprite screenshotSprite = Sprite.Create(screenshot, new Rect(0, 0, _captureWidth, _captureHeight), new Vector2(0.5f, 0.5f));
        Debug.Log("Sprite 변환 완료!");

        // 사진 프리팹 인스턴스 생성
        GameObject photoInstance = Instantiate(_picturePrefab, new Vector3(x,y,0), Quaternion.identity);
        SpriteRenderer spriteRenderer = photoInstance.transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // 프리팹의 SpriteRenderer에 Sprite 적용
            spriteRenderer.sprite = screenshotSprite;
            Debug.Log("사진 프리팹 Sprite 교체 완료!");
        }
        else
        {
            Debug.LogError("사진 프리팹에 SpriteRenderer가 없습니다!");
        }
    }

    bool IsPlayerInCamera()
    {
        CapsuleCollider2D playerCollider = _player.GetComponent<CapsuleCollider2D>();
        if (playerCollider == null)
        {
            Debug.LogError("CapsuleCollider2D가 플레이어에 부착되어 있지 않습니다!");
            return false;
        }
        Bounds playerBounds = playerCollider.bounds;
        
        Vector3 camBottomLeft = _captureCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 camTopRight = _captureCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Bounds cameraBounds = new Bounds();
        cameraBounds.SetMinMax(camBottomLeft, camTopRight);
        
        Debug.Log($"Camera Bounds: Min({cameraBounds.min}), Max({cameraBounds.max})");
        Debug.Log($"Player Bounds: Min({playerBounds.min}), Max({playerBounds.max})");

        return playerBounds.Intersects(cameraBounds);
    }
}
