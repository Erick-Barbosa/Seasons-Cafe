using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClientBringer : MonoBehaviour {

    public static List<GameObject> clients = new();
    public static List<Sprite> clientSprites = new();
    public static List<Order> clientOrders = new();
    public static List<string> clientNames = new();

    public static List<GameObject> clientSpriteHolders = new();
    public static List<TextMeshProUGUI> clientOrdersTexs = new();
    public static int clientCount;
    public static void BringNewClient(GameObject clientPrefab, GameObject clientListInScene) {

        clientPrefab.name = (++clientCount).ToString();
        
        clientPrefab.GetComponent<Client>().hasStarted = false;
        GameObject instantiatedNewClient = Instantiate(clientPrefab, clientListInScene.transform);

        clients.Add(instantiatedNewClient);
        clientNames.Add(instantiatedNewClient.name);
    }

    public static void DeliverClientOrder(GameObject client) {
        int index = clientNames.IndexOf(client.name);

        if (index != -1) {
            Debug.Log(clients[index]);
            clients.RemoveAt(index);
            clientNames.RemoveAt(index);
            clientSprites.RemoveAt(index);

            CafeManager.Instance.RemoveOrderFromList(clientOrders[index]);

            clientOrders.RemoveAt(index);
            Destroy(client);

            PlayerHand.Instance.DropPickableObject();
        }
    }
    
    public static List<GameObject> GetClients() {
        return clients;
    }
    public static Sprite GetClientSpriteByIndex(int index) {
        return clientSprites[index];
    }
    public static Order GetClientOrderByIndex(int index) {
        return clientOrders[index];
    }
    public static string GetClientNameByIndex(int index) {
        return clientNames[index];
    }
    public static void AddClientSprite(Sprite sprite) {
        clientSprites.Add(sprite);
    }
    public static void AddClientOrder(Order order) {
        clientOrders.Add(order);
    }
}