using UnityEngine;

public class MilkMachine : UsableScript {
    public static MilkMachine Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    override public void OnInteract(GameObject holdingObject) {
        if (holdingObject.name == ConstUtils.Cup) {
            Cup.Instance.AddIngredient(ConstUtils.MilkMachine, ConstUtils.SingleUnity, this);
            HasFirst = true;
        }
        else
            InfoTextsManager.Instance.ShowMessage(MessageConsts.MissingCup);
    }

    public void DeliveredCoffee() {
        HasFirst = false;
    }
}
