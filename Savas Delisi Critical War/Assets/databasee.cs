// Copyright 2016 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

// Handler for UI buttons on the scene.  Also performs some
// necessary setup (initializing the firebase app, etc) on
// startup.
public class databasee : MonoBehaviour
{
    protected Firebase.Auth.FirebaseAuth auth;
    protected Firebase.Auth.FirebaseAuth otherAuth;
    protected Dictionary<string, Firebase.Auth.FirebaseUser> userByAuth =
      new Dictionary<string, Firebase.Auth.FirebaseUser>();
    public GameObject duzenletusu;
    public Scrollbar odalarscroll;
    public int cinsiyet;
    public int limit_last;
    public Text oyuncu_nick_profile_bak;
    public InputField facebooknick;
    public InputField instagramnick;
    int kac_prefab;
    int gecicisayi;
    String geciciyazi;
    public Text uid;
    public GameObject yukleniyorpaneli_top5;
    public GameObject yukleniyorpaneli_profil;
    public GameObject yukleniyorpaneli_anamenu;
    public Text[] leaders_email;
    public Text[] leaders_kupa;
    public GameObject[] cinsiyet_image;
    public int[] leaders_kupa_sayisi;
    ArrayList leaderBoard = new ArrayList();
    ArrayList push_id_message = new ArrayList();
    ArrayList Message_nickname = new ArrayList();
    ArrayList Message_text = new ArrayList();
    public int toplam_mesaj_sayisi;
    Vector2 scrollPosition = Vector2.zero;
    private Vector2 controlsScrollViewVector = Vector2.zero;
    public Text deneme;
    public GUISkin fb_GUISkin;
    public int ben_varmiyim;
    private const int MaxScores = 5;
    private string logText = "";
    public string email = "";
    public int kazandigi_mac;
    public int kaybettigi_mac;
    public int oynadigi_mac;
    public int berabere_mac;
    public int kill;
    public int death;
    public int kupa;
    public int oyun_puani;
    public int toplam_simsek;
    public Text[] global_nickname;
    public Text[] global_text;
    public int score ;
    private Vector2 scrollViewVector = Vector2.zero;
    protected bool UIEnabled = false;
    string gecici_key;
    const int kMaxLogSize = 16382;
    public GameObject[] message_kutucuk;
    public InputField mesaj;
    
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    
    // When the app starts, check to make sure that we have
    // the required dependencies to use Firebase, and if not,
    // add them if possible.
    protected virtual void Start()
    {

        limit_last = 1;
        leaderBoard.Clear();
        leaderBoard.Add("Firebase Top " + MaxScores.ToString() + " Scores");
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false)
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        for (int i = 0; i < 5; i++)
        {

            leaders_email[i].text = " ".ToString();                                                                                      // leaders_kupa[i].text = snapshot.Child(i.ToString()).Child("score").Value.ToString();

        }
        

   
    //if (PlayerPrefs.HasKey("emailll"))
    //{

    //    email = PlayerPrefs.GetString("emailll");

    //    //for (int i = 0; i < emailx.text.length; i++)
    //    //{
    //    //    if (emailx.text.substring(i, 1) == "@")
    //    //    {
    //    //        gameobject.find("_connectmenu").getcomponent<connectmenu>().kullanici_ismi.text = emailx.text.substring(0, emailx.text.length - i);//email_kayýt.text;
    //    //    }
    //    //}
    //    ////////////////////////

