using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuSceneScript : MonoBehaviour
{
    public GameObject friendPrefab;
    public GameObject messagePrefab;
    private UnityEngine.UI.Button btnExit;
    private UnityEngine.UI.Image btnLoadImage;
    private InputField username;
    private InputField password;
    private InputField email;
    private UnityEngine.UI.Button btnSaveChanges;
    private UnityEngine.UI.Button btnReset;
    private InputField report_username;
    private InputField report_reason;
    private UnityEngine.UI.Button btnReport;
    private InputField friend_username;
    private UnityEngine.UI.Button btnAddFriend;
    private ScrollRect scrollFriends;
    private ScrollRect _msg;
    private ScrollView scrollMessages;
    private InputField message;
    private UnityEngine.UI.Button btnSendMessage;
    private UnityEngine.UI.Button btnRefresh;
    GameObject selectedChat;
    // Start is called before the first frame update
    void Start()
    {
        scrollMessages = GameObject.Find("scrollMessages").GetComponent<ScrollView>() as ScrollView;
        btnExit = GameObject.FindWithTag("btnExit").GetComponent<UnityEngine.UI.Button>() as UnityEngine.UI.Button;
        btnLoadImage = GameObject.FindWithTag("btnLoadImage").GetComponent<UnityEngine.UI.Image>() as UnityEngine.UI.Image;
        username = GameObject.FindWithTag("username").GetComponent<InputField>() as InputField;
        password = GameObject.FindWithTag("password").GetComponent<InputField>() as InputField;
        email = GameObject.FindWithTag("email").GetComponent<InputField>() as InputField;
        btnSaveChanges = GameObject.FindWithTag("btnSaveChanges").GetComponent<UnityEngine.UI.Button>() as UnityEngine.UI.Button;
        btnReset = GameObject.FindWithTag("btnReset").GetComponent<UnityEngine.UI.Button>() as UnityEngine.UI.Button;
        report_username = GameObject.FindWithTag("report_username").GetComponent<InputField>() as InputField;
        report_reason = GameObject.FindWithTag("report_reason").GetComponent<InputField>() as InputField;
        btnReport = GameObject.FindWithTag("btnReport").GetComponent<UnityEngine.UI.Button>() as UnityEngine.UI.Button;
        friend_username = GameObject.FindWithTag("friend_username").GetComponent<InputField>() as InputField;
        btnAddFriend = GameObject.FindWithTag("btnAddFriend").GetComponent<UnityEngine.UI.Button>() as UnityEngine.UI.Button;
        scrollFriends = GameObject.Find("scrollFriends").GetComponent<ScrollRect>() as ScrollRect;
        _msg = GameObject.Find("scrollMessages").GetComponent<ScrollRect>() as ScrollRect;
        message = GameObject.FindWithTag("message").GetComponent<InputField>() as InputField;
        btnSendMessage = GameObject.FindWithTag("btnSendMessage").GetComponent<UnityEngine.UI.Button>() as UnityEngine.UI.Button;
        btnRefresh = GameObject.FindWithTag("btnRefresh").GetComponent<UnityEngine.UI.Button>() as UnityEngine.UI.Button;
        //btnRefresh.onClick.AddListener(RefreshMessages);
        btnSaveChanges.onClick.AddListener(SaveChanges);
        btnReset.onClick.AddListener(ResetInfo);
        btnExit.onClick.AddListener(Exit);
        btnReport.onClick.AddListener(Report);
        btnAddFriend.onClick.AddListener(AddFriend);
        btnSendMessage.onClick.AddListener(SendMessage);
        GetPersonalInfo();
        //LoadChatsList();
    }

    void GetPersonalInfo()
    {
        var settings = LocalSettings.GetSettings();
        NetworkData data = new NetworkData();
        ServerMessage request = new ServerMessage();
        request.ActionId = (int)Actions.GetPersonalInfo;
        request.Users.Add(new Assets.Database.User()
        {
            Username = settings.User.Username
        });
        ServerMessage response = data.Query(request);
        if(response.isOk)
        {
            
            /*Texture2D texture = new Texture2D(200, 200);
            texture.LoadImage(response.File);
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            btnLoadImage.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));*/
            settings.User.Username = response.Users[0].Username;
            username.text = settings.User.Username;
            settings.User.Email = response.Users[0].Email;
            email.text = settings.User.Email;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Exit()
    {
        NetworkData data = new NetworkData();
        ServerMessage request = new ServerMessage();
        request.Users.Add(new Assets.Database.User()
        {
            Username = LocalSettings.GetSettings().User.Username
        });
        request.ActionId = (int)Actions.SignOut;
        ServerMessage response = data.Query(request);
        if(response.isOk)
        {
            Application.Quit();
        }
    }

    void SaveChanges()
    {
        var settings = LocalSettings.GetSettings();
        NetworkData data = new NetworkData();
        ServerMessage request = new ServerMessage();
        request.ActionId = (int)Actions.SetPersonalInfo;
        request.Users.Add(new Assets.Database.User());
        if(username.text.Length != 0)
        {
            request.Users[0].Username = username.text;
        }
        if (email.text.Length != 0)
        {
            request.Users[0].Email = email.text;
        }
        if (password.text.Length != 0)
        {
            request.Password = password.text;
        }
        ServerMessage response = data.Query(request);
        if(response.isOk)
        {
            if (username.text.Length != 0)
            {
                request.Users[0].Username = settings.User.Username;
            }
            if (email.text.Length != 0)
            {
                request.Users[0].Email = settings.User.Email;
            }
        }
    }

    void ResetInfo()
    {
        var settings = LocalSettings.GetSettings();
        username.text = settings.User.Username;
        email.text = settings.User.Email;
        password.text = "";
    }

    void Report()
    {
        var settings = LocalSettings.GetSettings();
        NetworkData data = new NetworkData();
        ServerMessage request = new ServerMessage();
        request.ActionId = (int)Actions.SendReport;
        request.Users.Add(new Assets.Database.User()
        {
            Username = settings.User.Username
        });
        request.Users.Add(new Assets.Database.User()
        {
            Username = report_username.text
        });
        request.Messages.Add(new Assets.Database.Message()
        {
            Text = report_reason.text
        });
        ServerMessage response = data.Query(request);
        if(response.isOk)
        {
            report_username.text = "";
        }
    }

    void AddFriend()
    {
        var settings = LocalSettings.GetSettings();
        NetworkData data = new NetworkData();
        ServerMessage request = new ServerMessage();
        request.ActionId = (int)Actions.AddFriend;
        request.Users.Add(new Assets.Database.User()
        {
            Username = settings.User.Username
        });
        request.Users.Add(new Assets.Database.User()
        {
            Username = friend_username.text
        });
        ServerMessage response = data.Query(request);
    }

    void SendMessage()
    {
        var settings = LocalSettings.GetSettings();
        NetworkData data = new NetworkData();
        ServerMessage request = new ServerMessage();
        request.ActionId = (int)Actions.AddMessage;
        request.Messages.Add(new Assets.Database.Message()
        {
            Text = message.text,
            Time = DateTime.Now,
            ChatId = int.Parse(selectedChat.tag)
        });
        request.Users.Add(new Assets.Database.User()
        {
            Username = settings.User.Username
        });
        ServerMessage response = data.Query(request);
        if(response.isOk)
        {
            var message_prefab = Instantiate(messagePrefab, _msg.transform);
            var image = message.transform.Find("UserImageMessage").GetComponent<UnityEngine.UI.Image>();
            var text = message.transform.Find("UserImageMessage").GetComponent<InputField>();
            text.text = settings.User.Username + ":\n" + message.text;
        }
    }

    void LoadChatsList()
    {
        var settings = LocalSettings.GetSettings();
        NetworkData data = new NetworkData();
        ServerMessage request = new ServerMessage();
        request.ActionId = (int)Actions.GetChatsList;
        request.Users.Add(new Assets.Database.User()
        {
            Username = settings.User.Username
        });
        ServerMessage response = data.Query(request);
        if(response.isOk)
        {
            settings.Chats.AddRange(response.Chats);
            for(int i = 0; i < settings.Chats.Count; i++)
            {
                var chat = Instantiate(friendPrefab, scrollFriends.content.transform);
                chat.tag = settings.Chats[i].Id.ToString();
                chat.transform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { LoadMessages(chat); });
                var image = chat.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();
                var button = chat.transform.Find("btnRemoveFriend").GetComponent<UnityEngine.UI.Button>();
                Destroy(button);
                Texture2D texture = new Texture2D(80, 80);
                ServerMessage request_1 = new ServerMessage();
                request_1.ActionId = (int)Actions.GetFile;
                request_1.Filename = settings.Chats[i].ImagePath;
                ServerMessage response_1 = data.Query(request_1);
                if (response_1.isOk)
                {
                    texture.LoadImage(response_1.File);
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    image.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
                }
                var input_fields = chat.GetComponents<InputField>();
                for(int a = 0; a < input_fields.Length; a++)
                {
                    if(input_fields[a].tag == "username")
                    {
                        input_fields[a].text = settings.Chats[i].Name;
                    }
                    if (input_fields[a].tag == "email")
                    {
                        input_fields[a].text = "chat";
                    }
                }
            }
        }
    }

    void RefreshMessages()
    {
        LoadMessages(selectedChat);
    }

    void LoadMessages(GameObject oc)
    {
        /*selectedChat = oc;
        foreach (Transform child in scrollMessages.content.transform)
        {
            Destroy(child);
        }*/
        NetworkData data = new NetworkData();
        ServerMessage request = new ServerMessage();
        request.ActionId = (int)Actions.GetMessages;
        request.Chats.Add(new Assets.Database.Chat()
        {
            Id = int.Parse(oc.tag)
        });
        ServerMessage response = data.Query(request);
        if(response.isOk)
        {
            for(int i = 0; i < response.Messages.Count; i++)
            {
                var message = Instantiate(messagePrefab, _msg.transform);
                var image = message.transform.Find("UserImageMessage").GetComponent<UnityEngine.UI.Image>();
                var text = message.transform.Find("UserImageMessage").GetComponent<InputField>();
                text.text = response.Messages[i].User.Username + ":\n" + response.Messages[i].Text;
                ServerMessage request_1 = new ServerMessage();
                request_1.ActionId = (int)Actions.GetFile;
                request_1.Filename = response.Chats[i].ImagePath;
                ServerMessage response_1 = data.Query(request_1);
                if(response_1.isOk)
                {
                    Texture2D texture = new Texture2D(80, 80);
                    texture.LoadImage(response_1.File);
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    image.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
                }
            }
        }
    }

    void DeleteFriend(string username, GameObject obj)
    {
        var settings = LocalSettings.GetSettings();
        NetworkData data = new NetworkData();
        ServerMessage request = new ServerMessage();
        request.ActionId = (int)Actions.DelFriend;
        request.Users.Add(new Assets.Database.User()
        {
            Username = settings.User.Username
        });
        request.Users.Add(new Assets.Database.User()
        {
            Username = username
        });
        ServerMessage response = data.Query(request);
        if(request.isOk == true)
        {
            Destroy(obj);
        }
    }
}
