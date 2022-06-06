using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class oturum_ac : MonoBehaviour {

    protected Firebase.Auth.FirebaseAuth auth;
    protected Firebase.Auth.FirebaseAuth otherAuth;
    protected Dictionary<string, Firebase.Auth.FirebaseUser> userByAuth =
      new Dictionary<string, Firebase.Auth.FirebaseUser>();

    public GUISkin fb_GUISkin;
    private string logText = "";
    protected string email = "";
    protected string password = "";
    protected string displayName = "";
    protected string phoneNumber = "";
    protected string receivedCode = "";
    // Flag set when a token is being fetched.  This is used to avoid printing the token
    // in IdTokenChanged() when the user presses the get token button.
    private bool fetchingToken = false;
    // Enable / disable password input box.
    // NOTE: In some versions of Unity the password input box does not work in
    // iOS simulators.
    public bool usePasswordInput = false;
    private Vector2 controlsScrollViewVector = Vector2.zero;
    private Vector2 scrollViewVector = Vector2.zero;
    bool UIEnabled = true;
    public InputField emailx;
    public InputField passwordx;
    public InputField email_kayıt;
    public InputField password_kayıt;
    public Text hata_giriş;
    public Text hata_kayıt;
    public Text hata;
    public GameObject giriş_paneli;
    public int girisalmasayisi;
    public GameObject hatapaneli_giriş;
    public GameObject hatapaneli_kayıt;
    public Text hesabım_email;
    public Text hesabım_sifre;
    public GameObject kaydoltusu;
    
    public GameObject girişyapılmadıpaneli;
    //public Text hatayazisi;
    // Set the phone authentication timeout to a minute.
    private uint phoneAuthTimeoutMs = 60 * 1000;
    // The verification id needed along with the sent code for phone authentication.
    private string phoneAuthVerificationId;
    string email_oturum="emailll";
    string şifre_oturum = "şifree";
    // Options used to setup secondary authentication object.
    private Firebase.AppOptions otherAuthOptions = new Firebase.AppOptions
    {
        ApiKey = "",
        AppId = "",
        ProjectId = ""
    };

    const int kMaxLogSize = 16382;
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;

    // When the app starts, check to make sure that we have
    // the required dependencies to use Firebase, and if not,
    // add them if possible.
    public virtual void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false)
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();




        SignInWithPlayGames();
        //if (PlayGamesPlatform.Instance.IsAuthenticated())
        //{
        //    giriş_paneli.SetActive(false);
        //    hatapaneli_giriş.SetActive(false);


        //}
        //else
        //{

        //    SignInWithPlayGames();

        //}

        ////////////////////////////////////////////////////////////
        //if (PlayGamesPlatform.Instance.IsAuthenticated())
        //{
        //    giriş_paneli.SetActive(false);
        //    hatapaneli_giriş.SetActive(false);


        //}
        //else
        //{

        //    Invoke("SignInWithPlayGames", 0.01f);
        //}
        ///////////////////////////////////////////////////////////////////////////
        //if (PlayerPrefs.HasKey(email_oturum))
        //{

        //    hesabım_email.text = PlayerPrefs.GetString(email_oturum);
        //    emailx.text = PlayerPrefs.GetString(email_oturum);
        //    //for (int i = 0; i < emailx.text.Length; i++)
        //    //{
        //    //    if (emailx.text.Substring(i, 1) == "@")
        //    //    {
        //    //        GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi.text = emailx.text.Substring(0, emailx.text.Length - i);//email_kayıt.text;
        //    //    }
        //    //}
        //    ////////////////////////

        //    GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi.text = emailx.text;
        //    GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi_ANAMENU.text = emailx.text;
        //    GameObject.Find("firebase-message").GetComponent<databasee>().email= emailx.text;
        //}
        //if (PlayerPrefs.HasKey(şifre_oturum))
        //{

        //    passwordx.text = PlayerPrefs.GetString(şifre_oturum);
        //    hesabım_sifre.text = PlayerPrefs.GetString(şifre_oturum);

        //}
        //if (PlayerPrefs.GetInt("kaydolacılsın")==1)
        //{
        //    kaydoltusu.SetActive(false);
        //}
        //else
        //{
        //    kaydoltusu.SetActive(true);
        //}

        ///////////////////////////////////////////////////////////////////////////////
        //Firebase.Firebaseapp.checkandfixdependenciesasync().continuewith(task =>
        //{
        //    dependencystatus = task.result;
        //    if (dependencystatus == firebase.dependencystatus.available)
        //    {
        //        ınitializefirebase();
        //        gameobject.find("firebase-message").getcomponent<uıhandler>().initcagir();
        //        // gameobject.find("firebase-message").getcomponent<databasee>().intcagir_database();
        //        gameobject.find("firebase-message").getcomponent<firebasee>().initcagir_fireabase();
        //        gameobject.find("firebase-message").getcomponent<databasee>().intcagir_database();
        //    }
        //    else
        //    {

        //        debug.logerror(
        //          "could not resolve all firebase dependencies: " + dependencystatus);
        //        yenile();
        //    }
        //});
        /////////////////////////////////////////////////////////






        //emailx.text = PlayerPrefs.GetString("kayıtlı_gmail");
        //passwordx.text = PlayerPrefs.GetString("kayıtlı_şifre");
        //hesabım_email.text = PlayerPrefs.GetString("kayıtlı_gmail");
        //hesabım_sifre.text = PlayerPrefs.GetString("kayıtlı_şifre");
    }

    // Handle initialization of the necessary firebase modules:
    
    public void yenile()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            
dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                InitializeFirebase();
                GameObject.Find("firebase-message").GetComponent<UIHandler>().initcagir();
                //GameObject.Find("firebase-message").GetComponent<databasee>().intcagir_database();
                GameObject.Find("firebase-message").GetComponent<firebasee>().initcagir_fireabase();
                GameObject.Find("firebase-message").GetComponent<databasee>().intcagir_database();
            }
            else
            {
               
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
                //yenile();
                Invoke("yenile", 1);
            }
        });
        GameObject.Find("firebase-message").GetComponent<UIHandler>().surumcontrol();
        
    }
    protected void InitializeFirebase()
    {
        DebugLog("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        auth.IdTokenChanged += IdTokenChanged;
        
        // Specify valid options to construct a secondary authentication object.
        if (otherAuthOptions != null &&
            !(String.IsNullOrEmpty(otherAuthOptions.ApiKey) ||
              String.IsNullOrEmpty(otherAuthOptions.AppId) ||
              String.IsNullOrEmpty(otherAuthOptions.ProjectId)))
        {
            try
            {
                otherAuth = Firebase.Auth.FirebaseAuth.GetAuth(Firebase.FirebaseApp.Create(
                  otherAuthOptions, "Secondary"));
                otherAuth.StateChanged += AuthStateChanged;
                otherAuth.IdTokenChanged += IdTokenChanged;
            }
            catch (Exception)
            {
                DebugLog("ERROR: Failed to initialize secondary authentication object.");
            }
        }
        GetUserInfo();
        AuthStateChanged(this, null);
        GameObject.Find("firebase-message").GetComponent<UIHandler>().surumcontrol();
       // Invoke("yenile", 3);
    }

    // Exit if escape (or back, on mobile) is pressed.
    //protected virtual void Update()
    //{
    //    if (PhotonNetwork.connected)
    //    {
    //        if (PlayGamesPlatform.Instance.IsAuthenticated())
    //        {
    //            giriş_paneli.SetActive(false);
    //            hatapaneli_giriş.SetActive(false);
    //            girişyapılmadıpaneli.SetActive(false);

    //        }
    //        else
    //        {

    //            girişyapılmadıpaneli.SetActive(true);

    //        }
    //    }
    //if (Input.GetKeyDown(KeyCode.Escape))
    //{
    //    Application.Quit();
    //}
    //}
   public void girissorgula()
    {

        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            giriş_paneli.SetActive(false);
            hatapaneli_giriş.SetActive(false);
            girişyapılmadıpaneli.SetActive(false);

        }
        else
        {

            girişyapılmadıpaneli.SetActive(true);


        }
    }
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth.IdTokenChanged -= IdTokenChanged;
        auth = null;
        if (otherAuth != null)
        {
            otherAuth.StateChanged -= AuthStateChanged;
            otherAuth.IdTokenChanged -= IdTokenChanged;
            otherAuth = null;
        }
    }

    void DisableUI()
    {
        UIEnabled = false;
    }

    void EnableUI()
    {
        UIEnabled = true;
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

    // Display user information.
    void DisplayUserInfo(Firebase.Auth.IUserInfo userInfo, int indentLevel)
    {
        string indent = new String(' ', indentLevel * 2);
        var userProperties = new Dictionary<string, string> {
      {"Display Name", userInfo.DisplayName},
      {"Email", userInfo.Email},
      {"Photo URL", userInfo.PhotoUrl != null ? userInfo.PhotoUrl.ToString() : null},
      {"Provider ID", userInfo.ProviderId},
      {"User ID", userInfo.UserId}
    };
        foreach (var property in userProperties)
        {
            if (!String.IsNullOrEmpty(property.Value))
            {
                DebugLog(String.Format("{0}{1}: {2}", indent, property.Key, property.Value));
            }
        }
    }

    // Display a more detailed view of a FirebaseUser.
    void DisplayDetailedUserInfo(Firebase.Auth.FirebaseUser user, int indentLevel)
    {
        DisplayUserInfo(user, indentLevel);
        DebugLog("  Anonymous: " + user.IsAnonymous);
        DebugLog("  Email Verified: " + user.IsEmailVerified);
        var providerDataList = new List<Firebase.Auth.IUserInfo>(user.ProviderData);
        if (providerDataList.Count > 0)
        {
            DebugLog("  Provider Data:");
            foreach (var providerData in user.ProviderData)
            {
                DisplayUserInfo(providerData, indentLevel + 1);
            }
            
            
          
            
        }
        
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
        Firebase.Auth.FirebaseUser user = null;
        if (senderAuth != null) userByAuth.TryGetValue(senderAuth.App.Name, out user);
        if (senderAuth == auth && senderAuth.CurrentUser != user)
        {
            bool signedIn = user != senderAuth.CurrentUser && senderAuth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                DebugLog("Signed out " + user.UserId);
            }
            user = senderAuth.CurrentUser;
            userByAuth[senderAuth.App.Name] = user;
            if (signedIn)
            {
                DebugLog("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                DisplayDetailedUserInfo(user, 1);
            }
        }
    }
    // Track ID token changes.
    void IdTokenChanged(object sender, System.EventArgs eventArgs)
    {
        Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
        if (senderAuth == auth && senderAuth.CurrentUser != null && !fetchingToken)
        {
            senderAuth.CurrentUser.TokenAsync(false).ContinueWith(
              task => DebugLog(String.Format("Token[0:8] = {0}", task.Result.Substring(0, 8))));
        }
    }

    // Log the result of the specified task, returning true if the task
    // completed successfully, false otherwise.
    bool LogTaskCompletion(Task task, string operation)
    {
        bool complete = false;
        if (task.IsCanceled)
        {
            DebugLog(operation + " canceled.");
        }
        else if (task.IsFaulted)
        {
            DebugLog(operation + " encounted an error.");
            foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
            {
                string authErrorCode = "";
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    authErrorCode = String.Format("AuthError.{0}: ",
                      ((Firebase.Auth.AuthError)firebaseEx.ErrorCode).ToString());
                }
                DebugLog(authErrorCode + exception.ToString());
                hata.text = authErrorCode + exception.ToString();

                if (System.Text.RegularExpressions.Regex.Match(hata.text, "The password is invalid or the user does not have").Success)
                {
                    hata_giriş.text = "ŞİFRENİZ YANLIŞ; DİLERSENİZ ŞİFREMİ UNUTTUM KISMINDAN SIFIRLAYIN".ToString();
                    hatapaneli_giriş.SetActive(true);
                    ////// girişpaneli yanlış şifre girilince çıkar.
                    
                }
                else if (System.Text.RegularExpressions.Regex.Match(hata.text, "have been deleted").Success)
                {
                    hata_giriş.text = "BU E-MAİL ADRESİNE BAĞLI BİR HESAP MEVCUT DEĞİL".ToString();
                    hatapaneli_giriş.SetActive(true);
                    ////// giriş paneli kayıtlı olmayan bir email ile girilirse
                    
                }
                else if (System.Text.RegularExpressions.Regex.Match(hata.text, "Empty email or password are not").Success)
                {
                    hata_giriş.text = "LÜTFEN E-MAİL VE ŞİFRE KISIMLARINI BOŞ BIRAKMAYINIZ.".ToString();
                    hatapaneli_giriş.SetActive(true);
                    hata_kayıt.text = "LÜTFEN E-MAİL VE ŞİFRE KISIMLARINI BOŞ BIRAKMAYINIZ.".ToString();
                    hatapaneli_kayıt.SetActive(true);
                    ////// giriş paneli email ve şifre bölümü boş ise çıkar

                }
                else if (System.Text.RegularExpressions.Regex.Match(hata.text, "The email address is already in use by another").Success)
                {
                    hata_kayıt.text = "GİRDİĞİNİZ E-MAİL ADRESİNE AİT BİR HESAP ZATEN MEVCUT".ToString();
                    hatapaneli_kayıt.SetActive(true);
                    ////// KAYIT PANELİ girdiği email zaten mevcut ise çıkar
                    
                }
                else if (System.Text.RegularExpressions.Regex.Match(hata.text, "6 characters").Success)
                {
                    hata_kayıt.text = "ŞİFRENİZ EN AZ 6 HANELİ OLMALIDIR".ToString();
                    hatapaneli_kayıt.SetActive(true);
                    ////// KAYIT PANELİ ŞİFRE 6 HANEDEN AZ GİRİLDİYSE ÇIKAR
                    
                }
                else
                {
                    hata_kayıt.text = "".ToString();
                    hata_giriş.text = "".ToString();
                }
                ///////////////////////////////////////////
                //if (-1==String.Compare("The password is invalid or the user does not have", hata.text))
                //{
                //    hatapaneli_giriş.SetActive(true);
                //    ////// girişpaneli yanlış şifre girilince çıkar.
                //    hata_giriş.text = "ŞİFRENİZ YANLIŞ; DİLERSENİZ ŞİFREMİ UNUTTUM KISMINDAN SIFIRLAYIN";
                //}
                //else if (-1 == String.Compare("There is no user record corrosponding to this identifier", hata.text))
                //{
                //    hatapaneli_giriş.SetActive(true);
                //    ////// giriş paneli kayıtlı olmayan bir email ile girilirse
                //    hata_giriş.text = "BU E-MAİL ADRESİNE BAĞLI BİR HESAP MEVCUT DEĞİL";
                //}
                //else if (-1 == String.Compare("Empty email or password are not", hata.text))
                //{
                //    hatapaneli_giriş.SetActive(true);
                //    ////// giriş paneli email ve şifre bölümü boş ise çıkar
                //    hata_giriş.text = "LÜTFEN E-MAİL VE ŞİFRE KISIMLARINI BOŞ BIRAKMAYINIZ.";
                //}
                //else if (-1 == String.Compare("The email address is already in use by another", hata.text))
                //{
                //    hatapaneli_kayıt.SetActive(true);
                //    ////// KAYIT PANELİ girdiği email zaten mevcut ise çıkar
                //    hata_kayıt.text = "GİRDİĞİNİZ E-MAİL ADRESİNE AİT BİR HESAP ZATEN MEVCUT";
                //}
                //else if (-1 == String.Compare("6 characters", hata.text))
                //{
                //    hatapaneli_kayıt.SetActive(true);
                //    ////// KAYIT PANELİ ŞİFRE 6 HANEDEN AZ GİRİLDİYSE ÇIKAR
                //    hata_kayıt.text = "ŞİFRENİZ EN AZ 6 HANELİ OLMALIDIR";
                //}
                //else
                //{
                //    hatapaneli_kayıt.SetActive(false);
                //    hatapaneli_giriş.SetActive(false);
                //}

                hata.text = "";


                //    hata.text = authErrorCode + exception.ToString();
                //hata2.text = authErrorCode + exception.ToString();
            }
        }
        else if (task.IsCompleted)
        {
            DebugLog(operation + " completed");
            complete = true;
        }
        return complete;
    }
    public void SignInWithPlayGames()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            giriş_paneli.SetActive(true);
            Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            UnityEngine.Social.localUser.Authenticate((bool success) =>
            {
                if (!success)
                {
                    // hatapaneli_giriş.SetActive(true);
                    giriş_paneli.SetActive(true);
                    return;
                }
                string authcode = PlayGamesPlatform.Instance.GetServerAuthCode();
                if (string.IsNullOrEmpty(authcode))
                {
                    // hatapaneli_giriş.SetActive(true);
                    giriş_paneli.SetActive(true);

                    return;
                }
                Firebase.Auth.Credential credential = Firebase.Auth.PlayGamesAuthProvider.GetCredential(authcode);

                auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
                {
                    if (task.IsCanceled)
                    {
                        // hatapaneli_giriş.SetActive(true);
                        giriş_paneli.SetActive(true);
                        girişyapılmadıpaneli.SetActive(true);
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        //  hatapaneli_giriş.SetActive(true);
                        giriş_paneli.SetActive(true);
                        girişyapılmadıpaneli.SetActive(true);
                        return;
                    }
                    Firebase.Auth.FirebaseUser newUser = task.Result;
                    hatapaneli_giriş.SetActive(false);
                    giriş_paneli.SetActive(false);
                    girişyapılmadıpaneli.SetActive(false);
                    GameObject.Find("firebase-message").GetComponent<databasee>().yenimi_kontrol_et_database();
                    
                });

            });
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {

                dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    InitializeFirebase();
                    GameObject.Find("firebase-message").GetComponent<UIHandler>().initcagir();
                    //GameObject.Find("firebase-message").GetComponent<databasee>().intcagir_database();
                    GameObject.Find("firebase-message").GetComponent<firebasee>().initcagir_fireabase();
                    GameObject.Find("firebase-message").GetComponent<databasee>().intcagir_database();
                    
                }
                else
                {

                    Debug.LogError(
                      "Could not resolve all Firebase dependencies: " + dependencyStatus);
                    //yenile();
                    Invoke("yenile", 2);
                }
            });

            
            
        }
    }
    public void yeni_oyuncumu_kontrol_et()
    {

    }
    void Update()
    {
        
    }
    public Task CreateUserAsync()
    {
        //Debug.Log(email_kayıt);
        email = email_kayıt.text;
        password = password_kayıt.text;
        PlayerPrefs.SetString(email_oturum, email_kayıt.text);
        PlayerPrefs.SetString(şifre_oturum, password_kayıt.text);
        hesabım_email.text = email_kayıt.text;
        hesabım_sifre.text = password_kayıt.text;
        //for (int i = 0; i < email.Length; i++)
        //{
        //    //if (email.Substring(i, 1) == "@")
        //    //{
        //    //    GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi.text = email.Substring(0,i);//email_kayıt.text;
        //    //    GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi_ANAMENU.text = GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi.text;
                
        //    //    GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismikaydet();
        //    //}
        //}
        //for (int i = 0; i < "muratunlu207@gmail.com".Length; i++)
        //{
        //    if ("muratunlu207@gmail.com".ToString().Substring(i, 1) == "@")
        //    {
        //        Debug.Log("muratunlu207@gmail.com".ToString().Substring(0, i));
        //        //GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi.text = emailx.text.Substring(0, emailx.text.Length - i);//email_kayıt.text;
        //    }
        //}
        //GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi.text= email_kayıt.text;
        
        DebugLog(String.Format("Attempting to create user {0}...", email));
        DisableUI();
        
        // This passes the current displayName through to HandleCreateUserAsync
        // so that it can be passed to UpdateUserProfile().  displayName will be
        // reset by AuthStateChanged() when the new user is created and signed in.
        string newDisplayName = displayName;
        return auth.CreateUserWithEmailAndPasswordAsync(email, password)
          .ContinueWith((task) => {
              return HandleCreateUserAsync(task, newDisplayName: newDisplayName);
          }).Unwrap();

        




    }

    Task HandleCreateUserAsync(Task<Firebase.Auth.FirebaseUser> authTask,
                               string newDisplayName = null)
    {
        EnableUI();
        if (LogTaskCompletion(authTask, "User Creation"))
        {
            
            if (auth.CurrentUser != null)
            {
                DebugLog(String.Format("User Info: {0}  {1}", auth.CurrentUser.Email,
                                       auth.CurrentUser.ProviderId));
                
                return UpdateUserProfileAsync(newDisplayName: newDisplayName);
            }
            

        }
        // Nothing to update, so just return a completed Task.
        return Task.FromResult(0);
    }

    // Update the user's display name with the currently selected display name.
    public Task UpdateUserProfileAsync(string newDisplayName = null)
    {
        if (auth.CurrentUser == null)
        {
            DebugLog("Not signed in, unable to update user profile");
            return Task.FromResult(0);
        }
        displayName = newDisplayName ?? displayName;
        DebugLog("Updating user profile");
        DisableUI();
        return auth.CurrentUser.UpdateUserProfileAsync(new Firebase.Auth.UserProfile
        {
            
            DisplayName = displayName,
            PhotoUrl = auth.CurrentUser.PhotoUrl,
        }).ContinueWith(HandleUpdateUserProfile);
        
        
    }

    void HandleUpdateUserProfile(Task authTask)
    {
        EnableUI();
        if (LogTaskCompletion(authTask, "User profile"))
        {
            PlayerPrefs.SetInt("kaydolacılsın", 1);
            DisplayDetailedUserInfo(auth.CurrentUser, 1);
        }
    }

    public Task SigninAsync()
    {
       // Debug.Log(emailx.text);
        email = emailx.text;
        password = passwordx.text;
        PlayerPrefs.SetString(email_oturum, emailx.text);
        PlayerPrefs.SetString(şifre_oturum, passwordx.text);
        hesabım_email.text = emailx.text;
        hesabım_sifre.text = passwordx.text;
        //for (int i = 0; i < email.Length; i++)
        //{
        //    if (email.Substring(i, 1) == "@")
        //    {
        //        GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi.text = email.Substring(0, i);//email_kayıt.text;
        //        GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi_ANAMENU.text = GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi.text;
               

        //        GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismikaydet();
        //    }
        //}
        //GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().kullanici_ismi.text = emailx.text;
        
        DebugLog(String.Format("Attempting to sign in as {0}...", email));
        DisableUI();
        return auth.SignInWithEmailAndPasswordAsync(email, password)
          .ContinueWith(HandleSigninResult);
        
    }

    // This is functionally equivalent to the Signin() function.  However, it
    // illustrates the use of Credentials, which can be aquired from many
    // different sources of authentication.
    public Task SigninWithCredentialAsync()
    {
        DebugLog(String.Format("Attempting to sign in as {0}...", email));
        DisableUI();
        Firebase.Auth.Credential cred = Firebase.Auth.EmailAuthProvider.GetCredential(email, password);
        return auth.SignInWithCredentialAsync(cred).ContinueWith(HandleSigninResult);
    }

    // Attempt to sign in anonymously.
    public Task SigninAnonymouslyAsync()
    {
        DebugLog("Attempting to sign anonymously...");
        DisableUI();
        return auth.SignInAnonymouslyAsync().ContinueWith(HandleSigninResult);
    }

    void HandleSigninResult(Task<Firebase.Auth.FirebaseUser> authTask)
    {
        EnableUI();
        LogTaskCompletion(authTask, "Sign-in");
    }

    void LinkWithCredential()
    {
        if (auth.CurrentUser == null)
        {
            DebugLog("Not signed in, unable to link credential to user.");
            return;
        }
        DebugLog("Attempting to link credential to user...");
        Firebase.Auth.Credential cred = Firebase.Auth.EmailAuthProvider.GetCredential(email, password);
        auth.CurrentUser.LinkWithCredentialAsync(cred).ContinueWith(HandleLinkCredential);
    }

    void HandleLinkCredential(Task authTask)
    {
        if (LogTaskCompletion(authTask, "Link Credential"))
        {
            DisplayDetailedUserInfo(auth.CurrentUser, 1);
        }
    }

    public void ReloadUser()
    {
        if (auth.CurrentUser == null)
        {
            DebugLog("Not signed in, unable to reload user.");
            return;
        }
        DebugLog("Reload User Data");
        auth.CurrentUser.ReloadAsync().ContinueWith(HandleReloadUser);
    }

    void HandleReloadUser(Task authTask)
    {
        if (LogTaskCompletion(authTask, "Reload"))
        {
            DisplayDetailedUserInfo(auth.CurrentUser, 1);
        }
    }

    public void GetUserToken()
    {
        if (auth.CurrentUser == null)
        {
            DebugLog("Not signed in, unable to get token.");
            return;
        }
        DebugLog("Fetching user token");
        fetchingToken = true;
        auth.CurrentUser.TokenAsync(false).ContinueWith(HandleGetUserToken);
    }

    void HandleGetUserToken(Task<string> authTask)
    {
        fetchingToken = false;
        if (LogTaskCompletion(authTask, "User token fetch"))
        {
            DebugLog("Token = " + authTask.Result);
        }
    }

    void GetUserInfo()
    {
        if (auth.CurrentUser == null)
        {
            DebugLog("Not signed in, unable to get info.");
        }
        else
        {
            DebugLog("Current user info:");
            
            DisplayDetailedUserInfo(auth.CurrentUser, 1);
            
           
        }
    }

    public void SignOut()
    {
        DebugLog("Signing out.");
        auth.SignOut();
    }


    public Task DeleteUserAsync()
    {
        if (auth.CurrentUser != null)
        {
            DebugLog(String.Format("Attempting to delete user {0}...", auth.CurrentUser.UserId));
            DisableUI();
            return auth.CurrentUser.DeleteAsync().ContinueWith(HandleDeleteResult);
        }
        else
        {
            DebugLog("Sign-in before deleting user.");
            // Return a finished task.
            return Task.FromResult(0);
        }
    }

    void HandleDeleteResult(Task authTask)
    {
        EnableUI();
        LogTaskCompletion(authTask, "Delete user");
    }

    // Show the providers for the current email address.
    public void DisplayProvidersForEmail()
    {
        auth.FetchProvidersForEmailAsync(email).ContinueWith((authTask) => {
            if (LogTaskCompletion(authTask, "Fetch Providers"))
            {
                DebugLog(String.Format("Email Providers for '{0}':", email));
                foreach (string provider in authTask.Result)
                {
                    DebugLog(provider);
                }
            }
        });
    }

    // Send a password reset email to the current email address.
    public void SendPasswordResetEmail()
    {
        email = emailx.text;
        password = passwordx.text;
        auth.SendPasswordResetEmailAsync(email).ContinueWith((authTask) => {
            if (LogTaskCompletion(authTask, "Send Password Reset Email"))
            {
                DebugLog("Password reset email sent to " + email);
            }
        });
    }

    // Begin authentication with the phone number.
    public void VerifyPhoneNumber()
    {
        var phoneAuthProvider = Firebase.Auth.PhoneAuthProvider.GetInstance(auth);
        phoneAuthProvider.VerifyPhoneNumber(phoneNumber, phoneAuthTimeoutMs, null,
          verificationCompleted: (cred) => {
              DebugLog("Phone Auth, auto-verification completed");
              auth.SignInWithCredentialAsync(cred).ContinueWith(HandleSigninResult);
          },
          verificationFailed: (error) => {
              DebugLog("Phone Auth, verification failed: " + error);
          },
          codeSent: (id, token) => {
              phoneAuthVerificationId = id;
              DebugLog("Phone Auth, code sent");
          },
          codeAutoRetrievalTimeOut: (id) => {
              DebugLog("Phone Auth, auto-verification timed out");
          });
    }

    // Sign in using phone number authentication using code input by the user.
    public void VerifyReceivedPhoneCode()
    {
        var phoneAuthProvider = Firebase.Auth.PhoneAuthProvider.GetInstance(auth);
        // receivedCode should have been input by the user.
        var cred = phoneAuthProvider.GetCredential(phoneAuthVerificationId, receivedCode);
        auth.SignInWithCredentialAsync(cred).ContinueWith(HandleSigninResult);
    }

    // Determines whether another authentication object is available to focus.
    public bool HasOtherAuth { get { return auth != otherAuth && otherAuth != null; } }

    // Swap the authentication object currently being controlled by the application.
    public void SwapAuthFocus()
    {
        if (!HasOtherAuth) return;
        var swapAuth = otherAuth;
        otherAuth = auth;
        auth = swapAuth;
        DebugLog(String.Format("Changed auth from {0} to {1}",
                               otherAuth.App.Name, auth.App.Name));
    }
    public void kayıt_ol()
    {
        CreateUserAsync();
    }
    public void girişyap()
    {
        SigninAsync();
    }
    public void sıfırla()
    {
        SendPasswordResetEmail();
    }
    public void hesaptançık()
    {
        SignOut();
    }
    public void hesabısil()
    {
        DeleteUserAsync();
    }
    // Render the log output in a scroll view.
    //void GUIDisplayLog()
    //{
    //    scrollViewVector = GUILayout.BeginScrollView(scrollViewVector);
    //    GUILayout.Label(logText);
    //    GUILayout.EndScrollView();
    //}

    //// Render the buttons and other controls.
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
    //        GUILayout.Label("Password:", GUILayout.Width(Screen.width * 0.20f));
    //        password = usePasswordInput ? GUILayout.PasswordField(password, '*') :
    //            GUILayout.TextField(password);
    //        GUILayout.EndHorizontal();

    //        GUILayout.Space(20);

    //        GUILayout.BeginHorizontal();
    //        GUILayout.Label("Display Name:", GUILayout.Width(Screen.width * 0.20f));
    //        displayName = GUILayout.TextField(displayName);
    //        GUILayout.EndHorizontal();

    //        GUILayout.Space(20);

    //        GUILayout.BeginHorizontal();
    //        GUILayout.Label("Phone Number:", GUILayout.Width(Screen.width * 0.20f));
    //        phoneNumber = GUILayout.TextField(phoneNumber);
    //        GUILayout.EndHorizontal();

    //        GUILayout.Space(20);

    //        GUILayout.BeginHorizontal();
    //        GUILayout.Label("Phone Auth Received Code:", GUILayout.Width(Screen.width * 0.20f));
    //        receivedCode = GUILayout.TextField(receivedCode);
    //        GUILayout.EndHorizontal();

    //        GUILayout.Space(20);

    //        if (GUILayout.Button("Create User"))
    //        {
    //            CreateUserAsync();
    //        }
    //        if (GUILayout.Button("Sign In Anonymously"))
    //        {
    //            SigninAnonymouslyAsync();
    //        }
    //        if (GUILayout.Button("Sign In With Email"))
    //        {
    //            SigninAsync();
    //        }
    //        if (GUILayout.Button("Sign In With Credentials"))
    //        {
    //            SigninWithCredentialAsync();
    //        }
    //        if (GUILayout.Button("Link With Credential"))
    //        {
    //            LinkWithCredential();
    //        }
    //        if (GUILayout.Button("Reload User"))
    //        {
    //            ReloadUser();
    //        }
    //        if (GUILayout.Button("Get User Token"))
    //        {
    //            GetUserToken();
    //        }
    //        if (GUILayout.Button("Get User Info"))
    //        {
    //            GetUserInfo();
    //        }
    //        if (GUILayout.Button("Sign Out"))
    //        {
    //            SignOut();
    //        }
    //        if (GUILayout.Button("Delete User"))
    //        {
    //            DeleteUserAsync();
    //        }
    //        if (GUILayout.Button("Show Providers For Email"))
    //        {
    //            DisplayProvidersForEmail();
    //        }
    //        if (GUILayout.Button("Password Reset Email"))
    //        {
    //            SendPasswordResetEmail();
    //        }
    //        if (GUILayout.Button("Authenicate Phone Number"))
    //        {
    //            VerifyPhoneNumber();
    //        }
    //        if (GUILayout.Button("Verify Received Phone Code"))
    //        {
    //            VerifyReceivedPhoneCode();
    //        }
    //        if (HasOtherAuth && GUILayout.Button(String.Format("Switch to auth object {0}",
    //                                                           otherAuth.App.Name)))
    //        {
    //            SwapAuthFocus();
    //        }
    //        GUIDisplayCustomControls();
    //        GUILayout.EndVertical();
    //        GUILayout.EndScrollView();
    //    }
    //}

    //// Overridable function to allow additional controls to be added.
    //protected virtual void GUIDisplayCustomControls() { }

    ////Render the GUI:
    //void OnGUI()
    //{
    //    GUI.skin = fb_GUISkin;
    //    if (dependencyStatus != Firebase.DependencyStatus.Available)
    //    {
    //        GUILayout.Label("One or more Firebase dependencies are not present.");
    //        GUILayout.Label("Current dependency status: " + dependencyStatus.ToString());
    //        return;
    //    }
    //    Rect logArea, controlArea;

    //    if (Screen.width < Screen.height)
    //    {
    //        // Portrait mode
    //        controlArea = new Rect(0.0f, 0.0f, Screen.width, Screen.height * 0.5f);
    //        logArea = new Rect(0.0f, Screen.height * 0.5f, Screen.width, Screen.height * 0.5f);
    //    }
    //    else
    //    {
    //        // Landscape mode
    //        controlArea = new Rect(0.0f, 0.0f, Screen.width * 0.5f, Screen.height);
    //        logArea = new Rect(Screen.width * 0.5f, 0.0f, Screen.width * 0.5f, Screen.height);
    //    }

    //    GUILayout.BeginArea(logArea);
    //    GUIDisplayLog();
    //    GUILayout.EndArea();

    //    GUILayout.BeginArea(controlArea);
    //    GUIDisplayControls();
    //    GUILayout.EndArea();
    //}
}
