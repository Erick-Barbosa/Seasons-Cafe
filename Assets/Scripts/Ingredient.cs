using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient {

    public Ingredient(string ingredientName, int ingredientAmount) {
        Name = InfoTextsManager.Instance.GetDisplayNameFromObjectName(ingredientName);
        Amount = ingredientAmount;
        Icon = InfoTextsManager.Instance.GetObjectIcon(ingredientName);
    }
    private int maxIngredientAmount = ConstUtils.MaxIngredientAmount;
    public Image IngredientNotFoundIcon { get; private set; }
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public int Amount { get; private set; }
    public bool IncreaseIngredient(int amountToIncrease) {
        if (Amount+amountToIncrease <= maxIngredientAmount) {
            Amount += amountToIncrease;
            return true;
        } else {
            InfoTextsManager.Instance.ShowMessage(MessageConsts.MaxAmountReached);
            return false;
        }
    }
    public override string ToString() {
        return Name + " - " + Amount + "/ " + Icon.name;
    }

    public override bool Equals(object obj) {
        if ((obj == null) || !GetType().Equals(obj.GetType())) {
            return false;
        }
        else {
            Ingredient comingIngredient = (Ingredient)obj;
            return (Name == comingIngredient.Name) &&
                (Amount == comingIngredient.Amount);
        }
    }

    public override int GetHashCode() {
        return HashCode.Combine(maxIngredientAmount, IngredientNotFoundIcon, Name, Icon, Amount);
    }
}
