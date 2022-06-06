using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;
using UnityEngine.UI;
public class Purchaser : MonoBehaviour,IStoreListener {

    IStoreController controller;
    string[] Products = new string[] {"coin_5000","coin_12500","coin_30000","coin_80000","coin_180000","yapimci_destek","no_ads","yapimci_destekbuyuk" };
    void Start()
    {
        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
        foreach(string s in Products)
        {
            if (s.Contains("ads"))
            {
                builder.AddProduct(s, ProductType.NonConsumable);
            }
            else
            {
                builder.AddProduct(s, ProductType.Consumable);
            }
            builder.AddProduct(s,ProductType.Consumable);
        }
        UnityPurchasing.Initialize(this,builder);

        if(PlayerPrefs.GetInt("reklam_varmı")==1)
        {
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().reklamları_kaldır_butonu.interactable = false;
        }
    }
    public void BuyProduct(string ProductId)
    {
        if (ProductId.Contains("ads"))
        {
            Product product0 = controller.products.WithID(ProductId);
            if(product0.hasReceipt)
            {
                PlayerPrefs.SetInt("no_ads1", 1);
                GameObject.Find("magazascripti").GetComponent<oyunmagaza>().reklamlarkaldırıldı_paneli.SetActive(true);
                GameObject.Find("magazascripti").GetComponent<oyunmagaza>().reklamları_kaldır_butonu.interactable = false;
                PlayerPrefs.SetInt("reklam_varmı",1);
            }
            else
            {
                buyProduct(ProductId);
            }
        }
        else
        {
            buyProduct(ProductId);
        }
        
    }
    void buyProduct(string ProductId)
    {
        Product product = controller.products.WithID(ProductId);

        if(product!= null && product.availableToPurchase)
        {
            Debug.Log("Ürün satın alınıyor...");
            controller.InitiatePurchase(product);
        }
        else
        {
            Debug.Log("Ürün bulunamadı ya da satın alınabilir değil");
        }
    }
    public void OnInitialized(IStoreController controller,IExtensionProvider provider)
    {
        this.controller = controller;
        Product product0 = controller.products.WithID("no_ads");
        Product product1 = controller.products.WithID("coin_12500");
        Product product2 = controller.products.WithID("coin_30000");
        Product product3 = controller.products.WithID("coin_80000");
        Product product4 = controller.products.WithID("coin_180000");
        if (product0.hasReceipt|| product1.hasReceipt || product2.hasReceipt|| product3.hasReceipt|| product4.hasReceipt)
        {
            PlayerPrefs.SetInt("no_ads1", 1);
        }
        else
        {
            PlayerPrefs.SetInt("no_ads1", 0);
        }
        Debug.Log("Sistem Hazır.");
    }
    public void OnInitializeFailed(InitializationFailureReason reason)
    {
        Debug.Log("Yükleme Hatası: "+reason.ToString());
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (string.Equals(args.purchasedProduct.definition.id, Products[0], StringComparison.Ordinal))
        {
            //GameObject.Find("magazascripti").GetComponent<oyunmagaza>().top
            PlayerPrefs.SetInt("TOPLAMSİMSEK", PlayerPrefs.GetInt("TOPLAMSİMSEK")+5000);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek = PlayerPrefs.GetInt("TOPLAMSİMSEK");
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().magazatoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().anamenutoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().altınsatınalındı_paneli.SetActive(true);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().satınalınan_altınmiktarı.text = "5000 Altın bakiyenize eklendi";
            Debug.Log("5000 Altın eklendi.");
        }
        else if (string.Equals(args.purchasedProduct.definition.id, Products[1], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("TOPLAMSİMSEK", PlayerPrefs.GetInt("TOPLAMSİMSEK") + 12500);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek = PlayerPrefs.GetInt("TOPLAMSİMSEK");
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().magazatoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().anamenutoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            PlayerPrefs.SetInt("no_ads1", 1);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().altınsatınalındı_paneli.SetActive(true);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().satınalınan_altınmiktarı.text = "12500 Altın bakiyenize eklendi";
            Debug.Log("12500 Altın eklendi.");
        }
        else if (string.Equals(args.purchasedProduct.definition.id, Products[2], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("TOPLAMSİMSEK", PlayerPrefs.GetInt("TOPLAMSİMSEK") + 30000);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek = PlayerPrefs.GetInt("TOPLAMSİMSEK");
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().magazatoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().anamenutoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            PlayerPrefs.SetInt("no_ads1", 1);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().altınsatınalındı_paneli.SetActive(true);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().satınalınan_altınmiktarı.text = "30000 Altın bakiyenize eklendi";
            Debug.Log("30000 Altın eklendi.");
        }
        else if (string.Equals(args.purchasedProduct.definition.id, Products[3], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("TOPLAMSİMSEK", PlayerPrefs.GetInt("TOPLAMSİMSEK") + 80000);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek = PlayerPrefs.GetInt("TOPLAMSİMSEK");
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().magazatoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().anamenutoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            PlayerPrefs.SetInt("no_ads1", 1);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().altınsatınalındı_paneli.SetActive(true);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().satınalınan_altınmiktarı.text = "80000 Altın bakiyenize eklendi";
            Debug.Log("80000 Altın eklendi.");
        }
        else if (string.Equals(args.purchasedProduct.definition.id, Products[4], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("TOPLAMSİMSEK", PlayerPrefs.GetInt("TOPLAMSİMSEK") + 180000);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek = PlayerPrefs.GetInt("TOPLAMSİMSEK");
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().magazatoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().anamenutoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            PlayerPrefs.SetInt("no_ads1", 1);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().altınsatınalındı_paneli.SetActive(true);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().satınalınan_altınmiktarı.text = "180000 Altın bakiyenize eklendi";
            Debug.Log("180000 Altın eklendi.");
        }
        else if (string.Equals(args.purchasedProduct.definition.id, Products[5], StringComparison.Ordinal))
        {
            Debug.Log("Yapımcıya destek yapıldı.");
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().yapimci_destekpaneli.SetActive(true);
           
        }
        else if (string.Equals(args.purchasedProduct.definition.id, Products[6], StringComparison.Ordinal))
        {
            PlayerPrefs.SetInt("no_ads1",1);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().reklamlarkaldırıldı_paneli.SetActive(true);
            Debug.Log("Reklamlar kaldırıldı.");
        }
        else if (string.Equals(args.purchasedProduct.definition.id, Products[7], StringComparison.Ordinal))
        {
            Debug.Log("Yapımcıya destek yapıldı.");
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().yapimci_destekpaneli.SetActive(true);
        }




        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product,PurchaseFailureReason reason)
    {
        Debug.Log("Bu ürün satın alınamadı: "+product.ToString());
    }

}
