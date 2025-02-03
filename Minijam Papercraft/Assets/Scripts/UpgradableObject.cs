using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeObjects : MonoBehaviour
{
    private int[] _upgradeCosts = { 10, 50, 100, 250, 500, 1000, 2000, 5000, 10000, 17500, 25000, 50000, 75000, 100000, 150000 };
    public int CurrentLevel = 0;
    public int MaxLevel => _upgradeCosts.Length - 1;
    public int UpgradeCost => _upgradeCosts[CurrentLevel];

    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private TMP_Text _upgradeLevelText;
    [SerializeField] private Button _upgradeLevelButton;

    [SerializeField] private TMP_Text _upgradeCostText;

    public virtual void UpdateUpgradeTexts()
    {
        _upgradeLevelText.text = "Level " + (CurrentLevel + 1);
        _upgradeCostText.text = UpgradeCost.AbreviateMoney();
        _upgradeLevelButton.interactable = PlayerManager.Instance.Money >= UpgradeCost;
    }

    public virtual void ShowUpgradePanel()
    {
        UpdateUpgradeTexts();
        _upgradePanel.SetActive(true);
    }

    public void HideUpgradePanel()
    {
        _upgradePanel.SetActive(false);
    }

    public virtual void Upgrade()
    {
        if (CurrentLevel >= _upgradeCosts.Length || PlayerManager.Instance.Money < UpgradeCost)
            return;

        PlayerManager.Instance.AddMoney(-UpgradeCost);
        CurrentLevel++;
        UpdateUpgradeTexts();
    }
}
