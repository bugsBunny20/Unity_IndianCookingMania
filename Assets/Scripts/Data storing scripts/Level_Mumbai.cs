using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Level/Mumbai")]
public class Level_Mumbai : ScriptableObject {

    public int LevelID;
    public int CustomerCount;

    public int ExtraCustCount;

    public int RewardCoins;
    public int RewardXP;
    public int RewardDiamonds;

    private int XPEarned;
    private int CoinsEarned;
    private int TipsEarned;
    private int StarsEarned;

    [System.Serializable]
    public class CustomerOrderDishCount
    {
        public int dish1Count;
        public int dish2Count;
        public int dish3Count;
        public int beverageCount;

        public int topping11Count;
        public int topping12Count;
        public int topping13Count;

        public int topping21Count;
        public int topping22Count;
        public int topping23Count;

        public bool splDish;

    }


    public List<CustomerOrderDishCount> CustomerOrder;


    public int Star1Score;
    public int Star2Score;
    public int Star3Score;
     
    public bool Dish2CanBeOrdered;
    public bool Dish3CanBeOrdered;

    public bool SplDishCanBeOrdered;

    public bool topping11CanBeOrdered;
    public bool topping12CanBeOrdered;
    public bool topping13CanBeOrdered;

    //public bool topping21CanBeOrdered;
    //public bool topping22CanBeOrdered;
    //public bool topping23CanBeOrdered;

    public string unloackable;

}
