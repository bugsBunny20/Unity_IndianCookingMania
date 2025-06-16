using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSettlement : MonoBehaviour
{
    public static OrderSettlement instance;

    public int tipCoinsEarned;
    public int orderTotal;
    public int xpEarned;
    public AudioSource source { get { return GetComponent<AudioSource>(); } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void FinalSettlement()
    {
        //Debug.Log("<color=grey>Coins position: " + this.name + " Order Payment:</color> " + orderTotal + tipCoinsEarned);
        LevelController.instance.coinsEarned += orderTotal;
        LevelController.instance.tipsEarned = tipCoinsEarned;
        LevelController.instance.coinEarnedText.text = LevelController.instance.coinsEarned.ToString();
        LevelController.instance.xpText.text = GameController.instance.xpEarned.ToString() + "/" + GameController.instance.xpLevelRequirement.ToString();
        LevelController.instance.xpLevelText.text = GameController.instance.currentXpLevel.ToString();
    }


    private void OnDisable()
    {
        source.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
