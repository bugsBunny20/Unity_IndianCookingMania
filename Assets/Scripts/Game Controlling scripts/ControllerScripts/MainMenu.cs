using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Change once cities are unlockde
    public GameObject[] cityDetails;

    //To Display Earnings
    public Text coinText;
    public Text diamondText;
    public Text xpText;
    public Text xpLevelText;

    public GameObject storePopUp;
    public GameObject videoPopUp;
    public GameObject achievementPopUp;
    public GameObject SettingsPopUp;

    public string earnedXpText;
    public string targetXpText;

    private int startingTut;

    //Tutorial objects.
    public GameObject[] tutObjects;

    private void Awake()
    {

        foreach (string city in GameController.instance.unlockedCities)
        {
            if (city == "Mumbai") ;
            else
                GameObject.Find(city).SetActive(false);
        }

        //start audio

        coinText.text = GameController.instance.totalCoins.ToString();
        diamondText.text = GameController.instance.totalDiamonds.ToString();
        earnedXpText = GameController.instance.xpEarned.ToString();
        targetXpText = GameController.instance.xpLevelRequirement.ToString();
        xpText.text = earnedXpText + "/" + targetXpText;
        xpLevelText.text = (GameController.instance.currentXpLevel + 1).ToString();

        startingTut = GameConstants.GetGlobalTut();

        ShowTutorial();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowTutorial()
    {
        if (startingTut == 0)
        {
            GameConstants.SetGlobalTut(++startingTut);
            Debug.Log(GameConstants.GetGlobalTut() + " " + startingTut);
            tutObjects[0].SetActive(true);
        }
        else
        if (startingTut == 1)
        {
            GameConstants.SetGlobalTut(++startingTut);
            PlayerPrefs.Save();
            Debug.Log(GameConstants.GetGlobalTut() + " " + startingTut);

            tutObjects[0].SetActive(false);
            tutObjects[1].SetActive(true);
        }
        else
        if (startingTut == 2)
        {
            GameConstants.SetGlobalTut(++startingTut);
            Debug.Log(GameConstants.GetGlobalTut() + " " + startingTut);

            tutObjects[1].SetActive(false);
            cityDetails[0].SetActive(true);
            tutObjects[2].SetActive(true);
        }
    }

    /// <summary>
    ///Enable/Disable Pop-up of city details(info)
    /// </summary>
    /// <param name="cityNum">Value is passed by On-click event attached to the city buttons</param>
    public void ShowLevelDetails(int cityNum)
    {
        switch (cityNum)
        {
            case 1:
                if (cityDetails[0].activeSelf)
                    cityDetails[0].SetActive(false);
                else
                    cityDetails[0].SetActive(true);
                break;
            case 2:
                if (cityDetails[1].activeSelf)
                    cityDetails[1].SetActive(false);
                else
                    cityDetails[1].SetActive(true);
                break;
            case 3:
                if (cityDetails[2].activeSelf)
                    cityDetails[2].SetActive(false);
                else
                    cityDetails[2].SetActive(true);
                break;
            case 4:
                if (cityDetails[3].activeSelf)
                    cityDetails[3].SetActive(false);
                else
                    cityDetails[3].SetActive(true);
                break;
        }

    }

    /// <summary>
    /// Loads scene according to chapter selected
    /// </summary>
    /// <param name="cityName">Name passed by on-click event attached to play button in city details pop up</param>
    public void LoadLevel(string cityName)
    {
        switch (cityName)
        {
            case "Mumbai":
                cityDetails[0].SetActive(false);
                tutObjects[2].SetActive(false);
                SceneManager.LoadScene(2);
                break;

            case "Culcutta":
                {
                    cityDetails[1].SetActive(false);
                    //SceneManager.LoadScene(3);
                    break;
                }


            case "Delhi":
                cityDetails[2].SetActive(false);
                //SceneManager.LoadScene(4);
                break;

            case "Chennai":
                cityDetails[3].SetActive(false);
                //SceneManager.LoadScene(5);
                break;
        }
    }

    public void ButtonClicked(string BtnName)
    {
        switch(BtnName)
        {
            case "Store":
                if (!storePopUp.activeSelf)
                {
                    storePopUp.SetActive(true);
                    StoreHandler.instance.DisplayTab(1);
                }
                else
                    storePopUp.SetActive(false);
                break;

            case "Video":
                if (!videoPopUp.activeSelf)
                    videoPopUp.SetActive(true);
                else
                    videoPopUp.SetActive(false);
                break;

            case "Achievements":
                if (!achievementPopUp.activeSelf)
                    achievementPopUp.SetActive(true);
                else
                    achievementPopUp.SetActive(false);
                break;

            case "Settings":
                if (!SettingsPopUp.activeSelf)
                    SettingsPopUp.SetActive(true);
                else
                    SettingsPopUp.SetActive(false);
                break;
        }
    }

}
