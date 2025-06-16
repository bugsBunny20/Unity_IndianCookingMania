using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelData_",menuName ="LevelData/CityLevelData")]
public class LevelData : ScriptableObject
{
    public string cityName;
    public int currentLevel;
   
}
