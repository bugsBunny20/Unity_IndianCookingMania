using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Update", menuName = "LevelData/Upgrade")]
public class UpgradeData : ScriptableObject
{
    
    //Earning spent and XP earned on each upgrade

    [System.Serializable]

    public class CostAndXpEarned
    {
        public int costPrice;
        public int Diamonds;
        public int xpEarned;
    }
    
    public List<CostAndXpEarned> dish1BaseUpgradeBuyingDetails;
    public List<CostAndXpEarned> dish2BaseUpgradeBuyingDetails;

    public List<CostAndXpEarned> dish1UpgradeBuyingDetails;
    public List<CostAndXpEarned> dish2UpgradeBuyingDetails;
    public List<CostAndXpEarned> dish3UpgradeBuyingDetails;

    public List<CostAndXpEarned> splDishUpgradeBuyingDetails;

    public List<CostAndXpEarned> beverageUpgradeBuyingDetails;

    public List<CostAndXpEarned> topping1UpgradeBuyingDetails;
    public List<CostAndXpEarned> topping2UpgradeBuyingDetails;
    public List<CostAndXpEarned> topping3UpgradeBuyingDetails;

    //kitchen upgrade earning

    public List<CostAndXpEarned> tableTop1UpgradeBuyingDetails;
    public List<CostAndXpEarned> tableTop2UpgradeBuyingDetails;

    public List<CostAndXpEarned> fryPan1UpgradeBuyingDetails;
    public List<CostAndXpEarned> fryPan2UpgradeBuyingDetails;
    public List<CostAndXpEarned> fryPan3UpgradeBuyingDetails;

    public List<CostAndXpEarned> foodWarmerUpgradeBuyingDetails;

    public List<CostAndXpEarned> beverageMachineUpgradeBuyingDetails;

    //timer,price and xp update dictionary or list

    [System.Serializable]
    public class OverallUpgradeData
    {
        public float cookTime;
        public int itemPrice;
        public int xpCount;
    }

    public List<OverallUpgradeData> dish1BaseUpgradeData;
    public List<OverallUpgradeData> dish2BaseUpgradeData;

    public List<OverallUpgradeData> dish1UpgradeData;
    public List<OverallUpgradeData> dish2UpgradeData;
    public List<OverallUpgradeData> dish3UpgradeData;
    public List<OverallUpgradeData> splDishUpgradeData;

    public List<OverallUpgradeData> topping1UpgradeData;
    public List<OverallUpgradeData> topping2UpgradeData;
    public List<OverallUpgradeData> topping3UpgradeData;

    public List<OverallUpgradeData> beverageUpgradeData;    

}
