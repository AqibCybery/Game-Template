
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Security;
using UnityEngine.Purchasing;
public class UnityInAppsIntegration : MonoBehaviour, IStoreListener
{
    public static UnityInAppsIntegration THIS;
    public SubscriptionInfo info;
    public static IStoreController m_StoreController;                                                                  // Reference to the Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider;                                                         // Reference to store-specific Purchasing subsystems.
    private IGooglePlayStoreExtensions m_GooglePlayExtensions;
    private IAppleExtensions m_AppleExtensions;
    // Product identifiers for all products capable of being purchased: "convenience" general identifiers for use with Purchasing, and their store-specific identifier counterparts 
    // for use with and outside of Unity Purchasing. Define store-specific identifiers also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)
    private static string[] kProductIDConsumableArray = new string[4];                                                       // General handle for the consumable product.
    private static string RemoveAds = "removeads";
    private static string kProductIDConsumable = "consumable";                                                         // General handle for the consumable product.
    private static string kProductIDNonConsumable = "nonconsumable";                                                  // General handle for the non-consumable product.
    private static string kProductIDSubscription = "monthly_sub";                                                   // General handle for the subscription product.

    private static string kProductNameAppleConsumable = "com.unity3d.test.services.purchasing.consumable";             // Apple App Store identifier for the consumable product.
    private static string kProductNameAppleNonConsumable = "com.unity3d.test.services.purchasing.nonconsumable";      // Apple App Store identifier for the non-consumable product.
    private static string kProductNameAppleSubscription = "com.unity3d.test.services.purchasing.subscription";       // Apple App Store identifier for the subscription product.

    private static string kProductNameGooglePlayConsumable = "com.unity3d.test.services.purchasing.consumable";        // Google Play Store identifier for the consumable product.
    private static string kProductNameGooglePlayNonConsumable = "com.unity3d.test.services.purchasing.nonconsumable";     // Google Play Store identifier for the non-consumable product.
    private static string kProductNameGooglePlaySubscription = "com.unity3d.test.services.purchasing.subscription";  // Google Play Store identifier for the subscription product.
    public string CoinsKey;
    private void Awake()
    {
#if UNITY_ANDROID
        CoinsKey = "pack";
#elif UNITY_IPHONE
             CoinsKey = "coinspack";
#endif
    }
    public bool checkSubscribtion()
    {
        try
        {
            Result r;
            r = info.isSubscribed();
            if (r == Result.True)
                return true;
            else
                return false;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public List<string> CoinsKeyList = new List<string>();
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            CoinsKeyList.Add(CoinsKey + (i + 1));
        }
        THIS = this;
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        for (int i = 0; i < CoinsKeyList.Count; i++)
        {
            kProductIDConsumableArray[i] = CoinsKeyList[i];
            builder.AddProduct(kProductIDConsumableArray[i], ProductType.Consumable, new IDs() { { kProductIDConsumableArray[i], AppleAppStore.Name }, { kProductIDConsumableArray[i], GooglePlay.Name }, });
        }
        builder.AddProduct(RemoveAds, ProductType.NonConsumable, new IDs() { { RemoveAds, AppleAppStore.Name }, { RemoveAds, GooglePlay.Name }, });

        //  builder.AddProduct("NoAds",ProductType.NonConsumable, new IDs() { { "NoAds", AppleAppStore.Name }, { "NoAds", GooglePlay.Name }, });
        //foreach (string list in LevelEditorBase.THIS.InAppIDs)
        //Debug.Log(" IDs " + list);
        // Create a builder, first passing in a suite of Unity provided stores.

        // Add a product to sell / restore by way of its identifier, associating the general identifier with its store-specific identifiers.
        builder.AddProduct(kProductIDConsumable, ProductType.Consumable, new IDs() { { kProductNameAppleConsumable, AppleAppStore.Name }, { kProductNameGooglePlayConsumable, GooglePlay.Name }, });// Continue adding the non-consumable product.
        builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable, new IDs() { { kProductNameAppleNonConsumable, AppleAppStore.Name }, { kProductNameGooglePlayNonConsumable, GooglePlay.Name }, });// And finish adding the subscription product.
        builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs() { { kProductIDSubscription, AppleAppStore.Name }, { kProductIDSubscription, GooglePlay.Name }, });// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);

    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        //try
        //{
            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
            m_GooglePlayExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();
            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
            Dictionary<string, string> Dict;
