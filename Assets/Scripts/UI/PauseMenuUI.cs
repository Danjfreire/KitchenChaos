using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenu);
        });

        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePause();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnPauseToggled += GameManager_OnPauseToggled;
        gameObject.SetActive(false);
    }

    private void GameManager_OnPauseToggled(object sender, GameManager.PauseToggledEventArgs e)
    {
        gameObject.SetActive(e.isPaused);
    }
}
