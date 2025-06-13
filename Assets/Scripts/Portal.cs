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
  

    private bool playerInTrigger = false;
    private Transform player;
    private Vector3 lastLocalPos;

    public void OnTriggerEnteredFromChild(Collider other)
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

        Debug.Log($"[Portal] �÷��̾� ��Ż �̵� �Ϸ�: {other.name}");
    }

    private void OnTriggerExit(Collider other)
    {
        if (player != null && other.transform == player)
        {
            playerInTrigger = false;
            player = null;
        }
    }




    private void Update()
    {
        if (!playerInTrigger || isExit || !targetPortal || player == null) return;

        Vector3 currentLocalPos = transform.InverseTransformPoint(player.position);

        // Z�� �������� ��Ż�� ����ߴ��� �Ǵ�
        if (lastLocalPos.z > 0 && currentLocalPos.z <= 0)
        {
            TeleportPlayer();
            playerInTrigger = false;
        }

        lastLocalPos = currentLocalPos;
    }

    private void TeleportPlayer()
    {
        if (!player || !targetPortal) return;

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
