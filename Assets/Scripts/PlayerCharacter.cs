using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("�ܺ� ����")]
    [SerializeField] private Transform xrCamera;         // Main Camera (HMD)

    [Header("�ڽ� ������Ʈ ����")]
    [SerializeField] private Transform characterVisual; // ĸ�� ������Ʈ
    [SerializeField] private Transform colliderPointer; // �ݶ��̴� ����ȭ

    [Header("����")]
    [SerializeField] private float capsuleHeight = 2f;

    private float initialBodyHeightY;

    private void Start()
    {
        if (characterVisual != null)
        {
            initialBodyHeightY = characterVisual.localPosition.y; // ���� �� ���� ����
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

        // Visual ��ġ�� XZ�� ī�޶� ���󰡰� Y�� �ʱ� ��ġ ����
        characterVisual.localPosition = new Vector3(headPos.x, initialBodyHeightY, headPos.z);

        // ColliderPointer ��ġ�� �ٵ� ���� �������� ī�޶� �߽� ����
        colliderPointer.localPosition = new Vector3(headPos.x, capsuleHeight * 0.5f, headPos.z);
    }

}
