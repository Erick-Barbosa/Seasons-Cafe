using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedCup : PickableScript {
    public Order order;
    private Order currentCupSelectedOrder;

    new private void OnMouseOver() {
        base.OnMouseOver();

        if (PlayerHand.Instance.CurrentSelectedObject && !PlayerHand.Instance.IsPlayerHoldingSomething) {
            currentCupSelectedOrder = PlayerHand.Instance.CurrentSelectedObject.GetComponent<FinishedCup>().order;
            InfoTextsManager.Instance.ShowIngredients(currentCupSelectedOrder.GetCompleteOrder());
        }
    }

    new private void OnMouseExit() {
        base.OnMouseExit();

        InfoTextsManager.Instance.HideIngredients(
            currentCupSelectedOrder.GetCompleteOrder()
        );
    }
}
