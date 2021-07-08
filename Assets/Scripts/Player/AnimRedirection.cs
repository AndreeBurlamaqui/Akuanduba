using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRedirection : MonoBehaviour
{

    public PlayerController playerParent;
    public PlayerAudio audioPlayer;

    public void Footsteps()
    {
        playerParent.ApplyFootstep();
        audioPlayer.FootstepsSFX();
    }

    public void FinishLedgeJump()
    {
        playerParent.FinishLedgeClimb();
    }

    public void Jump()
    {
        audioPlayer.JumpSFX();
    }
}
