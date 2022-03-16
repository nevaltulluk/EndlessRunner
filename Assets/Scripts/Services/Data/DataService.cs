
using UnityEngine;

public class DataService
{
    public UserDataModel UserData;
    
    public DataService()
    {
        UserData = new UserDataModel();
        InitData();
    }

    private void InitData()
    {
        UserData.CoinAmount = 0;
        UserData.HighScore = 0;
        
        if (PlayerPrefs.HasKey("Coin"))
        {
            UserData.CoinAmount = PlayerPrefs.GetInt("Coin");
        }
        
        if (PlayerPrefs.HasKey("HighScore"))
        {
            UserData.HighScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Coin", UserData.CoinAmount);
        PlayerPrefs.SetInt("HighScore", UserData.HighScore);
    }
}
