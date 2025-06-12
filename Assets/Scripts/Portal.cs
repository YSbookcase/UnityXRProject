using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("�ڽ� ������Ʈ ����")]
    [SerializeField] private Transform portalView;      // Quad ��
    [SerializeField] private Collider portalTrigger;    // BoxCollider (isTrigger)
    [SerializeField] private Camera portalCamera;       // �ⱸ ��Ż���� ����

    [Header("��Ż ����")]
    [SerializeField] private Portal targetPortal;

    [Header("����")]
    [SerializeField] private bool isExit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isExit || !targetPortal) return;
        if (!other.CompareTag("Player")) return;

        Transform player = other.transform;

        Vector3 localPos = transform.InverseTransformPoint(player.position);
        Vector3 localDir = transform.InverseTransformDirection(player.forward);

        player.position = targetPortal.transform.TransformPoint(localPos);
        player.forward = targetPortal.transform.TransformDirection(localDir);

        if (player.TryGetComponent(out Rigidbody rb))
        {
            Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
            rb.velocity = targetPortal.transform.TransformDirection(localVel);
        }
    }
}