    //    // GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi.text = emailx.text;
    //}

}
    
    
    public void intcagir_database()
    {
        InitializeFirebase();
    }
    // Initialize the Firebase database:
    protected virtual void InitializeFirebase()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        // NOTE: You'll need to replace this url with your Firebase App's database
       
        // path in order for the database connection to work correctly in editor.
        app.SetEditorDatabaseUrl("https://savas-delisi.firebaseio.com/");
        if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);


        //yenimi_kontrol_et_database();
        
        //Invoke("gonder_veri", 3);
        // StartListener();
    }

    public void start_listener()
    {
        StartListener();
    }
    protected void Startglobal_dinleme()
    {
        FirebaseDatabase.DefaultInstance
        .GetReference("Message").LimitToLast(20)
        .ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        push_id_message.Clear();
        Message_nickname.Clear();
        Message_text.Clear();
        // Do something with the data in args.Snapshot
        if (args.Snapshot.Exists)
        {
            
            
                foreach (var childSnapshot in args.Snapshot.Children)
                {

                    push_id_message.Add(childSnapshot.Key);
                    Message_nickname.Add(childSnapshot.Child("nickname").Value.ToString());
                    Message_text.Add(childSnapshot.Child("text").Value.ToString());

                }
                
                for (int i = 0; i < Message_nickname.Count; i++)
                  {

                    //counter++;

                    //var newText = Instantiate(itemPrefab) as Image;
                    //var nickname = newText.transform.GetChild(0);
                    //var text = newText.transform.GetChild(1);
                    //nickname.GetComponent<Text>().text = Message_nickname[i].ToString();
                    //text.GetComponent<Text>().text = Message_text[i].ToString();
                    ////newText.text = string.Format("Item {0}", counter.ToString());
                    //newText.transform.parent = gridThatStoresTheItems.transform;
       
                    
                }
                limit_last += 1;
                gecici_key = args.Snapshot.Key;
            
            /////////////////////
                foreach (var childSnapshot in args.Snapshot.Children)
                {

                    push_id_message.Add(childSnapshot.Key);
                    Message_nickname.Add(childSnapshot.Child("nickname").Value.ToString());
                    Message_text.Add(childSnapshot.Child("text").Value.ToString());

                }
                if (GameObject.FindGameObjectWithTag("message") != null)
                {
                    for (int i = 0; i < kac_prefab; i++)
                    {
                        Destroy(GameObject.FindGameObjectWithTag("message"));
                    }
                }
                kac_prefab = Message_nickname.Count;
                for (int i = 0; i < Message_nickname.Count; i++)
                {

                    //counter++;

                    //var newText = Instantiate(itemPrefab) as Image;
                    //var nickname = newText.transform.GetChild(0);
                    //var text = newText.transform.GetChild(1);
                    //nickname.GetComponent<Text>().text = Message_nickname[i].ToString();
                    //text.GetComponent<Text>().text = Message_text[i].ToString();
                    ////newText.text = string.Format("Item {0}", counter.ToString());
                    //newText.transform.parent = gridThatStoresTheItems.transform;


                }
                limit_last += 1;
                gecici_key = args.Snapshot.Key;
            
        }
    }
    //public void AddItemToGrid()
    //{
    //    counter++;
    //    var newText = Instantiate(itemPrefab) as Image;
    //    //newText.text = string.Format("Item {0}", counter.ToString());
    //    newText.transform.parent = gridThatStoresTheItems.transform;
    //}
    protected void StartListener()
    {
        FirebaseDatabase.DefaultInstance
          .GetReference("Message").LimitToLast(10)
          .ValueChanged += (object sender2, ValueChangedEventArgs e2) =>
          {
              if (e2.DatabaseError != null)
              {
                  Debug.LogError(e2.DatabaseError.Message);
                  return;
              }
              Debug.Log("Received values for Leaders.");
              //string title = leaderBoard[0].ToString();
              push_id_message.Clear();
              Message_nickname.Clear();
              Message_text.Clear();
              
              
                  foreach (var childSnapshot in e2.Snapshot.Children)
                  {

                      push_id_message.Add(childSnapshot.Key);
                      Message_nickname.Add(childSnapshot.Child("nickname").Value.ToString());
                      Message_text.Add(childSnapshot.Child("text").Value.ToString());

                  }
                  int gecici = 0;
                  for (int i = Message_nickname.Count - 1; i > -1; i--)
                  {
                      message_kutucuk[gecici].SetActive(true);
                      global_nickname[gecici].text = Message_nickname[i].ToString();
                      global_text[gecici].text = Message_text[i].ToString();
                      gecici += 1;
                      
                  }
                  

              
              toplam_mesaj_sayisi = Message_nickname.Count;
          };
    }
    public void mesaj_send()
    {
        DatabaseReference pushedPostRef = FirebaseDatabase.DefaultInstance.GetReference("/Message/").Push();
        String postId = pushedPostRef.Key;
        //if(toplam_mesaj_sayisi>2)
        //{
        //    FirebaseDatabase.DefaultInstance.GetReference("/Message/").Child(push_id_message[0].ToString() + "/").Child("nickname").SetValueAsync(null);
        //    FirebaseDatabase.DefaultInstance.GetReference("/Message/").Child(push_id_message[0].ToString() + "/").Child("text").SetValueAsync(null);

        //    FirebaseDatabase.DefaultInstance.GetReference("/Message/").Child(postId).Child("nickname").SetValueAsync("Oguzhan felan");

        //    FirebaseDatabase.DefaultInstance.GetReference("/Message/").Child(postId).Child("text").SetValueAsync("silme basarili la");

        //}
        //else if(toplam_mesaj_sayisi <= 2)
        //{
        // }
        if (string.IsNullOrEmpty(mesaj.text))
        {

            mesaj.text = "";
        }
        else
        {
            FirebaseDatabase.DefaultInstance.GetReference("/Message/").Child(postId).Child("nickname").SetValueAsync(PlayGamesPlatform.Instance.localUser.userName);
            
            FirebaseDatabase.DefaultInstance.GetReference("/Message/").Child(postId).Child("text").SetValueAsync(mesaj.text);
            mesaj.text="";
        }
        
       
        
    }
    public void mesaj_temizle()
    {
        mesaj.text = "";
    }
    public void global_mesaj_Send()
    {
        DatabaseReference pushedPostRef = FirebaseDatabase.DefaultInstance.GetReference("/Message/").Push();
        String postId = pushedPostRef.Key;
        //score = PlayerPrefs.GetInt("kupasayýsý");
        FirebaseDatabase.DefaultInstance
      .GetReference("/Message/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              // Do something with snapshot...            
              
              if (snapshot.ChildrenCount > 3)
              {
                  Message_nickname.Clear();
                  Message_text.Clear();

                  foreach (var childSnapshot in snapshot.Children)
                  {


                      push_id_message.Add(childSnapshot.Key);


                  }

                  

                  FirebaseDatabase.DefaultInstance.GetReference("/Message/").Child(postId).Child("nickname").SetValueAsync("Oguzhan felan");

                  FirebaseDatabase.DefaultInstance.GetReference("/Message/").Child(postId).Child("text").SetValueAsync("silme basarili la");


              }
              else
              {
                  FirebaseDatabase.DefaultInstance.GetReference("/Message/").Child(postId).Child("nickname").SetValueAsync("Murat Ünlü");

                  FirebaseDatabase.DefaultInstance.GetReference("/Message/").Child(postId).Child("text").SetValueAsync("oldu amk olduuuu");
              }


          }
      });
    }
    public void global_sifirlama()
    {
        odalarscroll.value = 0;
    }
    // Exit if escape (or back, on mobile) is pressed.
    protected virtual void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().profilmenusu_kullaniciismi.text!= PlayGamesPlatform.Instance.localUser.userName)
        {
            duzenletusu.SetActive(false);

        }
        else
        {
            duzenletusu.SetActive(true);
        }
    }
    
    // Output text to the debug log text field, as well as the console.
    public void DebugLog(string s)
    {
        Debug.Log(s);
        logText += s + "\n";

        while (logText.Length > kMaxLogSize)
        {
            int index = logText.IndexOf("\n");
            logText = logText.Substring(index + 1);
        }

        scrollViewVector.y = int.MaxValue;
    }

    // A realtime database transaction receives MutableData which can be modified
    // and returns a TransactionResult which is either TransactionResult.Success(data) with
    // modified data or TransactionResult.Abort() which stops the transaction with no changes.
    public void kapattusu_sifirla()
    {
        for (int i = 0; i < 5; i++)
        {

            leaders_email[i].text = " ".ToString();                                                                                      // leaders_kupa[i].text = snapshot.Child(i.ToString()).Child("score").Value.ToString();
            
        }
    }
    public void listele_message()
    {
        push_id_message.Clear();
        Message_nickname.Clear();
        Message_text.Clear();
        FirebaseDatabase.DefaultInstance
         .GetReference("/Message/")
         .GetValueAsync().ContinueWith(task =>
         {
             if (task.IsFaulted)
             {
                 deneme.text = "olmadý amk".ToString();
                 // Handle the error...
             }
             else if (task.IsCompleted)
             {
                 DataSnapshot snapshot = task.Result;
                 
                 //for (int i = 0; i < 5; i++)
                 //{
                 //    leaders_kupa_sayisi[i] = Convert.ToInt32(snapshot.Child(i.ToString()).Child("score").Value.ToString());     //snapshot.Child(i.ToString()).Child("score").Value.ToString();// snapshot.Child(i.ToString()).Child("email").Value.ToString();
                 //    leaders_email[i].text = snapshot.Child(i.ToString()).Child("email").Value.ToString();                                                                                       // leaders_kupa[i].text = snapshot.Child(i.ToString()).Child("score").Value.ToString();

                 //}
                 ////////////////onemli/////snapshot != null &&
                 if ( snapshot.ChildrenCount > 0)
                 {
                     foreach (var childSnapshot in snapshot.Children)
                     {


                         push_id_message.Add(childSnapshot.Key);
                         Message_nickname.Add(childSnapshot.Child("nickname").Value.ToString());
                         Message_text.Add(childSnapshot.Child("text").Value.ToString());

                     }
                     
                     int gecici = 0;
                     for (int i = Message_nickname.Count-1; i>-1; i--)
                     {
                         
                             global_nickname[gecici].text = Message_nickname[i].ToString();
                             global_text[gecici].text = Message_text[i].ToString();
                         gecici += 1;

                     }

                 }


                 
             }
         });

    }
    
    public void listele_leaders()
    {
        
        FirebaseDatabase.DefaultInstance
         .GetReference("/Leaders/")
         .GetValueAsync().ContinueWith(task =>
         {
             if (task.IsFaulted)
             {
                 deneme.text = "olmadý amk".ToString();
                 // Handle the error...
             }
             else if (task.IsCompleted)
             {
                 DataSnapshot snapshot = task.Result;
                 for (int i = 0; i < 5; i++)
                 {
                     leaders_kupa_sayisi[i] = Convert.ToInt32(snapshot.Child(i.ToString()).Child("score").Value.ToString());     //snapshot.Child(i.ToString()).Child("score").Value.ToString();// snapshot.Child(i.ToString()).Child("email").Value.ToString();
                     leaders_email[i].text = snapshot.Child(i.ToString()).Child("email").Value.ToString();                                                                                       // leaders_kupa[i].text = snapshot.Child(i.ToString()).Child("score").Value.ToString();

                 }
                 for (int i = 0; i < leaders_kupa_sayisi.Length - 1; i++)
                 {
                     for (int j = i; j < leaders_kupa_sayisi.Length; j++)
                     {
                         // >(büyük) iþareti <(küçük ) olarak deðiþtirilirse büyükten küçüðe sýralanýr
                         if (leaders_kupa_sayisi[i] < leaders_kupa_sayisi[j])
                         {
                             gecicisayi = leaders_kupa_sayisi[j];
                             geciciyazi = leaders_email[j].text;
                             leaders_kupa_sayisi[j] = leaders_kupa_sayisi[i];
                             leaders_email[j].text= leaders_email[i].text;
                             leaders_kupa_sayisi[i] = gecicisayi;
                             leaders_email[i].text = geciciyazi;
                             ;
                         }

                     }

                 }
                 for (int i = 0; i < 5; i++)
                 {
                     // leaders_kupa_sayýsý[i] = Convert.ToInt32(leaders_kupa[i].text);// snapshot.Child(i.ToString()).Child("email").Value.ToString();
                     leaders_kupa[i].text = leaders_kupa_sayisi[i].ToString();
                 }
                 yukleniyorpaneli_top5.SetActive(false);
             }
         });
       
    }
    public void erkek_secildi()
    {

        PlayerPrefs.SetInt("cinsiyett", 1);
    }
        public void kadin_secildi()
    {
        PlayerPrefs.SetInt("cinsiyett", 2);
    }
    public void gizli_secildi()
    {
        PlayerPrefs.SetInt("cinsiyett", 0);
    }
    public void sosyal_ekle()
    {
        if (facebooknick.text != PlayerPrefs.GetString("facebook_nick") || instagramnick.text != PlayerPrefs.GetString("instagram_nick") ||cinsiyet!= PlayerPrefs.GetInt("cinsiyett"))
        {
            yukleniyorpaneli_profil.SetActive(true);
            
            uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
            PlayerPrefs.SetString("facebook_nick", facebooknick.text);
            PlayerPrefs.SetString("instagram_nick", instagramnick.text);
            GameObject.Find("layout").GetComponent<menugecisi>().oyuncu_facebook = facebooknick.text;
            GameObject.Find("layout").GetComponent<menugecisi>().oyuncu_instagram = instagramnick.text;
            cinsiyet = PlayerPrefs.GetInt("cinsiyett");
            FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("facebook").SetValueAsync(PlayerPrefs.GetString("facebook_nick"));
            FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("instagram").SetValueAsync(PlayerPrefs.GetString("instagram_nick"));
            FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("cinsiyet").SetValueAsync(PlayerPrefs.GetInt("cinsiyett"));
            FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("facebook").SetValueAsync(PlayerPrefs.GetString("facebook_nick"));
            FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("instagram").SetValueAsync(PlayerPrefs.GetString("instagram_nick"));
            FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("cinsiyet").SetValueAsync(PlayerPrefs.GetInt("cinsiyett"));
            if (cinsiyet == 0)
            {
                cinsiyet_image[0].SetActive(true);
                cinsiyet_image[1].SetActive(false);
                cinsiyet_image[2].SetActive(false);
            }
            else if (cinsiyet == 1)
            {
                cinsiyet_image[0].SetActive(false);
                cinsiyet_image[1].SetActive(true);
                cinsiyet_image[2].SetActive(false);
            }
            else if (cinsiyet == 2)
            {
                cinsiyet_image[0].SetActive(false);
                cinsiyet_image[1].SetActive(false);
                cinsiyet_image[2].SetActive(true);
            }
            yukleniyorpaneli_profil.SetActive(false);
        }
    }
    public void duzenletusuu()
    {
        facebooknick.text= PlayerPrefs.GetString("facebook_nick");
        instagramnick.text = PlayerPrefs.GetString("instagram_nick");
    }
    public void profile_bak_diger_user()
    {
        yukleniyorpaneli_profil.SetActive(true);
        FirebaseDatabase.DefaultInstance
         .GetReference("/users_profil/").Child(oyuncu_nick_profile_bak.text + "/")
         .GetValueAsync().ContinueWith(task =>
         {
             if (task.IsFaulted)
             {
                 
                 // Handle the error...
             }
             else if (task.IsCompleted)
             {
                 DataSnapshot snapshot = task.Result;

                 kazandigi_mac = Convert.ToInt32(snapshot.Child("kazandigimac").Value.ToString());
                 kaybettigi_mac = Convert.ToInt32(snapshot.Child("kaybettigimac").Value.ToString());
                 oynadigi_mac = Convert.ToInt32(snapshot.Child("oynadigimac").Value.ToString());
                 berabere_mac = Convert.ToInt32(snapshot.Child("beraberemac").Value.ToString());
                 death = Convert.ToInt32(snapshot.Child("death").Value.ToString());
                 kill = Convert.ToInt32(snapshot.Child("kill").Value.ToString());
                 kupa = Convert.ToInt32(snapshot.Child("kupa").Value.ToString());
                 oyun_puani = Convert.ToInt32(snapshot.Child("seviyepuani").Value.ToString());
                 cinsiyet = Convert.ToInt32(snapshot.Child("cinsiyet").Value.ToString());
                 if (snapshot.Child("facebook").Exists)
                 {
                     GameObject.Find("layout").GetComponent<menugecisi>().oyuncu_facebook = snapshot.Child("facebook").Value.ToString();
                 }

                 if (snapshot.Child("instagram").Exists)
                 {
                     GameObject.Find("layout").GetComponent<menugecisi>().oyuncu_instagram = snapshot.Child("instagram").Value.ToString();

                 }

                 if (cinsiyet == 0)
                 {
                     cinsiyet_image[0].SetActive(true);
                     cinsiyet_image[1].SetActive(false);
                     cinsiyet_image[2].SetActive(false);
                 }
                 else if (cinsiyet == 1)
                 {
                     cinsiyet_image[0].SetActive(false);
                     cinsiyet_image[1].SetActive(true);
                     cinsiyet_image[2].SetActive(false);
                 }
                 else if (cinsiyet == 2)
                 {
                     cinsiyet_image[0].SetActive(false);
                     cinsiyet_image[1].SetActive(false);
                     cinsiyet_image[2].SetActive(true);
                 }
                 GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().profilmenusu_kullaniciismi.text = oyuncu_nick_profile_bak.text;


                 GameObject.Find("magazascripti").GetComponent<oyunmagaza>().profilebak_diger_userr();
                 yukleniyorpaneli_profil.SetActive(false);
                 yukleniyorpaneli_anamenu.SetActive(false);



             }
         });
    }
    public void profilmenusu_yenile_veri()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
         .GetReference("/users/").Child(uid.text + "/")
         .GetValueAsync().ContinueWith(task =>
         {
             if (task.IsFaulted)
             {
                 deneme.text = "olmadý amk".ToString();
                 // Handle the error...
             }
             else if (task.IsCompleted)
             {
                 DataSnapshot snapshot = task.Result;

                 kazandigi_mac= Convert.ToInt32(snapshot.Child("kazandigimac").Value.ToString());
                 kaybettigi_mac = Convert.ToInt32(snapshot.Child("kaybettigimac").Value.ToString());
                 oynadigi_mac = Convert.ToInt32(snapshot.Child("oynadigimac").Value.ToString());
                 berabere_mac = Convert.ToInt32(snapshot.Child("beraberemac").Value.ToString());
                 death = Convert.ToInt32(snapshot.Child("death").Value.ToString());
                 kill = Convert.ToInt32(snapshot.Child("kill").Value.ToString());
                 kupa = Convert.ToInt32(snapshot.Child("kupa").Value.ToString());
                 oyun_puani = Convert.ToInt32(snapshot.Child("seviyepuani").Value.ToString());
                 toplam_simsek = Convert.ToInt32(snapshot.Child("altin_miktari").Value.ToString());
                 cinsiyet = Convert.ToInt32(snapshot.Child("cinsiyet").Value.ToString());
                 PlayerPrefs.SetInt("cinsiyett", cinsiyet);
                 if (snapshot.Child("facebook").Exists)
                 {
                     GameObject.Find("layout").GetComponent<menugecisi>().oyuncu_facebook = snapshot.Child("facebook").Value.ToString();
                     PlayerPrefs.SetString("facebook_nick", GameObject.Find("layout").GetComponent<menugecisi>().oyuncu_facebook);
                 }
                 
                 if (snapshot.Child("instagram").Exists)
                 {
                     GameObject.Find("layout").GetComponent<menugecisi>().oyuncu_instagram = snapshot.Child("instagram").Value.ToString();
                     PlayerPrefs.SetString("instagram_nick", GameObject.Find("layout").GetComponent<menugecisi>().oyuncu_instagram);
                 }
                 
                     GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().profilmenusu_kullaniciismi.text = PlayGamesPlatform.Instance.localUser.userName;

                 if (cinsiyet == 0)
                     {
                         cinsiyet_image[0].SetActive(true);
                         cinsiyet_image[1].SetActive(false);
                         cinsiyet_image[2].SetActive(false);
                     }
                     else if (cinsiyet == 1)
                     {
                         cinsiyet_image[0].SetActive(false);
                         cinsiyet_image[1].SetActive(true);
                         cinsiyet_image[2].SetActive(false);
                     }
                     else if (cinsiyet == 2)
                     {
                         cinsiyet_image[0].SetActive(false);
                         cinsiyet_image[1].SetActive(false);
                         cinsiyet_image[2].SetActive(true);
                     }
                 
                 if (PlayGamesPlatform.Instance.IsAuthenticated())
                 {

                     GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().playerName = PlayGamesPlatform.Instance.localUser.userName;
                     email = PlayGamesPlatform.Instance.localUser.userName;
                     PlayerPrefs.SetString(GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().playerNamePrefsName, GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().playerName);
                     GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi.text = GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().playerName;
                     GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismikaydet();
                 }
                 GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[1] = Convert.ToInt32(snapshot.Child("silahlar").Child("awp").Value.ToString());
                 GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[2] = Convert.ToInt32(snapshot.Child("silahlar").Child("pompali").Value.ToString());

                 

                 PlayerPrefs.SetInt("kazandýgýmaçsayýsýý", kazandigi_mac);
                 PlayerPrefs.SetInt("kaybettigimaçsayýsýý", kaybettigi_mac);
                 PlayerPrefs.SetInt("oynanan_macsayýsý", oynadigi_mac);
                 PlayerPrefs.SetInt("berabere_macsayýsý", berabere_mac);
                 PlayerPrefs.SetInt("öldürmesayýmpref", kill);
                 PlayerPrefs.SetInt("ölmesayýmpref", death);
                 PlayerPrefs.SetInt("kupasayýsý", kupa);
                 PlayerPrefs.SetInt("oyunpuani", oyun_puani);
                 PlayerPrefs.SetInt("TOPLAMSÝMSEK", toplam_simsek);
                 PlayerPrefs.SetInt("birincil_2_alýndý", GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[1]);
                 PlayerPrefs.SetInt("birincil_3_alýndý", GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[2]);

                 if (Application.loadedLevelName == "_MainMenu")
                 {
                     GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[1] = PlayerPrefs.GetInt("birincil_2_alýndý");
                     GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[2] = PlayerPrefs.GetInt("birincil_3_alýndý");
                 }

                 GameObject.Find("magazascripti").GetComponent<oyunmagaza>().profil_veri_yenile();
                 yukleniyorpaneli_profil.SetActive(false);
                 yukleniyorpaneli_anamenu.SetActive(false);



             }
         });
    }
    public void yenimi_kontrol_et_database()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
         .GetReference("/users/").Child(uid.text+ "/")
         .GetValueAsync().ContinueWith(task =>
         {
             if (task.IsFaulted)
             {
                 

             }
             else if (task.IsCompleted)
             {
                 DataSnapshot snapshot = task.Result;
                 if (snapshot.Exists)
                 {
                     
                     //int a= Convert.ToInt32(snapshot.ChildrenCount.ToString()); ;
                     uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
                     if (PlayerPrefs.GetInt("öldürmesayýmpref") != 0 || PlayerPrefs.GetInt("ölmesayýmpref") != 0 || PlayerPrefs.GetInt("TOPLAMSÝMSEK")!=0)
                     {
                         
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("seviyepuani").SetValueAsync(PlayerPrefs.GetInt("oyunpuani"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kupa").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kupa_data);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kill").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kill_database);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("death").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().death_database);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("oynadigimac").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().oynanan_mac_);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kazandigimac").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kazanan_mac_data);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kaybettigimac").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kaybeden_mac_data);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("beraberemac").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().berabere_mac_data);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("altin_miktari").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().altin_data);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("silahlar").Child("awp").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[1]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("silahlar").Child("pompali").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[2]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("cinsiyet").SetValueAsync(PlayerPrefs.GetInt("cinsiyett"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("facebook").SetValueAsync(PlayerPrefs.GetString("facebook_nick"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("instagram").SetValueAsync(PlayerPrefs.GetString("instagram_nick"));

                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("seviyepuani").SetValueAsync(PlayerPrefs.GetInt("oyunpuani"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kupa").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kupa_data);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kill").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kill_database);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("death").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().death_database);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("oynadigimac").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().oynanan_mac_);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kazandigimac").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kazanan_mac_data);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kaybettigimac").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kaybeden_mac_data);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("beraberemac").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().berabere_mac_data);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("altin_miktari").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().altin_data);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("silahlar").Child("awp").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[1]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("silahlar").Child("pompali").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[2]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("cinsiyet").SetValueAsync(PlayerPrefs.GetInt("cinsiyett"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("facebook").SetValueAsync(PlayerPrefs.GetString("facebook_nick"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("instagram").SetValueAsync(PlayerPrefs.GetString("instagram_nick"));
                         profilmenusu_yenile_veri();
                         
                     }
                     else
                     {

                         profilmenusu_yenile_veri();

                     }
                     
                 }
                 else
                 {

                     if (PlayerPrefs.GetInt("öldürmesayýmpref") != 0 || PlayerPrefs.GetInt("ölmesayýmpref") != 0 || PlayerPrefs.GetInt("TOPLAMSÝMSEK") != 0)
                     {
                         uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("seviyepuani").SetValueAsync(PlayerPrefs.GetInt("oyunpuani"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kupa").SetValueAsync(PlayerPrefs.GetInt("kupasayýsý"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kill").SetValueAsync(PlayerPrefs.GetInt("öldürmesayýmpref"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("death").SetValueAsync(PlayerPrefs.GetInt("ölmesayýmpref"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("oynadigimac").SetValueAsync(PlayerPrefs.GetInt("oynanan_macsayýsý"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kazandigimac").SetValueAsync(PlayerPrefs.GetInt("kazandýgýmaçsayýsýý"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kaybettigimac").SetValueAsync(PlayerPrefs.GetInt("kaybettigimaçsayýsýý"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("beraberemac").SetValueAsync(PlayerPrefs.GetInt("berabere_macsayýsý"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("silahlar").Child("awp").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[1]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("silahlar").Child("pompali").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[2]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("cinsiyet").SetValueAsync(PlayerPrefs.GetInt("cinsiyett"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("altin_miktari").SetValueAsync(PlayerPrefs.GetInt("TOPLAMSÝMSEK"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("facebook").SetValueAsync(PlayerPrefs.GetString("facebook_nick"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("instagram").SetValueAsync(PlayerPrefs.GetString("instagram_nick"));


                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("nickname").SetValueAsync(PlayGamesPlatform.Instance.localUser.userName);

                         /////////////////////////
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("seviyepuani").SetValueAsync(PlayerPrefs.GetInt("oyunpuani"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kupa").SetValueAsync(PlayerPrefs.GetInt("kupasayýsý"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kill").SetValueAsync(PlayerPrefs.GetInt("öldürmesayýmpref"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("death").SetValueAsync(PlayerPrefs.GetInt("ölmesayýmpref"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("oynadigimac").SetValueAsync(PlayerPrefs.GetInt("oynanan_macsayýsý"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kazandigimac").SetValueAsync(PlayerPrefs.GetInt("kazandýgýmaçsayýsýý"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kaybettigimac").SetValueAsync(PlayerPrefs.GetInt("kaybettigimaçsayýsýý"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("beraberemac").SetValueAsync(PlayerPrefs.GetInt("berabere_macsayýsý"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("altin_miktari").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().altin_data);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("silahlar").Child("awp").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[1]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("silahlar").Child("pompali").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[2]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("cinsiyet").SetValueAsync(PlayerPrefs.GetInt("cinsiyett"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("facebook").SetValueAsync(PlayerPrefs.GetString("facebook_nick"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("instagram").SetValueAsync(PlayerPrefs.GetString("instagram_nick"));
                         profilmenusu_yenile_veri();
                     }
                     else 
                     {
                         uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("seviyepuani").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kupa").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kill").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("death").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("oynadigimac").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kazandigimac").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kaybettigimac").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("beraberemac").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("altin_miktari").SetValueAsync(100);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("nickname").SetValueAsync(PlayGamesPlatform.Instance.localUser.userName);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("silahlar").Child("awp").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[1]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("silahlar").Child("pompali").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[2]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("cinsiyet").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("facebook").SetValueAsync(PlayerPrefs.GetString("facebook_nick"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("instagram").SetValueAsync(PlayerPrefs.GetString("instagram_nick"));


                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("seviyepuani").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kupa").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kill").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("death").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("oynadigimac").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kazandigimac").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("kaybettigimac").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("beraberemac").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("altin_miktari").SetValueAsync(100);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("silahlar").Child("awp").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[1]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("silahlar").Child("pompali").SetValueAsync(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().birincilsatinalindi[2]);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("cinsiyet").SetValueAsync(0);
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("facebook").SetValueAsync(PlayerPrefs.GetString("facebook_nick"));
                         FirebaseDatabase.DefaultInstance.GetReference("/users_profil/" + PlayGamesPlatform.Instance.localUser.userName + "/").Child("instagram").SetValueAsync(PlayerPrefs.GetString("instagram_nick"));

                         profilmenusu_yenile_veri();
                     }
                 }
                     
             }
            
         });

    }
    public void kill_veri()
    {
        //score = PlayerPrefs.GetInt("kupasayýsý");
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("kill/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kill").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString())+1);

          }
      });
    
}
    public void death_veri()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("death/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("death").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString()) + 1);

          }
      });
    }
    public void oynadigi_mac_veri()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("oynadigimac/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("oynadigimac").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString()) + 1);
              
          }
      });
    }
    public void kazandigi_mac_veri()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("kazandigimac/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kazandigimac").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString()) + 1);
              
          }
      });
    }
    public void kaybettigi_mac_veri()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("kaybettigimac/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kaybettigimac").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString()) + 1);

          }
      });
    }
    public void berabere_mac_veri()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("beraberemac/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("beraberemac").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString()) + 1);
              
          }
      });
    }
    public void kupa_veri_tdm_ekle()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("kupa/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kupa").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString()) + 10);

          }
      });

    }
    public void kupa_veri_tdm_azalt()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("kupa/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kupa").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString()) - 10);

          }
      });
    }
    public void kupa_veri_ffa_ekle()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("kupa/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kupa").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString()) + 15);

          }
      });
    }
    public void kupa_veri_ffa_azalt()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("kupa/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kupa").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString()) -10);

          }
      });
    }
    public void seviye_puani_veri()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("seviyepuani/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("seviyepuani").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString()) + (GameObject.Find("asdf").GetComponent<oyunmagaza>().seviyepuani_sonra- GameObject.Find("asdf").GetComponent<oyunmagaza>().seviyepuani_once));

          }
      });
         

    }
    public void altin_veri_ekle_15()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
        FirebaseDatabase.DefaultInstance
      .GetReference("/users/").Child(uid.text + "/").Child("altin_miktari/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("altin_miktari").SetValueAsync(Convert.ToInt32(snapshot.Value.ToString()) + 15);
              
          }
      });
    }
    public void altin_ekle_ucretsiz()
    {
        uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
       

              FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("altin_miktari").SetValueAsync(PlayerPrefs.GetInt("TOPLAMSÝMSEK"));

        
    }
    //public void kullaniciekleveri()
    // {

    //     uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
    //     Invoke("userinfover", 0.5f);
    // }
    // public void kullanici_al_veri()
    // {

    //     uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();
    //     Invoke("userinfo_al", 0.5f);
    // }
    //public void userinfo_al()
    //{
    //    FirebaseDatabase.DefaultInstance
    //     .GetReference("/users/").Child(uid.text+"/")
    //     .GetValueAsync().ContinueWith(task =>
    //     {
    //         if (task.IsFaulted)
    //         {
    //             deneme.text = "olmadý amk".ToString();
    //             // Handle the error...
    //         }
    //         else if (task.IsCompleted)
    //         {
    //             DataSnapshot snapshot = task.Result;


    //             GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kupasayýsý_yazýsý.text = snapshot.Child("kupa").Value.ToString();                                                                                       // leaders_kupa[i].text = snapshot.Child(i.ToString()).Child("score").Value.ToString();
    //             GameObject.Find("magazascripti").GetComponent<oyunmagaza>().oyunpuaný = Convert.ToInt32(snapshot.Child("seviyepuani").Value.ToString());                                                                                    // leaders_kupa[i].text = snapshot.Child(i.ToString()).Child("score").Value.ToString();
    //             GameObject.Find("magazascripti").GetComponent<oyunmagaza>().öldürme_sayýsýy.text= snapshot.Child("kill").Value.ToString();
    //             GameObject.Find("magazascripti").GetComponent<oyunmagaza>().ölüm_sayýsýy.text = snapshot.Child("death").Value.ToString();
    //             GameObject.Find("magazascripti").GetComponent<oyunmagaza>().oynadýgý_maçsayýsýy.text = snapshot.Child("oynadýgýmac").Value.ToString();
    //             GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kazandýgý_maçsayýsýy.text = snapshot.Child("kazandýgýmac").Value.ToString();
    //             GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kaybettigi_maçsayýsýy.text = snapshot.Child("kaybettigimac").Value.ToString();
    //             GameObject.Find("magazascripti").GetComponent<oyunmagaza>().beraberemacsayýsýy.text = snapshot.Child("beraberemac").Value.ToString();


    //             yukleniyorpaneli_top5.SetActive(false);
    //         }
    //     });
    //}
    //public void userinfover()
    //{

    //    FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("seviyepuani").SetValueAsync(score);
    //    FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kupa").SetValueAsync(score);
    //    FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kill").SetValueAsync(score);
    //    FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("death").SetValueAsync(score);
    //    FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("oynadýgýmac").SetValueAsync(score);
    //    FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kazandýgýmac").SetValueAsync(score);
    //    FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("kaybettigimac").SetValueAsync(score);
    //    FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("beraberemac").SetValueAsync(score);


    //}
    //public void kullanici_ismi_kaydet()
    //{
    //    uid.text = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId.ToString();       
    //    FirebaseDatabase.DefaultInstance.GetReference("/users/" + uid.text + "/").Child("nickname").SetValueAsync(GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_name_firebase.text);

    //}
    //public void global_chat()
    //{
    //    FirebaseDatabase.DefaultInstance
    //    .GetReference("Global_Message")
    //    .ValueChanged += HandleValueChanged;
    //}

    //void HandleValueChanged(object sender, ValueChangedEventArgs args)
    //{
    //    if (args.DatabaseError != null)
    //    {
    //        Debug.LogError(args.DatabaseError.Message);
    //        return;
    //    }
    //    // Do something with the data in args.Snapshot
    //    leaderBoard.Clear();

    //    for(int i =0;i <Convert.ToInt32(args.Snapshot.ChildrenCount.ToString());i++)
    //    {
    //        string gecici=
    //        dfgh
    //    }
        
    //}

    public void gonder_veri_lidertahtasi()
    {
        
        //score = PlayerPrefs.GetInt("kupasayýsý");
        FirebaseDatabase.DefaultInstance
      .GetReference("/Leaders/")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              deneme.text = "olmadý amk".ToString();
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              // Do something with snapshot...            
              if (email == snapshot.Child("0").Child("email").Value.ToString())
              {
                  FirebaseDatabase.DefaultInstance.GetReference("/Leaders/" + "0" + "/").Child("score").SetValueAsync(kupa);
                  ben_varmiyim = 1;
                  listele_leaders();
              }
              else if (email == snapshot.Child("1").Child("email").Value.ToString())
              {
                  FirebaseDatabase.DefaultInstance.GetReference("/Leaders/" + "1" + "/").Child("score").SetValueAsync(kupa);
                  ben_varmiyim = 1;
                  listele_leaders();
              }
              else if (email == snapshot.Child("2").Child("email").Value.ToString())
              {
                  FirebaseDatabase.DefaultInstance.GetReference("/Leaders/" + "2" + "/").Child("score").SetValueAsync(kupa);
                  ben_varmiyim = 1;
                  listele_leaders();
              }
              else if (email == snapshot.Child("3").Child("email").Value.ToString())
              {
                  FirebaseDatabase.DefaultInstance.GetReference("/Leaders/" + "3" + "/").Child("score").SetValueAsync(kupa);
                  ben_varmiyim = 1;
                  listele_leaders();
              }
              else if (email == snapshot.Child("4").Child("email").Value.ToString())
              {
                  FirebaseDatabase.DefaultInstance.GetReference("/Leaders/" + "4" + "/").Child("score").SetValueAsync(kupa);
                  ben_varmiyim = 1;
                  listele_leaders();
              }
              else//if (ben_varmiyim == 0)
              {
                  AddScore();

              }
              listele_leaders();
              ben_varmiyim = 0;

          }
      });
    }



    TransactionResult AddScoreTransaction(MutableData mutableData)
    {
        List<object> leaders = mutableData.Value as List<object>;
        //score = GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kupasayisii;
        
        
        if (leaders == null)
        {
            leaders = new List<object>();
        }
        else if (mutableData.ChildrenCount >= MaxScores)
        {
            // If the current list of scores is greater or equal to our maximum allowed number,
            // we see if the new score should be added and remove the lowest existing score.
            long minScore = long.MaxValue;
            object minVal = null;
            foreach (var child in leaders)
            {
                if (!(child is Dictionary<string, object>))
                    continue;
                long childScore = (long)((Dictionary<string, object>)child)["score"];
                if (childScore < minScore)
                {
                    minScore = childScore;
                    minVal = child;
                }
            }
            // If the new score is lower than the current minimum, we abort.
            if (minScore > kupa)
            {
                return TransactionResult.Abort();
            }
            // Otherwise, we remove the current lowest to be replaced with the new score.
            leaders.Remove(minVal);
        }

        // Now we add the new score as a new entry that contains the email address and score.
        Dictionary<string, object> newScoreMap = new Dictionary<string, object>();
        //score = GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kupasayisii;
        newScoreMap["score"] = kupa;// GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kupasayisii;
        newScoreMap["email"] = email;

       
        leaders.Add(newScoreMap);
        
        mutableData.Value = leaders;
        return TransactionResult.Success(mutableData);
    }

    public void AddScore()
    {
        //score = GameObject.Find("magazascripti").GetComponent<oyunmagaza>().kupasayisii;
        if (kupa == 0 || string.IsNullOrEmpty(email))
        {
            DebugLog("invalid score or email.");
            return;
        }
        DebugLog(String.Format("Attempting to add score {0} {1}",
          email, kupa.ToString()));

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Leaders");

        DebugLog("Running Transaction...");
        // Use a transaction to ensure that we do not encounter issues with
        // simultaneous updates that otherwise might create more than MaxScores top scores.

        reference.RunTransaction(AddScoreTransaction)
          .ContinueWith(task => {
              if (task.Exception != null)
              {
                  DebugLog(task.Exception.ToString());
              }
              else if (task.IsCompleted)
              {
                  DebugLog("Transaction complete.");
                  
              }
              
          });
    }

    // Render the log output in a scroll view.
    //void GUIDisplayLog()
    //{
    //    scrollViewVector = GUILayout.BeginScrollView(scrollViewVector);
    //    GUILayout.Label(logText);
    //    GUILayout.EndScrollView();
    //}

    // Render the buttons and other controls.
    //void GUIDisplayControls()
    //{
    //    if (UIEnabled)
    //    {
    //        controlsScrollViewVector =
    //            GUILayout.BeginScrollView(controlsScrollViewVector);
    //        GUILayout.BeginVertical();
    //        GUILayout.BeginHorizontal();
    //        GUILayout.Label("Email:", GUILayout.Width(Screen.width * 0.20f));
    //        email = GUILayout.TextField(email);
    //        GUILayout.EndHorizontal();

    //        GUILayout.Space(20);

    //        GUILayout.BeginHorizontal();
    //        GUILayout.Label("Score:", GUILayout.Width(Screen.width * 0.20f));
    //        int.TryParse(GUILayout.TextField(score.ToString()), out score);
    //        GUILayout.EndHorizontal();

    //        GUILayout.Space(20);

    //        if (!String.IsNullOrEmpty(email) && GUILayout.Button("Enter Score"))
    //        {
    //            AddScore();
    //        }

    //        GUILayout.Space(20);

    //        if (GUILayout.Button("Go Offline"))
    //        {
    //            FirebaseDatabase.DefaultInstance.GoOffline();
    //        }

    //        GUILayout.Space(20);

    //        if (GUILayout.Button("Go Online"))
    //        {
    //            FirebaseDatabase.DefaultInstance.GoOnline();
    //        }

    //        GUILayout.EndVertical();
    //        GUILayout.EndScrollView();
    //    }
    //}

    //void GUIDisplayLeaders()
    //{
    //    GUI.skin.box.fontSize = 36;
    //    scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
    //    GUILayout.BeginVertical(GUI.skin.box);

    //    foreach (string item in leaderBoard)
    //    {
    //        GUILayout.Label(item, GUI.skin.box, GUILayout.ExpandWidth(true));
    //    }

    //    GUILayout.EndVertical();
    //    GUILayout.EndScrollView();
    //}

    //// Render the GUI:
    //void OnGUI()
    //{
    //    GUI.skin = fb_GUISkin;
    //    if (dependencyStatus != DependencyStatus.Available)
    //    {
    //        GUILayout.Label("One or more Firebase dependencies are not present.");
    //        GUILayout.Label("Current dependency status: " + dependencyStatus.ToString());
    //        return;
    //    }
    //    Rect logArea, controlArea, leaderBoardArea;

    //    if (Screen.width < Screen.height)
    //    {
    //        // Portrait mode
    //        controlArea = new Rect(0.0f, 0.0f, Screen.width, Screen.height * 0.5f);
    //        leaderBoardArea = new Rect(0, Screen.height * 0.5f, Screen.width * 0.5f, Screen.height * 0.5f);
    //        logArea = new Rect(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width * 0.5f, Screen.height * 0.5f);
    //    }
    //    else
    //    {
    //        // Landscape mode
    //        controlArea = new Rect(0.0f, 0.0f, Screen.width * 0.5f, Screen.height);
    //        leaderBoardArea = new Rect(Screen.width * 0.5f, 0, Screen.width * 0.5f, Screen.height * 0.5f);
    //        logArea = new Rect(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width * 0.5f, Screen.height * 0.5f);
    //    }

    //    GUILayout.BeginArea(leaderBoardArea);
    //    GUIDisplayLeaders();
    //    GUILayout.EndArea();

    //    GUILayout.BeginArea(logArea);
    //    GUIDisplayLog();
    //    GUILayout.EndArea();

    //    GUILayout.BeginArea(controlArea);
    //    GUIDisplayControls();
    //    GUILayout.EndArea();
    //}
}
