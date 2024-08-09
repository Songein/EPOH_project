using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinJu : MonoBehaviour
{
    public GameObject player; // 플레이어 게임 오브젝트
    private PlayerHealth player_health; //PlayerHealth 스크립트 참조
    [SerializeField] float attack_power = 8f; // 보스 공격 세기



    public GameObject shadow_prefab; // 그림자 오브젝트 프리팹
    public float shadow_speed = 10.0f; // 그림자 이동 속도


    //달리기 변수
    private Vector3 leftEdge;
    private Vector3 rightEdge;
    private SpriteRenderer warning_renderer;
    public float warning_duration = 3.0f; // 전조 영역 지속 시간
    public Color warning_color = Color.red; // 전조 영역 색상

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // 플레이어가 있는지 확인

        
        if (player != null)
        {
            Debug.Log("플레이어 발견");
        }

        // 씬의 가장 왼쪽과 오른쪽 좌표를 설정 (카메라 뷰포트를 기준으로 계산)
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
    }

    public void Run()
    {
        StartCoroutine(Running());
    }

    private IEnumerator Running()
    {
        // 현재 오브젝트의 위치에 따라 이동할 방향을 결정
        Transform objTransform = this.transform;
        Vector3 targetPosition = objTransform.position.x < (rightEdge.x + leftEdge.x) / 2 ? rightEdge : leftEdge;
        Vector3 startPosition = objTransform.position;
        
        // 전조 영역 생성 및 유지
        GameObject warning_area = new GameObject("WarningArea");
        warning_area.transform.position = new Vector3((startPosition.x + targetPosition.x) / 2, objTransform.position.y, objTransform.position.z);
        warning_renderer = warning_area.AddComponent<SpriteRenderer>();

        // 전조 영역의 색상 
        Color warningColorWithAlpha = new Color(warning_color.r, warning_color.g, warning_color.b, 0.5f); // 알파값 0.5 설정
        warning_renderer.color = warningColorWithAlpha;
        warning_renderer.sortingOrder = 20; // 오브젝트 위로 표시되도록 순서 설정
        warning_renderer.sprite = CreateWarningSprite(); // 
        
        // 전조 영역의 크기 설정 (오브젝트가 이동할 거리만큼)
        float warning_width = Mathf.Abs(targetPosition.x - startPosition.x);
        warning_renderer.transform.localScale = new Vector3(warning_width, objTransform.localScale.y, 1);

        yield return new WaitForSeconds(warning_duration); // 전조 영역 유지

        Destroy(warning_area); // 전조 영역 삭제

        // 그림자 오브젝트 생성 및 이동
        GameObject shadow_object = Instantiate(shadow_prefab, objTransform.position, Quaternion.identity);
        Vector3 shadow_target_position = new Vector3(targetPosition.x, shadow_object.transform.position.y, shadow_object.transform.position.z);
        
        // 그림자의 y 좌표를 고정하여 수평으로만 이동하도록 설정
        float fixedY = objTransform.position.y;

        while (Vector3.Distance(shadow_object.transform.position, shadow_target_position) > 0.1f)
        {
            shadow_object.transform.position = Vector3.MoveTowards(shadow_object.transform.position, new Vector3(shadow_target_position.x, fixedY, shadow_target_position.z), shadow_speed * Time.deltaTime);
            yield return null;
        }

        Destroy(shadow_object); // 그림자 오브젝트 삭제
    }

    // 기본 사각형 스프라이트 생성 함수
    private Sprite CreateWarningSprite()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.red);
        texture.Apply();

        Rect rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        Vector2 pivot = new Vector2(0.5f, 0.5f);

        return Sprite.Create(texture, rect, pivot, 1.0f);
    }

    public void Scratch()
    {
        StartCoroutine(Scratching());
    }

    private IEnumerator Scratching()
    {

    }

    public void Track()
    {
        StartCoroutine(Tracking());
    }

    private IEnumerator Tracking()
    {

    }


    public void Bite()
    {
        StartCoroutine(Biting());
    }

    private IEnumerator Biting()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision) // 충돌시 데미지
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("충돌");

            //PlayerHealth 스크립트 할당
            player_health = collision.gameObject.GetComponent<PlayerHealth>();
            player_health.Damage(attack_power); //보스의 공격 세기만큼 플레이어의 hp 감소
        }
    }


}
