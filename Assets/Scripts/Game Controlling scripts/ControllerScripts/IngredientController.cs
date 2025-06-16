using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class IngredientController : MonoBehaviour
{
    public static IngredientController instance;

    //Holds pngs of the food 
    public GameObject[] tableTop1;
    public GameObject[] tableTop2;
    public GameObject[] tableTop3;
    public GameObject[] fryPan1;
    public GameObject[] fryPan2;
    public GameObject fryPan3;
    public GameObject[] beverage;
    public GameObject[] chaiAnim;
    public GameObject[] splDish;


    //To check if tabletops or frypans are already occupied or not
    public bool[] tt1Occupied= { false, false, false };
    public bool[] tt2Occupied= { false, false, false };
    public bool[] tt3Occupied= { false, false, false };
    public bool[] kettleOccupied= { false, false, false };
    public bool[] fp1Occupied= { false, false, false,false};
    public bool[] fp2Occupied= { false, false, false,false};

    //To check how many places are unlocked
    public int tableTop1counter;
    public int tableTop2counter;
    public int tableTop3counter;
    public int fryPan1Counter;
    public int fryPan2Counter;
    public int beverageCounter;

    private bool patticeEnabled;

    int storingIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }

        else
            Destroy(this);
        storingIndex = SceneManager.GetActiveScene().buildIndex-2;
    }

    // Start is called before the first frame update
    void Start()
    {
        tableTop1counter = 0;
        fryPan1Counter = 0;
        beverageCounter = 0;
        tableTop2counter = 0;
        fryPan2Counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Once game is ended disable all the objects
        if (LevelController.instance.levelEnded)
            disableGameObject();

        if (LevelController.instance.LevelStarted && !patticeEnabled)
            EnablePattice();
    }

    public void EnablePattice()
    {
        patticeEnabled = true;
        for (int i = 1; i < 9; i++)
            if (GameConstants.GetCityData("splDishCount_" + storingIndex, i) == 1)
                splDish[i-1].SetActive(true);
    }

    public void disableGameObject()
    {
        fryPan1Counter = 0;
        fryPan2Counter = 0;
        beverageCounter = 0;
        tableTop1counter = 0;
        tableTop2counter = 0;
        for(int i = 0; i < kettleOccupied.Length; i++)
        {
            chaiAnim[i].SetActive(false);
            kettleOccupied[i] = false;
        }

        for(int i = 0; i < tt1Occupied.Length; i++)
        {
            tt1Occupied[i] = false;
        }

        for (int i = 0; i < tt2Occupied.Length; i++)
        {
            tt2Occupied[i] = false;
        }

        for (int i = 0; i < tt3Occupied.Length; i++)
        {
            tt3Occupied[i] = false;
        }

        for (int i = 0; i < fp1Occupied.Length; i++)
        {
            fp1Occupied[i] = false;
        }

        for (int i = 0; i < fp2Occupied.Length; i++)
        {
            fp2Occupied[i] = false;
        }
        patticeEnabled = false;
    }


    public void IngredientClicked(string ingredientName)
    {
        switch (ingredientName)
        {
            case "base1":
                if (!tt1Occupied[0] && tableTop1counter <= UpgradeSystem.instance.currentTT1Index)
                {
                    
                    tableTop1[0].SetActive(true);
                    tt1Occupied[0] = true;
                    tableTop1counter++;
                    if (LevelController.instance.tutorialCounter == 0)
                    {
                        LevelController.instance.ShowTutorial(1);
                    }
                }

                else if(!tt1Occupied[1] && tableTop1counter<=UpgradeSystem.instance.currentTT1Index)
                {
                    tableTop1[1].SetActive(true);
                    tt1Occupied[1] = true;
                    tableTop1counter++;
                }

                else if (!tt1Occupied[2] && tableTop1counter <= UpgradeSystem.instance.currentTT1Index)
                {
                    tableTop1[2].SetActive(true);
                    tt1Occupied[2] = true;
                    tableTop1counter++;
                }
                break;


            case "base2":
                if (!tt2Occupied[0] && tableTop2counter <= UpgradeSystem.instance.currentTT2Index)
                {

                    tableTop2[0].SetActive(true);
                    tt2Occupied[0] = true;
                    tableTop2counter++;
                }
                else if (!tt2Occupied[1] && tableTop2counter <= UpgradeSystem.instance.currentTT2Index)
                {
                    tableTop2[1].SetActive(true);
                    tt2Occupied[1] = true;
                    tableTop2counter++;
                }
                else if (!tt2Occupied[2] && tableTop2counter <= UpgradeSystem.instance.currentTT2Index)
                {
                    tableTop2[2].SetActive(true);
                    tt2Occupied[2] = true;
                    tableTop2counter++;
                }
                break;


            case "rawFood1":
                //Debug.Log("<color=red>Table occupied :</color>" + fp1Occupied[0] + " " + fp1Occupied[1] + " " + fp1Occupied[2]);

                if (!fp1Occupied[0] && fryPan1Counter <= UpgradeSystem.instance.currentFP1Index)
                {
                    
                    fryPan1[0].SetActive(true);
                    fryPan1[0].GetComponent<IngredientManager>().pauseBurning = false;
                    fryPan1[0].GetComponent<IngredientManager>().isCooking = true;

                    fp1Occupied[0] = true;
                    fryPan1Counter++;
                    if (LevelController.instance.tutorialCounter == 0)
                    {
                        LevelController.instance.ShowTutorial(2);
                    }
                }
                else if (!fp1Occupied[1] && fryPan1Counter <= UpgradeSystem.instance.currentFP1Index)
                {
                    fryPan1[1].SetActive(true);
                    fryPan1[1].GetComponent<IngredientManager>().pauseBurning = false;
                    fryPan1[1].GetComponent<IngredientManager>().isCooking = true;
                    fp1Occupied[1] = true;
                    fryPan1Counter++;
                }
                else if (!fp1Occupied[2] && fryPan1Counter <= UpgradeSystem.instance.currentFP1Index)
                {
                    fryPan1[2].SetActive(true);
                    fryPan1[2].GetComponent<IngredientManager>().pauseBurning = false;
                    fryPan1[2].GetComponent<IngredientManager>().isCooking = true;
                    fp1Occupied[2] = true;
                    fryPan1Counter++;
                }
                break;


            case "rawFood2":
                if (!fp2Occupied[0] && fryPan2Counter <= UpgradeSystem.instance.currentFP2Index)
                {

                    fryPan2[0].SetActive(true);

                    fryPan2[0].GetComponent<IngredientManager>().pauseBurning = false;
                    fryPan2[0].GetComponent<IngredientManager>().isCooking = true;

                    fp2Occupied[0] = true;
                    fryPan2Counter++;
                }
                else if (!fp2Occupied[1] && fryPan2Counter <= UpgradeSystem.instance.currentFP2Index)
                {
                    fryPan2[1].SetActive(true);
                    fryPan2[1].GetComponent<IngredientManager>().pauseBurning = false;
                    fryPan2[1].GetComponent<IngredientManager>().isCooking = true;
                    fp2Occupied[1] = true;
                    fryPan2Counter++;
                }

                else if (!fp2Occupied[2] && fryPan2Counter <= UpgradeSystem.instance.currentFP2Index)
                {
                    fryPan2[2].SetActive(true);
                    fryPan2[2].GetComponent<IngredientManager>().pauseBurning = false;
                    fryPan2[2].GetComponent<IngredientManager>().isCooking = true;
                    fp2Occupied[2] = true;
                    fryPan2Counter++;
                }
                break;


            case "rawFood3":
                if (!fryPan3.activeSelf)
                {
                    //fryPan3.GetComponent<Button>().interactable = false;
                    fryPan3.SetActive(true);
                    fryPan3.GetComponent<IngredientManager>().isCooking = true;
                }
                break;
            //for(int i=0; i <= UpgradeSystem.instance.currentFP3Index; i++)
            //{
            //    if (!tt3Occupied[i])
            //    {
            //        tableTop3[i].SetActive(true);
            //        tt3Occupied[i] = true;
            //    }
            //}
            //break;


            case "beverage":
                for(int i=0;i <= UpgradeSystem.instance.currentBevMacIndex; i++)
                {
                    if (!kettleOccupied[0] && beverageCounter <= UpgradeSystem.instance.currentBevMacIndex)
                    {

                        beverage[0].SetActive(true);
                        chaiAnim[0].SetActive(true);
                        beverage[0].GetComponent<IngredientManager>().isCooking = true;

                        kettleOccupied[0] = true;
                        beverageCounter++;
                    }

                    if (!kettleOccupied[1] && beverageCounter <= UpgradeSystem.instance.currentBevMacIndex)
                    {
                        beverage[1].SetActive(true);
                        chaiAnim[1].SetActive(true);
                        beverage[1].GetComponent<IngredientManager>().isCooking = true;

                        kettleOccupied[1] = true;
                        beverageCounter++;
                    }

                    if (!kettleOccupied[2] && beverageCounter <= UpgradeSystem.instance.currentBevMacIndex)
                    {
                        beverage[2].SetActive(true);
                        chaiAnim[2].SetActive(true);
                        beverage[2].GetComponent<IngredientManager>().isCooking = true;

                        kettleOccupied[2] = true;
                        beverageCounter++;
                    }
                }

                break;
        }
    }

}
