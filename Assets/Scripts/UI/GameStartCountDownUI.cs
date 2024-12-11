using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;
        gameObject.SetActive(false);
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountingDownToStart()) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        countDownText.text = Mathf.Ceil(GameManager.Instance.GetStartCountDown()).ToString();
    }
}
