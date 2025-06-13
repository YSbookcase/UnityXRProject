using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("외부 참조")]
    [SerializeField] private Transform xrCamera;         // Main Camera (HMD)

    [Header("자식 컴포넌트 참조")]
    [SerializeField] private Transform characterVisual; // 캡슐 오브젝트
    [SerializeField] private Transform colliderPointer; // 콜라이더 동기화

    [Header("상태")]
    [SerializeField] private float capsuleHeight = 2f;

    private float initialBodyHeightY;

    private void Start()
    {
        if (characterVisual != null)
        {
            initialBodyHeightY = characterVisual.localPosition.y; // 생성 시 높이 저장
        }

        if (colliderPointer.TryGetComponent(out CapsuleCollider capsule))
        {
            capsuleHeight = capsule.height;
        }
    }

    private void LateUpdate()
    {
        ChildObjectSyn();
    }

    private void ChildObjectSyn()
    {
        if (xrCamera == null || characterVisual == null || colliderPointer == null) return;

        Vector3 headPos = xrCamera.localPosition;

        // Visual 위치는 XZ만 카메라 따라가고 Y는 초기 위치 유지
        characterVisual.localPosition = new Vector3(headPos.x, initialBodyHeightY, headPos.z);

        // ColliderPointer 위치는 바디 높이 기준으로 카메라 중심 보정
        colliderPointer.localPosition = new Vector3(headPos.x, capsuleHeight * 0.5f, headPos.z);
    }

}
