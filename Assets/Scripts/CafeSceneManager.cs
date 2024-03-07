using UnityEngine;
using UnityEngine.SceneManagement;

public class CafeSceneManager : MonoBehaviour {
    private Scene currentScene;
    private GameObject currentSceneButton;

    private void Start() {
        currentScene = SceneManager.GetActiveScene();
        currentSceneButton = transform.Find(ConstUtils.SceneButtonPreText + currentScene.name + ConstUtils.SceneButtonPosText).gameObject;
    }
}
