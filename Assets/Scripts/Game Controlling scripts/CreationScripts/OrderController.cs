using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public static OrderController instance;

    [SerializeField]
    public int TargetCoins;
    public int orderTotal;

    private bool splDishFlag;

    public Level_Mumbai LevelM;

    [SerializeField]
    private int randDish;

    public GameObject[] unlockedObjs;

    [SerializeField]
    private int totalCoinstemp;
    [SerializeField]
    private List<int> availableDishNum = new List<int> { 0, 1 };
    [SerializeField]
    private List<int> availableToppings;

    private int dish1counter;
    private int dish2counter;
    private int dish3counter;

    private int topping11Counter;
    private int topping12Counter;
    private int topping13Counter;

    private int topping21Counter;
    private int topping22Counter;
    private int topping23Counter;

    private int beverageCounter;
    private int beveragePourCount;

    [System.Serializable]

    public class CustIntialization
    {
        public int seatNum;
        public int dish1;
        public int topping11;
        public int topping12;
        public int topping13;
        public int dish2;
        public int topping21;
        public int topping22;
        public int topping23;
        public int dish3;
        public int beverage;
        public bool specialDish;
    }

    [Space]
    [Header("List for holding each order")]
    public static List<CustIntialization> CustomerOrder;

    [Space]
    [SerializeField]
    private int CustNo;

    private int storingIndex;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        storingIndex = LevelController.instance.storingIndex;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OrderCreation(int levelNum)
    {
        //Debug.Log("Inside Order Creation" + levelNum);

        LevelM = Resources.Load<Level_Mumbai>("Mumbai_Levels/Level_" + levelNum);

        TargetCoins = LevelM.Star1Score;
        orderTotal = 0;
        availableToppings = new List<int>();
        totalCoinstemp = 0;
        beveragePourCount = 1;
        CustomerOrder = new List<CustIntialization>();
        CustNo = LevelM.CustomerCount;

        if (LevelM.Dish2CanBeOrdered)
            availableDishNum.Add(2);
        if (LevelM.Dish3CanBeOrdered)
            availableDishNum.Add(3);

        if (LevelM.topping11CanBeOrdered)
            availableToppings.Add(11);
        if (LevelM.topping12CanBeOrdered)
            availableToppings.Add(12);
        if (LevelM.topping13CanBeOrdered)
            availableToppings.Add(13);




        if (LevelM.unloackable != null)
        {
            switch (LevelM.unloackable)
            {
                case "dish2":
                    {
                        GameConstants.SetCityData("dish2Unlocked_", storingIndex, 1);
                        UpgradeSystem.instance.updateUpgrades(6);
                        GameConstants.SetCityData("foodWarmerUnlocked_", storingIndex, 1);
                        UpgradeSystem.instance.updateUpgrades(10);
                        break;
                    }
                    
                case "topping1":
                    {
                        GameConstants.SetCityData("topping1unlocked_", storingIndex, 1);
                        UpgradeSystem.instance.updateUpgrades(11);
                        break;
                    }
                    
                case "topping2":
                    {
                        GameConstants.SetCityData("topping2unlocked_", storingIndex, 1);
                        UpgradeSystem.instance.updateUpgrades(12);
                        break;
                    }

                case "topping3":
                    {
                        GameConstants.SetCityData("topping3unlocked_", storingIndex, 1);
                        UpgradeSystem.instance.updateUpgrades(13);
                        break;
                    }
                    
                case "dish3":
                    {
                        GameConstants.SetCityData("dish3Unlocked_", storingIndex, 1);
                        UpgradeSystem.instance.updateUpgrades(14);
                        break;
                    }

                case "splDish":
                    {
                        GameConstants.SetCityData("splDishUnlocked_", storingIndex, 1);
                        UpgradeSystem.instance.updateUpgrades(16);
                        break;
                    }

            }
        }

        if (GameConstants.GetCityData("beverageMachine_", storingIndex) >= 1 &&
            GameConstants.GetCityData("fryPan1_", storingIndex) >= 1 &&
             GameConstants.GetCityData("tableTop1_", storingIndex) >= 1 &&
             GameConstants.GetCityData("fryPan2_", storingIndex) >= 1 &&
             GameConstants.GetCityData("tableTop2_", storingIndex) >= 1 ||
             GameConstants.GetCityData("fryPan1_", storingIndex) >= 1)
            CreateOrder();
        else if (levelNum < 31)
            //if (levelNum < 31)
            CreateOrder2();
        else
            CreateOrder();
    }

    public void CreateOrder2()
    {
        for(int i=0; i < LevelM.CustomerCount; i++)
        {
            //if (GameConstants.GetCityData("splDishUnlocked_", storingIndex) == 1)
            //{
            //    randDish = Random.Range(-1, 2);
            //    if (randDish == 1)
            //    {
            //        splDishFlag = true;
            //        Debug.Log("*************Special Dish");
            //    }

            //}
            if (GameConstants.GetCityData("splDishUnlocked_", storingIndex) == 1)
                splDishFlag = LevelM.CustomerOrder[i].splDish;
            //print("Value of i" + i);
            CustomerOrder.Add(new CustIntialization
            {
                //seatNum=0,
                dish1 = LevelM.CustomerOrder[i].dish1Count,
                topping11 = LevelM.CustomerOrder[i].topping11Count,
                topping12 = LevelM.CustomerOrder[i].topping12Count,
                topping13 = LevelM.CustomerOrder[i].topping13Count,
                dish2 = LevelM.CustomerOrder[i].dish2Count,
                topping21 = LevelM.CustomerOrder[i].topping21Count,
                topping22 = LevelM.CustomerOrder[i].topping22Count,
                topping23 = LevelM.CustomerOrder[i].topping23Count,
                dish3 = LevelM.CustomerOrder[i].dish3Count,
                beverage = LevelM.CustomerOrder[i].beverageCount,
                specialDish = splDishFlag
            });
        }
        CustomerCreator.instance.CustomerCreation();
    }

    public void CreateOrder()
    {
    //Debug.Log("Inside create order");


    customerCreate:
        orderTotal = 0;
        for (int i = 0; i < CustNo; i++)
        {
            dish1counter = 0;
            dish2counter = 0;
            dish3counter = 0;
            topping11Counter = 0;
            topping12Counter = 0;
            topping13Counter = 0;
            topping21Counter = 0;
            topping22Counter = 0;
            topping23Counter = 0;
            beverageCounter = 0;
            splDishFlag = false;

            if (GameConstants.GetCityData("cookingTut_", 0) == 0 && i == 0 && LevelM.LevelID == 1)
            {
                dish1counter = 1;
                goto CustOrderList;
            }

        order:

            for (int j = 0; j < 3; j++)
            {
                randDish = Random.Range(-2, availableDishNum.Count);
                switch (randDish)
                {
                    case 0:
                        beverageCounter++;
                        beverageCounter = Mathf.Clamp(beverageCounter, 0, beveragePourCount);
                        orderTotal += UpgradeSystem.instance.beverageCurrentPrice;
                        break;
                    case 1:
                        dish1counter++;
                        dish1counter = Mathf.Clamp(dish1counter, 0, 2);

                        if (LevelM.topping11CanBeOrdered)
                            for (int k = 0; k < 3; k++)
                            {
                                randDish = Random.Range(-3, availableToppings.Count + 1);
                                switch (randDish)
                                {
                                    case 0:
                                        if (topping11Counter <= dish1counter)
                                        {
                                            topping11Counter++;
                                            orderTotal += UpgradeSystem.instance.topping1CurrentPrice;
                                        }
                                        break;
                                    case 1:
                                        if (topping12Counter <= dish1counter)
                                        {
                                            topping12Counter++;
                                            orderTotal += UpgradeSystem.instance.topping2CurrentPrice;
                                        }
                                        break;
                                    case 2:
                                        if (topping13Counter <= dish1counter)
                                        {
                                            topping13Counter++;
                                            orderTotal += UpgradeSystem.instance.topping3CurrentPrice;
                                        }

                                        break;
                                    default:
                                        break;
                                }
                            }
                        orderTotal += UpgradeSystem.instance.dish1CurrentPrice;
                        break;

                    case 2:
                        dish2counter++;
                        dish2counter = Mathf.Clamp(dish2counter, 0, 2);
                        if (LevelM.topping11CanBeOrdered)

                            for (int k = 0; k < 3; k++)
                            {
                                randDish = Random.Range(-3, availableToppings.Count + 1);
                                switch (randDish)
                                {
                                    case 0:
                                        if (topping21Counter <= dish2counter)
                                        {
                                            topping21Counter++;
                                            orderTotal += UpgradeSystem.instance.topping1CurrentPrice;
                                        }
                                        break;
                                    case 1:
                                        if (topping22Counter <= dish2counter)
                                        {
                                            topping22Counter++;
                                            orderTotal += UpgradeSystem.instance.topping2CurrentPrice;
                                        }
                                        break;
                                    case 2:
                                        if (topping23Counter <= dish2counter)
                                        {
                                            topping23Counter++;
                                            orderTotal += UpgradeSystem.instance.topping3CurrentPrice;
                                        }

                                        break;
                                    case -1:
                                        break;
                                    case -2:
                                        break;
                                    case -3:
                                        break;
                                }
                            }
                        orderTotal += UpgradeSystem.instance.dish2CurrentPrice;
                        break;
                    case 3:
                        dish3counter++;
                        dish3counter = Mathf.Clamp(dish3counter, 0, 2);
                        orderTotal += UpgradeSystem.instance.dish3CurrentPrice;
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
            }
            if (GameConstants.GetCityData("splDishUnlocked_", storingIndex) == 1)
            {

                randDish = Random.Range(-1, 2);
                if (randDish == 1)
                {
                    splDishFlag = true;
                }
                orderTotal += UpgradeSystem.instance.splDishCurrentPrice;
            }

            //Check if created order is empty
            if (beverageCounter == 0 && dish1counter == 0 && dish2counter == 0 && dish3counter == 0)
            {
                //j--;
                goto order;
            }

        //Check if complted order reaches target total coins or not

        CustOrderList:

            CustomerOrder.Add(new CustIntialization
            {
                seatNum = Random.Range(0, 4),
                dish1 = dish1counter,
                topping11 = topping11Counter,
                topping12 = topping12Counter,
                topping13 = topping13Counter,
                dish2 = dish2counter,
                topping21 = topping21Counter,
                topping22 = topping22Counter,
                topping23 = topping23Counter,
                dish3 = dish3counter,
                beverage = beverageCounter,
                specialDish = splDishFlag
            });

            //Debug.Log("Order number " + i + " : dish1 count = " + CustomerOrder[i].dish1 + " beverage count= " + CustomerOrder[i].beverage);

        }

        if (orderTotal < TargetCoins)
        {
            Debug.Log("Inside target coins check");
            goto customerCreate;
        }

        CustomerCreator.instance.CustomerCreation();
        Debug.Log("outside create order");

    }

}
