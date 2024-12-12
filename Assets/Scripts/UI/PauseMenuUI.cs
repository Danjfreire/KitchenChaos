using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenu);
        });

        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePause();
        });

        optionsButton.onClick.AddListener(() => {
            Hide();
            OptionsUI.Instance.Show(() => {
                Show();
            });
        });
    }

    private void Start()
    {
        GameManager.Instance.OnPauseToggled += GameManager_OnPauseToggled;
        Hide();
    }

    private void GameManager_OnPauseToggled(object sender, GameManager.PauseToggledEventArgs e)
    {
        if (e.isPaused) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
