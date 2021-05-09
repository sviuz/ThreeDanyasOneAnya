using System.Collections.Generic;
using Data;
using UnityEngine;
using UserInfo;

namespace Panels {
    public class ContactListView : MonoBehaviour {
        [SerializeField] private GameObject contactPrefab;
        private List<User> contactList;

        void Start() {
            contactList = WorkWithData.GetAllFriendsFromServer();
        }

        private void DisplayContacts() {
            foreach (var contact in contactList) {
                if (contact!= null) {
                    //Instantiate(contactPrefab);
                    //TODO сделать реализацию создания префабов в scroll rect
                    //TODO данные для префабов брать в 
                }
            }
        }

        private void SetUIData(User user) {
        
        }
    
    }
}