using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    public static LevelController instance;

    [Space]
    //Get data of city specific levels
    public LevelData cityLvlData;

    //stars achieved per level objects
    [System.Serializable]
    public class starsEarnedEnable
    {
        public GameObject star1;
        public GameObject star2;
        public GameObject star3;
    }

    [Header("Game object for stars")]
    public List<starsEarnedEnable> starsEarned;

    [Space]
    [Header("Game object for stars")]
    public Button[] enableButton;

    //Game objects for pop ups through out the game
    [Space]
    [Header("POPUP Game Objects")]

    public GameObject LevelGoalPopUp;
    public GameObject LevelPopUp;
    public GameObject UpgradePopUp;
    public GameObject MenuPopUp;
    public GameObject PausePopUp;
    public GameObject xpLevelPopUp;

    [Space]
    [Header("To enable/disable menu and pause buttons")]
    public Button MenupopBtn;
    public Button PausepopBtn;

    [SerializeField]
    int custNum;

    public Text goalCoins;
    public Text levelNum;


    [Space]
    [Header("Text displayed about earnings")]
    public Text totalCoinText;
    public Text totalDiamondText;
    public Text xpText;
    public Text xpLevelText;
    public Text coinEarnedText;
    public Text custCountText;

    public int coinsEarned;
    public int tipsEarned;
    public int targetCoins;
    [Space]
    public string earnedXpText;
    public string targetXpText;


    [Space]
    [Header("UI during game")]
    public GameObject coinStarPanel;
    public GameObject timerPanel;
    public GameObject custCountPanel;
    public GameObject coinPanel;
    public GameObject diamondPanel;

    [Space]
    [Header("To display stars earned during game")]
    public int starCount = 0;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    [Space]
    [Header("Timer related")]
    [SerializeField]
    public float levelTime;
    public Image timerFill;

    [Space]
    public Image coinFill;

    [Space]
    [Header("Positioning of customers")]
    public int custPos;
    public bool pos1;
    public bool pos2;
    public bool pos3;
    public bool pos4;
    public int range;
    public int custCount;
    public int custLeft;
    public int custLost;
    public bool[] seatFilled = { false, false, false, false };//do not change this
    public int delay;
    public int startingIndex;
    public bool startPositioning;
    public Vector3[] seat;

    [Space]
    [Header("Flags related to levels condition")]
    public bool LevelStarted;
    public bool levelEnded;
    public bool restartClicked;
    public bool endScreenShown;
    public bool tutChecked = false;
    public bool pauseTimers = false;

    [Space]
    public GameObject winConfetti;

    [Space]
    [Header("Tutorial game objects")]
    public GameObject[] globalTut;
    public GameObject[] prepTuts;
    public GameObject LockForTut;
    public int tutorialCounter;

    [Space]
    [Header("Coin game objects")]
    public GameObject[] earnedCoinsObj;

    private int currentLevel;
    public int activeLevel;
    private int activeScene;
    public int storingIndex;
    private string Index;

    [Space]
    [Header("Win/Lose screen")]

    public GameObject gameEndedPopUp;
    public GameObject LevelFailed;
    public GameObject LevelSuccess;
    public GameObject[] winScreenStar;

    public Text earnedCoinText;
    public Text tipsEarnedText;
    public Text bonusCoinsText;
    public Text totalCoinsEndText;
    public Text servedCustText;
    public Text lostCustText;

    public bool showXPLevelUp;
    public int currentXPLevel;
    public Text xpLevelRewardCoins;
    public Text xpLevelRewardDiamonds;
    public Text xpLevelUp;

    public GameObject[] levelButtons;

    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    public GameObject Gadi;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        Debug.Log("Screen height : " + Screen.height + " Screen width : " + Screen.width);
        float screenRatio =Mathf.Round(((float)Screen.width / (float)Screen.height)*100.0f)/100.0f;
        Debug.Log("Screen ratio: "+screenRatio);
        if(screenRatio == 1.67f)
        {
            Gadi.transform.localScale = new Vector3(0.52f, 0.22f, 1.0f);
            Gadi.GetComponent<RectTransform>().localPosition = new Vector3(Gadi.GetComponent<RectTransform>().localPosition.x, 
                                                                            73.0f, Gadi.GetComponent<RectTransform>().localPosition.z);

        }
        if (screenRatio == 1.78f)
        {
            if(Screen.width==1920)
            {
                Gadi.GetComponent<RectTransform>().localPosition = new Vector3(Gadi.GetComponent<RectTransform>().localPosition.x,
                                                                               -237.0f, Gadi.GetComponent<RectTransform>().localPosition.z);
            }
            else if(Screen.width==1280)
            {
                Gadi.GetComponent<RectTransform>().localPosition = new Vector3(Gadi.GetComponent<RectTransform>().localPosition.x,
                                                                                -58.0f, Gadi.GetComponent<RectTransform>().localPosition.z);

            }
            else
            {
                Gadi.GetComponent<RectTransform>().localPosition = new Vector3(Gadi.GetComponent<RectTransform>().localPosition.x,
                                                                               -417.0f, Gadi.GetComponent<RectTransform>().localPosition.z);
            }
            Gadi.transform.localScale = new Vector3(0.51f, 0.21f, 1.0f);


        }
        if (screenRatio == 2.0f)
        {
            Gadi.transform.localScale = new Vector3(0.5f, 0.19f, 1.0f);
            Gadi.GetComponent<RectTransform>().localPosition = new Vector3(Gadi.GetComponent<RectTransform>().localPosition.x, 
                                                                -272.0f, Gadi.GetComponent<RectTransform>().localPosition.z);

        }
        if (screenRatio == 2.06f)
        {
            Gadi.transform.localScale = new Vector3(0.5f, 0.185f, 1.0f);
            Gadi.GetComponent<RectTransform>().localPosition = new Vector3(Gadi.GetComponent<RectTransform>().localPosition.x, 
                                                                           -463.0f, Gadi.GetComponent<RectTransform>().localPosition.z);

        }

        delay = 10;

        //gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;

        //get index of active scene


        totalCoinText.text = GameConstants.GetTotalCoins().ToString();
        totalDiamondText.text = GameConstants.GetTotalDiamonds().ToString();
        earnedXpText = GameConstants.GetEarnedXP().ToString();
        targetXpText = GameController.instance.xpLevelRequirement.ToString();
        xpText.text = earnedXpText + "/" + targetXpText;
        xpLevelText.text = (GameConstants.GetXPCurrentLevel() + 1).ToString();
        currentXPLevel = GameConstants.GetXPCurrentLevel() + 1;

        levelTime = 180;

        startPositioning = false;
        activeScene = SceneManager.GetActiveScene().buildIndex;
        storingIndex = activeScene - 2;
        tutorialCounter = GameConstants.GetCityData("cookingTut_", activeLevel);

        switch (activeScene)
        {
            case 2:
                //filename = "";
                cityLvlData = Resources.Load<LevelData>("LevelData/LevelData_Mumbai");
                currentLevel = GameConstants.GetCityData("currentLevel_", storingIndex);
                break;
            case 3:
                //filename = "";
                cityLvlData = Resources.Load<LevelData>("LevelData/LevelData_");
                currentLevel = GameConstants.GetCityData("currentLevel_", storingIndex);
                break;
            case 4:
                //filename = "";
                cityLvlData = Resources.Load<LevelData>("LevelData/LevelData_");
                currentLevel = GameConstants.GetCityData("currentLevel_", storingIndex);
                break;
            case 5:
                //filename = "";
                cityLvlData = Resources.Load<LevelData>("LevelData/LevelData_");
                currentLevel = GameConstants.GetCityData("currentLevel_", storingIndex);
                break;
        }

        EnableStars();
        EnableLevelButton();

        if (GameConstants.GetGlobalTut() == 3)
        {
            GameConstants.SetGlobalTut(4);
            globalTut[0].SetActive(true);
        }
        //play audio
    }

    public void PauseGame()
    {
        if (!PausePopUp.activeSelf)
        {
            pauseTimers = true;
            startPositioning = false;
            StopCoroutine(CustomerPositioning());
            PausepopBtn.interactable = false;
            PausePopUp.SetActive(true);
            
        }
        else
        {
            CustomerController.instance.pauseWaitingTime = false;
            pauseTimers = false;
            StartCoroutine(LevelTimer());
            startPositioning = true;
            StartCoroutine(CustomerPositioning());
            PausePopUp.SetActive(false);
            PausepopBtn.interactable = true;
        }
    }


    public void RestartLevel()
    {
        if (PausePopUp.activeSelf)
        {
            PausePopUp.SetActive(false);
            LoadLevel(activeLevel);
        }
        else
        {
            gameEndedPopUp.SetActive(false);
            LoadLevel(activeLevel-1);
        }
    }


        /// <summary>
        /// To create customers and orders
        /// </summary>
        /// <param name="num">Accepts level number from level button clicked</param>
        public void LoadLevel(int num)
    {

        if (LevelPopUp.activeSelf)
        {
            AudioManager.instance.PlaySoud("btn");
        }
        source.Play(0);
        //Debug.Log("Inside Load Level");
        winConfetti.SetActive(false);
        if (GameConstants.GetCityData("cookingTut_", 0) == 0)
            LockForTut.SetActive(true);

        MenupopBtn.gameObject.SetActive(false);
        PausepopBtn.gameObject.SetActive(true);
        PausepopBtn.interactable = false;
        levelTime = 180;
        tutChecked = false;
        activeLevel = num;
        coinEarnedText.text = "000";
        coinsEarned = 0;
        custLost = 0;
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        MenupopBtn.interactable = false;
        custCountText.text = "0";
        coinPanel.SetActive(false);
        diamondPanel.SetActive(false);
        timerPanel.SetActive(true);
        custCountPanel.SetActive(true);
        coinStarPanel.SetActive(true);
        xpText.text = GameConstants.GetEarnedXP().ToString() + "/" + GameController.instance.xpLevelRequirement.ToString();
        xpLevelText.text = (GameController.instance.currentXpLevel + 1).ToString();

        LevelPopUp.SetActive(false);


        OrderController.instance.OrderCreation(num);
        //Debug.Log("After calling orderCreation");
        LevelStarted = true;

        StartCoroutine(goalPopupWait(num));

        pos1 = pos2 = pos3 = pos4 = false;

        for (int i = 0; i <= 3; i++)
        {
            seatFilled[i] = false;
        }
    }

    /// <summary>
    /// Coroutine to show target that should be achieved for each level
    /// </summary>
    /// <param name="num">Level number is passed so that it can be shown in pop up</param>
    /// <returns></returns>
    IEnumerator goalPopupWait(int num)
    {

        yield return new WaitForSeconds(0.6f);
        levelNum.text = "Level " + num;
        goalCoins.text = OrderController.instance.LevelM.Star1Score.ToString();
        LevelGoalPopUp.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        LevelGoalPopUp.SetActive(false);
        if (tutorialCounter == 3 && activeLevel == 4)
        {
            pauseTimers = true;
            LockForTut.SetActive(true);
            startPositioning = false;
            StopCoroutine(CustomerPositioning());
            ShowTutorial(21);
            Debug.Log("Inside tutorial 3");
            tutChecked = false;
            
            
        }
        if (tutorialCounter == 5 && activeLevel == 8)
        {
            LockForTut.SetActive(true);
            ShowTutorial(30);
            tutChecked = false;
        }
        if (tutorialCounter == 6 && activeLevel == 11)
        {
            LockForTut.SetActive(true);
            ShowTutorial(32);
            tutChecked = false;
        }
        if (tutorialCounter == 7 && activeLevel == 12)
        {
            LockForTut.SetActive(true);
            ShowTutorial(34);
            tutChecked = false;
        }
        if (tutorialCounter == 8 && activeLevel == 18)
        {
            LockForTut.SetActive(true);
            pauseTimers = true;
        }
        if (GameConstants.GetGlobalTut() == 5 && activeLevel == 21)
        {
            pauseTimers = true;
            tutChecked = false;
            ShowGlobalTut(40);
            startPositioning = false;
        }
    }

    /// <summary>
    /// Function to actually start the level
    /// </summary>
    public void StartLevel()
    {

        levelTime = 180;
        //PausepopBtn.interactable = true;
        if ((tutorialCounter == 3 && activeLevel == 4) || (GameConstants.GetGlobalTut() == 5 && activeLevel == 21))
        {
            for (int i = 0; i <= UpgradeSystem.instance.currentBevMacIndex; i++)
                IngredientController.instance.chaiAnim[i].SetActive(true);
            Debug.Log("Not starting the Level");

        }

        else
        {
            //Debug.Log("Starting the Level");
            pauseTimers = false;
            startPositioning = true;

            StartCoroutine(LevelTimer());

            if (tutorialCounter > 1)
            {
                IngredientController.instance.IngredientClicked("beverage");
            }
            else
                for (int i = 0; i <= UpgradeSystem.instance.currentBevMacIndex; i++)
                    IngredientController.instance.chaiAnim[i].SetActive(true);
            PausepopBtn.interactable = true;

        }

        restartClicked = false;
        startingIndex = 0;
        custLeft = custCount = OrderController.CustomerOrder.Count;
        targetCoins = OrderController.instance.TargetCoins;
        custCountText.text = custCount.ToString();

    }

    /// <summary>
    /// Coroutine for decreasing level time
    /// </summary>
    /// <returns></returns>
    public IEnumerator LevelTimer()
    {
        while (levelTime > 0 && !pauseTimers)
        {
            levelTime -= Time.deltaTime;

            timerFill.fillAmount = levelTime / 180.0f;
            yield return 0;
        }

    }

    /// <summary>
    /// Corountine for positioning customers
    /// </summary>
    /// <returns></returns>
    public IEnumerator CustomerPositioning()
    {
        yield return new WaitForSeconds(0.15f);
        try
        {
            if (custCount > 0 && levelTime > 0 && (tutorialCounter != 3 || tutorialCounter != 4))
            {
                for (int i = startingIndex; i < OrderController.instance.LevelM.CustomerCount; i++)
                {
                    if (!pos1 || !pos2 || !pos3 || !pos4)
                    {
                        switch (activeLevel)
                        {
                            case 1:
                                if (GameConstants.GetCityData("cookingTut_", storingIndex) == 0)
                                    custPos = 1;

                                else if (GameConstants.GetCityData("cookingTut_", storingIndex) == 1)
                                {
                                    tutChecked = false;
                                    custPos = 2;
                                }

                                else
                                    custPos = Random.Range(0, 4);
                                break;

                            case 18:
                                if (GameConstants.GetCityData("cookingTut_", storingIndex) == 8)
                                {
                                    tutChecked = false;
                                    custPos = 2;
                                }
                                else
                                    custPos = Random.Range(0, 4);
                                break;

                            default:
                                custPos = Random.Range(0, 4);
                                break;
                        }



                        startPositioning = false;

                        string custName;

                        if (!seatFilled[custPos])
                        {
                            custNum = i + 1;
                            //Debug.Log("**********Value of i" + i);
                            //Debug.Log("**********Value of custNum" + custNum);
                            custName = "Customer_" + custNum;
                            //Debug.Log("This is giving error*********" + custName);
                            if (!GameObject.Find(custName).GetComponent<CustomerController>().isOnSeat &&
                               !GameObject.Find(custName).GetComponent<CustomerController>().isPositioning)
                            {
                                GameObject.Find(custName).GetComponent<CustomerController>().CustomerPosition(custPos);
                                GameObject.Find(custName).GetComponent<CustomerController>().destination = seat[custPos];
                                seatFilled[custPos] = true;
                                //OrderController.CustomerOrder[custNum - 1].seatNum = custPos;

                                switch (custPos)
                                {
                                    case 0:
                                        pos1 = true;
                                        break;
                                    case 1:
                                        pos2 = true;
                                        break;
                                    case 2:
                                        pos3 = true;
                                        break;
                                    case 3:
                                        pos4 = true;
                                        break;
                                }
                            }
                            else
                            {
                                if (tutChecked)
                                    startPositioning = true;
                            }
                        }
                        else
                        {
                            if (tutChecked)
                            {
                                startPositioning = true;
                            }
                            break;
                        }
                    }
                    else
                    {
                        //Debug.Log("All positions filled" + pos1 + " " + pos2 + " " + pos3 + " " + pos4);
                        if (tutChecked)
                            startPositioning = false;
                    }
                }

            }
        }
        catch (System.Exception e)

        {
            startingIndex++;
            startPositioning = true;

        }
        yield return new WaitForSeconds(delay);
        yield return new WaitForSeconds(0.5f);
        if (!startPositioning && levelTime > 0)
            startPositioning = true;

    }

    /// <summary>
    /// Constantly check for customer positioning
    /// if level time is up check whether player has completed the target coins or not
    /// </summary>
    private void Update()
    {
        if (startPositioning && levelTime > 0)
        {
            range = Random.Range(2, 100);
            if (range < delay)
            {
                if (tutorialCounter == 3 && activeLevel == 4)
                    StopCoroutine(CustomerPositioning());
                else if (globalTut[3].activeSelf)
                    StopCoroutine(CustomerPositioning());
                else
                    StartCoroutine(CustomerPositioning());
            }
        }

        //if (levelTime > 0 && (!pos1 || !pos2 || !pos3 || !pos4))
        //    startPositioning = true;

        if ((levelTime < 0 || custLeft == 0) & !levelEnded && LevelStarted)
        {
            levelEnded = true;
            LevelStarted = false;
            CalculationOfEarnings(-1);
        }

    }

    /// <summary>
    /// Enabling stars on level select pop up
    /// </summary>
    private void EnableStars()
    {
        for (int i = 0; i < 40; i++)
        {
            Index = "Level_" + storingIndex;

            int starNum = GameConstants.GetCityData(Index, i + 1);

            switch (starNum)
            {

                case 1:
                    starsEarned[i].star1.SetActive(true);
                    break;

                case 2:
                    starsEarned[i].star1.SetActive(true);
                    starsEarned[i].star2.SetActive(true);
                    break;

                case 3:
                    starsEarned[i].star1.SetActive(true);
                    starsEarned[i].star2.SetActive(true);
                    starsEarned[i].star3.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Enabling stars on level select pop up
    /// </summary>
    private void EnableLevelButton()
    {

        for (int i = 0; i < GameConstants.GetCityData("currentLevel_", storingIndex); i++)
        {
            enableButton[i].interactable = true;
        }
    }

    /// <summary>
    /// To enable coin object
    /// </summary>
    /// <param name="coinPosition"></param>
    public void ShowCoinsObject(int coinPosition)
    {
        switch (coinPosition)
        {
            case 0:
                earnedCoinsObj[0].SetActive(true);
                break;
            case 1:
                earnedCoinsObj[1].SetActive(true);
                break;
            case 2:
                earnedCoinsObj[2].SetActive(true);
                break;
            case 3:
                earnedCoinsObj[3].SetActive(true);
                break;
        }
    }

    /// <summary>
    /// This function is called when coins are collected 
    /// 1. To check how many stars are earned
    /// 2.Calculated coins earned
    /// 3.Calculate xp earned
    /// </summary>   
    /// /// <param name="coinPosition"></param>
    public void CalculationOfEarnings(int coinPosition)
    {
        //disable coins
        //mark seat as empty and start positioning
        switch (coinPosition)
        {
            case 0:
                pos1 = false;
                seatFilled[0] = false;
                earnedCoinsObj[0].GetComponent<OrderSettlement>().source.Play(0);
                earnedCoinsObj[0].GetComponent<OrderSettlement>().FinalSettlement();
                StartCoroutine(DelayForCoins(coinPosition));
                startPositioning = true;

                break;

            case 1:
                pos2 = false;
                seatFilled[1] = false;

                earnedCoinsObj[1].GetComponent<OrderSettlement>().source.Play(0);
                earnedCoinsObj[1].GetComponent<OrderSettlement>().FinalSettlement();
                StartCoroutine(DelayForCoins(coinPosition));
                startPositioning = true;

                if (tutorialCounter == 0)
                    ShowTutorial(8);
                break;

            case 2:
                pos3 = false;
                seatFilled[2] = false;
                earnedCoinsObj[2].GetComponent<OrderSettlement>().source.Play(0);
                earnedCoinsObj[2].GetComponent<OrderSettlement>().FinalSettlement();
                StartCoroutine(DelayForCoins(coinPosition));
                startPositioning = true;
                break;

            case 3:
                pos4 = false;
                seatFilled[3] = false;
                earnedCoinsObj[3].GetComponent<OrderSettlement>().source.Play(0);
                earnedCoinsObj[3].GetComponent<OrderSettlement>().FinalSettlement();
                StartCoroutine(DelayForCoins(coinPosition));
                startPositioning = true;
                break;

            default:
                for (int i = 0; i < 4; i++)
                {
                    if (earnedCoinsObj[i].activeSelf)
                    {
                        earnedCoinsObj[i].GetComponent<OrderSettlement>().FinalSettlement();
                        earnedCoinsObj[i].SetActive(false);
                        seatFilled[i] = false;
                        switch (i)
                        {
                            case 0:
                                pos1 = false;
                                break;
                            case 1:
                                pos2 = false;
                                break;
                            case 2:
                                pos3 = false;
                                break;
                            case 3:
                                pos4 = false;
                                break;
                        }
                    }

                }

                if (levelEnded)
                {
                    if (coinsEarned >= targetCoins)
                        winConfetti.SetActive(true);
                    StartCoroutine(GameEnded());

                }
                break;
        }

        coinFill.fillAmount = (float)coinsEarned / (float)OrderController.instance.LevelM.Star3Score;
        Debug.Log("*****Coin fill amount :" + coinFill.fillAmount);

        //enable stars once earned
        if (coinsEarned >= targetCoins && coinsEarned < OrderController.instance.LevelM.Star2Score)
        {
            Debug.Log("1st star");
            star1.SetActive(true);
            starCount = 1;
        }
        if (coinsEarned >= OrderController.instance.LevelM.Star2Score && coinsEarned < OrderController.instance.LevelM.Star3Score)
        {
            Debug.Log("2nd star");
            star1.SetActive(true);
            star2.SetActive(true);
            starCount = 2;
        }
        if (coinsEarned >= OrderController.instance.LevelM.Star3Score)
        {
            Debug.Log("3rd star");
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            starCount = 3;

        }

        //xp calculations
        earnedXpText = GameConstants.GetEarnedXP().ToString();
        targetXpText = GameController.instance.xpLevelRequirement.ToString();
        xpText.text = earnedXpText + "/" + targetXpText;
        xpLevelText.text = (GameController.instance.currentXpLevel + 1).ToString();
        //Debug.Log("********Earned Xp" + earnedXpText);
    }

    IEnumerator DelayForCoins(int coinPosition)
    {
        yield return new WaitForSeconds(0.25f);
        earnedCoinsObj[coinPosition].SetActive(false);
    }

    /// <summary>
    /// Coroutine to display Level End Pop Up with delay.
    /// </summary>
    /// <returns></returns>
    IEnumerator GameEnded()
    {
        source.Stop();
        //so that it's called only once
        if (endScreenShown)
        {
            yield break;
        }
        Debug.Log("Activate gameEndedPopUp");

        endScreenShown = true;
        EndGameCalculation();

        yield return new WaitForSeconds(1.5f);

        earnedCoinText.text = coinsEarned.ToString();
        tipsEarnedText.text = tipsEarned.ToString();
        totalCoinsEndText.text = GameConstants.GetTotalCoins().ToString();
        servedCustText.text = (OrderController.instance.LevelM.CustomerCount - custLeft).ToString();
        custLost += custLeft;
        lostCustText.text = custLost.ToString();
        if (coinsEarned < targetCoins)
        {
            LevelFailed.SetActive(true);
            LevelSuccess.SetActive(false);

        }
        else
        {
            if (tutorialCounter == 2)
                ShowTutorial(13);


            LevelSuccess.SetActive(true);
            LevelFailed.SetActive(false);

            for (int i = 0; i < starCount; i++)
                winScreenStar[i].SetActive(true);
            if (GameConstants.GetCityData(Index, activeLevel) == 0)
                bonusCoinsText.text = OrderController.instance.LevelM.RewardCoins.ToString();
        }

        if (currentXPLevel < (GameController.instance.currentXpLevel + 1))
        {
            xpLevelRewardCoins.text = GameController.instance.scoreDetails.xpLevelDetails[GameController.instance.currentXpLevel].rewardCoins.ToString();
            xpLevelRewardDiamonds.text = GameController.instance.scoreDetails.xpLevelDetails[GameController.instance.currentXpLevel].rewardDiamonds.ToString();
            xpLevelUp.text = "You have reached level " + (currentLevel + 1);

            currentXPLevel = GameController.instance.currentXpLevel + 1;

            xpLevelPopUp.SetActive(true);
            if (GameConstants.GetGlobalTut() == 4)
            {
                ShowGlobalTut(18);
            }
        }
        else
        {
            gameEndedPopUp.SetActive(true);
        }

        Debug.Log("Activate gameEndedPopUp");

    }

    /// <summary>
    /// Calculates final earnings of coins,diamond and XP.
    /// Enable rewards if applicable
    /// </summary>
    public void EndGameCalculation()
    {
        if (coinsEarned >= targetCoins)
        {
            Index = "Level_" + storingIndex;

            //Give reward if the level is cleared for the first time
            if (GameConstants.GetCityData(Index, activeLevel) == 0)
            {
                GameController.instance.totalCoins += OrderController.instance.LevelM.RewardCoins;
                GameConstants.SetTotalCoins(GameController.instance.totalCoins);
                GameController.instance.totalDiamonds += OrderController.instance.LevelM.RewardDiamonds;
                GameConstants.SetTotalDiamonds(GameController.instance.totalDiamonds);
                GameController.instance.xpEarned += OrderController.instance.LevelM.RewardXP;
                GameController.instance.XpCalculation();
            }


            if (currentXPLevel < (GameController.instance.currentXpLevel + 1))
            {
                GameController.instance.totalCoins += GameController.instance.scoreDetails.xpLevelDetails[GameController.instance.currentXpLevel].rewardCoins;
                GameConstants.SetTotalCoins(GameController.instance.totalCoins);
                GameController.instance.totalDiamonds += GameController.instance.scoreDetails.xpLevelDetails[GameController.instance.currentXpLevel].rewardDiamonds;
                GameConstants.SetTotalDiamonds(GameController.instance.totalDiamonds);
            }


            //update star count.
            if (starCount >= GameConstants.GetCityData(Index, activeLevel))
            {
                Debug.Log("Earned stars are greater");
                GameConstants.SetCityData(Index, activeLevel, starCount);
                PlayerPrefs.Save();
            }
            activeLevel++;

            if (activeLevel > GameConstants.GetCityData("currentLevel_", storingIndex))
            {
                GameConstants.SetCityData("currentLevel_", storingIndex, activeLevel);
                currentLevel = GameConstants.GetCityData("currentLevel_", storingIndex);

            }

        }

        //update total coins.
        GameController.instance.totalCoins += coinsEarned;
        GameConstants.SetTotalCoins(GameController.instance.totalCoins);

        // //update total coins,diamonds and xp text.
        totalCoinText.text = GameConstants.GetTotalCoins().ToString();
        totalDiamondText.text = GameConstants.GetTotalDiamonds().ToString();
        xpText.text = GameConstants.GetEarnedXP().ToString() + "/" + GameController.instance.xpLevelRequirement.ToString();



    }

    #region Button Clicks
    /// <summary>
    /// Function to Open Level Selection Pop Up
    /// </summary>
    public void openLevelSelect()
    {
        UpgradePopUp.SetActive(false);
        gameEndedPopUp.SetActive(false);
        MenupopBtn.gameObject.SetActive(true);
        //PausepopBtn.gameObject.SetActive(false);
        MenupopBtn.interactable = true;

        LevelPopUp.SetActive(true);
        EnableStars();
        EnableLevelButton();
        MenupopBtn.interactable = true;
    }
    
    /// <summary>
    /// Enable/Diable Upgrade Menu
    /// </summary>
    public void OpenCloseUpgrade()
    {


        if (!UpgradePopUp.activeSelf)
        {
            endScreenShown = false;
            for (int i = 0; i < 3; i++)
                winScreenStar[i].SetActive(false);
            levelEnded = false;
            winConfetti.SetActive(false);
            restartClicked = true;
            gameEndedPopUp.SetActive(false);

            coinPanel.SetActive(true);
            diamondPanel.SetActive(true);
            timerPanel.SetActive(false);
            custCountPanel.SetActive(false);
            coinStarPanel.SetActive(false);

            if (tutorialCounter == 2)
                ShowTutorial(14);

            MenuPopUp.SetActive(false);
            UpgradePopUp.SetActive(true);
        }
        else
        {
            UpgradePopUp.SetActive(false);
            MenuPopUp.SetActive(true);
        }
    }
    
    /// <summary>
    /// Enable/Disable Menu
    /// </summary>
    public void OpenCloseMenu()
    {
        if (!MenuPopUp.activeSelf)
        {
            MenupopBtn.interactable = false;
            MenuPopUp.SetActive(true);
        }
        else
        {
            MenupopBtn.interactable = true;
            MenuPopUp.SetActive(false);
        }
    }

    /// <summary>
    /// GO TO Main Menu Function
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseXpLevelUp()
    {
        xpLevelPopUp.SetActive(false);
        gameEndedPopUp.SetActive(true);
    }
    #endregion

    public void NextClicked()
    {
        if (activeScene == 2 && globalTut[0].activeSelf)
        {
            globalTut[0].SetActive(false);
        }

        LoadLevel(currentLevel);

    }

    #region Tutorial

    public void ShowTutorial(int tutNum)
    {
        switch (activeLevel)
        {
            case 1:
                if (tutorialCounter == 0)
                {
                    startPositioning = false;
                    switch (tutNum)
                    {
                        case 0:

                            prepTuts[0].SetActive(true);
                            StartCoroutine(TutWaitTime());
                            break;

                        case 1:
                            prepTuts[1].SetActive(false);
                            prepTuts[2].SetActive(true);
                            break;

                        case 2:
                            prepTuts[2].SetActive(false);
                            prepTuts[3].SetActive(true);
                            break;

                        case 3:
                            prepTuts[3].SetActive(false);
                            prepTuts[4].SetActive(true);
                            break;

                        case 4:
                            prepTuts[4].SetActive(false);
                            prepTuts[5].SetActive(true);
                            break;

                        case 5:
                            prepTuts[5].SetActive(false);
                            prepTuts[6].SetActive(true);
                            break;

                        case 6:
                            prepTuts[6].SetActive(false);
                            prepTuts[7].SetActive(true);
                            break;

                        case 7:
                            prepTuts[7].SetActive(false);
                            prepTuts[8].SetActive(true);
                            break;

                        case 8:
                            prepTuts[8].SetActive(false);
                            tutorialCounter = 1;
                            GameConstants.SetCityData("cookingTut_", storingIndex, 1);
                            tutChecked = true;
                            pauseTimers = false;
                            StartCoroutine(CustomerPositioning());
                            LockForTut.SetActive(true);
                            break;
                    }
                }
                if (tutorialCounter == 1)
                {


                    switch (tutNum)
                    {
                        case 9:
                            prepTuts[9].SetActive(true);
                            IngredientController.instance.IngredientClicked("beverage");
                            LockForTut.SetActive(false);
                            break;

                        case 10:
                            prepTuts[9].SetActive(false);
                            prepTuts[10].SetActive(true);
                            break;

                        case 11:
                            prepTuts[10].SetActive(false);
                            prepTuts[11].SetActive(true);
                            break;
                        case 12:
                            prepTuts[11].SetActive(false);
                            tutorialCounter = 2;
                            GameConstants.SetCityData("cookingTut_", storingIndex, 2);
                            tutChecked = true;

                            pauseTimers = false;
                            StartCoroutine(LevelTimer());
                            break;
                    }

                }
                break;

            case 2:
                if (tutorialCounter == 2)
                {
                    switch (tutNum)
                    {
                        case 13:
                            prepTuts[12].SetActive(true);
                            break;

                        case 14:
                            prepTuts[12].SetActive(false);
                            prepTuts[13].SetActive(true);
                            break;

                        case 15:
                            prepTuts[13].SetActive(false);
                            prepTuts[14].SetActive(true);
                            break;

                        case 16:
                            prepTuts[14].SetActive(false);
                            prepTuts[15].SetActive(true);
                            break;

                        case 17:
                            prepTuts[15].SetActive(false);
                            UpgradeSystem.instance.buyButtonClicked(0);
                            tutorialCounter = 3;
                            GameConstants.SetCityData("cookingTut_", storingIndex, 3);
                            break;
                    }

                }
                break;

            case 4:
                LockForTut.SetActive(false);
                startPositioning = false;
                StopCoroutine(CustomerPositioning());

                if (tutorialCounter == 3)
                {
                    
                    switch (tutNum)
                    {
                        case 21:
                            prepTuts[16].SetActive(true);
                            break;

                        case 22:
                            prepTuts[16].SetActive(false);
                            prepTuts[17].SetActive(true);
                            IngredientController.instance.IngredientClicked("base2");
                            break;

                        case 23:
                            tutChecked = false;
                            prepTuts[17].SetActive(false);
                            prepTuts[18].SetActive(true);
                            IngredientController.instance.IngredientClicked("rawFood2");
                            break;

                        case 24:
                            tutChecked = true;
                            prepTuts[18].SetActive(false);
                            prepTuts[19].SetActive(true);
                            break;

                        case 25:
                            tutChecked = false;
                            prepTuts[19].SetActive(false);
                            prepTuts[20].SetActive(true);
                            break;

                        case 26:
                            tutChecked = true;
                            prepTuts[20].SetActive(false);
                            tutorialCounter = 4;
                            GameConstants.SetCityData("cookingTut_", storingIndex, 4);
                            prepTuts[17].SetActive(true);
                            break;
                    }
                }
                if (tutorialCounter == 4)
                {
                    switch (tutNum)
                    {
                        case 23:
                            Debug.Log("mandaaaaaaaaaaaaaa");
                            IngredientController.instance.IngredientClicked("rawFood2");
                            prepTuts[17].SetActive(false);
                            prepTuts[21].SetActive(true);
                            break;
                        case 27:
                            tutChecked = false;
                            prepTuts[21].SetActive(false);
                            prepTuts[22].SetActive(true);
                            break;

                        case 28:
                            prepTuts[22].SetActive(false);
                            prepTuts[23].SetActive(true);
                            StartCoroutine(TutWaitTime());
                            break;

                        case 29:
                            prepTuts[23].SetActive(false);
                            Debug.Log("Inside tut");
                            tutorialCounter = 5;
                            GameConstants.SetCityData("cookingTut_", storingIndex, 5);
                            StartLevel();
                            pauseTimers = false;
                            break;

                    }

                }

                break;

            case 8:
                {
                    startPositioning = false;
                    LockForTut.SetActive(false);

                    switch (tutNum)
                    {
                        case 30:
                            tutChecked = false;
                            prepTuts[24].SetActive(true);
                            StartCoroutine(TutWaitTime());

                            break;
                        case 31:
                            tutChecked = true;
                            prepTuts[24].SetActive(false);
                            tutorialCounter = 6;
                            GameConstants.SetCityData("cookingTut_", storingIndex, 6);
                            startPositioning = true;
                            StartLevel();
                            break;
                    }

                }
                break;

            case 11:
                {
                    startPositioning = false;
                    LockForTut.SetActive(false);

                    switch (tutNum)
                    {
                        case 32:
                            tutChecked = false;
                            prepTuts[25].SetActive(true);
                            StartCoroutine(TutWaitTime());

                            break;
                        case 33:
                            tutChecked = true;
                            prepTuts[25].SetActive(false);
                            tutorialCounter = 7;
                            GameConstants.SetCityData("cookingTut_", storingIndex, 7);
                            startPositioning = true;
                            StartLevel();
                            break;
                    }

                }
                break;

            case 12:
                {
                    startPositioning = false;
                    LockForTut.SetActive(false);

                    switch (tutNum)
                    {
                        case 34:
                            tutChecked = false;
                            prepTuts[26].SetActive(true);
                            StartCoroutine(TutWaitTime());

                            break;
                        case 35:
                            tutChecked = true;
                            prepTuts[26].SetActive(false);
                            tutorialCounter = 8;
                            GameConstants.SetCityData("cookingTut_", storingIndex, 8);
                            startPositioning = true;

                            StartLevel();
                            break;
                    }
                }
                break;

            case 18:
                {
                    startPositioning = false;
                    switch (tutNum)
                    {
                        case 36:
                            LockForTut.SetActive(false);
                            prepTuts[27].SetActive(true);
                            break;

                        case 37:
                            prepTuts[27].SetActive(false);
                            prepTuts[28].SetActive(true);
                            IngredientController.instance.IngredientClicked("rawFood3");
                            break;

                        case 38:
                            prepTuts[28].SetActive(false);
                            prepTuts[29].SetActive(true);
                            break;

                        case 39:
                            tutChecked = true;
                            prepTuts[29].SetActive(false);
                            tutorialCounter = 9;
                            GameConstants.SetCityData("cookingTut_", storingIndex, 9);
                            startPositioning = true;
                            StartLevel();
                            break;
                    }
                }
                break;
        }
    }

    public IEnumerator TutWaitTime()
    {
        switch (activeLevel)
        {

            case 1:
                {

                    yield return new WaitForSeconds(1.5f);
                    if (tutorialCounter == 0)
                    {
                        prepTuts[0].SetActive(false);
                        prepTuts[1].SetActive(true);
                        LockForTut.SetActive(false);

                    }

                    break;
                }

            case 4:
                {
                    yield return new WaitForSeconds(2.0f);

                    if (tutorialCounter == 4 && !tutChecked)
                    {
                        yield return new WaitForSeconds(2.0f);
                        tutChecked = true;
                        ShowTutorial(29);
                    }
                    break;
                }
            case 8:
                {
                    yield return new WaitForSeconds(2.0f);

                    if (tutorialCounter == 5 && !tutChecked)
                    {
                        yield return new WaitForSeconds(2.0f);
                        tutChecked = true;
                        ShowTutorial(31);
                    }
                    break;
                }
            case 11:
                {
                    yield return new WaitForSeconds(2.0f);

                    if (tutorialCounter == 6 && !tutChecked)
                    {
                        yield return new WaitForSeconds(2.0f);
                        tutChecked = true;
                        ShowTutorial(33);
                    }
                    break;
                }

            case 12:
                {
                    yield return new WaitForSeconds(2.0f);

                    if (tutorialCounter == 7 && !tutChecked)
                    {
                        yield return new WaitForSeconds(2.0f);
                        tutChecked = true;
                        ShowTutorial(35);
                    }
                    break;
                }
        }
    }

    public void ShowGlobalTut(int tutNum)
    {
        switch (tutNum)
        {
            case 18:
                tutChecked = false;
                globalTut[1].SetActive(true);
                break;

            case 19:
                globalTut[1].SetActive(false);
                globalTut[2].SetActive(true);
                break;

            case 20:
                GameConstants.SetGlobalTut(5);
                globalTut[2].SetActive(false);
                break;

            case 40:
                prepTuts[30].SetActive(true);
                startPositioning = false;
                StopCoroutine(CustomerPositioning());
                break;

            case 41:
                prepTuts[30].SetActive(false);
                prepTuts[31].SetActive(true);
                break;

            case 42:
                prepTuts[31].SetActive(false);
                prepTuts[32].SetActive(true);
                break;

            case 43:
                prepTuts[32].SetActive(false);
                GameConstants.SetGlobalTut(6);
                startPositioning = true;
                StartLevel();
                break;

            case 44:
                globalTut[3].SetActive(true);
                break;

            case 45:
                globalTut[3].SetActive(false);
                globalTut[4].SetActive(true);
                break;

            case 46:
                GameConstants.SetBurnTut(1);
                globalTut[4].SetActive(false);
                startPositioning = true;
                StartCoroutine(CustomerPositioning());
                CustomerController.instance.pauseWaitingTime = false;

                pauseTimers = false;
                break;
        }

    }

    #endregion

}