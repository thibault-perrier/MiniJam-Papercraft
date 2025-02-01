using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPrinter : UpgradeObjects
{
    [SerializeField] private TMP_Text _moneyPerSecondText;

    [SerializeField] private float _moneyPerSecond = 0.0f;
    [SerializeField] private int _moneyPerClick = 1;

    private float[] _printTimer = { 2.0f, 1.85f, 1.7f, 1.55f, 1.4f, 1.25f, 1.1f, 0.95f, 0.8f, 0.65f, 0.5f, 0.4f, 0.3f, 0.2f, 0.1f };
    private int[] _printAmmount = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5 };


    private void Start()
    {
        _moneyPerSecond = _printAmmount[CurrentLevel] / _printTimer[CurrentLevel];
        StartCoroutine(PrintMoney());
    }

    private IEnumerator PrintMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(_printTimer[CurrentLevel]);
            PlayerManager.Instance.AddMoney(_printAmmount[CurrentLevel]);
            UpdateUpgradeTexts();
        }
    }

    public void PrintMoneyOnClick()
    {
        PlayerManager.Instance.AddMoney(_moneyPerClick);
    }

    public override void UpdateUpgradeTexts()
    {
        base.UpdateUpgradeTexts();
        _moneyPerSecondText.text = "Bill printing rate: $ " + _moneyPerSecond.AbreviateMoney() + "/s";
    }

    public override void ShowUpgradePanel()
    {
        base.ShowUpgradePanel();
    }

    public override void Upgrade()
    {
        if (CurrentLevel >= MaxLevel)
        {
            Debug.Log("Max level reached");
            return;
        }
        base.Upgrade();
        _moneyPerSecond = _printAmmount[CurrentLevel] / _printTimer[CurrentLevel];
    }
}
