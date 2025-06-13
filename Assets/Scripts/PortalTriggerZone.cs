using UnityEngine;

public class PortalTriggerZone : MonoBehaviour
{
    [SerializeField] private Portal parentPortal;

    private void OnTriggerEnter(Collider other)
    {
        parentPortal?.OnTriggerEnteredFromChild(other);
    }
}
