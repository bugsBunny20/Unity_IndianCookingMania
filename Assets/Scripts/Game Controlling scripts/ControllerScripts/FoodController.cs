using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FoodController : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    public static FoodController instance;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public GameObject rawItem;
    public int rawItemIndex;

    public Transform originalParent;

    public  bool itemIsInHand;
    public Camera cam;

    PointerEventData orderData;

    private void Awake()
    {
        canvas= GameObject.FindGameObjectWithTag("ParentCanvas").GetComponent<Canvas>(); 
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform= GetComponent<RectTransform>();
        originalParent = this.transform.parent;
    }

    void start()
    {
    }


    public void Update()
    {
        if (LevelController.instance.levelEnded)
            disableGameObject();
       
    }

    private void OnDisable()
    {
        Debug.LogError("Food Controller Disabled");
    }

    public void LateUpdate()
    {
    }

    public void disableGameObject()
    {
        if(this.gameObject.layer!=24)
        {
            this.gameObject.SetActive(false);
            this.transform.parent = originalParent;
            this.transform.SetAsLastSibling();
            if (this.tag == "beverage")
            {
                transform.localPosition = new Vector3(-7.8f, -7.2f);
            }
            transform.localPosition = Vector3.zero;
            this.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
            canvasGroup.blocksRaycasts = true;
            if (this.gameObject.layer == 20 || this.gameObject.layer == 21)
                rawItem.GetComponent<IngredientManager>().pauseBurning = false;
            if(this.gameObject.layer ==18 || this.gameObject.layer == 19)
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
                this.transform.GetChild(1).gameObject.SetActive(false);
                this.transform.GetChild(2).gameObject.SetActive(false);
            }
            
        }
        else
        {
            this.transform.parent = originalParent;
            this.transform.SetAsLastSibling();
            
            
            transform.localPosition = Vector3.zero;
            this.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
            canvasGroup.blocksRaycasts = true;

            if (this.gameObject.layer == 18)
                this.tag = "dish1";
            else if (this.gameObject.layer == 19)
                this.tag = "dish2";
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,1.0f);
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.parent = originalParent;
        this.transform.SetAsLastSibling();
        if (this.tag == "beverage")
        {
            transform.localPosition = new Vector3(-7.8f, -7.2f);
        }
        transform.localPosition = Vector3.zero;
        this.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
        canvasGroup.blocksRaycasts = true;
        if (this.gameObject.layer == 20 || this.gameObject.layer == 21)
            rawItem.GetComponent<IngredientManager>().pauseBurning = false;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        this.transform.parent = GameObject.FindGameObjectWithTag("ParentCanvas").transform;
        this.GetComponent<RectTransform>().SetAsLastSibling();

        if ((this.gameObject.layer == 20 || this.gameObject.layer == 21) && originalParent.gameObject.layer !=10)
        {
            rawItem.GetComponent<IngredientManager>().pauseBurning = true;

            if (LevelController.instance.tutorialCounter == 0)
            {
                LevelController.instance.ShowTutorial(4);
            }

            if (LevelController.instance.tutorialCounter == 3 && LevelController.instance.tutChecked)
            {
                LevelController.instance.ShowTutorial(25);
            }
            else if(LevelController.instance.tutorialCounter == 4)
            {
                LevelController.instance.ShowTutorial(28);

            }

        }

        
        if (this.gameObject.layer == 18 || this.gameObject.layer == 19)
        {
            if (LevelController.instance.tutorialCounter == 0)
            {
                LevelController.instance.ShowTutorial(6);
            }
        }

        if (this.gameObject.layer == 16)
        {
            if (LevelController.instance.tutorialCounter == 1)
            {
                LevelController.instance.ShowTutorial(11);
            }
        }

        if( (this.gameObject.layer == 20 || this.gameObject.layer == 21) && GameConstants.GetBurnTut() == 0 && this.GetComponent<Image>().color != new Color32(255, 255, 255, 255))
        {
            LevelController.instance.ShowGlobalTut(45);
        }

    }


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.tag =="topping1" && (this.gameObject.layer == 18 || this.gameObject.layer == 19))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("In chatni topping.");
            if (this.tag == "dish1")
                this.tag = "d1t1";
           else if (this.tag == "d1t2")
                this.tag = "d1t1t2";
            else if (this.tag == "d1t3")
                this.tag = "d1t1t3";
            else if (this.tag == "d1t2t3")
                this.tag = "d1t1t2t3";

            else if (this.tag == "dish2")
                this.tag = "d2t1";
            else if (this.tag == "d2t2")
                this.tag = "d2t1t2";
            else if (this.tag == "d2t3")
                this.tag = "d2t1t3";
            else if (this.tag == "d2t2t3")
                this.tag = "d2t1t2t3";
        }

        if (eventData.pointerDrag.tag == "topping2" && (this.gameObject.layer == 18 || this.gameObject.layer == 19))
        {
            transform.GetChild(1).gameObject.SetActive(true);
            Debug.Log("In kanda topping.");
            if (this.tag == "dish1")
                this.tag = "d1t2";
            else if (this.tag == "d1t1")
                this.tag = "d1t1t2";
            else if (this.tag == "d1t3")
                this.tag = "d1t2t3";
            else if (this.tag == "d1t1t3")
                this.tag = "d1t1t2t3";

            else if (this.tag == "dish2")
                this.tag = "d2t2";
            else if (this.tag == "d2t1")
                this.tag = "d2t1t2";
            else if (this.tag == "d2t3")
                this.tag = "d2t1t3";
            else if (this.tag == "d2t1t3")
                this.tag = "d2t1t2t3";

        }

        if (eventData.pointerDrag.tag == "topping3" && (this.gameObject.layer == 18 || this.gameObject.layer == 19))
        {
            transform.GetChild(2).gameObject.SetActive(true);
            Debug.Log("In mirchi topping.");
            if (this.tag == "dish1")
                this.tag = "d1t3";
            else if (this.tag == "d1t1")
                this.tag = "d1t1t3";
            else if (this.tag == "d1t2")
                this.tag = "d1t2t3";
            else if (this.tag == "d1t1t2")
                this.tag = "d1t1t2t3";

            else if (this.tag == "dish2")
                this.tag = "d2t3";
            else if (this.tag == "d2t1")
                this.tag = "d2t1t3";
            else if (this.tag == "d2t2")
                this.tag = "d2t1t3";
            else if (this.tag == "d2t1t2")
                this.tag = "d2t1t2t3";

        }

        eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
        eventData.pointerDrag.transform.localPosition = Vector3.zero;
        eventData.pointerDrag.transform.localScale = Vector3.one;
        eventData.pointerDrag.transform.SetAsLastSibling();
    }

}
