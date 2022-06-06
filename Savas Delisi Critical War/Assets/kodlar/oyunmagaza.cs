using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class oyunmagaza : MonoBehaviour {

    private const bool allowCarrierDataNetwork = true;
    private const string pingAddress = "8.8.8.8"; // Google Public DNS server
    private const float waitingTime = 2f;
    public GameObject profil_paneli;
    public GameObject cinsiyet_paneli;
    public GameObject netyok_savas_paneli_hatası;
    private Ping ping;
    private float pingStartTime;
    public GameObject play_giris_paneli;
    public GameObject girispaneli_onemli;
    public GameObject nethatasıanapanel;
    public GameObject nethatası_baglanıyor;
    public string skin_hangi_silahta="ump40_";
    public GameObject yapimci_destekpaneli;
    public Button reklamları_kaldır_butonu;
    public GameObject reklamlarkaldırıldı_paneli;
    public GameObject altınsatınalındı_paneli;
    public Text satınalınan_altınmiktarı;
    public int tekdefalık;
    public GameObject seviyeatladın_paneli;
    public Text hangiseviyeye_gecti;
    public GameObject kusanıldıresmi;
    public GameObject kusanbutonu;
    public Text kusanıldıyazisi;
    public Text anamenutoplamsimsek;
    public Text magazatoplamsimsek;
    public Slider levelibresi;
    public int oyunpuanı;
    public int seviye;
    public GameObject NETHATASIPANELİ;
    public int ibrepuanı;
    public Text seviyeyazısı;
    public Text seviyeyazisi_anamenu;
    public Text ibre_seviyegecişpuanı;
    public int geçispuanı_sınırı;
    public int oyunculimitiayar;
    public int kupasayısı;
    public int  kupasayisii;
    public Text kupasayısı_yazısı;
    public Text lig_ismi;
    public GameObject yetersizbakiye;
    public GameObject satınalpaneli;
    public Text öldürme_sayısıy;
    public Text ölüm_sayısıy;
    public Text oynadıgı_maçsayısıy;
    public Text kazandıgı_maçsayısıy;
    public Text kaybettigi_maçsayısıy;
    public Text toplam_oynadıgısürey;
    public Text beraberemacsayısıy;
    public Text kdoraniy;
    public GameObject yeniliklerpaneli;
    public int öldürme_sayısı;
    public int ölüm_sayısı;
    public int kazandıgımaçsayısı;
    public int kaybettigimacsayısı;
    public float oynanansuree;
    public double kdorani;
    
    public int saniye;
    public int dakika;
    public int saat;

    public int toplamşimşek;


    public int kill_database;
    public int death_database;
    public int oynanan_mac_;
    public int kazanan_mac_data;
    public int kaybeden_mac_data;
    public int berabere_mac_data;
    public int altin_data;
    public int kupa_data;
    public GameObject satınalbutonu;
    public GameObject skin_satınal_paneli;
    public GameObject[] birincilsilahlar;
    public GameObject[] ikincilsilahlar;
    public GameObject[] bıçaklar;

    public GameObject ump40_skin_obje;
    public GameObject awp_skin_obje;
    public GameObject pompalı_skin_obje;
    public GameObject tabanca_skin_obje;
    public GameObject bıçak_skin_obje;

    public Material[] ump40_skins;
    public Material[] awp_skins;
    public Material[] pompalı_skins;
    public Material[] tabanca_skins;
    public Material[] bıçak_skins;

    public Text skin_fiyatı_yazisi;
    public Text skin_ismi_yazisi;

    public int skin_geçmesayısı;

    public int seviyepuani_once;
    public int seviyepuani_sonra;


    public int[] birincilkuşanıldı;
    public int[] ikincilkuşanıldı;
    public int[] bıcakkuşanıldı;

    public int[] birincilfiyatlar;
    public int[] ikincilfiyatlar;
    public int[] bıcakfiyatlar;

    public int[] birincilsilah_anamenu;
    
    public int[] birincilsatinalindi;    
    public int[] ikincilsatinalindi;
    public int[] bıcaksatinalindi;

    public GameObject ump40_skin_kuşan_butonu;
    public GameObject awp_skin_kuşan_butonu;
    public GameObject pompalı_skin_kuşan_butonu;
    public GameObject tabanca_skin_kuşan_butonu;
    public GameObject bıçak_skin_kuşan_butonu;

    public GameObject ump40_skin_satınal_butonu;
    public GameObject awp_skin_satınal_butonu;
    public GameObject pompalı_skin_satınal_butonu;
    public GameObject tabanca_skin_satınal_butonu;
    public GameObject bıçak_skin_satınal_butonu;

    public int[] ump40_skin_satınal_fiyatı;
    public int[] awp_skin_satınal_fiyatı;
    public int[] pompalı_skin_satınal_fiyatı;
    public int[] tabanca_skin_satınal_fiyatı;
    public int[] bıçak_skin_satınal_fiyatı;

    public string[] ump40_skin_ismi;
    public string[] awp_skin_ismi;
    public string[] pompalı_skin_ismi;
    public string[] tabanca_skin_ismi;
    public string[] bıçak_skin_ismi;

    public Text satınalfiyatı;
    public int birincil_silahsayısı;
    public int ikincil_silahsayısı;
    public int bıcaksayısı;
    public int a = 0;
    public int b = 0;
    public int c = 0;
    
    public Slider hasar_slider;
    public Slider atıshizi_slider;
    public Slider isabetorani_slider;
    public Slider sarjorkapasitesi_slider;


    public float[] birincilhasar;
    public float[] birincilatıshizi;
    public float[] birincilisabetorani;
    public float[] birincilsarjorkapasitesi;

    public float[] ikincilhasar;
    public float[] ikincilatıshizi;
    public float[] ikincilisabetorani;
    public float[] ikincilsarjorkapasitesi;

    public float[] bıcakhasar;
    public float[] bıcakatıshizi;
    public float[] bıcakisabetorani;
    public float[] bıcaksarjorkapasitesi;

    
    

   
    public GameObject birincildevamyokileri;
    public GameObject ikincildevamyokileri;
    public GameObject bıcakdevamyokileri;
    public GameObject birincildevamyokgeri;
    public GameObject ikincildevamyokgeri;
    public GameObject bıcakdevamyokgeri;
    // Use this for initialization
    public int hangibaslık;
    public int kacıncısilah;
    PlayerWeapons ps;
    public void English_dil()
    {

    }
    public void hangi_ligte()
    {int kupa_= Convert.ToInt32(kupasayısı_yazısı.text);


        if (kupa_ < 200)
        {
            lig_ismi.text = "DERECESİZ";
        }
        if (kupa_ >= 200 & kupa_ < 600)
        {
            lig_ismi.text = "BRONZ";
        }
        if (kupa_ >= 600 & kupa_ < 1200)
        {
            lig_ismi.text = "GÜMÜŞ";
        }
        if (kupa_ >= 1200 & kupa_ < 1800)
        {
            lig_ismi.text = "ALTIN";
        }
        if (kupa_ >= 1800 & kupa_ < 2400)
        {
            lig_ismi.text = "KRİSTAL";
        }
        if (kupa_ >= 2400 & kupa_ < 3000)
        {
            lig_ismi.text = "USTA";
        }
        if (kupa_ >= 3000 & kupa_ < 3600)
        {
            lig_ismi.text = "ŞAMPİYON";
        }
        if (kupa_ >= 1800)
        {
            lig_ismi.text = "TİTAN";
        }
       // kupasayısı_yazısı.text = kupasayısı.ToString();

    }
    public void  hangileveldeyim()
    {
        
        if (oyunpuanı<100)
        {
            seviye = 1;
            ibrepuanı = oyunpuanı;
            geçispuanı_sınırı = 100;
        }
        else if(oyunpuanı>=100 & oyunpuanı<250)
        {
            seviye = 2;
            ibrepuanı = oyunpuanı - 100;
            geçispuanı_sınırı = 150;
        }
        else if (oyunpuanı >= 250 & oyunpuanı < 500)
        {
            seviye = 3;
            ibrepuanı = oyunpuanı - 250;
            geçispuanı_sınırı = 250;
        }
        else if (oyunpuanı >= 500 & oyunpuanı < 900)
        {
            seviye = 4;
            ibrepuanı = oyunpuanı - 500;
            geçispuanı_sınırı = 400;
        }
        else if (oyunpuanı >= 900 & oyunpuanı < 1550)
        {
            seviye = 5;
            ibrepuanı = oyunpuanı - 900;
            geçispuanı_sınırı = 650;
        }
        else if (oyunpuanı >= 1550 & oyunpuanı < 2600)
        {
            seviye = 6;
            ibrepuanı = oyunpuanı - 1550;
            geçispuanı_sınırı = 1050;
        }
        else if (oyunpuanı >= 2600 & oyunpuanı < 4300)
        {
            seviye = 7;
            ibrepuanı = oyunpuanı - 2600;
            geçispuanı_sınırı = 1700;
        }
        else if (oyunpuanı >= 4300 & oyunpuanı < 7050)
        {
            seviye = 8;
            ibrepuanı = oyunpuanı - 4300;
            geçispuanı_sınırı = 2750;
        }
        else if (oyunpuanı >= 7050 & oyunpuanı < 11500)
        {
            seviye = 9;
            ibrepuanı = oyunpuanı - 7050;
            geçispuanı_sınırı = 4450;
        }
        else if (oyunpuanı >= 11500 & oyunpuanı < 18700)
        {
            seviye = 10;
            ibrepuanı = oyunpuanı - 11500;
            geçispuanı_sınırı = 7200;
        }
        else if (oyunpuanı >= 18700 & oyunpuanı < 30350)
        {
            seviye = 11;
            ibrepuanı = oyunpuanı - 18700;
            geçispuanı_sınırı = 11650;
        }
        else if (oyunpuanı >= 30350 & oyunpuanı < 49200)
        {
            seviye = 12;
            ibrepuanı = oyunpuanı - 30350;
            geçispuanı_sınırı = 18850;
        }
        else if (oyunpuanı >= 49200 & oyunpuanı < 79700)
        {
            seviye = 13;
            ibrepuanı = oyunpuanı - 49200;
            geçispuanı_sınırı = 30500;
        }
        else if (oyunpuanı >= 79700 & oyunpuanı < 129050)
        {
            seviye = 14;
            ibrepuanı = oyunpuanı - 79700;
            geçispuanı_sınırı = 49350;
        }
        else if (oyunpuanı >= 129050 & oyunpuanı < 208900)
        {
            seviye = 15;
            ibrepuanı = oyunpuanı - 129050;
            geçispuanı_sınırı = 79850;
        }
        else if (oyunpuanı >= 208900 & oyunpuanı < 338100)
        {
            seviye = 16;
            ibrepuanı = oyunpuanı - 208900;
            geçispuanı_sınırı = 129200;
        }
        else if (oyunpuanı >= 338100 & oyunpuanı < 547150)
        {
            seviye = 17;
            ibrepuanı = oyunpuanı - 338100;
            geçispuanı_sınırı = 209050;
        }
        else if (oyunpuanı >= 547150 & oyunpuanı < 885400)
        {
            seviye = 18;
            ibrepuanı = oyunpuanı - 547150;
            geçispuanı_sınırı = 338250;
        }
        else if (oyunpuanı >= 885400 & oyunpuanı < 1432700)
        {
            seviye = 19;
            ibrepuanı = oyunpuanı - 885400;
            geçispuanı_sınırı = 547300;
        }
        else if (oyunpuanı >= 1432700 & oyunpuanı < 2318250)
        {
            seviye = 20;
            ibrepuanı = oyunpuanı - 1432700;
            geçispuanı_sınırı = 885550;
        }
        else if (oyunpuanı >= 2318250 & oyunpuanı < 3751100)
        {
            seviye = 21;
            ibrepuanı = oyunpuanı - 2318250;
            geçispuanı_sınırı = 1432850;
        }
        else if (oyunpuanı >= 3751100 & oyunpuanı < 6069500)
        {
            seviye = 22;
            ibrepuanı = oyunpuanı - 3751100;
            geçispuanı_sınırı = 2318400;
        }
        else if (oyunpuanı >= 6069500 & oyunpuanı < 9820750)
        {
            seviye = 23;
            ibrepuanı = oyunpuanı - 6069500;
            geçispuanı_sınırı = 3751250;
        }
        else if (oyunpuanı >= 9820750 & oyunpuanı < 15890400)
        {
            seviye = 24;
            ibrepuanı = oyunpuanı - 9820750;
            geçispuanı_sınırı = 6069650;
        }
        else if (oyunpuanı >= 15890400)
        {
            
               seviye = 25;
            ibrepuanı = oyunpuanı - 15890400;
            geçispuanı_sınırı = 9820900;
        }
        if (Application.loadedLevelName == "_MainMenu")
        {
            seviyeyazısı.text = seviye.ToString();
            
            ibre_seviyegecişpuanı.text = ibrepuanı + " / " + geçispuanı_sınırı.ToString();
            levelibresi.value = (float)ibrepuanı / (float)geçispuanı_sınırı;
            
           // Debug.Log((float)ibrepuanı / (float)geçispuanı_sınırı);
        }
    }
    public void hasararttır()
    {
        if (hangibaslık == 1)
        {
            for (int i = 0; i < birincil_silahsayısı; i++)
            {
                if (birincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            //ps.primaryWeapons[kacıncısilah].headDamage = ps.primaryWeapons[kacıncısilah].headDamage+5;
            //ps.primaryWeapons[kacıncısilah].torsoDamage = ps.primaryWeapons[kacıncısilah].torsoDamage+5;
            //ps.primaryWeapons[kacıncısilah].limbsDamage = ps.primaryWeapons[kacıncısilah].limbsDamage+5;
            if (birincilhasar[kacıncısilah] < 100)
            {
                if (toplamşimşek >= 50)
                {

                    birincilhasar[kacıncısilah] = birincilhasar[kacıncısilah] + 5;
                    hasar_slider.value = birincilhasar[kacıncısilah] / 100f;
                    toplamşimşek = toplamşimşek - 50;
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);
                }
            }
            // birincilsilahlar[kacıncısilah].
        }
        if (hangibaslık == 2)
        {
            for (int i = 0; i < ikincil_silahsayısı; i++)
            {
                if (ikincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            //ps.secondaryWeapons[kacıncısilah].headDamage = ps.secondaryWeapons[kacıncısilah].headDamage+5;
            //ps.secondaryWeapons[kacıncısilah].torsoDamage = ps.secondaryWeapons[kacıncısilah].torsoDamage+5;
            //ps.secondaryWeapons[kacıncısilah].limbsDamage = ps.secondaryWeapons[kacıncısilah].limbsDamage+5;
            if (ikincilhasar[kacıncısilah] < 100)
            {
                if (toplamşimşek >= 50)
                {
                    ikincilhasar[kacıncısilah] = ikincilhasar[kacıncısilah] + 5;
                    hasar_slider.value = ikincilhasar[kacıncısilah] / 100f;
                    toplamşimşek = toplamşimşek - 50;
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);
                }
            }
        }
        if (hangibaslık == 3)
        {
            for (int i = 0; i < bıcaksayısı; i++)
            {
                if (bıçaklar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            //ps.specialWeapons[kacıncısilah].headDamage = ps.specialWeapons[kacıncısilah].headDamage+5;
            //ps.specialWeapons[kacıncısilah].torsoDamage = ps.specialWeapons[kacıncısilah].torsoDamage+5;
            //ps.specialWeapons[kacıncısilah].limbsDamage = ps.specialWeapons[kacıncısilah].limbsDamage+5;
            if (bıcakhasar[kacıncısilah] < 100)
            {
                if (toplamşimşek >= 50)
                {
                    bıcakhasar[kacıncısilah] = bıcakhasar[kacıncısilah] + 5;
                    hasar_slider.value = bıcakhasar[kacıncısilah] / 100f;
                    toplamşimşek = toplamşimşek - 50;
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);
                }
            }
        }
        
    }
    
    public void atıshızıarttır()
    {
        if (hangibaslık == 1)
        {
            for (int i = 0; i < birincil_silahsayısı; i++)
            {
                if (birincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            // ps.primaryWeapons[kacıncısilah].fireRate = 0;
            if (birincilatıshizi[kacıncısilah] < 100)
            {
                if (toplamşimşek >= 50)
                {
                    birincilatıshizi[kacıncısilah] = birincilatıshizi[kacıncısilah] + 5;
                    atıshizi_slider.value = birincilatıshizi[kacıncısilah] / 100f;
                    toplamşimşek = toplamşimşek - 50;
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);
                }
            }
            // birincilsilahlar[kacıncısilah].
        }
        if (hangibaslık == 2)
        {
            for (int i = 0; i < ikincil_silahsayısı; i++)
            {
                if (ikincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            // ps.secondaryWeapons[kacıncısilah].fireRate = 0;
            if (ikincilatıshizi[kacıncısilah] < 100)
            {
                if (toplamşimşek >= 50)
                {
                    ikincilatıshizi[kacıncısilah] = ikincilatıshizi[kacıncısilah] + 5;
                    atıshizi_slider.value = ikincilatıshizi[kacıncısilah] / 100f;
                    toplamşimşek = toplamşimşek - 50;
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);
                }
            }
        }
        if (hangibaslık == 3)
        {
            for (int i = 0; i < bıcaksayısı; i++)
            {
                if (bıçaklar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            // ps.specialWeapons[kacıncısilah].fireRate = 0;
            if (bıcakatıshizi[kacıncısilah] < 100)
            {
                if (toplamşimşek >= 50)
                {
                    bıcakatıshizi[kacıncısilah] = bıcakatıshizi[kacıncısilah] + 5;
                    atıshizi_slider.value = bıcakatıshizi[kacıncısilah] / 100f;
                    toplamşimşek = toplamşimşek - 50;
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);
                }
            }
        }

    }
    public void sarjorarttır()
    {
        if (hangibaslık == 1)
        {
            for (int i = 0; i < birincil_silahsayısı; i++)
            {
                if (birincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            // ps.primaryWeapons[kacıncısilah].reserveBullets = 0;
            if (birincilsarjorkapasitesi[kacıncısilah] < 100)
            {
                if (toplamşimşek >= 50)
                {
                    birincilsarjorkapasitesi[kacıncısilah] = birincilsarjorkapasitesi[kacıncısilah] + 5;
                    sarjorkapasitesi_slider.value = birincilsarjorkapasitesi[kacıncısilah] / 100f;
                    toplamşimşek = toplamşimşek - 50;
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);
                }
            }

        }
        if (hangibaslık == 2)
        {
            for (int i = 0; i < ikincil_silahsayısı; i++)
            {
                if (ikincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            // ps.secondaryWeapons[kacıncısilah].reserveBullets = 0;
            if (ikincilsarjorkapasitesi[kacıncısilah] < 100)
            {
                if (toplamşimşek >= 50)
                {
                    ikincilsarjorkapasitesi[kacıncısilah] = ikincilsarjorkapasitesi[kacıncısilah] + 5;
                    sarjorkapasitesi_slider.value = ikincilsarjorkapasitesi[kacıncısilah] / 100f;
                    toplamşimşek = toplamşimşek - 50;
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);
                }
            }
        }
        if (hangibaslık == 3)
        {
            for (int i = 0; i < bıcaksayısı; i++)
            {
                if (bıçaklar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            //  ps.specialWeapons[kacıncısilah].reserveBullets = 0;
            if (bıcaksarjorkapasitesi[kacıncısilah] < 100)
            {
                if (toplamşimşek >= 50)
                {
                    bıcaksarjorkapasitesi[kacıncısilah] = bıcaksarjorkapasitesi[kacıncısilah] + 5;
                    sarjorkapasitesi_slider.value = bıcaksarjorkapasitesi[kacıncısilah] / 100f;
                    toplamşimşek = toplamşimşek - 50;
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);
                    
                    
                }
            }
        }
        
    }
    public void şimşek_kazan()
    {
        PlayerPrefs.SetInt("TOPLAMSİMSEK",toplamşimşek);
        Debug.Log(toplamşimşek);
    }
    
    public void netvarmi()
    {
        nethatası_baglanıyor.SetActive(false);
        nethatasıanapanel.SetActive(true);

    }
    
    void sayac()
    {
        
           PlayerPrefs.SetInt("oynanan_sure", PlayerPrefs.GetInt("oynanan_sure") + 2);

        Debug.Log(PlayerPrefs.GetInt("oynanan_sure"));

        saniye = PlayerPrefs.GetInt("oynanan_sure") % 60;
        dakika  =((PlayerPrefs.GetInt("oynanan_sure") - saniye) /60) %60;
        saat = (PlayerPrefs.GetInt("oynanan_sure") - dakika*60) / 60;
        if (Application.loadedLevelName == "_MainMenu")
        {
           // toplam_oynadıgısürey.text = saat.ToString() + " s " + dakika.ToString() + " dk " + saniye.ToString() + " sn";
        }
        Invoke("sayac", 2f);

    }
    private void InternetIsNotAvailable()
    {

        if (Application.loadedLevelName == "_MainMenu")
        {

            if (PlayGamesPlatform.Instance.IsAuthenticated())
            {
                
            }
            else
            {
                NETHATASIPANELİ.SetActive(true);
                GameObject.Find("firebase-message").GetComponent<oturum_ac>().girişyapılmadıpaneli.SetActive(false);
            }
            
        }

    }

    private void InternetAvailable()
    {
        if (Application.loadedLevelName == "_MainMenu")
        {
            NETHATASIPANELİ.SetActive(false);
            GameObject.Find("firebase-message").GetComponent<oturum_ac>().girissorgula();
        }
    }
    public void Update()
    {
        if (ping != null)
        {
            bool stopCheck = true;
            if (ping.isDone)
                InternetAvailable();
            else if (Time.time - pingStartTime < waitingTime)
                stopCheck = false;
            else
                InternetIsNotAvailable();
            if (stopCheck)
            {
                
                ping = null;
            }
        }
        else if (ping==null)
        {
            netsorgulaharbi();
        }

        if (!PhotonNetwork.connected)
        {
            if (PhotonNetwork.connectionState != ConnectionState.InitializingApplication && PhotonNetwork.connectionState != ConnectionState.Connecting &&
                       PhotonNetwork.connectionState != ConnectionState.Disconnecting && PhotonNetwork.connectionState != ConnectionState.Connected)
            {
                if (Application.loadedLevelName == "_MainMenu")
                {
                    netyok_savas_paneli_hatası.SetActive(true);
                }
            }
        }
        else
        {
            if (Application.loadedLevelName == "_MainMenu")
            {
                netyok_savas_paneli_hatası.SetActive(false);
            }
        }
        }
    void netsorgulaharbi()
    {
        netsorgula();
       // Invoke("netsorgulaharbi", 1.5f);
    }
    void netsorgula()
    {
        
        bool internetPossiblyAvailable;
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossiblyAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                internetPossiblyAvailable = allowCarrierDataNetwork;
                break;
            default:
                internetPossiblyAvailable = false;
                break;
        }
        if (!internetPossiblyAvailable)
        {
            InternetIsNotAvailable();
            return;
        }
        ping = new Ping(pingAddress);
        pingStartTime = Time.time;
    }
    //IEnumerator checkInternetConnection(Action<bool> action)
    //{
    //    WWW www = new WWW("http://google.com");
    //    yield return www;
    //    if (www.error != null)
    //    {
    //        action(false);
    //        if (Application.loadedLevelName == "_MainMenu")
    //        {

    //            NETHATASIPANELİ.SetActive(true);
    //        }
    //    }
    //    else
    //    {
    //        action(true);
    //        if (Application.loadedLevelName == "_MainMenu")
    //        {
    //            NETHATASIPANELİ.SetActive(false);
    //            GameObject.Find("firebase-message").GetComponent<oturum_ac>().girissorgula();
    //        }
    //    }
    //}
    public void profil_veri_yenile()
    {
        if (Application.loadedLevelName == "_MainMenu")
        {
            kazandıgı_maçsayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().kazandigi_mac.ToString();
            kaybettigi_maçsayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().kaybettigi_mac.ToString();
            oynadıgı_maçsayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().oynadigi_mac.ToString();
            beraberemacsayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().berabere_mac.ToString();
            öldürme_sayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().kill.ToString();
            ölüm_sayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().death.ToString();
            kupasayısı_yazısı.text = GameObject.Find("firebase-message").GetComponent<databasee>().kupa.ToString();
            
            oyunpuanı = GameObject.Find("firebase-message").GetComponent<databasee>().oyun_puani;

            toplamşimşek = GameObject.Find("firebase-message").GetComponent<databasee>().toplam_simsek;
            anamenutoplamsimsek.text = toplamşimşek.ToString();
            if (PlayerPrefs.GetInt("ölmesayımpref") != 0)
            {
                kdorani = (double)GameObject.Find("firebase-message").GetComponent<databasee>().kill / (double)GameObject.Find("firebase-message").GetComponent<databasee>().death;
                kdorani = Math.Round(kdorani, 2);

                //Debug.Log(kdorani);
                kdoraniy.text = kdorani.ToString();
            }
            else if (PlayerPrefs.GetInt("ölmesayımpref") == 0)
            {

                kdoraniy.text = "%100";
            }
            hangileveldeyim();
            hangi_ligte();
            if (seviye > PlayerPrefs.GetInt("eskiseviye"))
            {
                seviyeatladın_paneli.SetActive(true);
                hangiseviyeye_gecti.text = "SEVİYE " + seviye.ToString();
                PlayerPrefs.SetInt("eskiseviye", seviye);
            }
            if (Application.loadedLevelName == "_MainMenu")
            {
                seviyeyazısı.text = seviye.ToString();
                seviyeyazisi_anamenu.text = seviye.ToString();
                ibre_seviyegecişpuanı.text = ibrepuanı + " / " + geçispuanı_sınırı.ToString();
                levelibresi.value = (float)ibrepuanı / (float)geçispuanı_sınırı;

                // Debug.Log((float)ibrepuanı / (float)geçispuanı_sınırı);
            }
        }
        
    }
    public void profilebak_diger_userr()
    {
        if (Application.loadedLevelName == "_MainMenu")
        {
            kazandıgı_maçsayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().kazandigi_mac.ToString();
            kaybettigi_maçsayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().kaybettigi_mac.ToString();
            oynadıgı_maçsayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().oynadigi_mac.ToString();
            beraberemacsayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().berabere_mac.ToString();
            öldürme_sayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().kill.ToString();
            ölüm_sayısıy.text = GameObject.Find("firebase-message").GetComponent<databasee>().death.ToString();
            kupasayısı_yazısı.text = GameObject.Find("firebase-message").GetComponent<databasee>().kupa.ToString();

            oyunpuanı = GameObject.Find("firebase-message").GetComponent<databasee>().oyun_puani;

           
            if (GameObject.Find("firebase-message").GetComponent<databasee>().death != 0)
            {
                kdorani = (double)GameObject.Find("firebase-message").GetComponent<databasee>().kill / (double)GameObject.Find("firebase-message").GetComponent<databasee>().death;
                kdorani = Math.Round(kdorani, 2);

                //Debug.Log(kdorani);
                kdoraniy.text = kdorani.ToString();
            }
            else if (GameObject.Find("firebase-message").GetComponent<databasee>().death == 0)
            {

                kdoraniy.text = "%100";
            }
            hangileveldeyim();
            hangi_ligte();
        }

    }
    void Start () {
        if(PlayerPrefs.GetInt("geciciamkkk")==0)
        {
            profil_paneli.SetActive(true);
            cinsiyet_paneli.SetActive(true);
            PlayerPrefs.SetInt("geciciamkkk", 1);
        }
        toplamşimşek = PlayerPrefs.GetInt("TOPLAMSİMSEK");
        oyunpuanı = PlayerPrefs.GetInt("oyunpuani");
    //    public int kill_database;
    //public int death_database;
    //public int oynanan_mac_;
    //public int kazanan_mac_data;
    //public int kaybeden_mac_data;
    //public int berabere_mac_data;
    //public int altin_data;
    //public int kupa_data;
    kaybettigimacsayısı = PlayerPrefs.GetInt("kaybettigimaçsayısıı");
        kazandıgımaçsayısı = PlayerPrefs.GetInt("kazandıgımaçsayısıı");

        kupasayısı = PlayerPrefs.GetInt("kupasayısı");
        if (Application.loadedLevelName == "_MainMenu")
        {
            hangi_ligte();
            kazandıgı_maçsayısıy.text = (PlayerPrefs.GetInt("kazandıgımaçsayısıı")).ToString();
            kaybettigi_maçsayısıy.text = (PlayerPrefs.GetInt("kaybettigimaçsayısıı")).ToString();
            oynadıgı_maçsayısıy.text = (PlayerPrefs.GetInt("oynanan_macsayısı")).ToString();
            beraberemacsayısıy.text = (PlayerPrefs.GetInt("berabere_macsayısı")).ToString();
            saniye = PlayerPrefs.GetInt("oynanan_sure") % 60;
            dakika = ((PlayerPrefs.GetInt("oynanan_sure") - saniye) / 60) % 60;
            saat = (PlayerPrefs.GetInt("oynanan_sure") - dakika * 60) / 60;
            // toplam_oynadıgısürey.text = saat.ToString() + " s " + dakika.ToString() + " dk " + saniye.ToString() + " sn";
            öldürme_sayısıy.text = (PlayerPrefs.GetInt("öldürmesayımpref")).ToString();
            ölüm_sayısıy.text = (PlayerPrefs.GetInt("ölmesayımpref")).ToString();
            
            birincilsatinalindi[1] = PlayerPrefs.GetInt("birincil_2_alındı");
            birincilsatinalindi[2] = PlayerPrefs.GetInt("birincil_3_alındı");
            
            kazanan_mac_data = PlayerPrefs.GetInt("kazandıgımaçsayısıı");
            kaybeden_mac_data = PlayerPrefs.GetInt("kaybettigimaçsayısıı");
            oynanan_mac_ = PlayerPrefs.GetInt("oynanan_macsayısı");
            berabere_mac_data = PlayerPrefs.GetInt("berabere_macsayısı");
            kill_database = PlayerPrefs.GetInt("öldürmesayımpref");
            death_database = PlayerPrefs.GetInt("ölmesayımpref");
            altin_data = toplamşimşek;
                kupa_data = kupasayısı;
            if (PlayerPrefs.GetInt("birdefalıkk") == 0)
            {
                PlayerPrefs.SetInt("eskiseviye", 1);
                PlayerPrefs.SetInt("birdefalıkk", 1);
            }
            if (PlayerPrefs.GetInt("ölmesayımpref") != 0)
            {
                kdorani = (double)PlayerPrefs.GetInt("öldürmesayımpref") / (double)PlayerPrefs.GetInt("ölmesayımpref");
                kdorani = Math.Round(kdorani, 2);

                //Debug.Log(kdorani);
                kdoraniy.text = kdorani.ToString();
            }
            else if (PlayerPrefs.GetInt("ölmesayımpref") == 0)
            {

                kdoraniy.text = "%100";
            }
        }
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false)
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            GameObject.Find("firebase-message").GetComponent<databasee>().yenimi_kontrol_et_database();
        }
        girispaneli_onemli.SetActive(true);
        if (Application.loadedLevelName == "_MainMenu")
        {
            nethatası_baglanıyor.SetActive(false);
            nethatasıanapanel.SetActive(true);
            netsorgulaharbi();
        }

        //StartCoroutine(checkInternetConnection((isConnected) => {
        //    // handle connection status here
        //    if(isConnected)
        //    {
        //        if (Application.loadedLevelName == "_MainMenu")
        //        {
        //            NETHATASIPANELİ.SetActive(false);
        //            GameObject.Find("firebase-message").GetComponent<oturum_ac>().girissorgula();
        //        }
        //    }
        //    else
        //    {
        //        if (Application.loadedLevelName == "_MainMenu")
        //        {

        //            NETHATASIPANELİ.SetActive(true);
        //        }
        //    }
        //}));


        //////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////
        //Invoke("netvarmi", 0);
        ps = GetComponent<PlayerWeapons>();
        
        
        kupasayisii = kupasayısı;
        if (Application.loadedLevelName == "_MainMenu")
        {
            GameObject.Find("firebase-message").GetComponent<databasee>().score = kupasayisii;
        }
        //GameObject.Find("firebase-message").GetComponent<databasee>().kupa.text = GameObject.Find("firebase-message").GetComponent<databasee>().score.ToString();
        
        
        else if(Application.loadedLevelName != "_MainMenu")
        {
           
            if (PlayerPrefs.GetInt("harita") ==0)
            {
                GameObject.Find("asdf").GetComponent<Scoreboard>().oyunsonuharita.text = "DÖRT KÖŞE";
            }
            if (PlayerPrefs.GetInt("harita") == 1)
            {
                GameObject.Find("asdf").GetComponent<Scoreboard>().oyunsonuharita.text = "CEZA EVİ";
            }
            if (PlayerPrefs.GetInt("harita") == 2)
            {
                GameObject.Find("asdf").GetComponent<Scoreboard>().oyunsonuharita.text = "AWP KULE";
            }
            ///////////
            if (PlayerPrefs.GetInt("oyunmod_") == 0)
            {
                GameObject.Find("asdf").GetComponent<Scoreboard>().oyunsonumod.text = "KLASİK";
            }
            if (PlayerPrefs.GetInt("oyunmod_") == 1)
            {
                GameObject.Find("asdf").GetComponent<Scoreboard>().oyunsonumod.text = "HERKES TEK";
            }
            sayac();
        }
        
        
        
        //satınalsilahgosterimi();
        
        //toplamşimşek = 0;
        // PlayerPrefs.SetInt("TOPLAMSİMSEK",toplamşimşek);



        /////// YENİLİK BİLDİRİMİ//////////////////////////////////
        if(PlayerPrefs.GetInt("yenilik")==0)
        {
            yeniliklerpaneli.SetActive(true);
            PlayerPrefs.SetInt("yenilik", 1);
        }
        ////////////////////////////////////////////////////////////////

        Debug.Log(PlayerPrefs.GetInt("öldürmesayımpref") + " öldürme sayısı");
        Debug.Log(PlayerPrefs.GetInt("ölmesayımpref") + " ölme sayısı");

        
        //ump40_skins_satınalındı[0] = 1;
        //awp_skins_satınalındı[0] = 1;
        //pompalı_skins_satınalındı[0] = 1;
        //tabanca_skins_satınalındı[0] = 1;
        //bıçak_skins_satınalındı[0] = 1;
        PlayerPrefs.SetInt("ump40_0_satınalındı",1);
        PlayerPrefs.SetInt("awp_0_satınalındı", 1);
        PlayerPrefs.SetInt("pompalı_0_satınalındı", 1);
        PlayerPrefs.SetInt("tabanca_0_satınalındı", 1);
        PlayerPrefs.SetInt("bıçak_0_satınalındı", 1);
        if(0== PlayerPrefs.GetInt("deneme456784"))
        {
            PlayerPrefs.SetInt("ump40_0_kusanıldı", 1);
            PlayerPrefs.SetInt("awp_0_kusanıldı", 1);
            PlayerPrefs.SetInt("pompalı_0_kusanıldı", 1);
            PlayerPrefs.SetInt("tabanca_0_kusanıldı", 1);
            PlayerPrefs.SetInt("bıçak_0_kusanıldı", 1);

            PlayerPrefs.SetInt("deneme456784", 1);

        }


        if (Application.loadedLevelName == "_MainMenu")
        {
            birincilsatinalindi[0] = 1;
            ikincilsatinalindi[0] = 1;
            bıcaksatinalindi[0] = 1;

            birincilkuşanıldı[0] = PlayerPrefs.GetInt("birincil_1_kusanıldı");
            birincilkuşanıldı[1] = PlayerPrefs.GetInt("birincil_2_kusanıldı");
            birincilkuşanıldı[2] = PlayerPrefs.GetInt("birincil_3_kusanıldı");
        }

        tekdefalık = PlayerPrefs.GetInt("tekdefalik");
        if (tekdefalık==0)
        {
            birincilkuşanıldı[0] = 1;
            PlayerPrefs.SetInt("tekdefalik", 1);
            tekdefalık = 1;
        }
        if (Application.loadedLevelName == "_MainMenu")
        {
            ikincilkuşanıldı[0] = 1;
            bıcakkuşanıldı[0] = 1;


            kusanıldıyazisi.text = "Kuşanıldı";
            kusanıldıresmi.SetActive(true);
            kontrol();


            birincil_silahsayısı = birincilsilahlar.Length;
            ikincil_silahsayısı = ikincilsilahlar.Length;
            bıcaksayısı = bıçaklar.Length;


            if (birincil_silahsayısı > 1)
            {
                birincildevamyokileri.SetActive(false);
            }
            else
            {
                birincildevamyokileri.SetActive(true);
            }

            if (ikincil_silahsayısı > 1)
            {
                ikincildevamyokileri.SetActive(false);
            }
            else
            {
                ikincildevamyokileri.SetActive(true);
            }
            if (bıcaksayısı > 1)
            {
                bıcakdevamyokileri.SetActive(false);
            }
            else
            {
                bıcakdevamyokileri.SetActive(true);
            }


            anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);

            yenile_silah_skinlerini();
        }
        //for (int i = 0; i < 11; i++)
        //{
        //    ump40_skins_satınalındı[i] = PlayerPrefs.GetInt("ump40_" + i.ToString() + "_satınalındı");
        //}
        //for (int i = 0; i < 11; i++)
        //{
        //    awp_skins_satınalındı[i] = PlayerPrefs.GetInt("awp_" + i.ToString() + "_satınalındı");
        //}
        //for (int i = 0; i < 11; i++)
        //{
        //    pompalı_skins_satınalındı[i] = PlayerPrefs.GetInt("pompalı_" + i.ToString() + "_satınalındı");
        //}
        //for (int i = 0; i < 11; i++)
        //{
        //    tabanca_skins_satınalındı[i] = PlayerPrefs.GetInt("tabanca_" + i.ToString() + "_satınalındı");
        //}
        //for (int i = 0; i < 11; i++)
        //{
        //    bıçak_skins_satınalındı[i] = PlayerPrefs.GetInt("bıçak_" + i.ToString() + "_satınalındı");
        //}
        //ump40_skins_satınalındı[0] = 1;
        //awp_skins_satınalındı[0] = 1;
        //pompalı_skins_satınalındı[0] = 1;
        //tabanca_skins_satınalındı[0] = 1;
        //bıçak_skins_satınalındı[0] = 1;
    }
    public void yenile_silah_skinlerini()
    {
        for (int i = 0; i < 11; i++)
        {
            if (PlayerPrefs.GetInt("ump40_" + i.ToString() + "_kusanıldı") == 1)
            {
                birincilsilahlar[0].GetComponent<MeshRenderer>().material = ump40_skins[i];
            }
        }
        for (int i = 0; i < 11; i++)
        {
            if (PlayerPrefs.GetInt("awp_" + i.ToString() + "_kusanıldı") == 1)
            {
                birincilsilahlar[1].GetComponent<MeshRenderer>().material = awp_skins[i];
            }
        }
        for (int i = 0; i < 11; i++)
        {
            if (PlayerPrefs.GetInt("pompalı_" + i.ToString() + "_kusanıldı") == 1)
            {
                birincilsilahlar[2].GetComponent<MeshRenderer>().material = pompalı_skins[i];
            }
        }
        for (int i = 0; i < 11; i++)
        {
            if (PlayerPrefs.GetInt("tabanca_" + i.ToString() + "_kusanıldı") == 1)
            {
                ikincilsilahlar[0].GetComponent<MeshRenderer>().material = tabanca_skins[i];
            }
        }
        for (int i = 0; i < 11; i++)
        {
            if (PlayerPrefs.GetInt("bıçak_" + i.ToString() + "_kusanıldı") == 1)
            {
                bıçaklar[0].GetComponent<MeshRenderer>().material = bıçak_skins[i];
            }
        }

    }
    public void skin_sıfırla_ump40()
    {
        skin_hangi_silahta = "ump40_";

        skin_geçmesayısı = 0;
        ump40_skin_obje.GetComponent<MeshRenderer>().material = ump40_skins[skin_geçmesayısı];
        skin_fiyatı_yazisi.text= ump40_skin_satınal_fiyatı[0].ToString();
        skin_ismi_yazisi.text = ump40_skin_ismi[0].ToString();
        
        
        
        if (0 == PlayerPrefs.GetInt("ump40_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            ump40_skin_satınal_butonu.SetActive(true);
            ump40_skin_kuşan_butonu.SetActive(false);
            
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("ump40_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                ump40_skin_kuşan_butonu.SetActive(false);
                ump40_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                ump40_skin_kuşan_butonu.SetActive(true);
                ump40_skin_satınal_butonu.SetActive(false);
            }
        }
        awp_skin_satınal_butonu.SetActive(false);
        awp_skin_kuşan_butonu.SetActive(false);
        pompalı_skin_satınal_butonu.SetActive(false);
        pompalı_skin_kuşan_butonu.SetActive(false);
        tabanca_skin_satınal_butonu.SetActive(false);
        tabanca_skin_kuşan_butonu.SetActive(false);
        bıçak_skin_satınal_butonu.SetActive(false);
        bıçak_skin_kuşan_butonu.SetActive(false);
    }
    public void skin_sıfırla_awp()
    {
        skin_hangi_silahta = "awp_";

        skin_geçmesayısı = 0;
        awp_skin_obje.GetComponent<MeshRenderer>().material = awp_skins[skin_geçmesayısı];
        skin_fiyatı_yazisi.text = awp_skin_satınal_fiyatı[0].ToString();
        skin_ismi_yazisi.text = awp_skin_ismi[0].ToString();
        if (0 == PlayerPrefs.GetInt("awp_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            awp_skin_satınal_butonu.SetActive(true);
            awp_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("awp_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                awp_skin_kuşan_butonu.SetActive(false);
                awp_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                awp_skin_kuşan_butonu.SetActive(true);
                awp_skin_satınal_butonu.SetActive(false);
            }
        }
        ump40_skin_satınal_butonu.SetActive(false);
        ump40_skin_kuşan_butonu.SetActive(false);
        
        pompalı_skin_satınal_butonu.SetActive(false);
        pompalı_skin_kuşan_butonu.SetActive(false);
        tabanca_skin_satınal_butonu.SetActive(false);
        tabanca_skin_kuşan_butonu.SetActive(false);
        bıçak_skin_satınal_butonu.SetActive(false);
        bıçak_skin_kuşan_butonu.SetActive(false);
    }
    public void skin_sıfırla_pompalı()
    {
        skin_hangi_silahta = "pompalı_";

        skin_geçmesayısı = 0;
        skin_fiyatı_yazisi.text = pompalı_skin_satınal_fiyatı[0].ToString();
        skin_ismi_yazisi.text = pompalı_skin_ismi[0].ToString();
        pompalı_skin_obje.GetComponent<MeshRenderer>().material = pompalı_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("pompalı_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            pompalı_skin_satınal_butonu.SetActive(true);
            pompalı_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("pompalı_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                pompalı_skin_kuşan_butonu.SetActive(false);
                pompalı_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                pompalı_skin_kuşan_butonu.SetActive(true);
                pompalı_skin_satınal_butonu.SetActive(false);
            }
        }
        ump40_skin_satınal_butonu.SetActive(false);
        ump40_skin_kuşan_butonu.SetActive(false);
        awp_skin_satınal_butonu.SetActive(false);
        awp_skin_kuşan_butonu.SetActive(false);
        
        tabanca_skin_satınal_butonu.SetActive(false);
        tabanca_skin_kuşan_butonu.SetActive(false);
        bıçak_skin_satınal_butonu.SetActive(false);
        bıçak_skin_kuşan_butonu.SetActive(false);
    }
    public void skin_sıfırla_tabanca()
    {
        skin_hangi_silahta = "tabanca_";

        skin_geçmesayısı = 0;
        skin_fiyatı_yazisi.text = tabanca_skin_satınal_fiyatı[0].ToString();
        skin_ismi_yazisi.text = tabanca_skin_ismi[0].ToString();
        tabanca_skin_obje.GetComponent<MeshRenderer>().material = tabanca_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("tabanca_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            tabanca_skin_satınal_butonu.SetActive(true);
            tabanca_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("tabanca_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                tabanca_skin_kuşan_butonu.SetActive(false);
                tabanca_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                tabanca_skin_kuşan_butonu.SetActive(true);
                tabanca_skin_satınal_butonu.SetActive(false);
            }
        }
        ump40_skin_satınal_butonu.SetActive(false);
        ump40_skin_kuşan_butonu.SetActive(false);
        awp_skin_satınal_butonu.SetActive(false);
        awp_skin_kuşan_butonu.SetActive(false);
        pompalı_skin_satınal_butonu.SetActive(false);
        pompalı_skin_kuşan_butonu.SetActive(false);
        
        bıçak_skin_satınal_butonu.SetActive(false);
        bıçak_skin_kuşan_butonu.SetActive(false);
    }
    public void skin_sıfırla_bıçak()
    {
        skin_hangi_silahta = "bıçak_";
        skin_geçmesayısı = 0;
        skin_fiyatı_yazisi.text = bıçak_skin_satınal_fiyatı[0].ToString();
        skin_ismi_yazisi.text = bıçak_skin_ismi[0].ToString();
        bıçak_skin_obje.GetComponent<MeshRenderer>().material = bıçak_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("bıçak_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            bıçak_skin_satınal_butonu.SetActive(true);
            bıçak_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("bıçak_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                bıçak_skin_kuşan_butonu.SetActive(false);
                bıçak_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                bıçak_skin_kuşan_butonu.SetActive(true);
                bıçak_skin_satınal_butonu.SetActive(false);
            }
        }
        ump40_skin_satınal_butonu.SetActive(false);
        ump40_skin_kuşan_butonu.SetActive(false);
        awp_skin_satınal_butonu.SetActive(false);
        awp_skin_kuşan_butonu.SetActive(false);
        pompalı_skin_satınal_butonu.SetActive(false);
        pompalı_skin_kuşan_butonu.SetActive(false);
        tabanca_skin_satınal_butonu.SetActive(false);
        tabanca_skin_kuşan_butonu.SetActive(false);
        
    }
    public void skin_kusan()
    {
        if (0 == PlayerPrefs.GetInt(skin_hangi_silahta + skin_geçmesayısı.ToString() + "_kusanıldı"))
        {
            for (int i = 0; i < 11; i++)
            {
                if (skin_geçmesayısı == i)
                {
                    PlayerPrefs.SetInt(skin_hangi_silahta + skin_geçmesayısı.ToString() + "_kusanıldı", 1);
                    if (skin_hangi_silahta == "ump40_")
                    {
                        ump40_skin_kuşan_butonu.SetActive(false);

                    }
                    if (skin_hangi_silahta == "awp_")
                    {
                        awp_skin_kuşan_butonu.SetActive(false);

                    }
                    if (skin_hangi_silahta == "pompalı_")
                    {
                        pompalı_skin_kuşan_butonu.SetActive(false);

                    }
                    if (skin_hangi_silahta == "tabanca_")
                    {
                        tabanca_skin_kuşan_butonu.SetActive(false);

                    }
                    if (skin_hangi_silahta == "bıçak_")
                    {
                        bıçak_skin_kuşan_butonu.SetActive(false);

                    }
                }
                else
                {
                    PlayerPrefs.SetInt(skin_hangi_silahta + i.ToString() + "_kusanıldı", 0);
                }
            }

        }


           
    }
    public void skin_satın_al()
    {
        if (0 == PlayerPrefs.GetInt(skin_hangi_silahta + skin_geçmesayısı.ToString() + "_satınalındı"))
        {
            if(skin_hangi_silahta=="ump40_")
            {
                if(toplamşimşek>=ump40_skin_satınal_fiyatı[skin_geçmesayısı])
                {
                    toplamşimşek -= ump40_skin_satınal_fiyatı[skin_geçmesayısı];
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    PlayerPrefs.SetInt(skin_hangi_silahta + skin_geçmesayısı.ToString() + "_satınalındı",1);
                    ump40_skin_satınal_butonu.SetActive(false);
                    ump40_skin_kuşan_butonu.SetActive(true);
                    skin_satınal_paneli.SetActive(false);
                    

                }
                else
                {
                    yetersizbakiye.SetActive(true);
                }
            }
            if (skin_hangi_silahta == "awp_")
            {
                if (toplamşimşek >= awp_skin_satınal_fiyatı[skin_geçmesayısı])
                {
                    toplamşimşek -= awp_skin_satınal_fiyatı[skin_geçmesayısı];
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    PlayerPrefs.SetInt(skin_hangi_silahta + skin_geçmesayısı.ToString() + "_satınalındı", 1);
                    awp_skin_satınal_butonu.SetActive(false);
                    awp_skin_kuşan_butonu.SetActive(true);
                    skin_satınal_paneli.SetActive(false);
                }
                else
                {
                    yetersizbakiye.SetActive(true);
                }
            }
            if (skin_hangi_silahta == "pompalı_")
            {
                if (toplamşimşek >= pompalı_skin_satınal_fiyatı[skin_geçmesayısı])
                {
                    toplamşimşek -= pompalı_skin_satınal_fiyatı[skin_geçmesayısı];
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    PlayerPrefs.SetInt(skin_hangi_silahta + skin_geçmesayısı.ToString() + "_satınalındı", 1);
                    pompalı_skin_satınal_butonu.SetActive(false);
                    pompalı_skin_kuşan_butonu.SetActive(true);
                    skin_satınal_paneli.SetActive(false);
                }
                else
                {
                    yetersizbakiye.SetActive(true);
                }
            }
            if (skin_hangi_silahta == "tabanca_")
            {
                if (toplamşimşek >= tabanca_skin_satınal_fiyatı[skin_geçmesayısı])
                {
                    toplamşimşek -= tabanca_skin_satınal_fiyatı[skin_geçmesayısı];
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    PlayerPrefs.SetInt(skin_hangi_silahta + skin_geçmesayısı.ToString() + "_satınalındı", 1);
                    tabanca_skin_satınal_butonu.SetActive(false);
                    tabanca_skin_kuşan_butonu.SetActive(true);
                    skin_satınal_paneli.SetActive(false);
                }
                else
                {
                    yetersizbakiye.SetActive(true);
                }
            }
            if (skin_hangi_silahta == "bıçak_")
            {
                if (toplamşimşek >= bıçak_skin_satınal_fiyatı[skin_geçmesayısı])
                {
                    toplamşimşek -= bıçak_skin_satınal_fiyatı[skin_geçmesayısı];
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    PlayerPrefs.SetInt(skin_hangi_silahta + skin_geçmesayısı.ToString() + "_satınalındı", 1);
                    bıçak_skin_satınal_butonu.SetActive(false);
                    bıçak_skin_kuşan_butonu.SetActive(true);
                    skin_satınal_paneli.SetActive(false);
                }
                else
                {
                    yetersizbakiye.SetActive(true);
                }
            }

        }
    }
    public void ump40_desen_ileri()
    {
        skin_geçmesayısı += 1;
        
        if (skin_geçmesayısı>10)
        {
            skin_geçmesayısı = 0;
        }
        skin_fiyatı_yazisi.text = ump40_skin_satınal_fiyatı[skin_geçmesayısı].ToString();
        skin_ismi_yazisi.text = ump40_skin_ismi[skin_geçmesayısı].ToString();
        ump40_skin_obje.GetComponent<MeshRenderer>().material = ump40_skins[skin_geçmesayısı];
        
         if(0== PlayerPrefs.GetInt("ump40_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            ump40_skin_satınal_butonu.SetActive(true);
            ump40_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if(1 == PlayerPrefs.GetInt("ump40_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                ump40_skin_kuşan_butonu.SetActive(false);
                ump40_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                ump40_skin_kuşan_butonu.SetActive(true);
                ump40_skin_satınal_butonu.SetActive(false);
            }
        }
        
    }
    public void ump40_desen_geri()
    {

        skin_geçmesayısı -= 1;
        
        if (skin_geçmesayısı < 0)
        {
            skin_geçmesayısı = 10;
        }
        skin_fiyatı_yazisi.text = ump40_skin_satınal_fiyatı[skin_geçmesayısı].ToString();
        skin_ismi_yazisi.text = ump40_skin_ismi[skin_geçmesayısı].ToString();
        ump40_skin_obje.GetComponent<MeshRenderer>().material = ump40_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("ump40_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            ump40_skin_satınal_butonu.SetActive(true);
            ump40_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("ump40_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                ump40_skin_kuşan_butonu.SetActive(false);
                ump40_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                ump40_skin_kuşan_butonu.SetActive(true);
                ump40_skin_satınal_butonu.SetActive(false);
            }
        }
    }
    public void awp_desen_ileri()
    {
        skin_geçmesayısı += 1;
        if (skin_geçmesayısı > 10)
        {
            skin_geçmesayısı = 0;

        }
        skin_fiyatı_yazisi.text = awp_skin_satınal_fiyatı[skin_geçmesayısı].ToString();
        skin_ismi_yazisi.text = awp_skin_ismi[skin_geçmesayısı].ToString();
        awp_skin_obje.GetComponent<MeshRenderer>().material = awp_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("awp_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            awp_skin_satınal_butonu.SetActive(true);
            awp_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("awp_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                awp_skin_kuşan_butonu.SetActive(false);
                awp_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                awp_skin_kuşan_butonu.SetActive(true);
                awp_skin_satınal_butonu.SetActive(false);
            }
        }
    }
    public void awp_desen_geri()
    {
        skin_geçmesayısı -= 1;
        if (skin_geçmesayısı < 0)
        {
            skin_geçmesayısı = 10;
        }
        skin_fiyatı_yazisi.text = awp_skin_satınal_fiyatı[skin_geçmesayısı].ToString();
        skin_ismi_yazisi.text = awp_skin_ismi[skin_geçmesayısı].ToString();
        awp_skin_obje.GetComponent<MeshRenderer>().material = awp_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("awp_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            awp_skin_satınal_butonu.SetActive(true);
            awp_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("awp_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                awp_skin_kuşan_butonu.SetActive(false);
                awp_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                awp_skin_kuşan_butonu.SetActive(true);
                awp_skin_satınal_butonu.SetActive(false);
            }
        }
    }
    public void pompalı_desen_ileri()
    {
        skin_geçmesayısı += 1;
        if (skin_geçmesayısı > 10)
        {
            skin_geçmesayısı = 0;
        }
        skin_fiyatı_yazisi.text = pompalı_skin_satınal_fiyatı[skin_geçmesayısı].ToString();
        skin_ismi_yazisi.text = pompalı_skin_ismi[skin_geçmesayısı].ToString();
        pompalı_skin_obje.GetComponent<MeshRenderer>().material = pompalı_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("pompalı_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            pompalı_skin_satınal_butonu.SetActive(true);
            pompalı_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("pompalı_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                pompalı_skin_satınal_butonu.SetActive(false);
                pompalı_skin_kuşan_butonu.SetActive(false);
            }
            else
            {
                pompalı_skin_kuşan_butonu.SetActive(true);
                pompalı_skin_satınal_butonu.SetActive(false);
            }
        }
    }
    public void pompalı_desen_geri()
    {
        skin_geçmesayısı -= 1;
        if (skin_geçmesayısı < 0)
        {
            skin_geçmesayısı = 10;
        }
        skin_fiyatı_yazisi.text = pompalı_skin_satınal_fiyatı[skin_geçmesayısı].ToString();
        skin_ismi_yazisi.text = pompalı_skin_ismi[skin_geçmesayısı].ToString();
        pompalı_skin_obje.GetComponent<MeshRenderer>().material = pompalı_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("pompalı_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            pompalı_skin_satınal_butonu.SetActive(true);
            pompalı_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("pompalı_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                pompalı_skin_kuşan_butonu.SetActive(false);
                pompalı_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                pompalı_skin_kuşan_butonu.SetActive(true);
                pompalı_skin_satınal_butonu.SetActive(false);
            }
        }
    }
    public void tabanca_desen_ileri()
    {
        skin_geçmesayısı += 1;
        if (skin_geçmesayısı > 10)
        {
            skin_geçmesayısı = 0;
        }
        skin_fiyatı_yazisi.text = tabanca_skin_satınal_fiyatı[skin_geçmesayısı].ToString();
        skin_ismi_yazisi.text = tabanca_skin_ismi[skin_geçmesayısı].ToString();
        tabanca_skin_obje.GetComponent<MeshRenderer>().material = tabanca_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("tabanca_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            tabanca_skin_satınal_butonu.SetActive(true);
            tabanca_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("tabanca_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                tabanca_skin_satınal_butonu.SetActive(false);
                tabanca_skin_kuşan_butonu.SetActive(false);
            }
            else
            {
                tabanca_skin_kuşan_butonu.SetActive(true);
                tabanca_skin_satınal_butonu.SetActive(false);
            }
        }
    }
    public void tabanca_desen_geri()
    {
        skin_geçmesayısı -= 1;
        if (skin_geçmesayısı < 0)
        {
            skin_geçmesayısı = 10;

        }
        skin_fiyatı_yazisi.text = tabanca_skin_satınal_fiyatı[skin_geçmesayısı].ToString();
        skin_ismi_yazisi.text = tabanca_skin_ismi[skin_geçmesayısı].ToString();

        tabanca_skin_obje.GetComponent<MeshRenderer>().material = tabanca_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("tabanca_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            tabanca_skin_satınal_butonu.SetActive(true);
            tabanca_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("tabanca_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                tabanca_skin_kuşan_butonu.SetActive(false);
                tabanca_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                tabanca_skin_kuşan_butonu.SetActive(true);
                tabanca_skin_satınal_butonu.SetActive(false);
            }
        }
    }
    public void bıçak_desen_ileri()
    {
        skin_geçmesayısı += 1;
        if (skin_geçmesayısı > 10)
        {
            skin_geçmesayısı = 0;
        }
        skin_fiyatı_yazisi.text = bıçak_skin_satınal_fiyatı[skin_geçmesayısı].ToString();
        skin_ismi_yazisi.text = bıçak_skin_ismi[skin_geçmesayısı].ToString();
        bıçak_skin_obje.GetComponent<MeshRenderer>().material = bıçak_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("bıçak_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            bıçak_skin_satınal_butonu.SetActive(true);
            bıçak_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("bıçak_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                bıçak_skin_satınal_butonu.SetActive(false);
                bıçak_skin_kuşan_butonu.SetActive(false);
            }
            else
            {
                bıçak_skin_kuşan_butonu.SetActive(true);
                bıçak_skin_satınal_butonu.SetActive(false);
            }
        }
    }
    public void bıçak_desen_geri()
    {
        skin_geçmesayısı -= 1;
        if (skin_geçmesayısı < 0)
        {
            skin_geçmesayısı = 10;
        }
        skin_fiyatı_yazisi.text = bıçak_skin_satınal_fiyatı[skin_geçmesayısı].ToString();
        skin_ismi_yazisi.text = bıçak_skin_ismi[skin_geçmesayısı].ToString();
        bıçak_skin_obje.GetComponent<MeshRenderer>().material = bıçak_skins[skin_geçmesayısı];
        if (0 == PlayerPrefs.GetInt("bıçak_" + skin_geçmesayısı.ToString() + "_satınalındı"))

        {
            bıçak_skin_satınal_butonu.SetActive(true);
            bıçak_skin_kuşan_butonu.SetActive(false);
        }
        else
        {
            if (1 == PlayerPrefs.GetInt("bıçak_" + skin_geçmesayısı.ToString() + "_kusanıldı"))
            {
                bıçak_skin_kuşan_butonu.SetActive(false);
                bıçak_skin_satınal_butonu.SetActive(false);
            }
            else
            {
                bıçak_skin_kuşan_butonu.SetActive(true);
                bıçak_skin_satınal_butonu.SetActive(false);
            }
        }
    }
    public void kontrol()
    {

        if (hangibaslık == 1)
        {
            for (int i = 0; i < birincil_silahsayısı; i++)
            {
                if (birincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;


                    if (birincilkuşanıldı[kacıncısilah] == 0)
                    {
                        kusanıldıyazisi.text = "Kuşan";
                        kusanıldıresmi.SetActive(false);

                    }
                    else if (birincilkuşanıldı[kacıncısilah] == 1)
                    {
                        kusanıldıyazisi.text = "Kuşanıldı";
                        kusanıldıresmi.SetActive(true);

                    }
                }
            }
            if (birincilsatinalindi[kacıncısilah] == 0)
            {
                satınalbutonu.SetActive(true);
                
                kusanbutonu.SetActive(false);

                satınalfiyatı.text = Convert.ToString(birincilfiyatlar[kacıncısilah]);
            }
            else
            {
                satınalbutonu.SetActive(false);
                kusanbutonu.SetActive(true);


            }

            

           

            
        }
        if (hangibaslık == 2)
        {
            for (int i = 0; i < ikincil_silahsayısı; i++)
            {
                if (ikincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;
                    if (ikincilkuşanıldı[kacıncısilah] == 0)
                    {
                        kusanıldıyazisi.text = "Kuşan";
                        kusanıldıresmi.SetActive(false);

                    }
                    else if (ikincilkuşanıldı[kacıncısilah] == 1)
                    {
                        kusanıldıyazisi.text = "Kuşanıldı";
                        kusanıldıresmi.SetActive(true);

                    }
                }
            }
            if (ikincilsatinalindi[kacıncısilah] == 0)
            {
                satınalbutonu.SetActive(true);
                kusanbutonu.SetActive(false);
                satınalfiyatı.text = Convert.ToString(ikincilfiyatlar[kacıncısilah]);
            }
            else
            {
                satınalbutonu.SetActive(false);
                kusanbutonu.SetActive(true);

            }
            
        }
        if (hangibaslık == 3)
        {
            for (int i = 0; i < bıcaksayısı; i++)
            {
                if (bıçaklar[i].active == true)
                {
                    kacıncısilah = i;
                    if (bıcakkuşanıldı[kacıncısilah] == 0)
                    {
                        kusanıldıyazisi.text = "Kuşan";
                        kusanıldıresmi.SetActive(false);

                    }
                    else if (bıcakkuşanıldı[kacıncısilah] == 1)
                    {
                        kusanıldıyazisi.text = "Kuşanıldı";
                        kusanıldıresmi.SetActive(true);

                    }
                }
            }
            if (bıcaksatinalindi[kacıncısilah] == 0)
            {
                satınalbutonu.SetActive(true);
                kusanbutonu.SetActive(false);
                satınalfiyatı.text = Convert.ToString(bıcakfiyatlar[kacıncısilah]);
            }
            else
            {
                satınalbutonu.SetActive(false);
                kusanbutonu.SetActive(true);

            }
            
        }
    }
    public void birincil_ileri()
    {

       
        Invoke("ozellikler", 0);
        if (a < birincil_silahsayısı - 1)
        {

            a++;
            birincildevamyokgeri.SetActive(false);

            birincilsilahlar[a - 1].SetActive(false);
            birincilsilahlar[a].SetActive(true);
            birincildevamyokileri.SetActive(false);

            if (a == birincil_silahsayısı - 1)
            {
                birincildevamyokileri.SetActive(true);
            }
        }
        else
        {
            birincildevamyokileri.SetActive(true);


        }
        kontrol();
    }
    public void birincil_geri()
    {
        
        Invoke("ozellikler", 0);
        if (0 < a)
        {
            a--;
            birincildevamyokileri.SetActive(false);

            birincilsilahlar[a + 1].SetActive(false);
            birincilsilahlar[a].SetActive(true);
            birincildevamyokgeri.SetActive(false);

            if (a == 0)
            {
                birincildevamyokgeri.SetActive(true);
            }
        }
        else
        {
            birincildevamyokgeri.SetActive(true);
        }
        kontrol();
    }
    public void ikincil_ileri()
    {
        
        Invoke("ozellikler", 0);
        if (b < ikincil_silahsayısı - 1)
        {
            b++;
            ikincildevamyokgeri.SetActive(false);
            ikincilsilahlar[b - 1].SetActive(false);
            ikincilsilahlar[b].SetActive(true);
            ikincildevamyokileri.SetActive(false);

            if (b == ikincil_silahsayısı - 1)
            {
                ikincildevamyokileri.SetActive(true);
            }
        }
        else
        {

        }
        kontrol();
    }
    public void ikincil_geri()
    {
       
        Invoke("ozellikler", 0);
        if (0 < b)
        {
            b--;
            ikincildevamyokileri.SetActive(false);

            ikincilsilahlar[b + 1].SetActive(false);
            ikincilsilahlar[b].SetActive(true);
            ikincildevamyokgeri.SetActive(false);

            if (b == 0)
            {
                ikincildevamyokgeri.SetActive(true);
            }


        }
        else
        {
            ikincildevamyokgeri.SetActive(true);

        }
        kontrol();
    }


    public void bıcak_ileri()
    {
        
        Invoke("ozellikler", 0);
        if (c < bıcaksayısı - 1)
        {
            c++;
            bıcakdevamyokgeri.SetActive(false);
            bıçaklar[c - 1].SetActive(false);
            bıçaklar[c].SetActive(true);
            bıcakdevamyokileri.SetActive(false);

            if (c == bıcaksayısı - 1)
            {
                bıcakdevamyokileri.SetActive(true);
            }

        }
        else
        {
            bıcakdevamyokileri.SetActive(true);

        }
        kontrol();
    }
    public void bıcak_geri()
    {
        
        Invoke("ozellikler", 0);
        if (0 < c)
        {
            c--;
            bıcakdevamyokileri.SetActive(false);

            bıcakdevamyokgeri.SetActive(false);
            bıçaklar[c + 1].SetActive(false);
            bıçaklar[c].SetActive(true);
            if (c == 0)
            {
                bıcakdevamyokgeri.SetActive(true);
            }

        }
        else
        {
            bıcakdevamyokgeri.SetActive(true);

        }
        kontrol();
    }
    
    public void marketbutonu()
    {
        hangibaslık = 1;

        kontrol();
        Invoke("ozellikler", 0);
    }
    public void ozellikler()
    {
        
        if (hangibaslık == 1)
        {
            for (int i = 0; i < birincil_silahsayısı; i++)
            {
                if (birincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;
                    hasar_slider.value = birincilhasar[i] / 100f;
                    atıshizi_slider.value = birincilatıshizi[i] / 100f;
                    isabetorani_slider.value = birincilisabetorani[i] / 100f;
                    sarjorkapasitesi_slider.value = birincilsarjorkapasitesi[i] / 100f;
                }
            }

        }
        if (hangibaslık == 2)
        {
            for (int i = 0; i < ikincil_silahsayısı; i++)
            {
                if (ikincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;
                    hasar_slider.value = ikincilhasar[i] / 100f;
                    atıshizi_slider.value = ikincilatıshizi[i] / 100f;
                    isabetorani_slider.value = ikincilisabetorani[i] / 100f;
                    sarjorkapasitesi_slider.value = ikincilsarjorkapasitesi[i] / 100f;
                }
            }
        }
        if (hangibaslık == 3)
        {
            for (int i = 0; i < bıcaksayısı; i++)
            {
                if (bıçaklar[i].active == true)
                {
                    kacıncısilah = i;
                    hasar_slider.value = bıcakhasar[i] / 100f;
                    atıshizi_slider.value = bıcakatıshizi[i] / 100;
                    isabetorani_slider.value = bıcakisabetorani[i] / 100f;
                    sarjorkapasitesi_slider.value = bıcaksarjorkapasitesi[i] / 100f;
                }
            }
        }
    }
    public void birincil()
    {
        hangibaslık = 1;

        for (int i = 0; i < birincil_silahsayısı; i++)
        {
            if (birincilsilahlar[i].active == true)
            {
                kacıncısilah = i;
            }
        }
       
        Invoke("ozellikler", 0);
        kontrol();
    }
    public void ikincil()
    {
        hangibaslık = 2;
        for (int i = 0; i < ikincil_silahsayısı; i++)
        {
            if (ikincilsilahlar[i].active == true)
            {
                kacıncısilah = i;
            }
        }
       
        Invoke("ozellikler", 0);
        kontrol();
    }
    public void bıcaklar()
    {
        hangibaslık = 3;
        for (int i = 0; i < bıcaksayısı; i++)
        {
            if (bıçaklar[i].active == true)
            {
                kacıncısilah = i;
            }
        }
        
        Invoke("ozellikler", 0);
        kontrol();
    }

    


    public void kuşan()
    {
        if (hangibaslık == 1)
        {
            for (int i = 0; i < birincil_silahsayısı; i++)
            {
                if (birincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;

                    if(kacıncısilah==0)
                    {
                        
                        PlayerPrefs.SetInt("birincil_1_kusanıldı", 1);
                        PlayerPrefs.SetInt("birincil_2_kusanıldı", 0);
                        PlayerPrefs.SetInt("birincil_3_kusanıldı", 0);
                    }
                    if (kacıncısilah == 1)
                    {
                        PlayerPrefs.SetInt("birincil_1_kusanıldı", 0);
                        PlayerPrefs.SetInt("birincil_2_kusanıldı", 1);
                        PlayerPrefs.SetInt("birincil_3_kusanıldı", 0);
                    }
                    if (kacıncısilah == 2)
                    {
                        PlayerPrefs.SetInt("birincil_1_kusanıldı", 0);
                        PlayerPrefs.SetInt("birincil_2_kusanıldı", 0);
                        PlayerPrefs.SetInt("birincil_3_kusanıldı", 1);
                    }
                    if (birincilkuşanıldı[kacıncısilah] == 0)
                    {
                        kusanıldıyazisi.text = "Kuşanıldı";
                        kusanıldıresmi.SetActive(true);

                        for (int a = 0; a < birincil_silahsayısı; a++)
                        {
                            if (kacıncısilah == a)
                            {
                                
                                birincilkuşanıldı[a] = 1;
                                birincilsilah_anamenu[a] = 1;
                                PlayerPrefs.SetInt("birincilsilah",a);
                            }
                            else
                            {
                                birincilkuşanıldı[a] = 0;
                                birincilsilah_anamenu[a] = 0;
                            }
                           // satınalsilahgosterimi();
                        }

                    }



                }
            }
            if (hangibaslık == 2)
            {
                for (int i = 0; i < ikincil_silahsayısı; i++)
                {
                    if (ikincilsilahlar[i].active == true)
                    {
                        kacıncısilah = i;


                        if (ikincilkuşanıldı[kacıncısilah] == 0)
                        {
                            kusanıldıyazisi.text = "Kuşanıldı";
                            kusanıldıresmi.SetActive(true);

                            for (int a = 0; a < ikincil_silahsayısı; a++)
                            {
                                if (kacıncısilah == a)
                                {
                                    ikincilkuşanıldı[a] = 1;
                                    PlayerPrefs.SetInt("ikincilsilah", a);
                                }
                                else
                                {
                                    ikincilkuşanıldı[a] = 0;
                                }
                            }
                        }

                    }


                }
            }
            if (hangibaslık == 3)
            {
                for (int i = 0; i < bıcaksayısı; i++)
                {
                    if (bıçaklar[i].active == true)
                    {
                        kacıncısilah = i;


                        if (bıcakkuşanıldı[kacıncısilah] == 0)
                        {
                            kusanıldıyazisi.text = "Kuşanıldı";
                            kusanıldıresmi.SetActive(true);

                            for (int a = 0; a < bıcaksayısı; a++)
                            {
                                if (kacıncısilah == a)
                                {
                                    bıcakkuşanıldı[a] = 1;
                                    PlayerPrefs.SetInt("bicaksilah", a);
                                }
                                else
                                {
                                    bıcakkuşanıldı[a] = 0;
                                }
                            }
                        }

                    }


                }
            }
        }
    }
    
    public void satinal()
    {
        if (hangibaslık == 1)
        {
            for (int i = 0; i < birincil_silahsayısı; i++)
            {
                if (birincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            if (birincilsatinalindi[kacıncısilah] == 0)
            {
                if (birincilfiyatlar[kacıncısilah] <= toplamşimşek)
                {
                    toplamşimşek = toplamşimşek - birincilfiyatlar[kacıncısilah];
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    birincilsatinalindi[kacıncısilah] = 1;
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);

                    satınalbutonu.SetActive(false);
                    kusanbutonu.SetActive(true);
                    
                    if (kacıncısilah == 1)
                    {
                        PlayerPrefs.SetInt("birincil_1_kusanıldı", 0);
                        PlayerPrefs.SetInt("birincil_2_alındı", 1);
                        PlayerPrefs.SetInt("birincil_2_kusanıldı", 1);
                        PlayerPrefs.SetInt("birincil_3_kusanıldı", 0);
                        
                    }
                    if (kacıncısilah == 2)
                    {
                        PlayerPrefs.SetInt("birincil_1_kusanıldı", 0);
                        PlayerPrefs.SetInt("birincil_2_kusanıldı", 0);
                        PlayerPrefs.SetInt("birincil_3_alındı", 1);
                        PlayerPrefs.SetInt("birincil_3_kusanıldı", 1);
                        
                    }
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    for (int a = 0; a < birincil_silahsayısı; a++)
                    {
                        if (kacıncısilah == a)
                        {
                            birincilkuşanıldı[a] = 1;
                            birincilsilah_anamenu[a] = 1;
                            PlayerPrefs.SetInt("birincilsilah", a);
                            
                        }
                        else
                        {
                            birincilkuşanıldı[a] = 0;
                            birincilsilah_anamenu[a] = 0;
                        }
                    }

                    //satınalsilahgosterimi();
                    kusanıldıyazisi.text = "Kuşanıldı";
                    kusanıldıresmi.SetActive(true);
                    satınalpaneli.SetActive(false);


                }
                else
                    {
                    yetersizbakiye.SetActive(true);
                    }



            }



        }
        if (hangibaslık == 2)
        {
            for (int i = 0; i < ikincil_silahsayısı; i++)
            {
                if (ikincilsilahlar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            if (ikincilsatinalindi[kacıncısilah] == 0)
            {
                if (birincilfiyatlar[kacıncısilah] <= toplamşimşek) ;
                {
                    toplamşimşek = toplamşimşek - ikincilfiyatlar[kacıncısilah];
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    ikincilsatinalindi[kacıncısilah] = 1;
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);

                    satınalbutonu.SetActive(false);

                    kusanbutonu.SetActive(true);
                    
                   
                    for (int a = 0; a < ikincil_silahsayısı; a++)
                    {
                        if (kacıncısilah == a)
                        {
                            ikincilkuşanıldı[a] = 1;
                            PlayerPrefs.SetInt("ikincilsilah", a);
                        }
                        else
                        {
                            ikincilkuşanıldı[a] = 0;
                        }
                    }

                    kusanıldıyazisi.text = "Kuşanıldı";
                    kusanıldıresmi.SetActive(true);

                }



            }

        }

        if (hangibaslık == 3)
        {
            for (int i = 0; i < bıcaksayısı; i++)
            {
                if (bıçaklar[i].active == true)
                {
                    kacıncısilah = i;
                }
            }
            if (bıcaksatinalindi[kacıncısilah] == 0)
            {
                if (bıcakfiyatlar[kacıncısilah] <= toplamşimşek) ;
                {
                    toplamşimşek = toplamşimşek - bıcakfiyatlar[kacıncısilah];
                    PlayerPrefs.SetInt("TOPLAMSİMSEK", toplamşimşek);
                    bıcaksatinalindi[kacıncısilah] = 1;
                    magazatoplamsimsek.text = Convert.ToString(toplamşimşek);
                    anamenutoplamsimsek.text = Convert.ToString(toplamşimşek);

                    satınalbutonu.SetActive(false);
                    
                    kusanbutonu.SetActive(true);
                    for (int a = 0; a < bıcaksayısı; a++)
                    {
                        if (kacıncısilah == a)
                        {
                            bıcakkuşanıldı[a] = 1;
                            PlayerPrefs.SetInt("bicaksilah", a);
                        }
                        else
                        {
                            bıcakkuşanıldı[a] = 0;
                        }
                    }

                    kusanıldıyazisi.text = "Kuşanıldı";
                    kusanıldıresmi.SetActive(true);

                }



            }
        }
    }
    
    
    
}
