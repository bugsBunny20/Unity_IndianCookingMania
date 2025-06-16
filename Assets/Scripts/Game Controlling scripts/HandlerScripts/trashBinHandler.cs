using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class trashBinHandler : MonoBehaviour,IDropHandler
{
    public Text costDeducted;
    public GameObject throwSound;

    public void OnDrop(PointerEventData eventData)
    {
        

        int rawItemIndex = eventData.pointerDrag.GetComponent<FoodController>().rawItemIndex;
        throwSound.SetActive(true);

        switch (eventData.pointerDrag.tag)
        {
            case "topping1":
                costDeducted.text = "-2";
                LevelController.instance.coinsEarned -= 2;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();
                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                break;

            case "topping2":
                costDeducted.text = "-2";
                LevelController.instance.coinsEarned -= 2;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();
                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                break;

            case "topping3":
                costDeducted.text = "-2";
                LevelController.instance.coinsEarned -= 2;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();
                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                break;

            case "dish3":
                costDeducted.text = "-3";
                LevelController.instance.coinsEarned -= 3;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();
                eventData.pointerDrag.transform.gameObject.SetActive(false);
                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

                break;

            case "splDish":
                costDeducted.text = "-1";
                LevelController.instance.coinsEarned -= 1;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();
                eventData.pointerDrag.transform.gameObject.SetActive(false);
                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

                break;

            case "rawDish1":
                costDeducted.text = "-6";
                LevelController.instance.coinsEarned -= 6;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();

                eventData.pointerDrag.GetComponent<FoodController>().rawItem.SetActive(false);
                eventData.pointerDrag.GetComponent<FoodController>().rawItem.GetComponent<Image>().enabled=true;
                eventData.pointerDrag.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

                IngredientController.instance.fp1Occupied[rawItemIndex] = false;
                IngredientController.instance.fryPan1Counter--;

                eventData.pointerDrag.transform.gameObject.SetActive(false);

                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();

                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                if (GameConstants.GetBurnTut() == 0)
                    LevelController.instance.ShowGlobalTut(46);

                break;

            case "rawDish2":
                costDeducted.text = "-6";
                LevelController.instance.coinsEarned -= 6;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();

                eventData.pointerDrag.GetComponent<FoodController>().rawItem.SetActive(false);
                eventData.pointerDrag.GetComponent<FoodController>().rawItem.GetComponent<Image>().enabled = true;
                eventData.pointerDrag.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                eventData.pointerDrag.transform.gameObject.SetActive(false);

                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();

                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                

                IngredientController.instance.fp2Occupied[rawItemIndex] = false;
                IngredientController.instance.fryPan2Counter--;
                if (GameConstants.GetBurnTut() == 0)
                    LevelController.instance.ShowGlobalTut(46);
                break;

            case "beverage":
                costDeducted.text = "-3";
                LevelController.instance.coinsEarned -= 3;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();
                eventData.pointerDrag.transform.gameObject.SetActive(false);

                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();

                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                IngredientController.instance.IngredientClicked("Beverage");

                break;
        }

        switch (eventData.pointerDrag.gameObject.layer)
        {
            case 11:
                
                costDeducted.text = "-3";
                LevelController.instance.coinsEarned -= 3;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();
                eventData.pointerDrag.transform.gameObject.SetActive(false);

                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();

                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

                IngredientController.instance.tt1Occupied[rawItemIndex] = false;
                IngredientController.instance.tableTop1counter--;
                break;

            case 12:
                costDeducted.text = "-3";
                LevelController.instance.coinsEarned -= 3;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();
                eventData.pointerDrag.transform.gameObject.SetActive(false);

                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();

                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

                IngredientController.instance.tt2Occupied[rawItemIndex] = false;
                IngredientController.instance.tableTop2counter--;

                break;

            case 18:
                costDeducted.text = "-9";
                LevelController.instance.coinsEarned -= 9;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();
                eventData.pointerDrag.transform.gameObject.SetActive(false);

                eventData.pointerDrag.tag = "dish1";


                eventData.pointerDrag.transform.GetChild(0).gameObject.SetActive(false);
                eventData.pointerDrag.transform.GetChild(1).gameObject.SetActive(false);
                eventData.pointerDrag.transform.GetChild(2).gameObject.SetActive(false);

                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();
                
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                IngredientController.instance.tt1Occupied[rawItemIndex] = false;
                IngredientController.instance.tableTop1counter--;
                break;

            case 19:
                costDeducted.text = "-9";
                LevelController.instance.coinsEarned -= 9;
                LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();
                eventData.pointerDrag.transform.gameObject.SetActive(false);

                eventData.pointerDrag.tag = "dish2";

                eventData.pointerDrag.transform.GetChild(0).gameObject.SetActive(false);
                eventData.pointerDrag.transform.GetChild(1).gameObject.SetActive(false);
                eventData.pointerDrag.transform.GetChild(2).gameObject.SetActive(false);

                eventData.pointerDrag.transform.parent = eventData.pointerDrag.GetComponent<FoodController>().originalParent;
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
                eventData.pointerDrag.transform.localScale = Vector3.one;
                eventData.pointerDrag.transform.SetAsLastSibling();

                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                IngredientController.instance.tt2Occupied[rawItemIndex] = false;
                IngredientController.instance.tableTop2counter--;
                break;

        }


        StartCoroutine(disableText());
    }


   IEnumerator disableText()
    {
        yield return new WaitForSeconds(1.5f);
        costDeducted.text = "";
        throwSound.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
