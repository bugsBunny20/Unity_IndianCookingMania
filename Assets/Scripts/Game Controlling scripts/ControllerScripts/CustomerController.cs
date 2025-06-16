using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomerController : MonoBehaviour, IDropHandler
{

    public static CustomerController instance;


    public float WaitingTimer;
    public float sideOrderWaitingTimer;
    public bool decreasedCount = false;
    public int gender;

    public GameObject orederRequest;
    public GameObject timer;
    public Image timerFill;

    public GameObject SplOrederRequest;
    public Image splOrderTimerFill;
    public Image splOrderServed;

    public float t;

    private float speed = 200.0f;
    private float timeVariance;

    public Vector3 destination;

    [SerializeField]
    private Vector3 startingPosition;

    public bool isOnSeat;
    public bool isPositioning;
    public int seatPos;

    private bool isLeaving;
    public bool pauseWaitingTime;

    private bool patienceBarSliderFlag;
    private bool splOrderSliderFlag;

    private bool OrderComplete;

    public int orderPayment;
    public int tips;
    public int xpEarned;

    public Sprite[] custMoods;
    public AudioClip[] custMoodsAud;
    public AudioClip orderAccept;
    public AudioClip orderPopUp;
    public bool sadAudioPlayed;
    public bool angryAudioPlayed;

    public Vector3 startPos;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    public List<string> orderTags = new List<string>();



    //initiate starting position of customer and flags
    private void Awake()
    {
        if (instance == null)
            instance = this;

        
        //flags
        isOnSeat = false;
        isPositioning = false;
        isLeaving = false;
        OrderComplete = false;
        patienceBarSliderFlag = false;
        sadAudioPlayed = false;
        angryAudioPlayed = false;

        //initial position
        startingPosition = transform.localPosition;

    }

    // Use this for initialization
    void Start()
    {
        WaitingTimer = 35.0f;
        sideOrderWaitingTimer = 20.0f;
        gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        startPos = new Vector3(-1400.0f, 223.0f,1.0f);
    }


    // Update is called once per frame
    void Update()
    {
        if (patienceBarSliderFlag)
            StartCoroutine(patienceBar());
        if (splOrderSliderFlag)
            StartCoroutine(splOrderTimer());
        if (LevelController.instance.PausePopUp.activeSelf)
            pauseWaitingTime = true;

        if (pauseWaitingTime && !LevelController.instance.PausePopUp.activeSelf && seatPos !=0)
        {
            Debug.Log("Positioning*****");
            pauseWaitingTime = false;
            StartCoroutine(goToSeat());
        }

        //if(orderTags.Count==0 && !isLeaving)
        //{
        //    OrderComplete = true;
        //    StartCoroutine("leave");
        //    CheckOrder();
        //}
    }

    void LateUpdate()
    {
        if (LevelController.instance.custLeft == 0)
        {
            Destroy(this.gameObject);
        }
        if (LevelController.instance.levelTime < 0 && !LevelController.instance.restartClicked)
        {
            Destroy(this.gameObject);
        }
    }

    //to decide where customer would seat
    public void CustomerPosition(int custPos)
    {
        seatPos = custPos;
        isPositioning = true;
        StartCoroutine(goToSeat());

    }


    //customer walking animation
    public IEnumerator goToSeat()
    {
        this.gameObject.transform.SetAsFirstSibling();
        timeVariance = Random.value;
        while (!isOnSeat && !pauseWaitingTime)
        {
            //if (!LevelController.instance.pauseTimers)
            //{
            transform.localPosition = new Vector3(transform.localPosition.x + (Time.deltaTime * speed),
                                         startingPosition.y - 0.01f + (Mathf.Sin((Time.time + timeVariance) * 20)),
                                         transform.localPosition.z);

            //break loop once customer reaches destination
            if (transform.localPosition.x >= destination.x & !isOnSeat)
            {
                isOnSeat = true;

                LevelController.instance.custCount--;

                //AudioManager.instance.PlaySoud("SeatReached");
                
                LevelController.instance.custCountText.text = LevelController.instance.custCount.ToString();

                switch(LevelController.instance.activeLevel)
                {
                    case 1:
                        if (LevelController.instance.tutorialCounter == 0)
                        {
                            this.GetComponent<Image>().raycastTarget = true;
                            LevelController.instance.ShowTutorial(0);
                            LevelController.instance.pauseTimers = true;
                            orederRequest.SetActive(true);
                            timer.SetActive(true);
                            yield break;
                        }

                        else if (LevelController.instance.tutorialCounter == 1)
                        {
                            this.GetComponent<Image>().raycastTarget = true;
                            LevelController.instance.ShowTutorial(9);
                            LevelController.instance.pauseTimers = true;
                            orederRequest.SetActive(true);
                            timer.SetActive(true);
                            yield break;
                        }

                        else
                        {
                            orederRequest.SetActive(true);
                            timer.SetActive(true);
                            patienceBarSliderFlag = true; //start the patience bar
                            LevelController.instance.startPositioning = true;
                        }
                        break;

                    case 18:
                        if (LevelController.instance.tutorialCounter == 8)
                        {
                            this.GetComponent<Image>().raycastTarget = true;
                            LevelController.instance.ShowTutorial(36);
                            LevelController.instance.pauseTimers = true;
                            orederRequest.SetActive(true);
                            timer.SetActive(true);
                            yield break;
                        }
                        else
                        {
                            orederRequest.SetActive(true);
                            timer.SetActive(true);
                            patienceBarSliderFlag = true; //start the patience bar
                            LevelController.instance.startPositioning = true;
                        }
                        break;

                    default:
                        if (SplOrederRequest != null)
                        {
                            SplOrederRequest.SetActive(true);
                            splOrderSliderFlag = true;
                        }
                        orederRequest.SetActive(true);
                        timer.SetActive(true);
                        patienceBarSliderFlag = true; //start the patience bar
                        //LevelController.instance.startingIndex++;
                        //Debug.Break();
                        LevelController.instance.startPositioning = true;  //allow level controller to start customer positioning
                        break;
                }
                source.clip = orderPopUp;
                source.PlayOneShot(orderPopUp);
                yield break;
                
            }

            yield return 0;
            //}
        }
    }


    // show and animate progress bar based on customer's patience
    public IEnumerator patienceBar()
    {
        patienceBarSliderFlag = false;
        while (WaitingTimer > 0 && !isLeaving && !pauseWaitingTime && !LevelController.instance.globalTut[3].activeSelf)
        {
            WaitingTimer -= Time.deltaTime;

            timerFill.fillAmount = WaitingTimer / 35.0f;
            if (WaitingTimer <= 35.0f && WaitingTimer >= 20.0f)
            {
                this.transform.GetChild(2).GetComponent<Image>().sprite = custMoods[1];//happy
                //if (gender == 0)
                //    AudioManager.instance.PlaySoud("femaleHappy");
                //else
                //    AudioManager.instance.PlaySoud("maleHappy");

            }

            if (WaitingTimer < 20.0f && WaitingTimer >= 10.0f)
            {
                this.transform.GetChild(2).GetComponent<Image>().sprite = custMoods[2];//sad
                //if (gender == 0)
                //    AudioManager.instance.PlaySoud("femaleSad");
                //else
                //    AudioManager.instance.PlaySoud("maleSad");
                if (!sadAudioPlayed)
                    PlayAudio(1);
            }

            if (WaitingTimer < 10.0f && WaitingTimer >= 0.0f)
            {
                this.transform.GetChild(2).GetComponent<Image>().sprite = custMoods[3];//angry
                //if (gender == 0)
                //    AudioManager.instance.PlaySoud("femaleAngry");
                //else
                //    AudioManager.instance.PlaySoud("maleAngry");

                if (!angryAudioPlayed)
                    PlayAudio(2);
            }

            yield return 0;
        }
        if (WaitingTimer <= 0 )
        {

            if (SplOrederRequest != null && sideOrderWaitingTimer <= 0)
            {
                splOrderTimerFill.enabled = false;
                timerFill.enabled = false;
                settle();
            }

            else
            {
                timerFill.enabled = false;
                settle();
            }
            
        }


    }

    public void PlayAudio(int audioIndex)
    {
        if (audioIndex == 0)
        {
            source.clip = custMoodsAud[0];
            source.PlayOneShot(custMoodsAud[0]);

        }
        if (audioIndex == 1)
        {
            sadAudioPlayed = true;

            source.clip = custMoodsAud[1];
            source.PlayOneShot(custMoodsAud[1]);
        }

        if (audioIndex == 2)
        {
            angryAudioPlayed = true;

            source.clip = custMoodsAud[2];
            source.PlayOneShot(custMoodsAud[2]);
        }
    }
    //temporary tutorial need to build a tutorial system which will work independtly
    //public void TutClosed()
    //{
    //    patienceBarSliderFlag = true; //start the patience bar
    //    LevelController.instance.startPositioning = true;
    //}

    public IEnumerator StartWaitingTimer()
    {
        yield return new WaitForSeconds(0.5f);
        if (pauseWaitingTime)
        {
            //Debug.Log("Inside restart timer*****************");
            pauseWaitingTime = false;
            WaitingTimer += 5;
            if(WaitingTimer >= 20.0f && sadAudioPlayed)
            {
                sadAudioPlayed = false;
                angryAudioPlayed = false;
            }
            else if (WaitingTimer >= 10.0f && WaitingTimer < 20.0f && angryAudioPlayed)
                angryAudioPlayed = false;

            StartCoroutine(patienceBar());
        }
       
    }

    IEnumerator splOrderTimer()
    {
        splOrderSliderFlag = false;
        while (WaitingTimer > 0 && !isLeaving)
        {
            sideOrderWaitingTimer -= Time.deltaTime;

            splOrderTimerFill.fillAmount = sideOrderWaitingTimer / 20.0f;
            if (sideOrderWaitingTimer <= 0)
            {
                if (!orederRequest.activeSelf)
                {
                    timerFill.enabled = false;
                    settle();
                }
                splOrderTimerFill.enabled = false;
                SplOrederRequest.SetActive(false);
                break;

            }

            yield return 0;
        }
        


    }


    #region Checking whether order is fulfilled or not
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Inside Ondrop");

        for (int i = 0; i < orderTags.Count; i++)
        {
            int rawItemIndex = eventData.pointerDrag.GetComponent<FoodController>().rawItemIndex;
            if (eventData.pointerDrag.tag == orderTags[i])
            {
                
                eventData.pointerDrag.transform.gameObject.SetActive(false);
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.SetAsLastSibling();
                eventData.pointerDrag.GetComponent<RectTransform>().localScale = Vector3.one;
                eventData.pointerDrag.GetComponent<RectTransform>().localPosition = Vector3.zero;
                eventData.pointerDrag.transform.SetAsLastSibling();

                pauseWaitingTime = true;

                this.transform.GetChild(2).GetComponent<Image>().sprite = custMoods[1];//happy
                //if (gender == 0)
                //    AudioManager.instance.PlaySoud("femaleHappy");
                //else
                //    AudioManager.instance.PlaySoud("maleHappy");
                source.clip = custMoodsAud[0];
                source.PlayOneShot(custMoodsAud[0]);

                StartCoroutine(StartWaitingTimer());


                #region check for tag of order
                if (orderTags[i] == "dish1")
                {
                    IngredientController.instance.tt1Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop1counter--;

                    orderPayment += UpgradeSystem.instance.dish1CurrentPrice;

                }

                if (orderTags[i] == "beverage")
                {
                    IngredientController.instance.kettleOccupied[rawItemIndex] = false;
                    //Debug.Log(IngredientController.instance.kettleOccupied[0]);
                    IngredientController.instance.beverageCounter--;
                    IngredientController.instance.IngredientClicked("beverage");

                    orderPayment += UpgradeSystem.instance.beverageCurrentPrice;

                    if (LevelController.instance.tutorialCounter == 1)
                    {
                        LevelController.instance.ShowTutorial(12);
                    }
                }

                if (orderTags[i] == "dish2")
                {
                    IngredientController.instance.tt2Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop2counter--;

                    orderPayment += UpgradeSystem.instance.dish2CurrentPrice;
                }

                if (orderTags[i] == "dish3")
                {
                    IngredientController.instance.tt3Occupied[rawItemIndex] = false;

                    orderPayment += UpgradeSystem.instance.dish3CurrentPrice;
                    if (LevelController.instance.tutorialCounter == 8)
                    {
                        LevelController.instance.ShowTutorial(39);
                    }
                }

                if (orderTags[i] == "splDish")
                {
                    orderPayment += UpgradeSystem.instance.splDishCurrentPrice;
                    int indexNum = eventData.pointerDrag.GetComponent<FoodController>().rawItemIndex;
                    Debug.Log("splDishCount_" + LevelController.instance.storingIndex + indexNum);
                    GameConstants.SetCityData("splDishCount_" + LevelController.instance.storingIndex, indexNum, 0);
                    SplOrederRequest.SetActive(false);
                    sideOrderWaitingTimer = 0.0f;
                }

                if (orderTags[i] == "d1t1")
                {
                    IngredientController.instance.tt1Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop1counter--;

                    eventData.pointerDrag.transform.GetChild(0).gameObject.SetActive(false);

                    orderPayment += UpgradeSystem.instance.dish1CurrentPrice + UpgradeSystem.instance.topping1CurrentPrice;
                }

                if (orderTags[i] == "d1t2")
                {
                    IngredientController.instance.tt1Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop1counter--;

                    eventData.pointerDrag.transform.GetChild(1).gameObject.SetActive(false);

                    orderPayment += UpgradeSystem.instance.dish1CurrentPrice + UpgradeSystem.instance.topping2CurrentPrice;

                }

                if (orderTags[i] == "d1t3")
                {
                    IngredientController.instance.tt1Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop1counter--;

                    eventData.pointerDrag.transform.GetChild(2).gameObject.SetActive(false);

                    orderPayment += UpgradeSystem.instance.dish1CurrentPrice + UpgradeSystem.instance.topping3CurrentPrice;

                }

                if (orderTags[i] == "d1t1t2")
                {
                    IngredientController.instance.tt1Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop1counter--;

                    eventData.pointerDrag.transform.GetChild(0).gameObject.SetActive(false);
                    eventData.pointerDrag.transform.GetChild(1).gameObject.SetActive(false);


                    orderPayment += UpgradeSystem.instance.dish1CurrentPrice + UpgradeSystem.instance.topping1CurrentPrice+ UpgradeSystem.instance.topping2CurrentPrice;
                }

                if (orderTags[i] == "d1t2t3")
                {
                    IngredientController.instance.tt1Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop1counter--;

                    eventData.pointerDrag.transform.GetChild(1).gameObject.SetActive(false);
                    eventData.pointerDrag.transform.GetChild(2).gameObject.SetActive(false);


                    orderPayment += UpgradeSystem.instance.dish1CurrentPrice + UpgradeSystem.instance.topping2CurrentPrice + UpgradeSystem.instance.topping3CurrentPrice;
                }

                if (orderTags[i] == "d1t1t3")
                {
                    IngredientController.instance.tt1Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop1counter--;

                    eventData.pointerDrag.transform.GetChild(0).gameObject.SetActive(false);
                    eventData.pointerDrag.transform.GetChild(2).gameObject.SetActive(false);


                    orderPayment += UpgradeSystem.instance.dish1CurrentPrice + UpgradeSystem.instance.topping1CurrentPrice + UpgradeSystem.instance.topping3CurrentPrice;
                }

                if (orderTags[i] == "d1t1t2t3")
                {
                    IngredientController.instance.tt1Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop1counter--;

                    eventData.pointerDrag.transform.GetChild(0).gameObject.SetActive(false);
                    eventData.pointerDrag.transform.GetChild(1).gameObject.SetActive(false);
                    eventData.pointerDrag.transform.GetChild(2).gameObject.SetActive(false);


                    orderPayment += UpgradeSystem.instance.dish1CurrentPrice + UpgradeSystem.instance.topping1CurrentPrice + UpgradeSystem.instance.topping2CurrentPrice + UpgradeSystem.instance.topping3CurrentPrice;
                }

                if (orderTags[i] == "d2t1")
                {
                   IngredientController.instance.tt2Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop2counter--;

                    eventData.pointerDrag.transform.GetChild(0).gameObject.SetActive(false);
                    //Debug.Log("disabled chutney!!!!!!!!!");

                    orderPayment += UpgradeSystem.instance.dish2CurrentPrice + UpgradeSystem.instance.topping1CurrentPrice;
                }

                if (orderTags[i] == "d2t2")
                {
                   IngredientController.instance.tt2Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop2counter--;
                    
                    eventData.pointerDrag.transform.GetChild(1).gameObject.SetActive(false);


                    orderPayment += UpgradeSystem.instance.dish2CurrentPrice +  UpgradeSystem.instance.topping2CurrentPrice;

                }

                if (orderTags[i] == "d2t3")
                {
                   IngredientController.instance.tt2Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop2counter--;

                    eventData.pointerDrag.transform.GetChild(2).gameObject.SetActive(false);


                    orderPayment += UpgradeSystem.instance.dish2CurrentPrice  + UpgradeSystem.instance.topping3CurrentPrice;

                }

                if (orderTags[i] == "d2t1t2")
                {
                   IngredientController.instance.tt2Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop2counter--;

                    eventData.pointerDrag.transform.GetChild(0).gameObject.SetActive(false);
                    eventData.pointerDrag.transform.GetChild(1).gameObject.SetActive(false);


                    orderPayment += UpgradeSystem.instance.dish2CurrentPrice + 
                                    UpgradeSystem.instance.topping1CurrentPrice + 
                                    UpgradeSystem.instance.topping2CurrentPrice;
                }

                if (orderTags[i] == "d2t2t3")
                {
                   IngredientController.instance.tt2Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop2counter--;


                    eventData.pointerDrag.transform.GetChild(1).gameObject.SetActive(false);
                    eventData.pointerDrag.transform.GetChild(2).gameObject.SetActive(false);


                    orderPayment += UpgradeSystem.instance.dish2CurrentPrice + 
                                    UpgradeSystem.instance.topping2CurrentPrice + 
                                    UpgradeSystem.instance.topping3CurrentPrice;
                }

                if (orderTags[i] == "d2t1t3")
                {
                   IngredientController.instance.tt2Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop2counter--;

                    eventData.pointerDrag.transform.GetChild(0).gameObject.SetActive(false);
                    eventData.pointerDrag.transform.GetChild(2).gameObject.SetActive(false);


                    orderPayment += UpgradeSystem.instance.dish2CurrentPrice + 
                                    UpgradeSystem.instance.topping1CurrentPrice + 
                                    UpgradeSystem.instance.topping3CurrentPrice;
                }

                if (orderTags[i] == "d2t1t2t3")
                {
                   IngredientController.instance.tt2Occupied[rawItemIndex] = false;
                    IngredientController.instance.tableTop2counter--;
                   

                    eventData.pointerDrag.transform.GetChild(0).gameObject.SetActive(false);
                    eventData.pointerDrag.transform.GetChild(1).gameObject.SetActive(false);
                    eventData.pointerDrag.transform.GetChild(2).gameObject.SetActive(false);


                    orderPayment += UpgradeSystem.instance.dish2CurrentPrice + 
                                    UpgradeSystem.instance.topping1CurrentPrice + 
                                    UpgradeSystem.instance.topping2CurrentPrice + 
                                    UpgradeSystem.instance.topping3CurrentPrice;
                }
                #endregion

                if (WaitingTimer <= 20.0f && WaitingTimer >= 10.0f)
                {
                    xpEarned += 2;
                }
                else
                {
                    xpEarned += 1;
                }

                if (orderTags[i] != "splDish")
                {
                    Destroy(orederRequest.transform.GetChild(i).gameObject);
                    CloseOrderReq();
                }

                if (WaitingTimer <= 35.0f && WaitingTimer >= 20.0f)
                {
                    tips += 3;
                }

                else if (WaitingTimer < 20.0f && WaitingTimer >= 10.0f)
                {
                    tips += 1;
                }

                //remove the item received from list
                orderTags.RemoveAt(i);
                source.clip =orderAccept;
                source.PlayOneShot(orderAccept);


                //check whether order is completed or not.
                if (orderTags.Count == 0)
                {
                    Debug.Log("Inside order complete");
                    OrderComplete = true;
                    this.transform.GetChild(2).GetComponent<Image>().sprite = custMoods[1];
                    source.clip = custMoodsAud[0];
                    source.PlayOneShot(custMoodsAud[0]);
                    //if (gender == 0)
                    //    AudioManager.instance.PlaySoud("femaleHappy");
                    //else
                    //    AudioManager.instance.PlaySoud("maleHappy");
                    settle(); //to add all the order money
                }
                eventData.pointerDrag.GetComponent<FoodController>().itemIsInHand = false;

                //destroy the item from order request.

                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.SetAsLastSibling();
                eventData.pointerDrag.GetComponent<RectTransform>().localScale = Vector3.one;
                eventData.pointerDrag.GetComponent<RectTransform>().localPosition = Vector3.zero;
                eventData.pointerDrag.transform.SetAsLastSibling();
            }

            //snap item to it's original position if it's not what customer ordered.
            else
            {
                eventData.pointerDrag.GetComponent<FoodController>().itemIsInHand = false;
                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.SetAsLastSibling();
                eventData.pointerDrag.GetComponent<RectTransform>().localScale = Vector3.one;
                eventData.pointerDrag.GetComponent<RectTransform>().localPosition = Vector3.zero;
                eventData.pointerDrag.transform.SetAsLastSibling();
            }
        }
    }
    #endregion

    public void CloseOrderReq()
    {
     
        if (orederRequest.transform.childCount < 2 && SplOrederRequest!=null)
        {
            StartCoroutine(CloseOrderRequest());

        }
    }

    IEnumerator CloseOrderRequest()
    {
        StartCoroutine(animate(Time.time, timer, 1.0f, 0.15f));
        yield return new WaitForSeconds(0.02f);
        orederRequest.SetActive(false);
        timer.SetActive(false);
        
    }

    void settle()
    {
        if (orderPayment > 0)
        {
            this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            StartCoroutine(ShowCoinAnim());
            if (LevelController.instance.tutorialCounter == 0 && !LevelController.instance.tutChecked)
            {
                LevelController.instance.ShowTutorial(7);
            }
        }
         if (orderPayment == 0)
        {
            Debug.Log("*************************Inside leavingggggggggggggg");
            LevelController.instance.custLost++;
            LevelController.instance.seatFilled[seatPos] = false;
            switch (seatPos)
            {
                case 0:
                    LevelController.instance.pos1 = false;
                    break;
                case 1:
                    LevelController.instance.pos2 = false;
                    break;
                case 2:
                    LevelController.instance.pos3 = false;
                    break;
                case 3:
                    LevelController.instance.pos4 = false;
                    break;
            }
            StartCoroutine(leave());
        }
    }

    public IEnumerator ShowCoinAnim()
    {
        yield return new WaitForSeconds(0.2f);
        this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        LevelController.instance.ShowCoinsObject(seatPos);
        //Debug.Log("<color=brown>Coins position: " + seatPos + " Order Payment:</color> " + orderPayment + tips);
        yield return new WaitForSeconds(0.2f);
        LevelController.instance.earnedCoinsObj[seatPos].GetComponent<OrderSettlement>().orderTotal = orderPayment + tips;
        LevelController.instance.earnedCoinsObj[seatPos].GetComponent<OrderSettlement>().tipCoinsEarned = tips;

        GameController.instance.xpEarned += xpEarned;
        GameController.instance.XpCalculation();
        //Debug.Log("<color=brown>XP earned in cc: " + xpEarned + " xp updated :</color> " + GameController.instance.xpEarned);
        StartCoroutine(leave());

    }

    public IEnumerator leave()
    {

        if (isLeaving)
        {
           
            yield break;
        }


        isLeaving = true;
        this.GetComponent<Image>().raycastTarget = false;

        this.gameObject.transform.SetAsFirstSibling();

        //animate popup disabling
        StartCoroutine(animate(Time.time, timer,1.0f,0.15f));
        yield return new WaitForSeconds(0.3f);

        //animate (close) request bubble
        StartCoroutine(animate(Time.time, orederRequest, 0.95f, 0));
        yield return new WaitForSeconds(0.4f);

        if (!decreasedCount)
        {
            decreasedCount = true;
            LevelController.instance.custLeft--;
        }

        while (transform.localPosition.x < 11000)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + (Time.deltaTime * speed),
                                             startingPosition.y - 0.05f + (Mathf.Sin(Time.time * 10) / 8),
                                             1.0f);

            if (transform.localPosition.x >= 1100)
            {
                Destroy(gameObject);
                yield break;
            }
            yield return 0;
        }
    }

    // animate customer.
    //***************************************************************************//

    IEnumerator animate(float _time, GameObject _go, float _in, float _out)
    {
        float t = 0.0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime * 10;
            _go.GetComponent<Image>().color = new Color(1, 1, 1, Mathf.SmoothStep(_in, _out, t));
            
            yield return 0;
        }
        float r = 0.0f;
        if (_go.GetComponent<Image>().color.a >= _out)
        {
            while (r <= 1.0f)
            {
                r += Time.deltaTime * 2;
                _go.GetComponent<Image>().color = new Color(1, 1, 1, Mathf.SmoothStep(_out, 0.01f, r));

                
                if (_go.GetComponent<Image>().color.a <= 0.01f)
                    _go.SetActive(false);
                yield return 0;
            }
        }
        yield return 0;
    }


}
