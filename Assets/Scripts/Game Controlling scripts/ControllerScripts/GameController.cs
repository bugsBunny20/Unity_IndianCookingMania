using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public static GameController instance;

    //sriptable object that stores details of each xp level
    public ScoreSystem scoreDetails;


    public int totalCoins;
    public int totalDiamonds;
    public int currentXpLevel;
    public int xpEarned;
    public int xpLevelRequirement;
    public List<string> unlockedCities;
    public int musicLevel;
    public int soundLevel;

    //game objects for animaition
    public GameObject companyLogo;
    public GameObject gameLogo;
    public GameObject Logo;

    int firstGame = 0;

    private void Awake()
    {
        firstGame = GameConstants.GetFirstGame();
        if (firstGame==0)
        {
            firstGame = 1;
            GameConstants.SetFirstGame(firstGame);
            LoadData();
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        else
            Destroy(this);

    }


    // Intialize all the global player progress
    void Start()
    {
        totalCoins = GameConstants.GetTotalCoins();
        totalDiamonds = GameConstants.GetTotalDiamonds();
        currentXpLevel = GameConstants.GetXPCurrentLevel();
        xpEarned = GameConstants.GetEarnedXP();
        xpLevelRequirement = scoreDetails.xpLevelDetails[currentXpLevel].totalToBeEarned;


        for (int i = 0; i <= GameConstants.GetUnlockedCity(); i++)
        {
            switch (i)
            {
                case 0:
                    unlockedCities.Add("Mumbai");
                    break;
                case 1:
                    unlockedCities.Add("Calcutta");
                    break;
                case 2:
                    unlockedCities.Add("Delhi");
                    break;
                case 3:
                    unlockedCities.Add("Chennai");
                    break;
            }
        }
        musicLevel = GameConstants.GetMusicVolume();
        soundLevel = GameConstants.GetSoundVolume();

        StartCoroutine("LogoAnimation");

    }


    public void XpCalculation()
    {
        //check if xp earned has crossed target xp for current xp level
        if (xpEarned >= scoreDetails.xpLevelDetails[currentXpLevel].totalToBeEarned)
        {
            Debug.Log("Inside xp calculations");
            xpEarned = xpEarned - scoreDetails.xpLevelDetails[currentXpLevel].totalToBeEarned;
            GameConstants.SetEarnedXP(xpEarned);

            totalCoins += scoreDetails.xpLevelDetails[currentXpLevel].rewardCoins;
            GameConstants.SetTotalCoins(totalCoins);
            totalDiamonds += scoreDetails.xpLevelDetails[currentXpLevel].rewardDiamonds;
            GameConstants.SetTotalCoins(totalDiamonds);

            GameConstants.SetXPCurrentLevel(++currentXpLevel);
            xpLevelRequirement = scoreDetails.xpLevelDetails[currentXpLevel].totalToBeEarned;
        }

        else
        {
            //Debug.Log("Inside assigning xp");

            GameConstants.SetEarnedXP(xpEarned);

        }
    }
    
    IEnumerator LogoAnimation()
    {
        yield return new WaitForSeconds(3.0f);
        gameLogo.SetActive(true);
        yield return new WaitForSeconds(8.0f);
        SceneManager.LoadScene(1);

    }


    private void LoadData()
    {
        Debug.Log("Inside Load data function");
        GameConstants.SetMusicVolume(10);
        GameConstants.SetSoundVolume(10);
        GameConstants.SetEarnedXP(0);
        GameConstants.SetXPCurrentLevel(0);
        GameConstants.SetTotalCoins(0);
        GameConstants.SetTotalDiamonds(0);
        GameConstants.SetUnlockedCities(0);
        GameConstants.SetGlobalTut(0);
        GameConstants.SetBurnTut(0);


        for(int i=0; i<4;i++)
        {
            GameConstants.SetCityData("cityID_" , i, 0);
            GameConstants.SetCityData("cookingTut_", i, 0);
            GameConstants.SetCityData("currentLevel_", i,1);
            GameConstants.SetCityData("beverageMachine_" , i, 0);
            GameConstants.SetCityData("beverage_" , i, 0);
            GameConstants.SetCityData("fryPan1_" , i, 0);
            GameConstants.SetCityData("dish1_" , i, 0);
            GameConstants.SetCityData("tableTop1_" , i, 0);
            GameConstants.SetCityData("base1_" , i, 0);
            GameConstants.SetCityData("fryPan2_" , i, 0);
            GameConstants.SetCityData("dish2_" , i, 0);
            GameConstants.SetCityData("tableTop2_" , i, 0);
            GameConstants.SetCityData("base2_" , i, 0);
            GameConstants.SetCityData("foodWarmer_" , i, 0);
            GameConstants.SetCityData("topping1_" , i, 0);
            GameConstants.SetCityData("topping2_" , i, 0);
            GameConstants.SetCityData("topping3_" , i, 0);
            GameConstants.SetCityData("fryPan3_" , i, 0);
            GameConstants.SetCityData("dish3_" , i, 0);
            GameConstants.SetCityData("splDish_" , i, 0);
            GameConstants.SetCityData("dish2Unlocked_", i, 0);
            GameConstants.SetCityData("dish3Unlocked_", i, 0);
            GameConstants.SetCityData("splDishUnlocked_", i, 0);
            GameConstants.SetCityData("topping1unlocked_", i, 0);
            GameConstants.SetCityData("topping2unlocked_", i, 0);
            GameConstants.SetCityData("topping3unlocked_", i, 0);
            GameConstants.SetCityData("foodWarmerUnlocked_", i, 0);

            for(int j=1;j<=40;j++)
            {
                string dataRetrieve = "Level_" + i;
                GameConstants.SetCityData(dataRetrieve, j, 0);
            }

            for(int j = 1; j <= 8; j++)
            {
                string dataRetrieve = "splDishCount_" + i;
                GameConstants.SetCityData(dataRetrieve, j, 1);

            }
        }
        PlayerPrefs.Save();
       
    }

}
