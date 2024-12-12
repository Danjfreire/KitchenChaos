using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        Instance = this;

        sfxButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateOptionsText();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateOptionsText();
        });

        closeButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });

    }

    private void Start()
    {
        GameManager.Instance.OnPauseToggled += GameManager_OnPauseToggled;

        gameObject.SetActive(false);
        UpdateOptionsText();
    }

    private void GameManager_OnPauseToggled(object sender, GameManager.PauseToggledEventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void UpdateOptionsText()
    {
        sfxText.text = "Sound Effects : " + Mathf.Round(SoundManager.Instance.GetVolume() * 10);
        musicText.text = "Music : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
