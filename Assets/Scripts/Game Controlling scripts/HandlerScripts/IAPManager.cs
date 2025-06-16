using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string COINSPACK_1 = "coinspack_1";
    public static string COINSPACK_2 = "coinspack_2";
    public static string COINSPACK_3 = "coinspack_3";
    public static string COINSPACK_4 = "coinspack_4";
    public static string COINSPACK_5 = "coinspack_5";

    public static string DIAMONDPCK_1 = "diamondpack_1";
    public static string DIAMONDPCK_2 = "diamondpack_2";
    public static string DIAMONDPCK_3 = "diamondpack_3";
    public static string DIAMONDPCK_4 = "diamondpack_4";
    public static string DIAMONDPCK_5 = "diamondpack_5";

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        builder.AddProduct(COINSPACK_1, ProductType.Consumable);
        builder.AddProduct(COINSPACK_2, ProductType.Consumable);
        builder.AddProduct(COINSPACK_3, ProductType.Consumable);
        builder.AddProduct(COINSPACK_4, ProductType.Consumable);
        builder.AddProduct(COINSPACK_5, ProductType.Consumable);

        // Continue adding the consumable product.
        builder.AddProduct(DIAMONDPCK_1, ProductType.Consumable);
        builder.AddProduct(DIAMONDPCK_2, ProductType.Consumable);
        builder.AddProduct(DIAMONDPCK_3, ProductType.Consumable);
        builder.AddProduct(DIAMONDPCK_4, ProductType.Consumable);
        builder.AddProduct(DIAMONDPCK_5, ProductType.Consumable);
        

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void BuyCoins(int ID)
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        switch (ID)
        {
            case 1:
                BuyProductID(COINSPACK_1);
                break;
            case 2:
                BuyProductID(COINSPACK_2);
                break;
            case 3:
                BuyProductID(COINSPACK_3);
                break;
            case 4:
                BuyProductID(COINSPACK_4);
                break;
            case 5:
                BuyProductID(COINSPACK_5);
                break;
        }
    }


    public void BuyDiamonds(int ID)
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        switch (ID)
        {
            case 1:
                BuyProductID(DIAMONDPCK_1);
                break;
            case 2:
                BuyProductID(DIAMONDPCK_2);
                break;
            case 3:
                BuyProductID(DIAMONDPCK_3);
                break;
            case 4:
                BuyProductID(DIAMONDPCK_4);
                break;
            case 5:
                BuyProductID(DIAMONDPCK_5);
                break;
        }
    }
    

    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, COINSPACK_1, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameController.instance.totalCoins += 2500;
           
        }
        if (String.Equals(args.purchasedProduct.definition.id, COINSPACK_2, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            GameController.instance.totalCoins += 8000;
        }
        if (String.Equals(args.purchasedProduct.definition.id, COINSPACK_3, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameController.instance.totalCoins += 15000;
        }
        if (String.Equals(args.purchasedProduct.definition.id, COINSPACK_4, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameController.instance.totalCoins += 35000;
        }
        if (String.Equals(args.purchasedProduct.definition.id, COINSPACK_5, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameController.instance.totalCoins += 100000;
        }
        if (String.Equals(args.purchasedProduct.definition.id, DIAMONDPCK_1, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameController.instance.totalDiamonds += 10;
        }
        if (String.Equals(args.purchasedProduct.definition.id, DIAMONDPCK_2, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameController.instance.totalDiamonds += 35;
        }
        if (String.Equals(args.purchasedProduct.definition.id, DIAMONDPCK_3, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameController.instance.totalDiamonds += 60;
        }
        if (String.Equals(args.purchasedProduct.definition.id, DIAMONDPCK_4, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameController.instance.totalDiamonds += 125;
        }
        if (String.Equals(args.purchasedProduct.definition.id, DIAMONDPCK_5, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameController.instance.totalDiamonds += 300;
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}
