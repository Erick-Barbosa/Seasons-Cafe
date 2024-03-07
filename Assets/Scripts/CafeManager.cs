using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CafeManager : MonoBehaviour {
    public List<GameObject> seats = new List<GameObject>();

    [SerializeField] private List<Sprite> clientObjects = new List<Sprite>();

    [SerializeField] private GameObject FinishedCupPrefab;
    [SerializeField] private GameObject ClientPrefab;
    [SerializeField] private GameObject FinishedCupListInScene;
    [SerializeField] private GameObject ClientListInScene;

    public static CafeManager Instance;

    private int maxClients = 7;
    private float timeRemaining;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        timeRemaining = 2;
        List<Order> ordersMade = OrderManager.ordersMade;

        int index = 0;
        foreach (Order order in ordersMade) {
            Instantiate(FinishedCupPrefab, FinishedCupListInScene.transform);

            FinishedCupListInScene.transform.GetChild(index).GetComponent<FinishedCup>().order = order;
            index++;
        }

        LoadClients();
    }

    public void RemoveOrderFromList(Order order) {
        OrderManager.ordersMade.Remove(order);
    }

    public void LoadClients() {
        int index = 0;
        foreach (GameObject client in ClientBringer.GetClients()) {
            ClientPrefab.GetComponent<Client>().hasStarted = true;

            GameObject instantiatedClient = Instantiate(ClientPrefab, ClientListInScene.transform);
                        
            instantiatedClient.GetComponent<Client>().order = ClientBringer.GetClientOrderByIndex(index);
            instantiatedClient.GetComponent<Client>().clientSprite = ClientBringer.GetClientSpriteByIndex(index);
            instantiatedClient.name = ClientBringer.GetClientNameByIndex(index);

            index++;
        }
    }

    private void Update() {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
        } else if (timeRemaining <= 0) {
            if (ClientBringer.GetClients().Count < maxClients) {
                timeRemaining = 2;
                ClientBringer.BringNewClient(ClientPrefab, ClientListInScene);
            }
        }
    }

    public Sprite GetRandomClientObject() {
        int randomIndex = UnityEngine.Random.Range(0, clientObjects.Count);

        return clientObjects[randomIndex];
    }
}
