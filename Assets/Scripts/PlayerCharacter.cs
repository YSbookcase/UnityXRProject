using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("외부 참조")]
    [SerializeField] private Transform xrCamera;         // Main Camera (HMD)

    [Header("자식 컴포넌트 참조")]
    [SerializeField] private Transform characterVisual; 
    [SerializeField] private Transform colliderPointer; 

    [Header("상태")]
    [SerializeField] private float capsuleHeight = 2f;

    private float initialBodyHeightY;

    private void Start()
    {
        if (characterVisual != null)
        {
            initialBodyHeightY = characterVisual.localPosition.y;
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

   
        characterVisual.localPosition = new Vector3(headPos.x, initialBodyHeightY, headPos.z);

        colliderPointer.localPosition = new Vector3(headPos.x, capsuleHeight * 0.5f, headPos.z);
    }

}