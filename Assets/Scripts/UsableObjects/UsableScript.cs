using UnityEngine;

public class UsableScript : MonoBehaviour
{
    protected GameObject selectingArrow;
    public string DisplayName { get; private set; }
    public bool HasFirst = false;
    public void Start() {
        selectingArrow = gameObject.transform.GetChild(0).gameObject;

        selectingArrow?.SetActive(false);

        selectingArrow.GetComponent<Renderer>().material.color = ConstUtils.UsableArrowColor;

        DisplayName = InfoTextsManager.Instance.GetDisplayNameFromObjectName(gameObject.name);
    }
    public virtual void OnInteract(GameObject holdingObject) {
        Debug.Log("On Interact"); 
    }

    private void OnTriggerStay2D(Collider2D collision) {
        PlayerHand.Instance.SetCurrentSelectedGameObject(gameObject, selectingArrow, false);
    }
    private void OnTriggerExit2D(Collider2D collision) {
        PlayerHand.Instance.UnsetCurrentSelectedGameObject();
    }
}
