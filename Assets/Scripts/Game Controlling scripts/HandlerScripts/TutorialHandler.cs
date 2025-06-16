using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    
    public static TutorialHandler instance;

    public GameObject[] cookingTutorialPopUps;
    public GameObject[] updateUnlockedPopUps;


    public void Update()
    {
        if(LevelController.instance.LevelGoalPopUp.activeSelf)
        {
            ShowTutorial(1);
        }
    }

    public void ShowTutorial(int tutNum)
    {
        //switch(OrderController.instance.LevelM.LevelID)
        //{
        //    case 1:
        //       if(GameConstants.GetCityData())
        //        break;
        //}
    }

}
