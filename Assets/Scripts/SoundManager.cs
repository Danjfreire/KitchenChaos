using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PREFS_SFX_VOLUME = "SfxVolume";

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefs;
    private float volume = 1f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PREFS_SFX_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailure += DeliveryManager_OnDeliveryFailure;
        Player.Instance.OnObjectPickedUp += Player_OnObjectPickedUp;
        
        // static events
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        KitchenCounter.OnAnyObjectPlaced += KitchenCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter counter = sender as TrashCounter;
        PlaySound(audioClipRefs.trash, counter.transform.position);
    }

    private void KitchenCounter_OnAnyObjectPlaced(object sender, System.EventArgs e)
    {
        KitchenCounter counter = sender as KitchenCounter;
        PlaySound(audioClipRefs.drop, counter.transform.position);
    }

    private void Player_OnObjectPickedUp(object sender, System.EventArgs e)
    {
        Vector3 position = Player.Instance.transform.position;
        PlaySound(audioClipRefs.pickUp, position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefs.chop, cuttingCounter.transform.position);
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

    private void PlaySound(AudioClip[] audioclipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        int idx = Random.Range(0, audioclipArray.Length);
        AudioSource.PlayClipAtPoint(audioclipArray[idx], position, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip audioclip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioclip, position, volumeMultiplier * volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipRefs.footstep, position, volumeMultiplier * volume);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f) {
            volume = 0;
        }

        PlayerPrefs.SetFloat(PREFS_SFX_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
