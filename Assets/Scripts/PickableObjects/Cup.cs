using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class Cup : PickableScript {
    static public List<Ingredient> IngredientsUsedList { get; private set; } = new List<Ingredient>();
    static public Cup Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public bool AddIngredient(string ingredientName, int amount, UsableScript usableScriptObject) {
        if (!usableScriptObject.HasFirst) {
            Ingredient newIngredient =
            new Ingredient(ingredientName, amount);
            IngredientsUsedList.Add(newIngredient);
            InfoTextsManager.Instance.ShowIngredients(IngredientsUsedList);
            return true;
        } else {
            return IncreaseIngredient(ingredientName, amount);
        }
    }
    public bool IncreaseIngredient(string ingredientName, int amount) {
        Ingredient ingredientToIncrease = IngredientsUsedList.Find(
            ingredient => ingredient.Name == InfoTextsManager.Instance.GetDisplayNameFromObjectName(ingredientName)
        );
        bool canIncrease = false;

        if (ingredientToIncrease != null) {
            canIncrease = ingredientToIncrease.IncreaseIngredient(amount);
            InfoTextsManager.Instance.ShowIngredients(IngredientsUsedList);
        } else {
            InfoTextsManager.Instance.ShowMessage(MessageConsts.WrongIngredient);
        }

        return canIncrease;
    }

    public bool MakeCoffeeCup() {
        Debug.Log(IngredientsUsedList.Find(ingredient => ingredient.Name.Contains(ConstUtils.Coffee)));
        if (IngredientsUsedList.Find(ingredient => ingredient.Name.Contains(ConstUtils.Coffee)) != null) {
            OrderManager.DeliverOrder(new Order(IngredientsUsedList));
            InfoTextsManager.Instance.HideIngredients(IngredientsUsedList);
            IngredientsUsedList.Clear();
            return true;
        }
        else {
            InfoTextsManager.Instance.ShowMessage(MessageConsts.NoCoffeeOnCup);
            return false;
        }
    }

    public override void OnInteract() {
        int random = Random.Range(3, 5);

        SoundsHolder.clipList[random].volume = 0.01f;
        SoundsHolder.clipList[random].Play();
    }
    public override void OnDrop() {
        SoundsHolder.clipList[2].volume = 0.01f;
        SoundsHolder.clipList[2].Play();
    }
}
