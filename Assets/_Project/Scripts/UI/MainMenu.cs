using UnityEngine;
using UnityEngine.SceneManagement;

// Attached to a GameObject in MenuScene; the Play button's OnClick calls PlayGame() to load the gameplay scene
public class MainMenu : MonoBehaviour
{
    public string gameplaySceneName = "SmallIslandScene";

    public void PlayGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }
}
