using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using UserInfo;

public class ContactListView : MonoBehaviour {
    [SerializeField] private GameObject contactPrefab;
    private List<User> contactList;

    void Start() {
        contactList = WorkWithData.GetAllFriendsFromServer();
    }

    private void DisplayContacts() {
        foreach (var contact in contactList) {
            if (contact!= null) {
            }
        }
    }

    private void SetUIData(User user) {
        
    }
    
}