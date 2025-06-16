using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class CustomerCreator : MonoBehaviour
{


    public static CustomerCreator instance;

    private float customerPatience;

    public OrderController OrderController;

    [Space]
    [Header("Order Background and timer sprites")]
    public Sprite OrderBGSprite;
    public Sprite timerBG;
    public Sprite timerFill;

    [Space]
    [Header("Special Order Background and timer sprites")]
    public Sprite spOrderBGSprite;
    //public Sprite timerBG;
    public Sprite spTimerFill;

    [Space]
    [Header("Dish sprites")]
    public Sprite[] dish1Sprites;
    public Sprite[] dish2Sprites;
    public Sprite dish3Sprites;
    public Sprite dish4Sprites;

    [Space]
    [Header("Beverage sprites")]
    public Sprite beverageSprites;

    [Space]
    [Header("Toppings sprites")]
    public Sprite[] dish1Toppings;
    public Sprite[] dish2Toppings;

    string custName;

    [SerializeField]
    private Vector2 dishPosition;
    private Vector2 positionOffset;

    [SerializeField]
    private int totalDishCounter;

    private int storingIndex;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        storingIndex = LevelController.instance.storingIndex;
    }

    /// <summary>
    /// Instiating prefab
    /// Creating order for each customer in that prefab
    /// </summary>

    public void CustomerCreation()
    {
        //Debug.Log("Inside Customer creation");

        for (int i = OrderController.CustomerOrder.Count; i >0; i--)
        {
            //Debug.Log("This is creating " + custName);

            totalDishCounter = 0;
            custName = "Customer_" + i;

            string orderName = "OrderDetails_" + i;
            string specialOrder = "specialOrder_" + i;
            string timerName = "timer_" + i;
            string timerFillName = "timerFill_" + i;
            string splTimerFillName = "splTimerFill_" + i;


            //to select random prefab of customer


            int custIndex = Random.Range(1,15);
            string tempCustName;


            if (custIndex < 8)
            {
                tempCustName = "Customer" + custIndex;
            }
            else
            {
                custIndex = custIndex % 7;
                custIndex++;
                tempCustName = "Customer" + custIndex;
            }

            string path;

            if (GameConstants.GetCityData("cookingTut_", 0) == 0 && (i==1 || i==2))
            {

                tempCustName = "Customer" + Random.Range(3,7);
                path = "Prefab/Mumbai/" + tempCustName;
            }
            else
                path = "Prefab/Mumbai/" + tempCustName;

            GameObject custObjet = Instantiate(Resources.Load<GameObject>(path));

            #region 1. Creating main customer
            //Assign the newly created Image GameObject as a Child of the Parent canvas.
            custObjet.GetComponent<RectTransform>().SetParent(GameObject.FindGameObjectWithTag("ParentCanvas").transform); 
            custObjet.GetComponent<RectTransform>().SetAsFirstSibling();

            //size and position of customer
            custObjet.GetComponent<RectTransform>().localScale = new Vector2(1.1f, 0.9f);
            custObjet.GetComponent<RectTransform>().localPosition = new Vector2(-1400.0f, 223.0f);

            //activateobject and script
            custObjet.SetActive(true);
            custObjet.GetComponent<CustomerController>().enabled = true;
            if (custIndex > 0 && custIndex < 4)
                custObjet.GetComponent<CustomerController>().gender = 0;
            else
                custObjet.GetComponent<CustomerController>().gender = 1;

            //Change laye,tag and name
            custObjet.layer = 15;
            custObjet.tag = "Customer";
            custObjet.name = custName;

            #endregion



            #region 2. Creating parent for Normal order

            GameObject orderParent = new GameObject();
            Image OrderBG = orderParent.AddComponent<Image>();
            orderParent.GetComponent<Image>().sprite = OrderBGSprite;

            //Assign the newly created Image GameObject as a Child of the Customer created above.
            orderParent.GetComponent<RectTransform>().SetParent(GameObject.Find(custName).transform);

            //Size and position
            orderParent.GetComponent<RectTransform>().localScale = new Vector2(1.0f, 2.5f);
            orderParent.GetComponent<RectTransform>().localPosition = new Vector2(143.0f, 20.0f);

            orderParent.layer = 5;
            orderParent.tag = "OrderDetails";
            orderParent.name = orderName;
            orderParent.SetActive(true);

            //To create sprites of order
            totalDishCounter = OrderController.CustomerOrder[i - 1].dish1 +
                               OrderController.CustomerOrder[i - 1].dish2 +
                               OrderController.CustomerOrder[i - 1].dish3 +
                               OrderController.CustomerOrder[i - 1].beverage;

            CreateOrder(totalDishCounter, i - 1, orderName);

            //Assign order parent to orrder game object in CustomerController script
            GameObject.Find(custName).GetComponent<CustomerController>().orederRequest = GameObject.Find(orderName);
            GameObject.Find(orderName).SetActive(false);


            #endregion

            #region 3. Creating timer for normal order

            //Timer panel creation

            GameObject timer = new GameObject();
            Image timerBg = timer.AddComponent<Image>();

            //Assign the newly created Image GameObject as a Child of the Customer created in first step.
            timer.GetComponent<RectTransform>().SetParent(GameObject.Find(custName).transform);

            timer.GetComponent<RectTransform>().localScale = new Vector2(0.1f, 2.3f);
            timer.GetComponent<RectTransform>().localPosition = new Vector2(177.0f, 26.0f);
            timer.GetComponent<Image>().sprite = timerBG;
            timer.name = timerName;

            GameObject.Find(custName).GetComponent<CustomerController>().timer = GameObject.Find(timerName);
            

            //Creation of filler
            GameObject timerFillobj = new GameObject();
            Image timerBar = timerFillobj.AddComponent<Image>();

            //Assign the newly created Image GameObject as a Child of the timer BG created above.
            timerFillobj.GetComponent<RectTransform>().SetParent(GameObject.Find(timerName).transform);

            timerFillobj.GetComponent<RectTransform>().localScale = new Vector2(0.4f, 1.0f);
            timerFillobj.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

            //Change the tyoe of image to fill
            timerFillobj.GetComponent<Image>().sprite = timerFill;
            timerFillobj.GetComponent<Image>().type = Image.Type.Filled;
            timerFillobj.GetComponent<Image>().fillMethod = Image.FillMethod.Vertical;

            timerFillobj.name = timerFillName;

            GameObject.Find(custName).GetComponent<CustomerController>().timerFill = GameObject.Find(timerFillName).GetComponentInChildren<Image>();

            GameObject.Find(timerName).SetActive(false);
            #endregion

            #region 4. Creation of Special Dish
            if (OrderController.CustomerOrder[i-1].specialDish)
            {
                //creation of Special order parent
                GameObject splOrderParent = new GameObject();
                Image splOrderBG = splOrderParent.AddComponent<Image>();
                splOrderParent.GetComponent<Image>().sprite = spOrderBGSprite;

                //Assign the newly created Image GameObject as a Child of the Customer created above.
                splOrderParent.GetComponent<RectTransform>().SetParent(GameObject.Find(custName).transform);

                //Size and position
                splOrderParent.GetComponent<RectTransform>().localScale = new Vector2(0.85f, 1.2f);
                splOrderParent.GetComponent<RectTransform>().localPosition = new Vector2(-125.0f, 80.0f);

                splOrderParent.layer = 5;
                splOrderParent.tag = "splDish";
                splOrderParent.name = specialOrder;
                splOrderParent.SetActive(true);

               
                //Timer fill bar
                GameObject splTimerFillobj = new GameObject();
                Image splTimerBar = splTimerFillobj.AddComponent<Image>();

                //Assign the newly created Image GameObject as a Child of the timer BG created above.
                splTimerFillobj.GetComponent<RectTransform>().SetParent(GameObject.Find(specialOrder).transform);

                splTimerFillobj.GetComponent<RectTransform>().localScale = new Vector2(0.08f, 0.57f);
                splTimerFillobj.GetComponent<RectTransform>().localPosition = new Vector2(-33.3f, 12.0f);

                //Change the tyoe of image to fill
                splTimerFillobj.GetComponent<Image>().sprite = spTimerFill;
                splTimerFillobj.GetComponent<Image>().type = Image.Type.Filled;
                splTimerFillobj.GetComponent<Image>().fillMethod = Image.FillMethod.Vertical;

                splTimerFillobj.name = splTimerFillName;

                GameObject.Find(custName).GetComponent<CustomerController>().splOrderTimerFill = GameObject.Find(splTimerFillName).GetComponentInChildren<Image>();
                
                //Assign Dish Image to the request
                GameObject dish4 = new GameObject();
                Image dish4Img = dish4.AddComponent<Image>();

                dish4.GetComponent<RectTransform>().SetParent(GameObject.Find(specialOrder).transform);
                dish4.GetComponent<RectTransform>().localScale = new Vector2(0.5f, 0.45f);
                dish4.GetComponent<RectTransform>().localPosition = new Vector2(7.0f,14.0f);
                dish4.SetActive(true);

                dish4.GetComponent<Image>().sprite = dish4Sprites;
                dish4.layer = 5;
                dish4.tag = "splDish";
                dish4.name = "splDish";

                GameObject.Find(custName).GetComponent<CustomerController>().orderTags.Add(dish4.tag);

                GameObject.Find(custName).GetComponent<CustomerController>().SplOrederRequest = GameObject.Find(specialOrder);
                GameObject.Find(specialOrder).SetActive(false);
            }
            #endregion
        }

        LevelController.instance.StartLevel();
        //Debug.Log("Customer Creation Done");

    }

    /// <summary>
    /// Creating sprites and assiging them to Order parent created in second step of Customer creation
    /// </summary>
    /// <param name="dishCount">Items present in order per customer</param>
    /// <param name="custID">ID or index of customer</param>
    /// <param name="orderName">To set order to the proper parent</param>

    public void CreateOrder(int dishCount, int custID, string orderName)
    {
        //To display image according to Items present in One order
        switch (dishCount)
        {
            case 1:
                dishPosition = new Vector2(-7.0f, -0.5f);
                positionOffset = new Vector2(0.0f, 0.0f);
                break;
            case 2:
                dishPosition = new Vector2(-10.0f, 23.0f);
                positionOffset = new Vector2(0.0f, 41.0f);

                break;
            case 3:
                dishPosition = new Vector2(-10.0f, 32.5f);
                positionOffset = new Vector2(0.0f, 30.5f);
                break;
        }

        int tagCount = 0;

        for (int i = 1; i <= 3; i++)
        {

            #region 2.1 Creation of Dish 1
            if (OrderController.CustomerOrder[custID].dish1 > 0)
            {
                GameObject dish1 = new GameObject();
                Image dish1Img = dish1.AddComponent<Image>();

                //Assign the newly created Image GameObject as a Child of the Order Panel.
                dish1.GetComponent<RectTransform>().SetParent(GameObject.Find(orderName).transform); 


                dish1.GetComponent<RectTransform>().localScale = new Vector2(0.75f, 0.35f);
                dish1.GetComponent<RectTransform>().localPosition = dishPosition;
                dish1.SetActive(true);
                dish1.GetComponent<Image>().sprite = dish1Sprites[0];
                dish1.layer = 5;
                dish1.tag = "dish1";
                dish1.name = "dish1";

                dishPosition -= positionOffset;

                string dishTag = "dish1";

                #region Selection of Tag
                if (OrderController.CustomerOrder[custID].topping11>0 && OrderController.CustomerOrder[custID].topping12 >0 && OrderController.CustomerOrder[custID].topping13>0)
                {
                    dishTag = "d1t1t2t3";
                }
                else if (OrderController.CustomerOrder[custID].topping11 > 0 && OrderController.CustomerOrder[custID].topping12 > 0)
                {
                    dishTag = "d1t1t2";
                }
                else if (OrderController.CustomerOrder[custID].topping11 > 0  && OrderController.CustomerOrder[custID].topping13 > 0)
                {
                    dishTag = "d1t1t3";
                }
                else if (OrderController.CustomerOrder[custID].topping12 > 0 && OrderController.CustomerOrder[custID].topping13 > 0)
                {
                    dishTag = "d1t2t3";
                }
                else if (OrderController.CustomerOrder[custID].topping11 > 0)
                {
                    dishTag = "d1t1";
                }
                else if (OrderController.CustomerOrder[custID].topping12 > 0)
                {
                    dishTag = "d1t2";
                }
                else if (OrderController.CustomerOrder[custID].topping13 > 0)
                {
                    dishTag = "d1t3";
                }
                #endregion

                if (OrderController.CustomerOrder[custID].topping11 > 0)
                {
                    GameObject topping1 = new GameObject();
                    Image topping1Img = topping1.AddComponent<Image>();

                    //Assign the newly created Image GameObject as a Child of the dish1 Panel.
                    topping1.GetComponent<RectTransform>().SetParent(GameObject.Find("dish1").transform);

                    topping1.GetComponent<Image>().sprite = dish1Toppings[0];
                    topping1.GetComponent<RectTransform>().localScale = new Vector2(1.0f, 1.0f);
                    topping1.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    topping1.SetActive(true);
                    dish1.layer = 5;
                    topping1.name = "topping1";
                    OrderController.CustomerOrder[custID].topping11--;
                }

                if (OrderController.CustomerOrder[custID].topping12 > 0)
                {
                    GameObject topping2 = new GameObject();
                    Image topping2Img = topping2.AddComponent<Image>();

                    //Assign the newly created Image GameObject as a Child of the dish1 Panel.
                    topping2.GetComponent<RectTransform>().SetParent(GameObject.Find("dish1").transform);

                    topping2.GetComponent<Image>().sprite = dish1Toppings[1];
                    topping2.GetComponent<RectTransform>().localScale = new Vector2(1.2f, 1.2f);
                    topping2.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    topping2.SetActive(true);
                    dish1.layer = 5;
                    topping2.name = "topping2";
                    OrderController.CustomerOrder[custID].topping12--;
                }

                if (OrderController.CustomerOrder[custID].topping13 > 0)
                {
                    GameObject topping3 = new GameObject();
                    Image topping3Img = topping3.AddComponent<Image>();

                    //Assign the newly created Image GameObject as a Child of the dish1 Panel.
                    topping3.GetComponent<RectTransform>().SetParent(GameObject.Find("dish1").transform);

                    topping3.GetComponent<Image>().sprite = dish1Toppings[2];
                    topping3.GetComponent<RectTransform>().localScale = new Vector2(1.2f, 1.2f);
                    topping3.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    topping3.SetActive(true);
                    dish1.layer = 5;
                    topping3.name = "topping3";
                    OrderController.CustomerOrder[custID].topping13--;
                }

                dish1.tag = dishTag;
                
                GameObject.Find(custName).GetComponent<CustomerController>().orderTags.Add(dish1.tag);
                tagCount++;
                OrderController.CustomerOrder[custID].dish1--;

            }
#endregion

            #region 2.2 Creation of Dish 2
            if (OrderController.CustomerOrder[custID].dish2 > 0)
            {
                GameObject dish2 = new GameObject();
                Image dish2Img = dish2.AddComponent<Image>();

                dish2.GetComponent<RectTransform>().SetParent(GameObject.Find(orderName).transform);
                dish2.GetComponent<RectTransform>().localScale = new Vector2(0.85f, 0.4f);
                dish2.GetComponent<RectTransform>().localPosition = dishPosition;
                dish2.SetActive(true);

                dish2.GetComponent<Image>().sprite = dish2Sprites[0];
                dish2.layer = 5;
                dish2.tag = "dish2";
                dish2.name = "dish2";
                dishPosition -= positionOffset;

                string dishTag = "dish2";

                #region Selection of Tag
                if (OrderController.CustomerOrder[custID].topping21 > 0 && OrderController.CustomerOrder[custID].topping22 > 0 && OrderController.CustomerOrder[custID].topping23 > 0)
                {
                    dishTag = "d2t1t2t3";
                }
                else if (OrderController.CustomerOrder[custID].topping21 > 0 && OrderController.CustomerOrder[custID].topping22 > 0)
                {
                    dishTag = "d2t1t2";
                }
                else if (OrderController.CustomerOrder[custID].topping21 > 0 && OrderController.CustomerOrder[custID].topping23 > 0)
                {
                    dishTag = "d2t1t3";
                }
                else if (OrderController.CustomerOrder[custID].topping22 > 0 && OrderController.CustomerOrder[custID].topping23 > 0)
                {
                    dishTag = "d2t2t3";
                }
                else if (OrderController.CustomerOrder[custID].topping21 > 0)
                {
                    dishTag = "d2t1";
                }
                else if (OrderController.CustomerOrder[custID].topping22 > 0)
                {
                    dishTag = "d2t2";
                }
                else if (OrderController.CustomerOrder[custID].topping23 > 0)
                {
                    dishTag = "d2t3";
                }
                #endregion

                if (OrderController.CustomerOrder[custID].topping21 > 0)
                {
                    GameObject topping1 = new GameObject();
                    Image topping1Img = topping1.AddComponent<Image>();

                    //Assign the newly created Image GameObject as a Child of the dish1 Panel.
                    topping1.GetComponent<RectTransform>().SetParent(GameObject.Find("dish2").transform);

                    topping1.GetComponent<Image>().sprite = dish2Toppings[0];
                    topping1.GetComponent<RectTransform>().localScale = new Vector2(1.0f, 1.0f);
                    topping1.GetComponent<RectTransform>().localPosition = Vector3.zero;

                    topping1.SetActive(true);

                    topping1.name = "topping1";
                    OrderController.CustomerOrder[custID].topping21--;
                }

                if (OrderController.CustomerOrder[custID].topping22 > 0)
                {
                    GameObject topping2 = new GameObject();
                    Image topping2Img = topping2.AddComponent<Image>();

                    //Assign the newly created Image GameObject as a Child of the dish1 Panel.
                    topping2.GetComponent<RectTransform>().SetParent(GameObject.Find("dish2").transform);

                    topping2.GetComponent<Image>().sprite = dish2Toppings[1];
                    topping2.GetComponent<RectTransform>().localScale = new Vector2(1.0f, 1.0f);
                    topping2.GetComponent<RectTransform>().localPosition =new Vector3(0.0f,-8.0f,0.1f);

                    topping2.SetActive(true);

                    topping2.name = "topping2";
                    OrderController.CustomerOrder[custID].topping22--;
                }

                if (OrderController.CustomerOrder[custID].topping23 > 0)
                {
                    GameObject topping3 = new GameObject();
                    Image topping3Img = topping3.AddComponent<Image>();

                    //Assign the newly created Image GameObject as a Child of the dish1 Panel.
                    topping3.GetComponent<RectTransform>().SetParent(GameObject.Find("dish2").transform);

                    topping3.GetComponent<Image>().sprite = dish2Toppings[2];
                    topping3.GetComponent<RectTransform>().localScale = new Vector2(1.0f, 1.0f);
                    topping3.GetComponent<RectTransform>().localPosition = Vector3.zero;

                    topping3.SetActive(true);

                    topping3.name = "topping3";
                    OrderController.CustomerOrder[custID].topping23--;
                }

                dish2.tag = dishTag;

                GameObject.Find(custName).GetComponent<CustomerController>().orderTags.Add(dish2.tag);
                tagCount++;
                OrderController.CustomerOrder[custID].dish2--;
            }
            #endregion

            #region 2.3 Creation of Dish 3
            if (OrderController.CustomerOrder[custID].dish3 > 0)
            {
                GameObject dish3 = new GameObject();
                Image dish3Img = dish3.AddComponent<Image>();

                dish3.GetComponent<RectTransform>().SetParent(GameObject.Find(orderName).transform);
                dish3.GetComponent<RectTransform>().localScale = new Vector2(0.65f, 0.25f);
                dish3.GetComponent<RectTransform>().localPosition = dishPosition;
                dish3.SetActive(true);

                dish3.GetComponent<Image>().sprite = dish3Sprites;
                dish3.layer = 5;
                dish3.tag = "dish3";
                dish3.name = "dish3";
                dishPosition -= positionOffset;

                GameObject.Find(custName).GetComponent<CustomerController>().orderTags.Add(dish3.tag);
                tagCount++;
                OrderController.CustomerOrder[custID].dish3--;

            }
            #endregion

            #region 2.4 Creation of Beverage
            if (OrderController.CustomerOrder[custID].beverage > 0)
            {
                GameObject beverage = new GameObject(); //Create the GameObject
                Image beverageImg = beverage.AddComponent<Image>(); //Add the Image Component script
                beverage.GetComponent<RectTransform>().SetParent(GameObject.Find(orderName).transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
                beverage.GetComponent<RectTransform>().localScale = new Vector2(0.3f, 0.25f);
                beverage.GetComponent<RectTransform>().localPosition = dishPosition;
                beverage.SetActive(true); //Activate the GameObject
                beverage.GetComponent<Image>().sprite = beverageSprites;
                beverage.layer = 5;
                beverage.tag = "beverage";
                beverage.name = "beverage";
                dishPosition -= positionOffset;
                GameObject.Find(custName).GetComponent<CustomerController>().orderTags.Add("beverage");
                tagCount++;
                OrderController.CustomerOrder[custID].beverage--;
            }
            #endregion
            
        }



    }
}
