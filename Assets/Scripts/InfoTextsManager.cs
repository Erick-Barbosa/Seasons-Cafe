using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InfoTextsManager : MonoBehaviour {
    public static InfoTextsManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI objectNameText;
    [SerializeField] private SpriteRenderer objectIcon;

    private Color actualColor;
    private Color startingColor;
    private int startingFadeTime = 4;

    [SerializeField] private List<Sprite> iconsList;
    [SerializeField] private List<TextMeshProUGUI> amountTextList;
    [SerializeField] private List<SpriteRenderer> iconsHolderList;
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        Instance = this;

        if (warningText != null) {
            warningText.enabled = false;
            startingColor = warningText.color;
        }
        if (objectNameText != null) {
            objectNameText.enabled = false;
        }

        foreach (var item in iconsHolderList) {
            item.enabled = false;
        }
        foreach (var item in amountTextList) {
            item.enabled = false;
        }

        ShowIngredients(Cup.IngredientsUsedList);
    }

    public void ShowMessage(string message) {
        warningText.enabled = true;
        warningText.text = message;

        actualColor = startingColor;
        StopAllCoroutines();
        StartCoroutine(HandleFade());
    }

    private IEnumerator HandleFade() {
        float fadeTime = startingFadeTime;
        while (fadeTime >= 0) {
            actualColor.a = actualColor.a * (fadeTime / startingFadeTime);
            warningText.color = actualColor;
            fadeTime--;
            yield return new WaitForSeconds(1);
            
        }
        warningText.enabled = false;
    }

    public void ShowSelectedObjectName(string objectName) {
        if (objectNameText != null) {
            if (objectName != string.Empty) {
                objectNameText.text = objectName;
                objectNameText.enabled = true;
            }
            else {
                objectNameText.enabled = false;
            }
        }
    }

    public string GetDisplayNameFromObjectName(string objectName) {
        switch (objectName) {
            case ConstUtils.SugarPouch:
            return ConstUtils.DisplaySugarPouch;
            case ConstUtils.WaterJar:
            return ConstUtils.DisplayWaterJar;
            case ConstUtils.MilkMachine:
            return ConstUtils.DisplayMilkMachine;
            case ConstUtils.CoffeeMachine:
            return ConstUtils.DisplayCoffeeMachine;
            case ConstUtils.CoffeeMachinePotLeft:
            return ConstUtils.DisplayCoffeeMachinePotLeft;
            case ConstUtils.CoffeeMachinePotRight:
            return ConstUtils.DisplayCoffeeMachinePotRight;
            case ConstUtils.CoffeePot1:
            return ConstUtils.DisplayCoffeePot1;
            case ConstUtils.CoffeePot2:
            return ConstUtils.DisplayCoffeePot2;
            case ConstUtils.CoffeePot3:
            return ConstUtils.DisplayCoffeePot3;
            case ConstUtils.CoffeePot4:
            return ConstUtils.DisplayCoffeePot4;
            case ConstUtils.CoffeePot5:
            return ConstUtils.DisplayCoffeePot5;
            default:
            return objectName;
        }
    }
    public Sprite GetObjectIcon(string ingredientName) {
        switch (ingredientName) {
            default:
            return iconsList.ElementAt(7);
            case ConstUtils.CoffeePot1:
            ;
            return iconsList.ElementAt(0);
            case ConstUtils.CoffeePot2:
            ;
            return iconsList.ElementAt(1);
            case ConstUtils.CoffeePot3:
            ;
            return iconsList.ElementAt(2);
            case ConstUtils.CoffeePot4:
            ;
            return iconsList.ElementAt(3);
            case ConstUtils.CoffeePot5:
            ;
            return iconsList.ElementAt(4);
            case ConstUtils.SugarPouch:
            ;
            return iconsList.ElementAt(5);
            case ConstUtils.MilkMachine:
            ;
            return iconsList.ElementAt(6);
        }
    }
    public void ShowIngredients(List<Ingredient> ingredientsUsedList) {
        int start = 0;
        foreach (Ingredient ingredient in ingredientsUsedList) {
            if (ingredient.Amount > 0) {
                amountTextList[start].text = ingredient.Amount.ToString();
                amountTextList[start].enabled = true;
                iconsHolderList[start].sprite = ingredient.Icon;
                iconsHolderList[start].enabled = true;
            }

            start++;
        }
    }

    public void HideIngredients(List<Ingredient> ingredientsUsedList) {
        int start = 0;
        foreach (Ingredient ingredient in ingredientsUsedList) {
            amountTextList[start].text = "";
            amountTextList[start].enabled = false;
            iconsHolderList[start].sprite = null;
            iconsHolderList[start].enabled = false;
            start++;
        }
    }
}