#if UNITY_IPHONE
                //Dict = m_AppleExtensions.GetProductDetails();
#elif UNITY_ANDROID
            //Dict = m_GooglePlayExtensions.GetProductJSONDictionary();
#endif
        //   foreach (Product item in controller.products.all)
        //    {
        //        if (item.receipt != null)
        //        {
        //            if (item.definition.type == ProductType.Subscription)
        //            {
        //                print("Subscription");
        //                string json = (Dict == null || !Dict.ContainsKey(item.definition.storeSpecificId)) ? null : Dict[item.definition.storeSpecificId];
        //                SubscriptionManager s = new SubscriptionManager(item, json);
        //                info = s.getSubscriptionInfo();
        //                if (info.getProductId() == kProductIDSubscription)
        //                {
        //                    if (info.isSubscribed() == Result.True)
        //                    {
        //                        int days = info.getRemainingTime().Days;
        //                        if (PlayerPrefs.GetInt("today") != days)
        //                        {
        //                            PlayerPrefs.SetString("SubscriptionReward", "You Recived 10 Coins.");
        //                            InitScriptName.InitScript.Instance.AddGems(10);
        //                            PlayerPrefs.SetInt("today", days);
        //                            if (days == info.getExpireDate().Day - 7 ||
        //                                days == info.getExpireDate().Day - 14 ||
        //                                days == info.getExpireDate().Day - 21 ||
        //                                days == info.getExpireDate().Day - 28)
        //                            {
        //                                InitScriptName.InitScript.Instance.AddGems(250);
        //                                PlayerPrefs.SetString("SubscriptionReward", "You Recived 260 Coins");
        //                            }
        //                            if (days == 1)
        //                            {
        //                                InitScriptName.InitScript.Instance.AddGems(1000);

        //                                PlayerPrefs.SetString("SubscriptionReward", "You Recived 1010 Coins");
        //                            }
        //                            MenuManager.Instance.MenuSubscription.SetActive(true);

        //                        }
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    MenuManager.Instance.debgingModeReset("Inilization Done");
        //}
        //catch (Exception e)
        //{
        //    MenuManager.Instance.debgingModeReset(e.Message + e.StackTrace);
        //}
    }
    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void BuyConsumable()
    {
        // Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(kProductIDConsumable);
    }


    public void BuyNonConsumable()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(RemoveAds);
    }


    public void BuySubscription()
    {


        // Buy the subscription product using its the general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(kProductIDSubscription);
    }


    public void BuyProductID(string productId)
    {

        Debug.Log("product ID : " + productId);

        // If the stores throw an unexpected exception, use try..catch to protect my logic here.
        try
        {
            //  If Purchasing has been initialized...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing system's products collection.
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                //Otherwise ...
                else
                {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");

            }
        }
        // Complete the unexpected exception handling ...
        catch (Exception e)
        {
            // ... by reporting any unexpected exception for later diagnosis.
            Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases. Apple currently requires explicit purchase restoration for IAP.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
           {
                   // The first phase of restoration. If no more responses are received on ProcessPurchase then no purchases are available to be restored.
                   Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
           });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //



    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
        // A consumable product has been purchased by this user.
        if (args.purchasedProduct.definition.id.Contains("pack"))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            //InitScriptName.InitScript.Instance.PurchaseSucceded();
        }
        else if (args.purchasedProduct.definition.id.Contains("removeads"))
        {
            PlayerPrefs.SetInt("NoAds", 1);
           // InitScriptName.InitScript.Instance.PurchaseSucceded();
            //MenuManager.Instance.AdsBuyButton.SetActive(false);
            //MenuManager.Instance.AdsEnjoyButton.SetActive(true);
        }
        else if (args.purchasedProduct.definition.id.Contains(kProductIDSubscription))
        {
            PlayerPrefs.SetInt("subscription", 1);
            //InitScriptName.InitScript.Instance.AddGems(10);
            PlayerPrefs.SetString("SubscriptionReward", "You Recived 10 Coins.");
            //MenuManager.Instance.MenuSubscription.SetActive(true);
            PlayerPrefs.SetInt("today", 30);
        }

        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing this reason with the user.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
