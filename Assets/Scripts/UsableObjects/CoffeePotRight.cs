using UnityEngine;

public class CoffeePotRight : UsableScript {
    private GameObject child;
    static public bool IsNotFilled { get; private set; } = true;
    public static CoffeePotRight Instance { get; private set; }

    override public void OnInteract(GameObject holdingObject) {
        FillPot(holdingObject);
    }
    private void Awake() {
        Instance = this;
    }

    new private void Start() {
        base.Start();

        child = transform.GetChild(1).gameObject;
        Debug.Log(child);
        if (IsNotFilled) {
            child.SetActive(false);
        }
    }

    private void FillPot(GameObject holdingObject) {
        if (IsNotFilled) {
            switch (holdingObject.name) {
                default:
                InfoTextsManager.Instance.ShowMessage(MessageConsts.MissingWaterJar);
                return;
                case ConstUtils.WaterJar:
                IsNotFilled = false;
                break;
            }
            child.SetActive(true);
        }
    }

    public void ResetPot() {
        child.SetActive(false);
        IsNotFilled = true;
    }
}
