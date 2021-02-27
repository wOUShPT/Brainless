using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsBehaviour : MonoBehaviour
{
    [SerializeField]private GameObject mainBackground;
    [SerializeField] private GameObject optionsBackground;
    [SerializeField] private GameObject creditsBackground;
    // Start is called before the first frame update
    void Start()
    {
        if(mainBackground != null)
        {
            BackToMainBackground();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        // Exit
        Application.Quit();
    }
    public void BackToMainBackground()
    {
        mainBackground.SetActive(true);
        optionsBackground.SetActive(false);
        creditsBackground.SetActive(false);
    }
    public void ToOptionBackground()
    {
        mainBackground.SetActive(false);
        optionsBackground.SetActive(true);
    }
    public void ToCreditsBackground()
    {
        mainBackground.SetActive(false);
        creditsBackground.SetActive(true);
    }
    public void FullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        Debug.Log("Fullscreen is" + isFullscreen);
    }
}
