using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    // volume options
    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;

    // key bindings options
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;

    // gamepad options
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAltButton;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;

    // menu callback
    private Action onCloseButtonAction;

    [SerializeField] private Button closeButton;
    private void Awake()
    {
        Instance = this;

        // sfx options
        sfxButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateOptionsText();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateOptionsText();
        });

        closeButton.onClick.AddListener(() => {
            Hide();
        });

        // keybindings
        moveUpButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Up, moveUpText);
        });

        moveDownButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Down, moveDownText);
        });

        moveLeftButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Left, moveLeftText);
        });

        moveRightButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Right, moveRightText);
        });

        interactButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Interact, interactText);
        });

        interactAltButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.InteractAlternate, interactAltText);
        });

        // gamepad bindings
        gamepadInteractButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Gamepad_Interact, gamepadInteractText);
        });

        interactAltButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Gamepad_InteractAlternate, gamepadInteractAltText);
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
        Hide();
    }

    private void UpdateOptionsText()
    {
        sfxText.text = "Sound Effects : " + Mathf.Round(SoundManager.Instance.GetVolume() * 10);
        musicText.text = "Music : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
    }

    private void RebindBinding(GameInput.Binding binding, TextMeshProUGUI btnTxt)
    {
        btnTxt.text = "";
        GameInput.Instance.RebindBinding(binding, () => {
            UpdateOptionsText();
        });
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        sfxButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        if (onCloseButtonAction != null) {
            onCloseButtonAction();
        }
    }
}
