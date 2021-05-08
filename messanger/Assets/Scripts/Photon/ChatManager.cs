using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Chat;
using UnityEngine;

public class ChatManager : MonoBehaviour, IChatClientListener {
    [SerializeField] private string userID;
    private ChatClient chatClient; 
    void Start() {
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.ChatAppID, PhotonNetwork.versionPUN, new Photon.Chat.AuthenticationValues(userID));
    }

    private void Update() {
        chatClient.Service();
    }

    public void DebugReturn(DebugLevel level, string message) {
        throw new System.NotImplementedException();
    }

    public void OnDisconnected() {
        throw new System.NotImplementedException();
    }

    public void OnConnected() {
        throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state) {
        throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages) {
        throw new System.NotImplementedException();
    }

    public void OnPrivateMessage(string sender, object message, string channelName) {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results) {
        throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels) {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message) {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user) {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user) {
        throw new System.NotImplementedException();
    }
}
