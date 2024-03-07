using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoffeePotLeft : UsableScript {
    List<Transform> children = new List<Transform>();
    private Transform currentActiveChild;

    [SerializeField] private TextMeshProUGUI coffeeAmountText;
    [SerializeField] private Button coffeeResetButton;
    static public bool IsFilled { get; private set; } = false;
    static public int IngredientAmount { get; private set; }
    public int MaxIngredientAmount { get; private set; } = 9;
    static public string IngredientName { get; private set; }
    public static CoffeePotLeft Instance { get; private set; }
    override public void OnInteract(GameObject holdingObject) {
        FillPot(holdingObject);
    }

    private void Awake() {
        Instance = this;
    }

    new private void Start() {
        base.Start();

        coffeeResetButton.onClick.AddListener(ResetPot);
        Debug.Log(IngredientAmount);
        if (IngredientAmount <= 0) {
            SetInfoVisibility(false);

            int childrenCount = transform.childCount - 1;
            while (childrenCount > 0) {
                children.Add(transform.GetChild(childrenCount));
                transform.GetChild(childrenCount).gameObject.SetActive(false);
                childrenCount--;
            }
        }
        else {
            currentActiveChild = children.Find(child => child.name == IngredientName);
            currentActiveChild.gameObject.SetActive(true);
            SetInfoVisibility(true);
            coffeeAmountText.text = IngredientAmount.ToString();
        }
    }
    private void FillPot(GameObject holdingObject) {
        if (!IsFilled) {
            switch (holdingObject.name) {
                default:
                InfoTextsManager.Instance.ShowMessage(MessageConsts.MissingCoffeePowder);
                return;
                case ConstUtils.CoffeePot1:
                IsFilled = true;
                IngredientName = ConstUtils.CoffeePot1;
                break;
                case ConstUtils.CoffeePot2:
                IsFilled = true;
                IngredientName = ConstUtils.CoffeePot2;
                break;
                case ConstUtils.CoffeePot3:
                IsFilled = true;
                IngredientName = ConstUtils.CoffeePot3;
                break;
                case ConstUtils.CoffeePot4:
                IsFilled = true;
                IngredientName = ConstUtils.CoffeePot4;
                break;
                case ConstUtils.CoffeePot5:
                IsFilled = true;
                IngredientName = ConstUtils.CoffeePot5;
                break;
            }
            currentActiveChild = children.Find(child => child.name == holdingObject.name);
            currentActiveChild.gameObject.SetActive(true);
            IngredientAmount++;
            SetInfoVisibility(true);
            coffeeAmountText.text = IngredientAmount.ToString();
        }
        else if (IsFilled && holdingObject.name == IngredientName) {
            if (IngredientAmount < MaxIngredientAmount) {
                IngredientAmount++;
                coffeeAmountText.text = IngredientAmount.ToString();
            }
        }
    }

    public void ResetPot() {
        currentActiveChild.gameObject.SetActive(false);
        IngredientAmount = 0;
        SetInfoVisibility(false);
        IsFilled = false;
    }

    private void SetInfoVisibility(bool value) {
        coffeeAmountText.enabled = value;
        coffeeResetButton.interactable = value;
    }
}
