using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("UI Texts")]
    [SerializeField]
    private TMP_Text _winAmountText;
    [SerializeField]
    private TMP_Text _balanceText;
    [SerializeField]
    private TMP_Text _costText;

    public void UpdateWinAmountText(int winAmount)
    {
        _winAmountText.text = winAmount.ToString();
    }

    public void UpdateBalanceText(int balance)
    {
        _balanceText.text = balance.ToString();
    }

    public void UpdateCostText(int cost)
    {
        _costText.text = cost.ToString();
    }
}
