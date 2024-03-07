using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Client : UsableScript {
    public Order order;
    public Sprite clientSprite;
    public bool hasStarted = false;

    [SerializeField] public List<TextMeshProUGUI> IngredientAmountTexts;
    [SerializeField] public List<GameObject> IngredientIconHolders;

    [SerializeField] public TextMeshProUGUI CoffeTypeText;
    [SerializeField] private GameObject IngredientsBackgroundImage;

    private CafeManager cafeManager;
    new private void Start() {
        base.Start();

        IngredientsBackgroundImage.SetActive(false);

        if (!hasStarted) {
            GenerateRandomOrder();
            SetRandomSprite();
            ClientBringer.GetClientSpriteByIndex(0);

            hasStarted = true;
        }

        for (int i = 0; i < IngredientAmountTexts.Count; i++) {
            if (order.completeOrder[i].Amount > 0) {
                IngredientIconHolders[i].GetComponent<SpriteRenderer>().sprite = order.completeOrder[i].Icon;
                IngredientAmountTexts[i].text = order.completeOrder[i].Amount.ToString();

                IngredientIconHolders[i].SetActive(true);
                IngredientAmountTexts[i].enabled = true;

                if (order.completeOrder[i].Name.Contains(ConstUtils.Coffee)) {
                    string coffeTypeString = InfoTextsManager.Instance.GetDisplayNameFromObjectName(order.completeOrder[i].Name);
                    CoffeTypeText.text = coffeTypeString.Substring(0, coffeTypeString.IndexOf(" "));
                }

            }
            else {
                IngredientIconHolders[i].SetActive(false);
                IngredientAmountTexts[i].enabled = false;
                IngredientAmountTexts[i].text = "";
            }
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = clientSprite;
    }

    public void GenerateRandomOrder() {
        string[] coffeeList = { 
            ConstUtils.CoffeePot1,
            ConstUtils.CoffeePot2,
            ConstUtils.CoffeePot3,
            ConstUtils.CoffeePot4,
            ConstUtils.CoffeePot5, 
        };

        int randomCoffeeIndex = Random.Range(0, coffeeList.Length - 1);
        int randomAmount1= Random.Range(1, ConstUtils.MaxIngredientAmount);
        int randomAmount2= Random.Range(0, ConstUtils.MaxIngredientAmount / 2) / 2;
        int randomAmount3= Random.Range(0, ConstUtils.MaxIngredientAmount / 2) / 2;

        List<Ingredient> orderRandomIngredients = new List<Ingredient> {
            new Ingredient(coffeeList[randomCoffeeIndex], randomAmount1),
            new Ingredient(ConstUtils.MilkMachine, randomAmount2),
            new Ingredient(ConstUtils.SugarPouch, randomAmount3)
        };
        Order newOrder = new Order(orderRandomIngredients);
        ClientBringer.AddClientOrder(newOrder);

        order = newOrder;
    }

    public void SetRandomSprite() {
        Sprite newSprite = CafeManager.Instance.GetRandomClientObject();
        ClientBringer.AddClientSprite(newSprite);
        
        clientSprite = newSprite;
    }

    private void OnMouseEnter() {
        IngredientsBackgroundImage.SetActive(true);
    }

    private void OnMouseExit() {
        IngredientsBackgroundImage.SetActive(false);
    }

    public override void OnInteract(GameObject holdingObject) {
        if (holdingObject.GetComponent<FinishedCup>().order.Equals(order)) {
            ClientBringer.DeliverClientOrder(gameObject);
            Destroy(holdingObject);
        } else {
            Debug.Log("incorrect order");
        }
    }
}
