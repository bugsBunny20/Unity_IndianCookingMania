using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IngredientManager : MonoBehaviour, IDropHandler
{
    public static IngredientManager instance;

    public GameObject dish1;
    public GameObject rawDish;

    public int ingrdientLayer;
    public string ingrdientTag;

    public bool isCooking;
    public bool startCooking;
    [SerializeField]
    public float cookingTime;
    public float tempCookingTime;
    public float burningTime;

    bool isCooked;

    public bool itemPlaced;
    public bool pauseBurning;

    float timeLeft;

    public void Awake()
    {
        if (instance = null)
            instance = this;
        ingrdientLayer = this.gameObject.layer;
        ingrdientTag = this.tag;
        burningTime = 5.0f;
        CookingTime();
        isCooked = false;
        startCooking = false;
    }

    public void CookingTime()
    {
        switch (ingrdientLayer)
        {

            case 13:
                pauseBurning = false;
                tempCookingTime = cookingTime = UpgradeSystem.instance.fryPan1cookTime;
                break;

            case 14:
                pauseBurning = false;
                tempCookingTime = cookingTime = UpgradeSystem.instance.fryPan2cookTime;
                break;


            case 16:
                //Debug.Log("Managing Beverage********************************************");
                tempCookingTime = cookingTime = UpgradeSystem.instance.beverageTimer;
                break;

            case 23:
                tempCookingTime= cookingTime = UpgradeSystem.instance.fryPan3cookTime;
                break;
        }


    }

    public void LateUpdate()
    {
        if (LevelController.instance.levelEnded)
        {
            this.gameObject.SetActive(false);
            this.gameObject.GetComponent<Image>().enabled = true;
        }

        
    }


    public void disableGameObject()
    {
        this.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (this.gameObject.layer == 11 && eventData.pointerDrag.GetComponent<Image>().color == new Color32(255, 255, 255, 255))
        {
            int rawItemIndex = eventData.pointerDrag.GetComponent<FoodController>().rawItemIndex;



            if (eventData.pointerDrag.tag == "rawDish1")
            {
                if (eventData.pointerDrag.GetComponent<FoodController>().originalParent.gameObject.layer == 10)
                {
                    eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                    eventData.pointerDrag.transform.parent.transform.parent.GetComponent<foodWarmer>().alreadyOccupied = false;
                }

                else
                {
                    eventData.pointerDrag.GetComponent<FoodController>().rawItem.GetComponent<IngredientManager>().itemPlaced = true;
                    eventData.pointerDrag.GetComponent<FoodController>().rawItem.GetComponent<IngredientManager>().pauseBurning = false;
                    eventData.pointerDrag.GetComponent<FoodController>().rawItem.gameObject.SetActive(false);
                    eventData.pointerDrag.GetComponent<FoodController>().rawItem.gameObject.GetComponent<Image>().enabled = true;
                    IngredientController.instance.fp1Occupied[rawItemIndex] = false;
                    IngredientController.instance.fryPan1Counter--;
                }
                //Debug.Log("VadaPav Done");


                dish1.SetActive(true);
                dish1.tag = "dish1";
                this.gameObject.SetActive(false);

                //isCooked = false;
                if (eventData.pointerDrag.gameObject.layer != 24)
                {
                    eventData.pointerDrag.transform.gameObject.SetActive(false);
                    eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                    eventData.pointerDrag.transform.localScale = Vector3.one;
                    eventData.pointerDrag.GetComponent<RectTransform>().localPosition = Vector3.zero;
                }

                if (LevelController.instance.tutorialCounter == 0)
                {
                    LevelController.instance.ShowTutorial(5);
                }
            }
            else
            {
                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.GetComponent<RectTransform>().localPosition = Vector3.zero;
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

            }
        }
        else if (this.gameObject.layer == 12 && eventData.pointerDrag.GetComponent<Image>().color == new Color32(255, 255, 255, 255))
        {
            int rawItemIndex = eventData.pointerDrag.GetComponent<FoodController>().rawItemIndex;

            if (eventData.pointerDrag.tag == "rawDish2" && eventData.pointerDrag.GetComponent<Image>().color == new Color32(255, 255, 255, 255))
            {
                if (eventData.pointerDrag.GetComponent<FoodController>().originalParent.gameObject.layer == 10)
                {
                    eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;

                    eventData.pointerDrag.transform.parent.transform.parent.GetComponent<foodWarmer>().alreadyOccupied = false;

                }

                else
                {
                    eventData.pointerDrag.GetComponent<FoodController>().rawItem.GetComponent<IngredientManager>().itemPlaced = true;

                    eventData.pointerDrag.GetComponent<FoodController>().rawItem.gameObject.SetActive(false);
                    eventData.pointerDrag.GetComponent<FoodController>().rawItem.gameObject.GetComponent<Image>().enabled = true;
                    eventData.pointerDrag.GetComponent<FoodController>().rawItem.GetComponent<IngredientManager>().pauseBurning = false;

                    IngredientController.instance.fp2Occupied[rawItemIndex] = false;
                    IngredientController.instance.fryPan2Counter--;
                }

                if (eventData.pointerDrag.gameObject.layer != 24)
                {
                    eventData.pointerDrag.transform.gameObject.SetActive(false);
                    eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                    eventData.pointerDrag.transform.localScale = Vector3.one;
                    eventData.pointerDrag.GetComponent<RectTransform>().localPosition = Vector3.zero;
                }

                dish1.SetActive(true);
                dish1.tag = "dish2";

                this.gameObject.SetActive(false);
                //isCooked = false;


                if (LevelController.instance.tutorialCounter == 3)
                {
                    LevelController.instance.ShowTutorial(26);
                }
            }
            else
            {
                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.GetComponent<RectTransform>().localPosition = Vector3.zero;
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

            }
        }

        else
        {
            eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
            eventData.pointerDrag.transform.localScale = Vector3.one;
            eventData.pointerDrag.GetComponent<RectTransform>().localPosition = Vector3.zero;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.layer != 11 && this.gameObject.layer != 12 && isCooking)
        {
            CookingTime();
            StartCoroutine(CookingTimer(cookingTime));
            isCooking = false;
        }
        if (!LevelController.instance.PausePopUp.activeSelf && tempCookingTime<cookingTime)
        {
            Debug.Log("Inside cooking*****");
            StartCooking();
        }
    }

    public void StartCooking()
    {
        Debug.Log("Inside cooking*****");
        StartCoroutine(CookingTimer(tempCookingTime));
    }

    IEnumerator CookingTimer(float cookingTime)
    {
        isCooking = false;


        while (cookingTime >= -5 && !pauseBurning && !LevelController.instance.PausePopUp.activeSelf /*&& !LevelController.instance.pauseTimers*/)
        {
            //Debug.Log("**********Inside cooking of  " + this.gameObject.tag);
            cookingTime -= Time.deltaTime;
            tempCookingTime = cookingTime;

            if (cookingTime <= 0 && cookingTime > -1)
            {
                ChangeItem();
                if (this.gameObject.layer == 13 && LevelController.instance.tutorialCounter == 0)
                {
                    pauseBurning = true;
                    Debug.Log("Inside pause burning");
                }
                else if (this.gameObject.layer == 14 && (LevelController.instance.tutorialCounter == 3 || LevelController.instance.tutorialCounter == 4))
                    pauseBurning = true;
            }

            if (cookingTime <= -5)
            {
                ChangeItemBurn();
                if(GameConstants.GetBurnTut() == 0 && (!LevelController.instance.globalTut[3].activeSelf || !LevelController.instance.globalTut[3].activeSelf))
                {
                    Debug.Log("Burn******" + GameConstants.GetBurnTut());
                    LevelController.instance.startPositioning = false;
                    CustomerController.instance.pauseWaitingTime = true;
                    LevelController.instance.ShowGlobalTut(44);
                }
            }
            yield return 0;
        }
        //isCooked = true;
    }

    public void ChangeItem()
    {
        //if (this.tag == "beverage")
        //{
        //    //call anim
        //}
        //Debug.Log("Hi in here");

        switch (ingrdientTag)
        {
            case "fryPan11":
                this.gameObject.GetComponent<Image>().enabled = false;

                dish1.SetActive(true);
                if (LevelController.instance.tutorialCounter == 0)
                {
                    LevelController.instance.ShowTutorial(3);
                }
                //isCooked = false;
                //IngredientController.instance.fp1Occupied[0] = false;
                //IngredientController.instance.fryPan1Counter--;
                break;

            case "fryPan21":
                this.gameObject.GetComponent<Image>().enabled = false;
                dish1.SetActive(true);
                if (LevelController.instance.tutorialCounter == 3 && !LevelController.instance.tutChecked)
                {
                    LevelController.instance.ShowTutorial(24);
                }
                else if (LevelController.instance.tutorialCounter == 4)
                {
                    Debug.Log("Haddddnaaaa************************8");
                    LevelController.instance.ShowTutorial(27);
                }
                //isCooked = false;
                //IngredientController.instance.fp2Occupied[0] = false;
                //IngredientController.instance.fryPan2Counter--;
                break;


            case "fryPan3":
                this.gameObject.SetActive(false);
                isCooking = false;
                for (int i = 0; i <= UpgradeSystem.instance.currentFP3Index; i++)
                {
                    if (!IngredientController.instance.tt3Occupied[i])
                    {
                        IngredientController.instance.tableTop3[i].SetActive(true);
                        IngredientController.instance.tt3Occupied[i] = true;
                    }
                }
                if (LevelController.instance.tutorialCounter == 8)
                {
                    LevelController.instance.ShowTutorial(38);
                }
                //IngredientController.instance.fryPan3.GetComponent<Button>().interactable = true;

                break;

            case "beverage":
                this.gameObject.SetActive(false);
                switch (this.name)
                {
                    case "emptyTeaGlass1":
                        IngredientController.instance.chaiAnim[0].SetActive(false);
                        break;
                    case "emptyTeaGlass2":
                        IngredientController.instance.chaiAnim[1].SetActive(false);
                        break;
                    case "emptyTeaGlass3":
                        IngredientController.instance.chaiAnim[2].SetActive(false);
                        break;

                }
                dish1.SetActive(true);

                if (LevelController.instance.tutorialCounter == 1)
                {
                    LevelController.instance.ShowTutorial(10);
                }

                isCooked = false;
                //IngredientController.instance.fp1Occupied[0] = false;
                //IngredientController.instance.fryPan1Counter--;
                break;
        }

    }

    public void ChangeItemBurn()
    {

        switch (ingrdientTag)
        {
            case "fryPan11":
                this.gameObject.SetActive(false);
                this.gameObject.GetComponent<Image>().enabled = true;
                dish1.gameObject.GetComponent<Image>().color = new Color32(123, 112, 112, 255);

                break;

            case "fryPan21":
                this.gameObject.SetActive(false);
                this.gameObject.GetComponent<Image>().enabled = true;
                dish1.gameObject.GetComponent<Image>().color = new Color32(123, 112, 112, 255);

                break;

        }

    }
}
