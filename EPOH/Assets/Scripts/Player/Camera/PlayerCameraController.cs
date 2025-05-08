using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCameraController : MonoBehaviour
{
    private void Awake()
    {
        // 씬 로드 시 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // 씬 로드 시 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject _cam = GameObject.Find("CM Player");

        if (_cam != null)
        {
            CinemachineVirtualCamera _playerFollowCamera = _cam.GetComponent<CinemachineVirtualCamera>();
            if (_playerFollowCamera != null)
            {
                Debug.LogWarning("CM Player 카메라 할당 완료.");
                _playerFollowCamera.Follow = PlayerController.Instance.transform;
            }
        }
    }
}
