using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {
    [SerializeField] private string scene;
    private Button changeSceneButton;
    private CafeSceneManager cafeSceneManager;

    private void Start() {
        changeSceneButton = gameObject.GetComponent<Button>();

        changeSceneButton.onClick.AddListener(() => ChangeToScene(scene));

        transform.GetChild(0).gameObject.SetActive(false);

        if (gameObject.name == ConstUtils.SceneButtonPreText + SceneManager.GetActiveScene().name + ConstUtils.SceneButtonPosText) {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void ChangeToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
