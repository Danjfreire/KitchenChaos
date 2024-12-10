using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefs;

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailure += DeliveryManager_OnDeliveryFailure;
    }

    private void DeliveryManager_OnDeliveryFailure(object sender, System.EventArgs e)
    {
        Vector3 position = DeliveryCounter.Instance.transform.position;
        PlaySound(audioClipRefs.deliveryFail, position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        Vector3 position = DeliveryCounter.Instance.transform.position;
        PlaySound(audioClipRefs.deliverySuccess, position);
    }

    private void PlaySound(AudioClip[] audioclipArray, Vector3 position, float volume = 1f)
    {
        int idx = Random.Range(0, audioclipArray.Length);
        AudioSource.PlayClipAtPoint(audioclipArray[idx], position, volume);
    }

    private void PlaySound(AudioClip audioclip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioclip, position, volume);
    }
}
