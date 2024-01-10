using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private int _startGoldAmount;
    [SerializeField]
    private int _spinCost;
    private int _gold = 0;
    public int Gold { get { return _gold; } set { _gold = value; } }
    private int _winAmount;
    [Header("Events")]
    [SerializeField]
    private UnityEvent<int> OnGoldChange;
    [SerializeField]
    private UnityEvent<int> OnWinAmountChange;
    [SerializeField]
    private UnityEvent<int> OnSpinCostChange;

    public void InitializeGold()
    {
        AddGold(_startGoldAmount);
        SetSpinCost(_spinCost);
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        OnGoldChange?.Invoke(Gold);
    }

    public void RemoveGold(int amount)
    {
        Gold -= amount;
        OnGoldChange?.Invoke(Gold);
    }

    public bool HasEnoughGoldForSpin()
    {
        if(Gold >= _spinCost)
        {
            RemoveGold(_spinCost);
            return true;
        }
        return false;
    }

    public void SetWinAmount(int amount)
    {
        _winAmount = amount;
        OnWinAmountChange?.Invoke(_winAmount);
    }

    public void SetSpinCost(int cost)
    {
        _spinCost = cost;
        OnSpinCostChange?.Invoke(_spinCost);
    }

}
