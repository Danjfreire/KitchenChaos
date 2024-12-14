using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{

    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    private float displayDurationMax = 2f;
    private float currentDisplayDuration = 2f;
    private bool isShowing = false;

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailure += DeliveryManager_OnDeliveryFailure;

        Hide();
    }

    private void Update()
    {
        if (isShowing) {
            currentDisplayDuration -= Time.deltaTime;
            if (currentDisplayDuration <= 0) {
                currentDisplayDuration = displayDurationMax;
                Hide();
            }
        }   
    }

    private void DeliveryManager_OnDeliveryFailure(object sender, System.EventArgs e)
    {
        background.color = failedColor;
        icon.sprite = failedSprite;
        messageText.text = "DELIVERY\nFAILED";
        Show();
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        background.color = successColor;
        icon.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        isShowing = false;
    }

    private void Show()
    {
        gameObject.SetActive(true);
        isShowing = true;
        currentDisplayDuration = displayDurationMax;
    }
}
