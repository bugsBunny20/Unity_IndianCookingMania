using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{

    public static UpgradeSystem instance;

    public UpgradeData upgradeData;

    #region Prices of Ingredients
    [Space]
    [Header("Current Price of ingredients")]
    public int dish1CurrentPrice;
    public int dish2CurrentPrice;
    public int dish3CurrentPrice;

    public int base1CurrentPrice;
    public int base2CurrentPrice;


    public int splDishCurrentPrice;

    public int beverageCurrentPrice;

    public int topping1CurrentPrice;
    public int topping2CurrentPrice;
    public int topping3CurrentPrice;
    #endregion


    #region currently avalaible stoves and tableTop
    public int tableTop1;
    public int tableTop2;

    public int fryPan1ctr;
    public int fryPan2ctr;
    public int fryPan3ctr;

    public int beverageCtr;
    #endregion

    #region Current cooking time
    public float fryPan1cookTime;
    public float fryPan2cookTime;
    public float fryPan3cookTime;
    public float beverageTimer;
    #endregion 

    //DO NOT EDIT OR CHANGE BELOW IN INSPECTOR
    #region Sprite array: Ingredient Upgrade
    [Space]
    [Header("Dish1 Base Upgrades")]
    public Image base1tray;
    public Image[] base1single;
    public Image[] base1completeDish;
    public Image upgradeBase1;

    public Sprite[] base1trayChanges;
    public Sprite[] base1completeDishChanges;
    public Sprite[] base1singleSprites;

    [Space]
    [Header("Dish2 Base Upgrades")]
    public Image base2tray;
    public Image[] base2single;
    public Image[] base2completeDish;
    public Image upgradeBase2;

    public Sprite[] base2trayChanges;
    public Sprite[] base2completeDishChanges;
    public Sprite[] base2singleSprites;
    #endregion

    #region GameObject Array : Kitchen upgrade
    [Space]
    [Header("Table Upgrades")]
    public GameObject[] tt1LockedGameIcon;
    public GameObject[] tt2LockedGameIcon;
    public GameObject[] tableTop3upgrade;//dish3 upgrade

    public GameObject[] tt1LockedUpgradeIcon;
    public GameObject[] tt2LockedUpgradeIcon;

    [Space]
    [Header("Stove Upgrades")]
    public GameObject[] stove1Upgrade;
    public GameObject[] stove2Upgrade;
    [Space]
    public GameObject[] kettleUpgrade;
    public GameObject[] foodWarmerUpgrade;

    public GameObject[] upgradeFoodWarmerIcon;
    #endregion

    //Current Index for each ingredient and appliance
    public int currentBevMacIndex;
    private int currentBevIndex;
    public int currentFP1Index;
    private int currentDish1Index;
    private int currentBase1Index;
    public int currentTT1Index;
    public int currentFP2Index;
    private int currentDish2Index;
    private int currentBase2Index;
    public int currentTT2Index;
    public int currentFWIndex;
    private int currentTopping1Index;
    private int currentTopping2Index;
    private int currentTopping3Index;
    public int currentFP3Index;
    private int currentDish3Index;
    private int currentSplDishIndex;

    [Space]
    [Header("Upgrade screen texts")]
    public Text[] CoinsRequired;
    public Text[] Diamondsrequired;
    public Text[] CurrentPortions;
    public Text[] NextPortions;
    public Text[] CurrentPrice;
    public Text[] NextPrice;
    public Text[] CurrentPrepTime;
    public Text[] NextPrepTime;

    [Space]
    [Header("Upgrade screen game objects")]
    public GameObject[] selectedOption;
    public GameObject[] displayDetails;
    public GameObject[] lockedDetails;
    public GameObject[] lockedSelectBtns;

    [System.Serializable]
    public class upgradedLevelIcon
    {
        public int upgradeLevel;
        public GameObject upgradeIcon1;
        public GameObject upgradeIcon2;
        public GameObject upgradeIcon3;
    }

    public List<upgradedLevelIcon> upgradeLevelIcons;

    public GameObject SamosaUnlocked;
    public GameObject dish3Unlocked;
    public GameObject splDishUnlocked;
    public GameObject samosaLockIcon;
    public GameObject foodWarmerUnlocked;
    public GameObject[] toppingsUnlocked;

    public GameObject notEnoughCoins;
    private int storingIndex;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        storingIndex = SceneManager.GetActiveScene().buildIndex - 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        upgradeData = Resources.Load<UpgradeData>("LevelData/UpgradeMumbai");

        //fetch the current index for upgrades of each item
        currentBevMacIndex = GameConstants.GetCityData("beverageMachine_", storingIndex);
        beverageTimer = upgradeData.beverageUpgradeData[currentBevMacIndex].cookTime;

        currentBevIndex = GameConstants.GetCityData("beverage_", storingIndex);
        beverageCurrentPrice = upgradeData.beverageUpgradeData[currentBevIndex].itemPrice;


        currentFP1Index = GameConstants.GetCityData("fryPan1_", storingIndex);
        fryPan1cookTime = upgradeData.dish1UpgradeData[currentFP1Index].cookTime;

        currentDish1Index = GameConstants.GetCityData("dish1_", storingIndex);
        currentBase1Index = GameConstants.GetCityData("base1_", storingIndex);
        dish1CurrentPrice = upgradeData.dish1BaseUpgradeData[currentDish1Index].itemPrice + upgradeData.dish1UpgradeData[currentBase1Index].itemPrice;


        currentTT1Index = GameConstants.GetCityData("tableTop1_", storingIndex);


        currentFP2Index = GameConstants.GetCityData("fryPan2_", storingIndex);
        fryPan2cookTime = upgradeData.dish2UpgradeData[currentFP2Index].cookTime;

        currentDish2Index = GameConstants.GetCityData("dish2_", storingIndex);
        currentBase2Index = GameConstants.GetCityData("base2_", storingIndex);
        dish2CurrentPrice = upgradeData.dish2BaseUpgradeData[currentDish2Index].itemPrice + upgradeData.dish2UpgradeData[currentBase2Index].itemPrice;


        currentTT2Index = GameConstants.GetCityData("tableTop2_", storingIndex);


        currentFWIndex = GameConstants.GetCityData("foodWarmer_", storingIndex);

        currentTopping1Index = GameConstants.GetCityData("topping1_", storingIndex);
        topping1CurrentPrice = upgradeData.topping1UpgradeData[currentTopping1Index].itemPrice;


        currentTopping2Index = GameConstants.GetCityData("topping2_", storingIndex);
        topping2CurrentPrice = upgradeData.topping2UpgradeData[currentTopping2Index].itemPrice;


        currentTopping3Index = GameConstants.GetCityData("topping3_", storingIndex);
        topping3CurrentPrice = upgradeData.topping3UpgradeData[currentTopping3Index].itemPrice;


        currentFP3Index = GameConstants.GetCityData("fryPan3_", storingIndex);
        fryPan3cookTime = upgradeData.dish3UpgradeData[currentFP1Index].cookTime;

        currentDish3Index = GameConstants.GetCityData("dish3_", storingIndex);
        dish3CurrentPrice = upgradeData.dish3UpgradeData[currentDish3Index].itemPrice;



        currentSplDishIndex = GameConstants.GetCityData("splDish_", storingIndex);

        for (int i = 0; i < 17; i++)
            updateUpgrades(i);

    }

    // Update is called once per frame
    void Update()
    {
        //if (LevelController.instance.LevelStarted)
        //{
        //    if (GameConstants.GetCityData("dish2Unlocked_", storingIndex) == 1)
        //    {
        //        SamosaUnlocked.SetActive(true);
        //        samosaLockIcon.SetActive(false);
        //    }


        //    if (GameConstants.GetCityData("foodWarmerUnlocked_", storingIndex) == 1)
        //       foodWarmerUnlocked.SetActive(true);


        //    if (GameConstants.GetCityData("topping1unlocked_", storingIndex) == 1)
        //        toppingsUnlocked[0].SetActive(true);

        //    if (GameConstants.GetCityData("topping2unlocked_", storingIndex) == 1)
        //        toppingsUnlocked[1].SetActive(true);

        //    if (GameConstants.GetCityData("topping3unlocked_", storingIndex) == 1)
        //        toppingsUnlocked[2].SetActive(true);

        //    if (GameConstants.GetCityData("dish3Unlocked_", storingIndex) == 1)
        //    {
        //        dish3Unlocked.SetActive(true);
        //    }

        //    if (GameConstants.GetCityData("splDishUnlocked_", storingIndex) == 1)
        //    {
        //        splDishUnlocked.SetActive(true);
        //    }

        //}

    }

    public void updateUpgrades(int upgradeNum)
    {
        if (GameConstants.GetCityData("dish2Unlocked_", storingIndex) == 1)
        {
            lockedSelectBtns[0].SetActive(false);
            lockedSelectBtns[1].SetActive(false);
            lockedSelectBtns[2].SetActive(false);
            lockedSelectBtns[3].SetActive(false);
            lockedSelectBtns[4].SetActive(false);
        }

        if (GameConstants.GetCityData("topping1unlocked_", storingIndex) == 1)
            lockedSelectBtns[5].SetActive(false);


        if (GameConstants.GetCityData("topping2unlocked_", storingIndex) == 1)
            lockedSelectBtns[6].SetActive(false);


        if (GameConstants.GetCityData("topping3unlocked_", storingIndex) == 1)
            lockedSelectBtns[7].SetActive(false);

        if (GameConstants.GetCityData("dish3Unlocked_", storingIndex) == 1)
        {
            lockedSelectBtns[8].SetActive(false);
            lockedSelectBtns[9].SetActive(false);

        }

        if (GameConstants.GetCityData("splDishUnlocked_", storingIndex) == 1)
            lockedSelectBtns[10].SetActive(false);

        switch (upgradeNum)
        {
            #region Beverage Machine
            case 0:
                currentBevMacIndex = GameConstants.GetCityData("beverageMachine_", storingIndex);
                beverageTimer = upgradeData.beverageUpgradeData[currentBevMacIndex].cookTime;

                CoinsRequired[0].text = upgradeData.beverageMachineUpgradeBuyingDetails[currentBevMacIndex].costPrice.ToString();
                Diamondsrequired[0].text = upgradeData.beverageMachineUpgradeBuyingDetails[currentBevMacIndex].Diamonds.ToString();

                CurrentPortions[0].text = (currentBevMacIndex + 1).ToString();
                NextPortions[0].text = (currentBevMacIndex + 2).ToString();

                CurrentPrepTime[0].text = upgradeData.beverageUpgradeData[currentBevMacIndex].cookTime.ToString();
                NextPrepTime[0].text = upgradeData.beverageUpgradeData[currentBevMacIndex + 1].cookTime.ToString();

                upgradeLevelIcons[0].upgradeLevel = currentBevMacIndex - 1;

                if (upgradeLevelIcons[0].upgradeLevel < currentBevMacIndex)
                {
                    switch (upgradeLevelIcons[0].upgradeLevel)
                    {
                        case -1:
                            break;

                        case 0:
                            upgradeLevelIcons[0].upgradeIcon1.SetActive(true);
                            break;

                        case 1:
                            upgradeLevelIcons[0].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[0].upgradeIcon2.SetActive(true);
                            kettleUpgrade[0].SetActive(true);
                            break;

                        case 2:
                            upgradeLevelIcons[0].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[0].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[0].upgradeIcon3.SetActive(true);
                            kettleUpgrade[0].SetActive(true);
                            kettleUpgrade[1].SetActive(true);
                            break;
                    }

                }


                break;
            #endregion

            #region bevereage
            case 1:
                currentBevIndex = GameConstants.GetCityData("beverage_", storingIndex);
                beverageCurrentPrice = upgradeData.beverageUpgradeData[currentBevIndex].itemPrice;

                CoinsRequired[1].text = upgradeData.beverageUpgradeBuyingDetails[currentBevIndex].costPrice.ToString();
                Diamondsrequired[1].text = upgradeData.beverageUpgradeBuyingDetails[currentBevIndex].Diamonds.ToString();

                CurrentPrice[0].text = upgradeData.beverageUpgradeData[currentBevIndex].itemPrice.ToString();
                NextPrice[0].text = upgradeData.beverageUpgradeData[currentBevIndex + 1].itemPrice.ToString();


                upgradeLevelIcons[1].upgradeLevel = currentBevIndex - 1;

                if (upgradeLevelIcons[1].upgradeLevel < currentBevIndex)
                {
                    switch (upgradeLevelIcons[1].upgradeLevel)
                    {
                        case -1:
                            break;

                        case 0:
                            upgradeLevelIcons[1].upgradeIcon1.SetActive(true);

                            break;

                        case 1:
                            upgradeLevelIcons[1].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[1].upgradeIcon2.SetActive(true);

                            break;
                        case 2:

                            upgradeLevelIcons[1].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[1].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[1].upgradeIcon3.SetActive(true);
                            break;
                    }

                }

                else
                {

                }

                break;
            #endregion

            #region frypan 1
            case 2:
                currentFP1Index = GameConstants.GetCityData("fryPan1_", storingIndex);
                fryPan1cookTime = upgradeData.dish1UpgradeData[currentFP1Index].cookTime;

                CoinsRequired[2].text = upgradeData.fryPan1UpgradeBuyingDetails[currentFP1Index].costPrice.ToString();
                Diamondsrequired[2].text = upgradeData.fryPan1UpgradeBuyingDetails[currentFP1Index].Diamonds.ToString();

                CurrentPortions[1].text = (currentFP1Index + 1).ToString();
                NextPortions[1].text = (currentFP1Index + 2).ToString();

                CurrentPrepTime[1].text = upgradeData.dish1UpgradeData[currentFP1Index].cookTime.ToString();
                NextPrepTime[1].text = upgradeData.dish1UpgradeData[currentFP1Index + 1].cookTime.ToString();

                upgradeLevelIcons[2].upgradeLevel = currentFP1Index - 1;

                if (upgradeLevelIcons[2].upgradeLevel < currentFP1Index)
                {
                    switch (upgradeLevelIcons[2].upgradeLevel)
                    {
                        case -1:
                            break;

                        case 0:
                            upgradeLevelIcons[2].upgradeIcon1.SetActive(true);
                            stove1Upgrade[0].SetActive(true);
                            break;

                        case 1:
                            upgradeLevelIcons[2].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[2].upgradeIcon2.SetActive(true);
                            stove1Upgrade[0].SetActive(true);
                            stove1Upgrade[1].SetActive(true);
                            break;
                        case 2:
                            upgradeLevelIcons[2].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[2].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[2].upgradeIcon3.SetActive(true);
                            stove1Upgrade[0].SetActive(true);
                            stove1Upgrade[1].SetActive(true);
                            stove1Upgrade[2].SetActive(true);
                            break;
                    }

                }

                else
                {

                }
                break;
            #endregion

            #region dish1
            case 3:
                currentDish1Index = GameConstants.GetCityData("dish1_", storingIndex);
                dish1CurrentPrice = upgradeData.dish1BaseUpgradeData[currentBase1Index].itemPrice + upgradeData.dish1UpgradeData[currentDish1Index].itemPrice;

                CoinsRequired[3].text = upgradeData.dish1UpgradeBuyingDetails[currentDish1Index].costPrice.ToString();
                Diamondsrequired[3].text = upgradeData.dish1UpgradeBuyingDetails[currentDish1Index].Diamonds.ToString();

                CurrentPrice[1].text = upgradeData.dish1UpgradeData[currentDish1Index].itemPrice.ToString();
                NextPrice[1].text = upgradeData.dish1UpgradeData[currentDish1Index + 1].itemPrice.ToString();

                upgradeLevelIcons[3].upgradeLevel = currentDish1Index - 1;

                if (upgradeLevelIcons[3].upgradeLevel < currentDish1Index)
                {
                    switch (upgradeLevelIcons[3].upgradeLevel)
                    {
                        case -1:
                            break;

                        case 0:
                            upgradeLevelIcons[3].upgradeIcon1.SetActive(true);
                            break;

                        case 1:
                            upgradeLevelIcons[3].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[3].upgradeIcon2.SetActive(true);
                            break;
                        case 2:
                            upgradeLevelIcons[3].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[3].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[3].upgradeIcon3.SetActive(true);
                            break;
                    }

                }
                else
                {

                }
                break;
            #endregion

            #region tabletop1
            case 4:
                currentTT1Index = GameConstants.GetCityData("tableTop1_", storingIndex);

                CoinsRequired[4].text = upgradeData.tableTop1UpgradeBuyingDetails[currentTT1Index].costPrice.ToString();

                CurrentPortions[2].text = (currentTT1Index + 1).ToString();
                NextPortions[2].text = (currentTT1Index + 2).ToString();
                //remove lock icon


                upgradeLevelIcons[4].upgradeLevel = currentTT1Index - 1;

                if (upgradeLevelIcons[4].upgradeLevel < currentTT1Index)
                {
                    switch (upgradeLevelIcons[4].upgradeLevel)
                    {
                        case -1:
                            upgradeLevelIcons[4].upgradeIcon1.SetActive(true);
                            break;

                        case 0:
                            upgradeLevelIcons[4].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[4].upgradeIcon2.SetActive(true);
                            tt1LockedGameIcon[0].SetActive(false);
                            tt1LockedUpgradeIcon[0].SetActive(false);
                            break;

                        case 1:
                            upgradeLevelIcons[4].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[4].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[4].upgradeIcon3.SetActive(true);
                            tt1LockedUpgradeIcon[1].SetActive(false);
                            tt1LockedGameIcon[1].SetActive(false);
                            break;
                        case 2:
                            break;
                    }

                }
                else
                {

                }

                break;
            #endregion

            #region dish1Base
            case 5:
                currentBase1Index = GameConstants.GetCityData("base1_", storingIndex);
                dish1CurrentPrice = upgradeData.dish1BaseUpgradeData[currentBase1Index].itemPrice + upgradeData.dish1UpgradeData[currentDish1Index].itemPrice;

                CoinsRequired[5].text = upgradeData.dish1BaseUpgradeBuyingDetails[currentBase1Index].costPrice.ToString();
                Diamondsrequired[4].text = upgradeData.dish1BaseUpgradeBuyingDetails[currentBase1Index].Diamonds.ToString();

                CurrentPrice[2].text = upgradeData.dish1BaseUpgradeData[currentBase1Index].itemPrice.ToString();
                NextPrice[2].text = upgradeData.dish1BaseUpgradeData[currentBase1Index + 1].itemPrice.ToString();


                //change sprite
                upgradeLevelIcons[5].upgradeLevel = currentBase1Index - 1;

                if (upgradeLevelIcons[5].upgradeLevel < currentBase1Index)
                {
                    switch (upgradeLevelIcons[5].upgradeLevel)
                    {
                        case -1:
                            break;

                        case 0:
                            upgradeLevelIcons[5].upgradeIcon1.SetActive(true);
                            base1tray.sprite = base1trayChanges[0];
                            upgradeBase1.sprite = base1trayChanges[0];
                            for (int k = 0; k < base1completeDish.Length; k++)
                            {
                                base1completeDish[k].sprite = base1completeDishChanges[0];
                                base1single[k].sprite = base1singleSprites[0];

                            }
                            break;

                        case 1:
                            upgradeLevelIcons[5].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[5].upgradeIcon2.SetActive(true);
                            base1tray.sprite = base1trayChanges[1];
                            upgradeBase1.sprite = base1trayChanges[1];
                            for (int k = 0; k < base1completeDish.Length; k++)
                            {
                                base1completeDish[k].sprite = base1completeDishChanges[1];
                                base1single[k].sprite = base1singleSprites[1];
                            }
                            break;
                        case 2:
                            upgradeLevelIcons[5].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[5].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[5].upgradeIcon3.SetActive(true);
                            base1tray.sprite = base1trayChanges[2];
                            upgradeBase1.sprite = base1trayChanges[2];
                            for (int k = 0; k < base1completeDish.Length; k++)
                            {
                                base1completeDish[k].sprite = base1completeDishChanges[2];
                                base1single[k].sprite = base1singleSprites[2];
                            }
                            break;
                    }

                }
                else
                {

                }

                break;
            #endregion

            #region frypan2
            case 6:

                currentFP2Index = GameConstants.GetCityData("fryPan2_", storingIndex);
                fryPan2cookTime = upgradeData.dish2UpgradeData[currentFP2Index].cookTime;

                CoinsRequired[6].text = upgradeData.fryPan2UpgradeBuyingDetails[currentFP2Index].costPrice.ToString();
                Diamondsrequired[5].text = upgradeData.fryPan2UpgradeBuyingDetails[currentFP2Index].Diamonds.ToString();

                CurrentPortions[3].text = (currentFP2Index + 1).ToString();
                NextPortions[3].text = (currentFP2Index + 2).ToString();

                CurrentPrepTime[2].text = upgradeData.dish1UpgradeData[currentFP2Index].cookTime.ToString();
                NextPrepTime[2].text = upgradeData.dish1UpgradeData[currentFP2Index + 1].cookTime.ToString();

                upgradeLevelIcons[6].upgradeLevel = currentFP2Index - 1;

                if (upgradeLevelIcons[6].upgradeLevel < currentFP2Index)
                {
                    switch (upgradeLevelIcons[6].upgradeLevel)
                    {
                        case -1:
                            if (GameConstants.GetCityData("dish2Unlocked_", storingIndex) == 1)
                            {
                                SamosaUnlocked.SetActive(true);
                                samosaLockIcon.SetActive(false);
                            }
                            break;

                        case 0:
                            upgradeLevelIcons[6].upgradeIcon1.SetActive(true);
                            SamosaUnlocked.SetActive(true);
                            samosaLockIcon.SetActive(false);
                            stove2Upgrade[0].SetActive(true);
                            break;

                        case 1:
                            upgradeLevelIcons[6].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[6].upgradeIcon2.SetActive(true);
                            SamosaUnlocked.SetActive(true);
                            samosaLockIcon.SetActive(false);
                            stove2Upgrade[0].SetActive(true);
                            stove2Upgrade[1].SetActive(true);
                            break;
                        case 2:
                            upgradeLevelIcons[6].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[6].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[6].upgradeIcon3.SetActive(true);
                            SamosaUnlocked.SetActive(true);
                            samosaLockIcon.SetActive(false);
                            stove2Upgrade[0].SetActive(true);
                            stove2Upgrade[1].SetActive(true);
                            stove2Upgrade[2].SetActive(true);
                            break;
                    }

                }

                else
                {

                }

                break;
            #endregion

            #region dish2
            case 7:
                currentDish1Index = GameConstants.GetCityData("dish2_", storingIndex);
                dish2CurrentPrice = upgradeData.dish2BaseUpgradeData[currentBase2Index].itemPrice + upgradeData.dish2UpgradeData[currentDish2Index].itemPrice;

                CoinsRequired[7].text = upgradeData.dish2UpgradeBuyingDetails[currentDish2Index].costPrice.ToString();
                Diamondsrequired[6].text = upgradeData.dish2UpgradeBuyingDetails[currentDish2Index].Diamonds.ToString();

                CurrentPrice[3].text = upgradeData.dish2UpgradeData[currentDish2Index].itemPrice.ToString();
                NextPrice[3].text = upgradeData.dish2UpgradeData[currentDish2Index + 1].itemPrice.ToString();

                upgradeLevelIcons[7].upgradeLevel = currentDish2Index - 1;

                if (upgradeLevelIcons[7].upgradeLevel < currentDish2Index)
                {
                    switch (upgradeLevelIcons[7].upgradeLevel)
                    {
                        case -1:
                            break;

                        case 0:
                            upgradeLevelIcons[7].upgradeIcon1.SetActive(true);
                            break;

                        case 1:
                            upgradeLevelIcons[7].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[7].upgradeIcon2.SetActive(true);
                            break;
                        case 2:
                            upgradeLevelIcons[7].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[7].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[7].upgradeIcon3.SetActive(true);
                            break;
                    }

                }
                else
                {

                }
                break;
            #endregion

            #region tabletop2
            case 8:
                currentTT2Index = GameConstants.GetCityData("tableTop2_", storingIndex);
                //Debug.Log("************Table top 2: " + currentTT2Index);

                CoinsRequired[8].text = upgradeData.tableTop2UpgradeBuyingDetails[currentTT2Index].costPrice.ToString();

                CurrentPortions[4].text = (currentTT2Index + 1).ToString();
                NextPortions[4].text = (currentTT2Index + 2).ToString();

                //remove lock icon

                upgradeLevelIcons[8].upgradeLevel = currentTT2Index - 1;
                //GameConstants.GetCityData("dish2Unlocked_", storingIndex) == 1

                if (upgradeLevelIcons[8].upgradeLevel < currentTT2Index)
                {
                    //Debug.Log("************Table top 2: " + upgradeLevelIcons[8].upgradeLevel);

                    switch (upgradeLevelIcons[8].upgradeLevel)
                    {
                        case -1:
                            //Debug.Log("************Table top 2: " + upgradeLevelIcons[8].upgradeLevel);

                            upgradeLevelIcons[8].upgradeIcon1.SetActive(true);
                            break;

                        case 0:
                            Debug.Log("************Table top 2: " + upgradeLevelIcons[8].upgradeLevel);
                            upgradeLevelIcons[8].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[8].upgradeIcon2.SetActive(true);
                            tt2LockedGameIcon[0].SetActive(false);
                            tt2LockedUpgradeIcon[0].SetActive(false);
                            break;

                        case 1:
                            upgradeLevelIcons[8].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[8].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[8].upgradeIcon3.SetActive(true);
                            tt2LockedGameIcon[0].SetActive(false);
                            tt2LockedUpgradeIcon[0].SetActive(false);
                            tt2LockedUpgradeIcon[1].SetActive(false);
                            tt2LockedGameIcon[1].SetActive(false);
                            break;
                        case 2:
                            break;
                    }

                }
                else
                {

                }

                break;
            #endregion

            #region dish2Base
            case 9:
                currentBase2Index = GameConstants.GetCityData("base2", storingIndex);
                dish2CurrentPrice = upgradeData.dish2BaseUpgradeData[currentBase2Index].itemPrice + upgradeData.dish2UpgradeData[currentDish2Index].itemPrice;

                CoinsRequired[9].text = upgradeData.dish2BaseUpgradeBuyingDetails[currentBase2Index].costPrice.ToString();
                Diamondsrequired[7].text = upgradeData.dish2BaseUpgradeBuyingDetails[currentBase2Index].Diamonds.ToString();

                CurrentPrice[4].text = upgradeData.dish2BaseUpgradeData[currentBase2Index].itemPrice.ToString();
                NextPrice[4].text = upgradeData.dish2BaseUpgradeData[currentBase2Index + 1].itemPrice.ToString();

                upgradeLevelIcons[9].upgradeLevel = currentBase2Index - 1;
                if (upgradeLevelIcons[9].upgradeLevel < currentBase2Index)
                {
                    switch (upgradeLevelIcons[9].upgradeLevel)
                    {
                        case -1:
                            break;

                        case 0:
                            upgradeLevelIcons[9].upgradeIcon1.SetActive(true);
                            base2tray.sprite = base2trayChanges[0];
                            upgradeBase2.sprite = base2trayChanges[0];
                            for (int k = 0; k < base2completeDish.Length; k++)
                            {
                                base2completeDish[k].sprite = base2completeDishChanges[0];
                                base2single[k].sprite = base2singleSprites[0];
                            }
                            break;

                        case 1:
                            upgradeLevelIcons[9].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[9].upgradeIcon2.SetActive(true);
                            base2tray.sprite = base2trayChanges[1];
                            upgradeBase2.sprite = base2trayChanges[1];
                            for (int k = 0; k < base2completeDish.Length; k++)
                            {
                                base2completeDish[k].sprite = base2completeDishChanges[1];
                                base2single[k].sprite = base2singleSprites[1];
                            }
                            break;
                        case 2:
                            upgradeLevelIcons[9].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[9].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[9].upgradeIcon3.SetActive(true);
                            base2tray.sprite = base2trayChanges[2];
                            upgradeBase2.sprite = base2trayChanges[2];
                            for (int k = 0; k < base2completeDish.Length; k++)
                            {
                                base2completeDish[k].sprite = base2completeDishChanges[2];
                                base2single[k].sprite = base2singleSprites[2];
                            }
                            break;
                    }

                }
                else
                {

                }
                break;
            #endregion

            #region foodwarmer
            case 10:
                currentFWIndex = GameConstants.GetCityData("foodWarmer_", storingIndex);

                CoinsRequired[10].text = upgradeData.foodWarmerUpgradeBuyingDetails[currentFWIndex].costPrice.ToString();

                CurrentPortions[5].text = (currentFWIndex + 1).ToString();
                NextPortions[5].text = (currentFWIndex + 2).ToString();

                upgradeLevelIcons[10].upgradeLevel = currentFWIndex - 1;

                if (upgradeLevelIcons[10].upgradeLevel < currentFWIndex)
                {
                    switch (upgradeLevelIcons[10].upgradeLevel)
                    {
                        case -1:
                            if (GameConstants.GetCityData("foodWarmerUnlocked_", storingIndex) == 1)
                            {
                                foodWarmerUpgrade[0].SetActive(true);
                                foodWarmerUnlocked.SetActive(true);
                            }
                            break;

                        case 0:
                            upgradeLevelIcons[10].upgradeIcon1.SetActive(true);

                            foodWarmerUnlocked.SetActive(true);

                            foodWarmerUpgrade[0].SetActive(true);
                            foodWarmerUpgrade[1].SetActive(true);

                            upgradeFoodWarmerIcon[0].SetActive(false);
                            upgradeFoodWarmerIcon[1].SetActive(true);
                            break;

                        case 1:
                            upgradeLevelIcons[10].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[10].upgradeIcon2.SetActive(true);

                            foodWarmerUnlocked.SetActive(true);

                            foodWarmerUpgrade[0].SetActive(true);
                            foodWarmerUpgrade[1].SetActive(true);
                            foodWarmerUpgrade[2].SetActive(true);

                            upgradeFoodWarmerIcon[0].SetActive(false);
                            upgradeFoodWarmerIcon[1].SetActive(false);
                            upgradeFoodWarmerIcon[2].SetActive(true);
                            break;
                        case 2:
                            upgradeLevelIcons[10].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[10].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[10].upgradeIcon3.SetActive(true);

                            foodWarmerUnlocked.SetActive(true);

                            foodWarmerUpgrade[0].SetActive(true);
                            foodWarmerUpgrade[1].SetActive(true);
                            foodWarmerUpgrade[2].SetActive(true);
                            foodWarmerUpgrade[3].SetActive(true);

                            upgradeFoodWarmerIcon[0].SetActive(false);
                            upgradeFoodWarmerIcon[1].SetActive(false);
                            upgradeFoodWarmerIcon[2].SetActive(false);
                            upgradeFoodWarmerIcon[3].SetActive(true);
                            break;
                    }

                }
                else
                {

                }
                break;
            #endregion

            #region topping1
            case 11:
                currentTopping1Index = GameConstants.GetCityData("topping1_", storingIndex);
                topping1CurrentPrice = upgradeData.topping1UpgradeData[currentTopping1Index].itemPrice;

                CoinsRequired[11].text = upgradeData.topping1UpgradeBuyingDetails[currentTopping1Index].costPrice.ToString();

                CurrentPrice[6].text = upgradeData.topping1UpgradeData[currentTopping1Index].itemPrice.ToString();
                NextPrice[6].text = upgradeData.topping1UpgradeData[currentTopping1Index + 1].itemPrice.ToString();

                upgradeLevelIcons[11].upgradeLevel = currentTopping1Index - 1;

                if (upgradeLevelIcons[11].upgradeLevel < currentTopping1Index)
                {
                    switch (upgradeLevelIcons[11].upgradeLevel)
                    {
                        case -1:
                            if (GameConstants.GetCityData("topping1unlocked_", storingIndex) == 1)
                                toppingsUnlocked[0].SetActive(true);
                            break;

                        case 0:
                            upgradeLevelIcons[11].upgradeIcon1.SetActive(true);
                            toppingsUnlocked[0].SetActive(true);

                            break;

                        case 1:
                            upgradeLevelIcons[11].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[11].upgradeIcon2.SetActive(true);
                            toppingsUnlocked[0].SetActive(true);

                            break;
                        case 2:
                            upgradeLevelIcons[11].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[11].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[11].upgradeIcon3.SetActive(true);
                            toppingsUnlocked[0].SetActive(true);

                            break;
                    }

                }
                else
                {

                }

                break;
            #endregion

            #region topping2
            case 12:
                currentTopping2Index = GameConstants.GetCityData("topping2_", storingIndex);
                topping2CurrentPrice = upgradeData.topping2UpgradeData[currentTopping2Index].itemPrice;

                CoinsRequired[12].text = upgradeData.topping2UpgradeBuyingDetails[currentTopping2Index].costPrice.ToString();

                CurrentPrice[7].text = upgradeData.topping2UpgradeData[currentTopping2Index].itemPrice.ToString();
                NextPrice[7].text = upgradeData.topping2UpgradeData[currentTopping2Index + 1].itemPrice.ToString();

                upgradeLevelIcons[12].upgradeLevel = currentTopping2Index - 1;

                if (upgradeLevelIcons[12].upgradeLevel < currentTopping2Index)
                {
                    switch (upgradeLevelIcons[12].upgradeLevel)
                    {
                        case -1:
                            if (GameConstants.GetCityData("topping2unlocked_", storingIndex) == 1)
                                toppingsUnlocked[1].SetActive(true);
                            break;

                        case 0:
                            upgradeLevelIcons[12].upgradeIcon1.SetActive(true);
                            toppingsUnlocked[1].SetActive(true);
                            break;

                        case 1:
                            upgradeLevelIcons[12].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[12].upgradeIcon2.SetActive(true);
                            toppingsUnlocked[1].SetActive(true);
                            break;
                        case 2:
                            upgradeLevelIcons[12].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[12].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[12].upgradeIcon3.SetActive(true);
                            toppingsUnlocked[1].SetActive(true);
                            break;
                    }

                }
                else
                {

                }

                break;
            #endregion

            #region topping3
            case 13:
                currentTopping3Index = GameConstants.GetCityData("topping3_", storingIndex);
                topping3CurrentPrice = upgradeData.topping3UpgradeData[currentTopping3Index].itemPrice;

                CoinsRequired[13].text = upgradeData.topping3UpgradeBuyingDetails[currentTopping3Index].costPrice.ToString();

                CurrentPrice[8].text = upgradeData.topping3UpgradeData[currentTopping3Index].itemPrice.ToString();
                NextPrice[8].text = upgradeData.beverageUpgradeData[currentTopping3Index + 1].itemPrice.ToString();

                upgradeLevelIcons[13].upgradeLevel = currentTopping3Index - 1;

                if (upgradeLevelIcons[13].upgradeLevel < currentTopping3Index)
                {
                    switch (upgradeLevelIcons[13].upgradeLevel)
                    {
                        case -1:
                            if (GameConstants.GetCityData("topping3unlocked_", storingIndex) == 1)
                                toppingsUnlocked[2].SetActive(true);
                            break;

                        case 0:
                            upgradeLevelIcons[13].upgradeIcon1.SetActive(true);
                            toppingsUnlocked[2].SetActive(true);
                            break;

                        case 1:
                            upgradeLevelIcons[13].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[13].upgradeIcon2.SetActive(true);
                            toppingsUnlocked[2].SetActive(true);
                            break;
                        case 2:
                            upgradeLevelIcons[13].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[13].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[13].upgradeIcon3.SetActive(true);
                            toppingsUnlocked[2].SetActive(true);
                            break;
                    }

                }
                else
                {

                }

                break;
            #endregion

            #region fryer
            case 14:
                currentFP3Index = GameConstants.GetCityData("fryPan3_", storingIndex);
                fryPan3cookTime = upgradeData.dish3UpgradeData[currentFP1Index].cookTime;

                CoinsRequired[14].text = upgradeData.fryPan3UpgradeBuyingDetails[currentFP3Index].costPrice.ToString();
                Diamondsrequired[8].text = upgradeData.fryPan3UpgradeBuyingDetails[currentFP3Index].Diamonds.ToString();

                CurrentPortions[6].text = (currentFP3Index + 1).ToString();
                if (currentFP3Index == 0)
                {
                    NextPortions[6].text = (currentFP3Index + 1).ToString();
                }
                else
                    NextPortions[6].text = (currentFP3Index + 2).ToString();

                CurrentPrepTime[3].text = upgradeData.dish3UpgradeData[currentFP3Index].cookTime.ToString();
                NextPrepTime[3].text = upgradeData.dish3UpgradeData[currentFP3Index + 1].cookTime.ToString();

                upgradeLevelIcons[14].upgradeLevel = currentFP3Index - 1;

                if (upgradeLevelIcons[14].upgradeLevel < currentFP3Index)
                {
                    switch (upgradeLevelIcons[14].upgradeLevel)
                    {
                        case -1:
                            if (GameConstants.GetCityData("dish3Unlocked_", storingIndex) == 1)
                                dish3Unlocked.SetActive(true);
                            break;

                        case 0:
                            upgradeLevelIcons[14].upgradeIcon1.SetActive(true);
                            dish3Unlocked.SetActive(true);
                            break;

                        case 1:
                            upgradeLevelIcons[14].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[14].upgradeIcon2.SetActive(true);
                            dish3Unlocked.SetActive(true);
                            break;
                        case 2:
                            upgradeLevelIcons[14].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[14].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[14].upgradeIcon3.SetActive(true);
                            dish3Unlocked.SetActive(true);
                            break;
                    }

                }

                else
                {

                }

                break;
            #endregion

            #region dish3
            case 15:
                currentDish3Index = GameConstants.GetCityData("dish3_", storingIndex);
                dish3CurrentPrice = upgradeData.dish3UpgradeData[currentDish3Index].itemPrice;

                CoinsRequired[15].text = upgradeData.dish3UpgradeBuyingDetails[currentDish3Index].costPrice.ToString();
                Diamondsrequired[9].text = upgradeData.dish3UpgradeBuyingDetails[currentDish3Index].Diamonds.ToString();

                CurrentPrice[3].text = upgradeData.dish3UpgradeData[currentDish3Index].itemPrice.ToString();
                NextPrice[3].text = upgradeData.dish3UpgradeData[currentDish3Index + 1].itemPrice.ToString();

                upgradeLevelIcons[15].upgradeLevel = currentDish3Index - 1;

                if (upgradeLevelIcons[15].upgradeLevel < currentDish3Index)
                {
                    switch (upgradeLevelIcons[15].upgradeLevel)
                    {
                        case -1:
                            break;

                        case 0:
                            upgradeLevelIcons[15].upgradeIcon1.SetActive(true);
                            break;

                        case 1:
                            upgradeLevelIcons[15].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[15].upgradeIcon2.SetActive(true);
                            break;
                        case 2:
                            upgradeLevelIcons[15].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[15].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[15].upgradeIcon3.SetActive(true);
                            break;
                    }

                }
                else
                {

                }
                break;
            #endregion

            #region splDish
            case 16:
                currentSplDishIndex = GameConstants.GetCityData("fryPan3_", storingIndex);

                Diamondsrequired[10].text = upgradeData.splDishUpgradeBuyingDetails[currentSplDishIndex].Diamonds.ToString();

                CurrentPrice[9].text = upgradeData.splDishUpgradeData[currentSplDishIndex].itemPrice.ToString();
                NextPrice[9].text = upgradeData.splDishUpgradeData[currentSplDishIndex + 1].itemPrice.ToString();

                upgradeLevelIcons[16].upgradeLevel = currentSplDishIndex - 1;

                if (upgradeLevelIcons[16].upgradeLevel < currentSplDishIndex)
                {
                    switch (upgradeLevelIcons[16].upgradeLevel)
                    {
                        case -1:
                            if (GameConstants.GetCityData("splDishUnlocked_", storingIndex) == 1)
                            {
                                upgradeLevelIcons[16].upgradeIcon1.SetActive(true);
                                splDishUnlocked.SetActive(true);
                                IngredientController.instance.EnablePattice();
                            }
                            break;

                        case 0:
                            upgradeLevelIcons[16].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[16].upgradeIcon2.SetActive(true);
                            splDishUnlocked.SetActive(true);
                            IngredientController.instance.EnablePattice();
                            break;

                        case 1:
                            upgradeLevelIcons[16].upgradeIcon1.SetActive(true);
                            upgradeLevelIcons[16].upgradeIcon2.SetActive(true);
                            upgradeLevelIcons[16].upgradeIcon3.SetActive(true);
                            splDishUnlocked.SetActive(true);
                            IngredientController.instance.EnablePattice();
                            break;

                        case 2:
                            break;
                    }
                    
                }
                break;
                #endregion
        }

        if (IngredientManager.instance != null)
            IngredientManager.instance.CookingTime();
    }

    public void upgradePopUpDetails(string upgradeName)
    {
        AudioManager.instance.PlaySoud("btn");
        switch (upgradeName)
        {
            case "beverageMachine":
                {
                    selectedOption[0].SetActive(true);
                    displayDetails[0].SetActive(true);
                    for (int i = 1; i < selectedOption.Length; i++)
                    {
                        selectedOption[i].SetActive(false);
                        displayDetails[i].SetActive(false);
                    }
                    for (int j = 0; j < lockedDetails.Length; j++)
                    {
                        lockedDetails[j].SetActive(false);
                    }
                    break;
                }


            case "bevereage":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 1)
                        {
                            selectedOption[1].SetActive(true);
                            displayDetails[1].SetActive(true);
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    for (int j = 0; j < lockedDetails.Length; j++)
                    {
                        lockedDetails[j].SetActive(false);
                    }
                    break;
                }

            case "fryPan1":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 2)
                        {
                            selectedOption[2].SetActive(true);
                            displayDetails[2].SetActive(true);
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    for (int j = 0; j < lockedDetails.Length; j++)
                    {
                        lockedDetails[j].SetActive(false);
                    }
                    break;
                }

            case "dish1":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 3)
                        {
                            selectedOption[3].SetActive(true);
                            displayDetails[3].SetActive(true);
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    for (int j = 0; j < lockedDetails.Length; j++)
                    {
                        lockedDetails[j].SetActive(false);
                    }
                    break;
                }

            case "tableTop1":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 4)
                        {
                            selectedOption[4].SetActive(true);
                            displayDetails[4].SetActive(true);
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    for (int j = 0; j < lockedDetails.Length; j++)
                    {
                        lockedDetails[j].SetActive(false);
                    }
                    break;
                }

            case "dish1Base":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 5)
                        {
                            selectedOption[5].SetActive(true);
                            displayDetails[5].SetActive(true);
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    for (int j = 0; j < lockedDetails.Length; j++)
                    {
                        lockedDetails[j].SetActive(false);
                    }
                    break;
                }

            case "fryPan2":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 6)
                        {
                            selectedOption[6].SetActive(true);
                            if (GameConstants.GetCityData("dish2Unlocked_", storingIndex) == 1)
                            {
                                displayDetails[6].SetActive(true);
                                lockedSelectBtns[0].SetActive(false);
                            }
                            else
                            {
                                for (int j = 0; j < lockedDetails.Length; j++)
                                {
                                    if (j == 0)
                                        lockedDetails[0].SetActive(true);
                                    else
                                        lockedDetails[j].SetActive(false);
                                }

                            }
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    break;
                }


            case "dish2":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 7)
                        {
                            selectedOption[7].SetActive(true);
                            if (GameConstants.GetCityData("dish2Unlocked_", storingIndex) == 1)
                            {
                                displayDetails[7].SetActive(true);
                                lockedSelectBtns[1].SetActive(false);
                            }
                            else
                            {
                                for (int j = 0; j < lockedDetails.Length; j++)
                                {
                                    if (j == 1)
                                        lockedDetails[1].SetActive(true);
                                    else
                                        lockedDetails[j].SetActive(false);
                                }

                            }
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    break;
                }

            case "tableTop2":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 8)
                        {
                            selectedOption[8].SetActive(true);
                            if (GameConstants.GetCityData("dish2Unlocked_", storingIndex) == 1)
                            {
                                displayDetails[8].SetActive(true);
                                lockedSelectBtns[2].SetActive(false);
                            }
                            else
                            {
                                for (int j = 0; j < lockedDetails.Length; j++)
                                {
                                    if (j == 2)
                                        lockedDetails[2].SetActive(true);
                                    else
                                        lockedDetails[j].SetActive(false);
                                }

                            }
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }
                    }
                    break;
                }

            case "dish2Base":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 9)
                        {
                            selectedOption[9].SetActive(true);
                            if (GameConstants.GetCityData("dish2Unlocked_", storingIndex) == 1)
                            {
                                displayDetails[9].SetActive(true);
                                lockedSelectBtns[3].SetActive(false);
                            }
                            else
                            {
                                for (int j = 0; j < lockedDetails.Length; j++)
                                {
                                    if (j == 3)
                                        lockedDetails[3].SetActive(true);
                                    else
                                        lockedDetails[j].SetActive(false);
                                }

                            }
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    break;
                }

            case "foodWarmer":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 10)
                        {
                            selectedOption[10].SetActive(true);
                            if (GameConstants.GetCityData("foodWarmerUnlocked_", storingIndex) == 1)
                            {
                                displayDetails[10].SetActive(true);
                                lockedSelectBtns[4].SetActive(false);
                            }
                            else
                            {
                                for (int j = 0; j < lockedDetails.Length; j++)
                                {
                                    if (j == 4)
                                        lockedDetails[4].SetActive(true);
                                    else
                                        lockedDetails[j].SetActive(false);
                                }

                            }
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    break;
                }

            case "topping1":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 11)
                        {
                            selectedOption[11].SetActive(true);
                            if (GameConstants.GetCityData("topping1unlocked_", storingIndex) == 1)
                            {
                                displayDetails[11].SetActive(true);
                                lockedSelectBtns[5].SetActive(false);
                            }
                            else
                            {
                                for (int j = 0; j < lockedDetails.Length; j++)
                                {
                                    if (j == 5)
                                        lockedDetails[5].SetActive(true);
                                    else
                                        lockedDetails[j].SetActive(false);
                                }

                            }
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    break;
                }

            case "topping2":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 12)
                        {
                            selectedOption[12].SetActive(true);
                            if (GameConstants.GetCityData("topping2unlocked_", storingIndex) == 1)
                            {
                                displayDetails[12].SetActive(true);
                                lockedSelectBtns[6].SetActive(false);
                            }
                            else
                            {
                                for (int j = 0; j < lockedDetails.Length; j++)
                                {
                                    if (j == 6)
                                        lockedDetails[6].SetActive(true);
                                    else
                                        lockedDetails[j].SetActive(false);
                                }

                            }
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    break;
                }

            case "topping3":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 13)
                        {
                            selectedOption[13].SetActive(true);
                            if (GameConstants.GetCityData("topping3unlocked_", storingIndex) == 1)
                            {
                                displayDetails[13].SetActive(true);
                                lockedSelectBtns[7].SetActive(false);
                            }
                            else
                            {
                                for (int j = 0; j < lockedDetails.Length; j++)
                                {
                                    if (j == 7)
                                        lockedDetails[7].SetActive(true);
                                    else
                                        lockedDetails[j].SetActive(false);
                                }

                            }
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    break;
                }

            case "fryPan3":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 14)
                        {
                            selectedOption[14].SetActive(true);
                            if (GameConstants.GetCityData("dish3Unlocked_", storingIndex) == 1)
                            {
                                displayDetails[14].SetActive(true);
                                lockedSelectBtns[8].SetActive(false);
                            }
                            else
                            {
                                for (int j = 0; j < lockedDetails.Length; j++)
                                {
                                    if (j == 8)
                                        lockedDetails[8].SetActive(true);
                                    else
                                        lockedDetails[j].SetActive(false);
                                }

                            }
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }
                    }
                    break;
                }

            case "dish3":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 15)
                        {
                            selectedOption[15].SetActive(true);
                            if (GameConstants.GetCityData("dish3Unlocked_", storingIndex) == 1)
                            {
                                displayDetails[15].SetActive(true);
                                lockedSelectBtns[9].SetActive(false);
                            }
                            else
                            {
                                for (int j = 0; j < lockedDetails.Length; j++)
                                {
                                    if (j == 9)
                                        lockedDetails[9].SetActive(true);
                                    else
                                        lockedDetails[j].SetActive(false);
                                }

                            }
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    break;
                }

            case "splDish":
                {
                    for (int i = 0; i < selectedOption.Length; i++)
                    {
                        if (i == 16)
                        {
                            selectedOption[16].SetActive(true);
                            if (GameConstants.GetCityData("splDishUnlocked_", storingIndex) == 1)
                            {
                                displayDetails[16].SetActive(true);
                                lockedSelectBtns[10].SetActive(false);
                            }
                            else
                            {
                                for (int j = 0; j < lockedDetails.Length; j++)
                                {
                                    if (j == 10)
                                        lockedDetails[10].SetActive(true);
                                    else
                                        lockedDetails[j].SetActive(false);
                                }

                            }
                        }
                        else
                        {
                            selectedOption[i].SetActive(false);
                            displayDetails[i].SetActive(false);
                        }

                    }
                    break;
                }
        }
    }

    public void buyButtonClicked(int btnIndex)
    {
        switch (btnIndex)
        {

            //current index will be increased  in upgrade system and then call intialize,upgradeInfo and upgrade details
            #region Beverage Machine
            case 0:
                Debug.Log("Inside buy beverage machine upgrade switch case!");

                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[0].text) &&
                    GameController.instance.totalDiamonds >= int.Parse(Diamondsrequired[0].text))
                {

                    GameController.instance.totalCoins -=
                        upgradeData.beverageMachineUpgradeBuyingDetails[currentBevMacIndex].costPrice;

                    GameController.instance.totalDiamonds -=
                        upgradeData.beverageMachineUpgradeBuyingDetails[currentBevMacIndex].Diamonds;

                    GameController.instance.xpEarned += upgradeData.beverageMachineUpgradeBuyingDetails[currentBevMacIndex].xpEarned;

                    if (currentBevMacIndex <= 2)
                    {
                        GameConstants.SetCityData("beverageMachine_", storingIndex, ++currentBevMacIndex);
                        updateUpgrades(0);
                    goto updateGameConstant;
                    }
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region Beverage
            case 1:
                Debug.Log("Inside buy beverage upgrade switch case!");

                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[1].text)
                    && GameController.instance.totalDiamonds >= int.Parse(Diamondsrequired[1].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.beverageUpgradeBuyingDetails[currentBevIndex].costPrice;

                    GameController.instance.totalDiamonds -=
                        upgradeData.beverageUpgradeBuyingDetails[currentBevIndex].Diamonds;

                    GameController.instance.xpEarned += upgradeData.beverageUpgradeBuyingDetails[currentBevIndex].xpEarned;

                    if (currentBevIndex <= 2)
                    {
                        GameConstants.SetCityData("beverage_", storingIndex, ++currentBevIndex);
                        updateUpgrades(1);
                    goto updateGameConstant;
                    }

                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region frypan1
            case 2:
                Debug.Log("Inside buy fry pan 1 upgrade switch case!");

                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[2].text)
                    && GameController.instance.totalDiamonds >= int.Parse(Diamondsrequired[2].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.fryPan1UpgradeBuyingDetails[currentFP1Index].costPrice;

                    GameController.instance.totalDiamonds -=
                        upgradeData.fryPan1UpgradeBuyingDetails[currentFP1Index].Diamonds;

                    GameController.instance.xpEarned += upgradeData.fryPan1UpgradeBuyingDetails[currentFP1Index].xpEarned;

                    if (currentFP1Index <= 2)
                    {
                        GameConstants.SetCityData("fryPan1_", storingIndex, ++currentFP1Index);
                        updateUpgrades(2);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region  dish1
            case 3:
                Debug.Log("Inside buy base 1 upgrade switch case!");

                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[3].text)
                    && GameController.instance.totalDiamonds >= int.Parse(Diamondsrequired[3].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.dish1UpgradeBuyingDetails[currentDish1Index].costPrice;

                    GameController.instance.totalDiamonds -=
                        upgradeData.dish1UpgradeBuyingDetails[currentDish1Index].Diamonds;

                    GameController.instance.xpEarned += upgradeData.dish1UpgradeBuyingDetails[currentDish1Index].xpEarned;

                    if (currentDish1Index <= 2)
                    {
                        GameConstants.SetCityData("dish1_", storingIndex, ++currentDish1Index);
                        updateUpgrades(3);
                    }

                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region tabletop1
            case 4:

                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[4].text))
                {
                    Debug.Log("Inside buy table top 1 upgrade switch case!");
                    GameController.instance.totalCoins -=
                        upgradeData.tableTop1UpgradeBuyingDetails[currentTT1Index].costPrice;

                    GameController.instance.xpEarned += upgradeData.tableTop1UpgradeBuyingDetails[currentTT1Index].xpEarned;

                    currentTT1Index += 1;

                    if (currentTT1Index <= 2)
                    {
                        GameConstants.SetCityData("tableTop1_", storingIndex, currentTT1Index);
                        updateUpgrades(4);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region dish1Base
            case 5:
                Debug.Log("Inside buy base 1 upgrade switch case!");
                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[5].text)
                     && GameController.instance.totalDiamonds >= int.Parse(Diamondsrequired[4].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.dish1BaseUpgradeBuyingDetails[currentBase1Index].costPrice;

                    GameController.instance.totalDiamonds -=
                        upgradeData.dish1BaseUpgradeBuyingDetails[currentBase1Index].Diamonds;

                    GameController.instance.xpEarned += upgradeData.dish1BaseUpgradeBuyingDetails[currentBase1Index].xpEarned;

                    if (currentBase1Index <= 2)
                    {
                        GameConstants.SetCityData("base1_", storingIndex, ++currentBase1Index);
                        updateUpgrades(5);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region frypan2
            case 6:
                Debug.Log("Inside fry pan 2 switch case!");
                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[6].text)
                     && GameController.instance.totalDiamonds >= int.Parse(Diamondsrequired[5].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.fryPan2UpgradeBuyingDetails[currentFP2Index].costPrice;

                    GameController.instance.totalDiamonds -=
                        upgradeData.fryPan2UpgradeBuyingDetails[currentFP2Index].Diamonds;

                    GameController.instance.xpEarned += upgradeData.fryPan2UpgradeBuyingDetails[currentFP2Index].xpEarned;

                    if (currentFP2Index <= 2)
                    {
                        GameConstants.SetCityData("fryPan2_", storingIndex, ++currentFP2Index);
                        updateUpgrades(6);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region dish2
            case 7:
                Debug.Log("Inside buy dish 2 upgrade switch case!");
                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[7].text)
                     && GameController.instance.totalDiamonds >= int.Parse(Diamondsrequired[6].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.dish2UpgradeBuyingDetails[currentDish2Index].costPrice;

                    GameController.instance.totalDiamonds -=
                        upgradeData.dish2UpgradeBuyingDetails[currentDish2Index].Diamonds;

                    GameController.instance.xpEarned += upgradeData.dish2UpgradeBuyingDetails[currentDish2Index].xpEarned;

                    if (currentDish2Index <= 2)
                    {
                        GameConstants.SetCityData("dish2_", storingIndex, ++currentDish2Index);
                        updateUpgrades(7);
                    }

                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region tabletop2
            case 8:
                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[8].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.tableTop2UpgradeBuyingDetails[currentTT2Index].costPrice;

                    GameController.instance.xpEarned += upgradeData.tableTop2UpgradeBuyingDetails[currentTT2Index].xpEarned;

                    currentTT2Index += 1;
                    if (currentTT2Index <= 2)
                    {
                        Debug.Log("Inside buy table top 2 upgrade switch case!");
                        GameConstants.SetCityData("tableTop2_", storingIndex,currentTT2Index);
                        updateUpgrades(8);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region dish2Base
            case 9:
                Debug.Log("Inside buy base 2 upgrade switch case!");
                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[9].text)
                     && GameController.instance.totalDiamonds >= int.Parse(Diamondsrequired[7].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.dish2BaseUpgradeBuyingDetails[currentBase2Index].costPrice;

                    GameController.instance.totalDiamonds -=
                        upgradeData.dish2BaseUpgradeBuyingDetails[currentBase2Index].Diamonds;

                    GameController.instance.xpEarned += upgradeData.dish2BaseUpgradeBuyingDetails[currentBase2Index].xpEarned;

                    if (currentBase2Index <= 2)
                    {
                        GameConstants.SetCityData("base2_", storingIndex, ++currentBase2Index);
                        updateUpgrades(9);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region foodwarmer
            case 10:
                Debug.Log("Inside buy food warmer upgrade switch case!");
                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[10].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.foodWarmerUpgradeBuyingDetails[currentFWIndex].costPrice;

                    GameController.instance.xpEarned += upgradeData.foodWarmerUpgradeBuyingDetails[currentFWIndex].xpEarned;

                    if (currentFWIndex <= 2)
                    {
                        GameConstants.SetCityData("foodWarmer_", storingIndex, ++currentFWIndex);
                        updateUpgrades(10);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region topping1
            case 11:
                Debug.Log("Inside buy topping1 upgrade switch case!");
                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[11].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.topping1UpgradeBuyingDetails[currentTopping1Index].costPrice;

                    GameController.instance.xpEarned += upgradeData.topping1UpgradeBuyingDetails[currentTopping1Index].xpEarned;

                    if (currentTopping1Index <= 2)
                    {
                        GameConstants.SetCityData("topping1_", storingIndex, ++currentTopping1Index);
                        updateUpgrades(11);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region topping2
            case 12:
                Debug.Log("Inside buy topping 2 upgrade switch case!");
                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[12].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.topping2UpgradeBuyingDetails[currentTopping2Index].costPrice;

                    GameController.instance.xpEarned += upgradeData.topping2UpgradeBuyingDetails[currentTopping2Index].xpEarned;

                    if (currentTopping2Index <= 2)
                    {
                        GameConstants.SetCityData("topping2_", storingIndex, ++currentTopping2Index);
                        updateUpgrades(12);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region topping3
            case 13:
                Debug.Log("Inside buy topping 3 upgrade switch case!");
                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[13].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.topping3UpgradeBuyingDetails[currentTopping3Index].costPrice;

                    GameController.instance.xpEarned += upgradeData.topping3UpgradeBuyingDetails[currentTopping3Index].xpEarned;

                    if (currentTopping3Index <= 2)
                    {
                        GameConstants.SetCityData("topping3_", storingIndex, ++currentTopping3Index);
                        updateUpgrades(13);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region fryer
            case 14:
                Debug.Log("Inside buy fryer upgrade switch case!");
                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[14].text)
                     && GameController.instance.totalDiamonds >= int.Parse(Diamondsrequired[8].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.fryPan3UpgradeBuyingDetails[currentFP3Index].costPrice;

                    GameController.instance.totalDiamonds -=
                        upgradeData.fryPan3UpgradeBuyingDetails[currentFP3Index].Diamonds;

                    GameController.instance.xpEarned += upgradeData.fryPan3UpgradeBuyingDetails[currentFP3Index].xpEarned;

                    if (currentFP3Index <= 2)
                    {
                        GameConstants.SetCityData("fryPan3_", storingIndex, ++currentFP3Index);
                        updateUpgrades(14);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region dish3
            case 15:
                Debug.Log("Inside buy dish 3 upgrade switch case!");
                if (GameController.instance.totalCoins >= int.Parse(CoinsRequired[15].text)
                     && GameController.instance.totalDiamonds >= int.Parse(Diamondsrequired[9].text))
                {
                    GameController.instance.totalCoins -=
                        upgradeData.dish3UpgradeBuyingDetails[currentDish3Index].costPrice;

                    GameController.instance.totalDiamonds -=
                        upgradeData.dish3UpgradeBuyingDetails[currentDish3Index].Diamonds;

                    GameController.instance.xpEarned += upgradeData.dish3UpgradeBuyingDetails[currentDish3Index].xpEarned;

                    if (currentDish3Index <= 2)
                    {
                        GameConstants.SetCityData("dish3_", storingIndex, ++currentDish3Index);
                        updateUpgrades(15);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
            #endregion

            #region splDish
            case 16:
                Debug.Log("Inside buy special dish upgrade switch case!");
                if (GameController.instance.totalDiamonds >= int.Parse(Diamondsrequired[10].text))
                {
                    GameController.instance.totalDiamonds -= upgradeData.splDishUpgradeBuyingDetails[currentSplDishIndex].Diamonds;

                    GameController.instance.xpEarned += upgradeData.splDishUpgradeBuyingDetails[currentSplDishIndex].xpEarned;

                    if (currentSplDishIndex <= 2)
                    {
                        GameConstants.SetCityData("splDish_", storingIndex, ++currentSplDishIndex);
                        updateUpgrades(16);
                    }
                    goto updateGameConstant;
                }

                else
                {
                    notEnoughCoins.SetActive(true);
                }
                break;
                #endregion
        }

    updateGameConstant:
        GameController.instance.XpCalculation();
        GameConstants.SetTotalDiamonds(GameController.instance.totalDiamonds);
        GameConstants.SetTotalCoins(GameController.instance.totalCoins);
        LevelController.instance.totalCoinText.text = GameConstants.GetTotalCoins().ToString();
        LevelController.instance.totalDiamondText.text = GameConstants.GetTotalDiamonds().ToString();
        LevelController.instance.xpText.text = GameController.instance.xpEarned.ToString() + "/" + GameController.instance.xpLevelRequirement.ToString();
        LevelController.instance.xpLevelText.text = (GameController.instance.currentXpLevel + 1).ToString();
    }

    public void closeNotEnoughCoins()
    {
        notEnoughCoins.SetActive(false);
    }
}
