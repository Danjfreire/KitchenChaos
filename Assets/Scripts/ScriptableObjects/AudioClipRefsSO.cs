using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] deliverySuccess;
    public AudioClip[] deliveryFail;
    public AudioClip[] chop;
    public AudioClip[] footstep;
    public AudioClip[] drop;
    public AudioClip panSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
