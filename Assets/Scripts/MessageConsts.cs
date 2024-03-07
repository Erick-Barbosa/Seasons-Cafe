using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MessageConsts {
    public const string MissingWaterJar = "You need to hold a water jar";
    public const string MissingCoffeePowder = "You need to hold a coffee powder";
    public const string MissingCup = "You need to hold a cup";
    public const string MissingIngredients = "You need to fill the machine with water and coffee";
    public const string FinishTheCoffee = "You need to finish the coffee to fill up another cup";
    public static string MaxAmountReached = "You can only fill the cup with " + ConstUtils.MaxIngredientAmount.ToString() + " units of each ingredient";
    public const string WrongIngredient = "You need to use the same powder to fill the same cup";
    public const string NoCoffeeOnCup = "You need to fill the cup with coffee";
}
