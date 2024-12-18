using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private KitchenCounter kitchenCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.counter == kitchenCounter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject obj in visualGameObjectArray)
        {
            obj.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject obj in visualGameObjectArray)
        {
            obj.SetActive(false);
        }
    }
}
