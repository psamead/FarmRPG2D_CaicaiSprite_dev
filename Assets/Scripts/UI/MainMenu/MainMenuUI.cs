using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button loadButton = null;
    [SerializeField] private Button quitButton = null;
    [SerializeField] private GameObject videoPanel = null;
    [SerializeField] private VideoPlayer introVideo = null;
    [SerializeField] private Image backgroundImage = null;
    
    private bool isIntroPlaying = false;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        loadButton.onClick.AddListener(OpenLoadGameMenu);
        quitButton.onClick.AddListener(QuitGame);
        
        videoPanel.SetActive(false);
        introVideo.loopPointReached += OnVideoEnd;
    }

    private void Update()
    {
        if (isIntroPlaying)
        {
            // Skip video on any key press
            if (Input.anyKeyDown)
            {
                LoadFarmScene();
            }
        }
    }

    private void StartGame()
    {
        // Hide Main Menu UI
        startButton.gameObject.SetActive(false);
        loadButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        if (backgroundImage != null) backgroundImage.gameObject.SetActive(false);

        // Play Intro Video
        if (introVideo != null && introVideo.clip != null)
        {
            isIntroPlaying = true;
            videoPanel.SetActive(true);
            introVideo.Play();
        }
        else
        {
            // If no video assigned, jump straight to game
            LoadFarmScene();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        LoadFarmScene();
    }

    private void LoadFarmScene()
    {
        isIntroPlaying = false;
        SceneControllerManager.Instance.FadeAndLoadScene(SceneName.Scene1_Farm.ToString(), new Vector3(2.6f, 4.6f, 0f));
    }

    private void OpenLoadGameMenu()
    {
        UIManager.Instance.OpenLoadGameMenu();
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
