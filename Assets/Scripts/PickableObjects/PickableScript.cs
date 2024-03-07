using UnityEngine;

public class PickableScript : MonoBehaviour {
    protected GameObject selectingArrow;
    public string DisplayName { get; private set; }

    public void Start() {
        selectingArrow = gameObject.transform.GetChild(0).gameObject;

        selectingArrow?.SetActive(false);

        DisplayName = InfoTextsManager.Instance.GetDisplayNameFromObjectName(gameObject.name);
    }

    public virtual void OnInteract() {
        SoundsHolder.clipList[0].volume = 0.1f;
        SoundsHolder.clipList[0].Play();
    }
    public virtual void OnDrop() {
        SoundsHolder.clipList[1].volume = 0.1f;
        SoundsHolder.clipList[1].Play();
    }

    public void OnMouseOver() {
        if (!PlayerHand.Instance.IsPlayerHoldingSomething) {
            PlayerHand.Instance.SetCurrentSelectedGameObject(gameObject, selectingArrow, true);
        }
    }

    public void OnMouseExit() {
        PlayerHand.Instance.UnsetCurrentSelectedGameObject();
    }
}
