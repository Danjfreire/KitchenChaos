using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    private void Start()
    {
        cuttingCounter.OnCuttingProgressChanged += CuttingCounter_OnCuttingProgressChanged;
        barImage.fillAmount = 0f;

        gameObject.SetActive(false);
    }

    private void CuttingCounter_OnCuttingProgressChanged(object sender, CuttingCounter.OnCuttingProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.normalizedProgress;

        if (e.normalizedProgress == 0f || e.normalizedProgress == 1f) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }


}
