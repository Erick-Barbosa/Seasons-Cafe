using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour {
    public static List<Order> ordersMade = new List<Order>();
    public static List<Order> ordersByClients = new List<Order>();

    public static void DeliverOrder(Order deliveringOrder) {
        ordersMade.Add(deliveringOrder);

        MilkMachine.Instance.DeliveredCoffee();
        CoffeeMachine.Instance.DeliveredCoffee();
        SugarPouch.Instance.DeliveredCoffee();
    }

    public void DoOrder(Order clientOrder) {
        ordersByClients.Add(clientOrder);
    }    
}
