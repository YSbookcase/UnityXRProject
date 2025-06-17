using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class PlayerManager : MonoBehaviour
{
    public ObservableProperty<int> Money { get; private set; } = new(0);

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _trackingRoot; // 이동 기준점 (예: XR Origin 또는 플레이어 루트)

    private Vector3 _lastPosition;
    private float _moveThreshold = 0.01f;


    //[SerializeField] private SelectionData selectionData;

    //public List<UnitData> SelectedUnits { get; private set; } = new();


    private void Awake()
    {
       
    }

    private void Start()
    {
        if (_trackingRoot == null)
            _trackingRoot = transform;

        _lastPosition = _trackingRoot.position;
    }


    private void Update()
    {
        MoveDetection();
        
        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    AddMoney(5000);
        //}
    }

    private void OnDestroy()
    {
        //Debug.LogWarning("PlayerManager가 Destroy 되었습니다!");
    }

    private void OnDisable()
    {
        //Debug.LogWarning("PlayerManager가 비활성화되었습니다!");
    }



    //public void SetSelectedUnits(List<UnitData> units)
    //{
    //    SelectedUnits = new List<UnitData>(units);
    //}

    public void AddMoney(int amount)
    {
        Money.Value += amount;
    }

    public bool TrySpendMoney(int amount)
    {
        if (Money.Value >= amount)
        {
            Money.Value -= amount;
            return true;
        }
        return false;
    }

    public void MoveDetection()
    {
        Vector3 currentPosition = _trackingRoot.position;
        float distanceMoved = Vector3.Distance(currentPosition, _lastPosition);

        bool isMoving = distanceMoved > _moveThreshold;

        _animator.SetBool("IsMove", isMoving);

        _lastPosition = currentPosition;
    }

}