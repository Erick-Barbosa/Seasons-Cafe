using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Order {
    public List<Ingredient> completeOrder;
    public Ingredient ingredient1 { get; private set; }
    public Ingredient ingredient2 { get; protected set; }
    public Ingredient ingredient3 { get; private set; }

    public Order(List<Ingredient> completeOrder) {
        this.completeOrder = completeOrder;

        ingredient1 = completeOrder.Find(ingredient => ingredient.Name.Contains(ConstUtils.Coffee));
        ingredient2 = completeOrder.Find(ingredient => ingredient.Name.Contains(ConstUtils.DisplayMilkMachine));
        if (ingredient2 == null)
            ingredient2 = new Ingredient(ConstUtils.DisplayMilkMachine, 0);
        ingredient3 = completeOrder.Find(ingredient => ingredient.Name.Contains(ConstUtils.DisplaySugarPouch));
        if (ingredient3 == null)
            ingredient3 = new Ingredient(ConstUtils.DisplaySugarPouch, 0);
    }

    public override string ToString() {
        return "Ingredient 1 = " + ingredient1 + " / " +
            "Ingredient 2 = " + ingredient2 + " / " +
            "Ingredient 3 = " + ingredient3;
    }

    public List<Ingredient> GetCompleteOrder() {
        List<Ingredient> actualIngredientsList = new List<Ingredient>();
        if (ingredient1 != null) {
            actualIngredientsList.Add(ingredient1);
        }
        
        if (ingredient2 != null) {
            actualIngredientsList.Add(ingredient2);
        }

        if (ingredient3 != null) {
            actualIngredientsList.Add(ingredient3);
        }

        return actualIngredientsList;
    }

    public override bool Equals(object obj) {
        if ((obj == null) || !GetType().Equals(obj.GetType())) {
            return false;
        } 
        else {
            Order comingOrder = (Order)obj;
            if (!(comingOrder.ingredient1.Name == ingredient1.Name)) {
                return false;
            }
            if (!(comingOrder.ingredient1.Amount == ingredient1.Amount)) {
                return false;
            }
            if (!(comingOrder.ingredient2.Amount == ingredient2.Amount)) {
                return false;
            }
            if (!(comingOrder.ingredient3.Amount == ingredient3.Amount)) {
                return false;
            }

            return true;
        }
    }

    public override int GetHashCode() {
        return System.HashCode.Combine(completeOrder, ingredient1, ingredient2, ingredient3);
    }
}
