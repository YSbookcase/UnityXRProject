using UnityEngine;

public class PortalTriggerZone : MonoBehaviour
{
    [SerializeField] private Portal1 parentPortal;

    private void OnTriggerEnter(Collider other)
    {
        parentPortal?.OnTriggerEnteredFromChild(other);
    }
}
