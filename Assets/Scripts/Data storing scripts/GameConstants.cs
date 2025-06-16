using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    public static void SetFirstGame(int volLevel)
    {
        PlayerPrefs.SetInt("FirstGame", volLevel);
    }
    public static int GetFirstGame()
    {
        return PlayerPrefs.GetInt("FirstGame");
    }

    public static void SetMusicVolume(int volLevel)
    {
        PlayerPrefs.SetInt("MusicVolume", volLevel);
    }

    public static int GetMusicVolume()
    {
        return PlayerPrefs.GetInt("MusicVolume");
    }

    public static void SetSoundVolume(int volLevel)
    {
        PlayerPrefs.SetInt("SoundVolume", volLevel);
    }
    public static int GetSoundVolume()
    {
        return   PlayerPrefs.GetInt("SoundVolume");
    }


    public static void SetXPCurrentLevel(int xpLevel)
    {
        PlayerPrefs.SetInt("CurrentXPLevel", xpLevel);
    }

    public static int GetXPCurrentLevel()
    {
       return PlayerPrefs.GetInt("CurrentXPLevel");
    }


    public static void SetEarnedXP(int xpEarned)
    {
        PlayerPrefs.SetInt("EarnedXp", xpEarned);
    }

    public static int GetEarnedXP()
    {
        return PlayerPrefs.GetInt("EarnedXp");
    }


    public static void SetTotalCoins(int totalCoins)
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
    }

    public static int GetTotalCoins()
    {
        return PlayerPrefs.GetInt("TotalCoins");
    }

    public static void SetTotalDiamonds(int totalDiamonds)
    {
        PlayerPrefs.SetInt("TotalDiamonds", totalDiamonds);
    }

    public static int GetTotalDiamonds()
    {
        return PlayerPrefs.GetInt("TotalDiamonds");
    }


    public static void SetUnlockedCities(int unlockedCityNum)
    {
        PlayerPrefs.SetInt("UnlockedCity", unlockedCityNum);
    }

    public static int GetUnlockedCity()
    {
        return PlayerPrefs.GetInt("UnlockedCity");
    }

    public static void SetGlobalTut(int tutDoneCount)
    {
        PlayerPrefs.SetInt("GlobalTut", tutDoneCount);
        PlayerPrefs.Save();
    }

    public static int GetGlobalTut()
    {
        return PlayerPrefs.GetInt("GlobalTut");
    }

    public static void SetBurnTut(int tutDoneCount)
    {
        PlayerPrefs.SetInt("BurnTut", tutDoneCount);
        PlayerPrefs.Save();
    }

    public static int GetBurnTut()
    {
        return PlayerPrefs.GetInt("BurnTut");
    }

    public static void SetCityData(string dataRetrieve, int index, int num)
    {
        PlayerPrefs.SetInt(dataRetrieve+ index,num);
        PlayerPrefs.Save();
    }
    public static int GetCityData(string dataRetrieve,int index)
    {
        return PlayerPrefs.GetInt(dataRetrieve+index);
    }

}
