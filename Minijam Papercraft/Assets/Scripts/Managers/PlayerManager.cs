using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public int Money = 0;
    public int Level { get; private set; }
    public int CurrentXP { get; private set; }
    public int XPToNextLevel { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize player stats
        Money = 0;
        Level = 1;
        CurrentXP = 0;
        XPToNextLevel = 100; // Example value for XP needed to level up
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        UIManager.Instance.UpdateMoneyText(Money);
    }

    public void GainXP(int amount)
    {
        CurrentXP += amount;

        while (CurrentXP >= XPToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        CurrentXP -= XPToNextLevel;
        Level++;
        XPToNextLevel = Mathf.RoundToInt(XPToNextLevel * 1.5f); // Example of increasing XP needed for next level
        Debug.Log("Leveled up! Current level: " + Level);
    }
}