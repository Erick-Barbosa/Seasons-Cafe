using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHand : MonoBehaviour {
    private InputSystem inputSystem;
    public static PlayerHand Instance { get; private set; }
    public GameObject HoldingObject { get; private set; }
    public GameObject CurrentSelectedObject { get; private set; }
    public GameObject CurrentSelectedObjectArrow { get; private set; }

    public bool IsPlayerHoldingSomething { get; private set; }

    public Vector3 selectedObjectInitialPosition;

    private int sortingAboveAll = 30;
    private int selectedObjectStartSorting;

    private void Awake() {
        Instance = this;

        IsPlayerHoldingSomething = false;

        inputSystem = new InputSystem();

        inputSystem.Enable();

        inputSystem.Player.Click.performed += Click_performed;
    }

    private void Update() {
        if (IsPlayerHoldingSomething) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            HoldingObject.transform.position = mousePosition;
        }
    }

    private void Click_performed(InputAction.CallbackContext obj) {
        if (CurrentSelectedObject && !IsPlayerHoldingSomething &&
            CurrentSelectedObject.CompareTag(TagUtils.PICKABLE_TAG)) {
            HoldPickableObject();
        }
        else if (CurrentSelectedObject && IsPlayerHoldingSomething &&
            CurrentSelectedObject.CompareTag(TagUtils.USABLE_TAG)) {
            ActOnUsableObject();
        }
        else if (IsPlayerHoldingSomething) {
            DropPickableObject();
        }
    }

    // Actions
    private void HoldPickableObject() {
        IsPlayerHoldingSomething = true;
        selectedObjectInitialPosition = CurrentSelectedObject.transform.position;
        selectedObjectStartSorting = CurrentSelectedObject.GetComponent<SpriteRenderer>().sortingOrder;
        HoldingObject = CurrentSelectedObject;

        UnsetCurrentSelectedGameObject();
        HoldingObject.GetComponent<SpriteRenderer>().sortingOrder = sortingAboveAll;
        HoldingObject.GetComponent<PickableScript>().OnInteract();
    }
    public void DropPickableObject() {
        HoldingObject.GetComponent<SpriteRenderer>().sortingOrder = selectedObjectStartSorting;

        HoldingObject.transform.position = Vector3.Lerp(HoldingObject.transform.position, selectedObjectInitialPosition, 1);

        IsPlayerHoldingSomething = false;

        HoldingObject.GetComponent<PickableScript>().OnDrop();
        HoldingObject = null;
        UnsetCurrentSelectedGameObject();
        
    }
    private void ActOnUsableObject() {
        CurrentSelectedObject.GetComponent<UsableScript>().OnInteract(HoldingObject);
    }
    private IEnumerator ReturnObjectToInitialPosition() {
        float value = 0.125f;
        while (value <= 1) {
            yield return new WaitForSeconds(0.5f);
            HoldingObject.transform.position = Vector3.Lerp(HoldingObject.transform.position, selectedObjectInitialPosition, value);
            value += value;
        }
    }

    // Setters
    public void SetCurrentSelectedGameObject(GameObject interactiveObjectUnderPlayersHand, GameObject arrowOfinteractiveObjectUnderPlayersHand, bool isPickable) {
        CurrentSelectedObject = interactiveObjectUnderPlayersHand;
        CurrentSelectedObjectArrow = arrowOfinteractiveObjectUnderPlayersHand;

        SetSelectingArrowVisibility(arrowOfinteractiveObjectUnderPlayersHand);
        string name;
        if (isPickable) {
            name = CurrentSelectedObject.GetComponent<PickableScript>().DisplayName;
        }
        else {
            name = CurrentSelectedObject.GetComponent<UsableScript>().DisplayName;
        }

        InfoTextsManager.Instance.ShowSelectedObjectName(name);
    }
    public void UnsetCurrentSelectedGameObject() {
        if (CurrentSelectedObject)
            CurrentSelectedObjectArrow.gameObject.SetActive(false);

        CurrentSelectedObject = null;
        CurrentSelectedObjectArrow = null;

        InfoTextsManager.Instance.ShowSelectedObjectName(string.Empty);
    }

    private void SetSelectingArrowVisibility(GameObject arrowFromGameObject) {
        if (arrowFromGameObject) {
            if (ShouldHandleUsableArrow(arrowFromGameObject)) {
                arrowFromGameObject.SetActive(true);
            }
            else if (ShouldHandlePickableArrow(arrowFromGameObject)) {
                arrowFromGameObject.SetActive(true);
            }
        }
    }

    // Conditions
    private bool ShouldHandleUsableArrow(GameObject arrowFromGameObject) {
        return arrowFromGameObject.gameObject.transform.parent.tag == TagUtils.USABLE_TAG && IsPlayerHoldingSomething;
    }

    private bool ShouldHandlePickableArrow(GameObject arrowFromGameObject) {
        return arrowFromGameObject.gameObject.transform.parent.tag == TagUtils.PICKABLE_TAG && !IsPlayerHoldingSomething;
    }
}
