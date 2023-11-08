using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        ApplicationModel.isLoaded = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        ApplicationModel.isLoaded = 1;

        SceneManager.LoadScene(ApplicationModel.locationName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
