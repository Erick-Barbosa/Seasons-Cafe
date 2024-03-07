using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarPouch : UsableScript {
    public static SugarPouch Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
    public override void OnInteract(GameObject holdingObject) {
        if (holdingObject.name == ConstUtils.Cup) {
                Cup.Instance.AddIngredient(ConstUtils.SugarPouch, ConstUtils.SingleUnity, this);
                HasFirst = true;
        } else {
            InfoTextsManager.Instance.ShowMessage(MessageConsts.MissingCup);
        }
    }

    public void DeliveredCoffee() {
        HasFirst = false;
    }
}
