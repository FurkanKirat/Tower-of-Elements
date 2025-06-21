using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{

    public void StartButton()
    {
        LoadTargetScene("GameMenuScene");
    }

    public void CreditsButton()
    {
        //LoadTargetScene("CreditsScene");
    }

    public void LoadTargetScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
}