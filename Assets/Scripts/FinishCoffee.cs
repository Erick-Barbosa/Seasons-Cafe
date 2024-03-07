using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishCoffee : MonoBehaviour {
    private Button finishButton;
    private TextMeshProUGUI finishButtonText;
    private Color finishButtonTextColor;
    private void Start() {
        finishButton = gameObject.GetComponent<Button>();
        finishButton.onClick.AddListener(FinishCoffeeCup);

        finishButtonText = finishButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        finishButtonText.text = ConstUtils.ButtonText;

        finishButtonTextColor = finishButtonText.color;
    }

    private void FinishCoffeeCup() {
        StopCoroutine(ButtonTextResponse());
        StartCoroutine(ButtonTextResponse());
    }

    private IEnumerator ButtonTextResponse() {
        if (Cup.Instance.MakeCoffeeCup()) {
            finishButtonText.color = Color.yellow;
            finishButtonText.text = ConstUtils.ButtonTextFinished;
            yield return new WaitForSeconds(0.3f);
            finishButtonText.color = finishButtonTextColor;
            finishButtonText.text = ConstUtils.ButtonText;
        }
    }
}
