using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreHandler : MonoBehaviour
{
    public static StoreHandler instance;

    public Button coinsBtn;
    public Button diamondsBtn;

    public GameObject coinsTab;
    public GameObject diamondsTab;

    public Text[] coinsPrice;
    public Text[] diamondssPrice;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTab(int btnNum)
    {
        switch (btnNum)
        {
            case 1:
                coinsBtn.interactable = false;
                coinsTab.SetActive(true);
                diamondsBtn.interactable = true;
                diamondsTab.SetActive(false);
                break;

            case 2:
                coinsBtn.interactable = true;
                coinsTab.SetActive(false);
                diamondsBtn.interactable = false;
                diamondsTab.SetActive(true);
                break;
        }
    }

    public void BuyCoins(int ID)
    {
        switch (ID)
        {
            case 0:
                break;

            case 1:
                IAPManager.instance.BuyCoins(ID);
                break;

            case 2:
                IAPManager.instance.BuyCoins(ID);
                break;

            case 3:
                IAPManager.instance.BuyCoins(ID);
                break;

            case 4:
                IAPManager.instance.BuyCoins(ID);
                break;

            case 5:
                IAPManager.instance.BuyCoins(ID);
                break;
        }
    }

    public void BuyDiamonds(int ID)
    {
        switch (ID)
        {
            case 0:
                break;

            case 1:
                IAPManager.instance.BuyDiamonds(ID);
                break;

            case 2:
                IAPManager.instance.BuyDiamonds(ID);
                break;

            case 3:
                IAPManager.instance.BuyDiamonds(ID);
                break;

            case 4:
                IAPManager.instance.BuyDiamonds(ID);
                break;

            case 5:
                IAPManager.instance.BuyDiamonds(ID);
                break;
        }
    }

}
