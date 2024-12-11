using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    private Player player;
    private float footstepTimer;
    private float footstepDelay = 0.3f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer += Time.deltaTime;

        if (footstepTimer >= footstepDelay) {
            footstepTimer = 0;

            if (player.IsWalking()) {
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, 1f);
            }
        }
        
    }

}
