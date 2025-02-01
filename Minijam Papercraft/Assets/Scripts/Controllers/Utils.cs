public static class Utils
{
    /// <summary>
    /// Abreviates a number to a more readable format (e.g. 1000 -> 1K, 1500000 -> 1.5M, 1050000000 -> 1.05B)
    /// </summary>
    /// <param name="money"></param>
    /// <returns>
    /// 
    /// </returns>
    /// </summary>
    public static string AbreviateMoney(this int money)
    {
        if (money >= 1000000000)
        {
            return (money / 1000000000.0f).ToString("0.##") + "B";
        }
        if (money >= 1000000)
        {
            return (money / 1000000.0f).ToString("0.##") + "M";
        }
        if (money >= 1000)
        {
            return (money / 1000.0f).ToString("0.##") + "K";
        }
        return money.ToString();
    }

    public static string AbreviateMoney(this float money)
    {
        if (money >= 1000000000.0f)
        {
            return (money / 1000000000.0f).ToString("0.##") + "B";
        }
        if (money >= 1000000.0f)
        {
            return (money / 1000000.0f).ToString("0.##") + "M";
        }
        if (money >= 1000.0f)
        {
            return (money / 1000.0f).ToString("0.##") + "K";
        }
        return money.ToString();
    }
}
