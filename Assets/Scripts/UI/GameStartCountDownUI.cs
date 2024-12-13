using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    private const string ANIM_TRIG_NUMBER_POPUP = "NumberPopup";

    [SerializeField] private TextMeshProUGUI countDownText;

    private Animator animator;
    private int previousCountdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetStartCountDown());
        countDownText.text = countdownNumber.ToString();

        if (previousCountdownNumber != countdownNumber) {
            previousCountdownNumber = countdownNumber;

            animator.SetTrigger(ANIM_TRIG_NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
    }
}
