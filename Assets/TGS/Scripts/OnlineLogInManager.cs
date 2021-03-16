
using Facebook.Unity;
using Facebook;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using LoginResult = PlayFab.ClientModels.LoginResult;
using UnityEngine.UI;

public class OnlineLogInManager : MonoBehaviour
{
    // holds the latest message to be displayed on the screen
    private string _message;
    public static OnlineLogInManager This;
    private void Awake()
    {
        This = this;
    }
    public Image ProfilePicture;
    public Text NameOfUser;

    void Start()
    {
        FB.Init(OnInitComplete, OnHideUnity, null);
    }

    void OnInitComplete()
    {

        Invoke("CheckLoginOrNot", 1f);
    }
    public void CheckLoginOrNot()
    {
        Debug.Log(FB.IsLoggedIn);
        if (FB.IsLoggedIn)
        {
            PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest
            {
                CreateAccount = true,
                AccessToken = AccessToken.CurrentAccessToken.TokenString
            },
                  OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);
        }
        else
        {
            OnFacebookInitialized();

        }
    }
    void OnHideUnity(bool unityIsHidden)
    {
    }

    private void OnFacebookInitialized()
    {

        // Once Facebook SDK is initialized, if we are logged in, we log out to demonstrate the entire authentication cycle.
        //if (!FB.IsLoggedIn)       
        // We invoke basic login procedure and pass in the callback to process the result
        var perms = new List<string>() { "public_profile" };
        FB.LogInWithReadPermissions(perms, OnFacebookLoggedIn);
    }

    private void OnFacebookLoggedIn(ILoginResult result)
    {
        Debug.Log(result.ToString());
        // If result has no errors, it means we have authenticated in Facebook successfully
        if (result == null || string.IsNullOrEmpty(result.Error))
        {
            FB.API("me/picture?type=square&height=128&width=128", HttpMethod.GET, FbGetPicture);
            FB.API("/me?fields=id,first_name,last_name,gender,email", HttpMethod.GET, graphCallback);

            /*
             * We proceed with making a call to PlayFab API. We pass in current Facebook AccessToken and let it create
             * and account using CreateAccount flag set to true. We also pass the callback for Success and Failure results
             */
            PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest
            {
                CreateAccount = true,
                AccessToken = AccessToken.CurrentAccessToken.TokenString
            },
                OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);
        }
        else
        {
            // If Facebook authentication failed, we stop the cycle with the message
        }
    }
    private void FbGetPicture(IGraphResult result)
    {
        if (result.Texture != null)
            ProfilePicture.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
    }
    private void graphCallback(IGraphResult result)
    {
        Debug.Log("hi= " + result.RawResult);
        string firstname;
        string lastname;
      
        if (result.ResultDictionary.TryGetValue("first_name", out firstname))
        {
            NameOfUser.text = firstname.ToString();
        }
        if (result.ResultDictionary.TryGetValue("last_name", out lastname))
        {
            NameOfUser.text += " " + lastname.ToString();
        }
        

    }
    // When processing both results, we just set the message, explaining what's going on.
    private void OnPlayfabFacebookAuthComplete(LoginResult result)
    {
        Debug.LogError("authPlayfabComplete");
        GetUserData();
        ClientGetTitleData();
    }
    public void ClientGetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
            result => {
                if (result.Data == null || !result.Data.ContainsKey("Name")) Debug.Log("No MonsterName");
                else Debug.Log("MonsterName: " + result.Data["Name"]);
            },
            error => {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }
    public Save ReturnGreater(Save online, Save local)
    {
        Debug.Log("online Data : " + online.Score + "Local :" + local.Score);
        Save DataToSave = new Save();
        DataToSave.Score = local.Score <= online.Score ? online.Score : local.Score;
        DataToSave.UnlockGuns = local.UnlockGuns <= online.UnlockGuns ? online.UnlockGuns : local.UnlockGuns;
        DataToSave.UnlockLevels = local.UnlockLevels <= online.UnlockLevels ? online.UnlockLevels : local.UnlockLevels;
        DataToSave.TotalLevels = local.TotalLevels;
        DataToSave.RemoveAds = local.RemoveAds;
        SetUserData(DataToSave);
        return DataToSave;
    }
    private void OnPlayfabFacebookAuthFailed(PlayFabError error)
    {

    }
    public void SetUserData(Save DataToUpdate)
    {
        if (FB.IsLoggedIn)
        {
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
            {
                Data = new System.Collections.Generic.Dictionary<string, string>()
            {
                { "UserData",JsonUtility.ToJson(DataToUpdate) }
            }
            },
            result => Debug.Log("Successfully updated user data"),

            error =>
            {

                Debug.Log("Got error setting user data Ancestor to Arthur");

                Debug.Log(error.GenerateErrorReport());

            });
        }
    }
    public Save GetUserData()
    {
        Save ReturnData = new Save();
        if (FB.IsLoggedIn)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            {

            }
            , result =>
            {
                if (result.Data == null || !result.Data.ContainsKey("UserData")) ReturnData = StoreData.LoadAsObj();
                else ReturnData = JsonConvert.DeserializeObject<Save>(result.Data["UserData"].Value);
                Debug.LogError(ReturnData.Score);
              //  StoreData.CreateSaveGameObject(ReturnData);
                StoreData.CreateSaveGameObject(ReturnGreater(ReturnData, StoreData.LoadAsObj()));
            }, (error) =>
            {
                Debug.Log("Got error retrieving user data:");
                Debug.Log(error.GenerateErrorReport());
            });
        }
        return ReturnData;
    }
}