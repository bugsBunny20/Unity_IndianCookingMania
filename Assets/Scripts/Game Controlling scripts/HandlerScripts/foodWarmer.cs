using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class foodWarmer : MonoBehaviour,IDropHandler
{
    public bool alreadyOccupied=false;
    public GameObject[] readyIngredient;
    public GameObject[] rawDish;


    public void OnDrop(PointerEventData eventData)
    {
        int rawItemIndex = eventData.pointerDrag.GetComponent<FoodController>().rawItemIndex;


        if (eventData.pointerDrag.tag == "rawDish1" && !alreadyOccupied)
        {
            alreadyOccupied = true;
            readyIngredient[0].SetActive(true);

            eventData.pointerDrag.transform.gameObject.SetActive(false);
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

            eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.transform.localScale = Vector3.one;
            eventData.pointerDrag.transform.SetAsLastSibling();

            eventData.pointerDrag.GetComponent<FoodController>().rawItem.GetComponent<IngredientManager>().pauseBurning = false;
            eventData.pointerDrag.GetComponent<FoodController>().rawItem.SetActive(false);
            eventData.pointerDrag.GetComponent<FoodController>().rawItem.GetComponent<Image>().enabled = true;

            IngredientController.instance.fp1Occupied[rawItemIndex] = false;
            IngredientController.instance.fryPan1Counter--;
            //eventData.pointerDrag.GetComponent<FoodController>().rawItem.GetComponent<IngredientManager>().itemPlaced = true;
            

        }
        else 
        if (eventData.pointerDrag.tag == "rawDish2" && !alreadyOccupied)
        {
            alreadyOccupied = true;
            readyIngredient[1].SetActive(true);

            eventData.pointerDrag.transform.gameObject.SetActive(false);
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

            eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.transform.localScale = Vector3.one;
            eventData.pointerDrag.transform.SetAsLastSibling();

            //rawDish[1].gameObject.SetActive(false);
            //rawDish[1].gameObject.GetComponent<Image>().enabled = true;
            //rawDish[1].gameObject.GetComponent<IngredientManager>().pauseBurning = false;

            eventData.pointerDrag.GetComponent<FoodController>().rawItem.GetComponent<IngredientManager>().pauseBurning = false;
            eventData.pointerDrag.GetComponent<FoodController>().rawItem.SetActive(false);
            eventData.pointerDrag.GetComponent<FoodController>().rawItem.GetComponent<Image>().enabled = true;


            IngredientController.instance.fp2Occupied[rawItemIndex] = false;
            IngredientController.instance.fryPan2Counter--;
           

        }
        else
        {
            eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.transform.localScale = Vector3.one;
            eventData.pointerDrag.transform.SetAsLastSibling();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelController.instance.levelEnded)
        {
            alreadyOccupied = false;
            readyIngredient[0].SetActive(false);
            readyIngredient[1].SetActive(false);
        }
    }
}
