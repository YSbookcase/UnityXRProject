using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class PlayerManager : MonoBehaviour
{
    public ObservableProperty<int> Money { get; private set; } = new(0);

    //[SerializeField] private SelectionData selectionData;

    //public List<UnitData> SelectedUnits { get; private set; } = new();


    private void Awake()
    {
       
    }


    private void Update()
    {
        
        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    AddMoney(5000);
        //}
    }

    private void OnDestroy()
    {
        //Debug.LogWarning("PlayerManager�� Destroy �Ǿ����ϴ�!");
    }

    private void OnDisable()
    {
        //Debug.LogWarning("PlayerManager�� ��Ȱ��ȭ�Ǿ����ϴ�!");
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
}