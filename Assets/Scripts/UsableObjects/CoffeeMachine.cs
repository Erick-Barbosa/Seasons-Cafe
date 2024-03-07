using UnityEngine;

public class CoffeeMachine : UsableScript {
    private bool canMakeCoffee;
    public static CoffeeMachine Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    override public void OnInteract(GameObject holdingObject) {
        canMakeCoffee = CoffeePotLeft.IsFilled && !CoffeePotRight.IsNotFilled;
        MakeCoffee(holdingObject);
    }

    private void MakeCoffee(GameObject holdingObject) {
        if (canMakeCoffee) {
            if (holdingObject.name == ConstUtils.Cup) {
                if (Cup.Instance.AddIngredient(
                    CoffeePotLeft.IngredientName,
                    CoffeePotLeft.IngredientAmount,
                    this
                )) {
                    CoffeePotLeft.Instance.ResetPot();
                    CoffeePotRight.Instance.ResetPot();
                    HasFirst = true;
                }
            }
            else {
                InfoTextsManager.Instance.ShowMessage(MessageConsts.MissingCup);
            }
        }
        else {
            InfoTextsManager.Instance.ShowMessage(MessageConsts.MissingIngredients);
        }
    }

    public void DeliveredCoffee() {
        HasFirst = false;
    }
}
