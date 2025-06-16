using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "LevelData/Score")]

public class ScoreSystem : ScriptableObject
{
    
    [System.Serializable]
    public class XPSystem
    {
        public int XPLevel;
        public int earnedXPForLevel;
        public int totalToBeEarned;
        public int rewardCoins;
        public int rewardDiamonds;
    }

    public List<XPSystem> xpLevelDetails;

}
