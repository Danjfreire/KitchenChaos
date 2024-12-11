using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{

    [SerializeField] private Image clockTimer;

    private void Update()
    {
        clockTimer.fillAmount = GameManager.Instance.GetPlayTimeNormalized();
    }

}
