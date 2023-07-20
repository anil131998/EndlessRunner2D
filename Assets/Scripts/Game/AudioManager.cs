using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance == null || Instance == this)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] private AudioSource runningAudio;
    [SerializeField] private AudioSource HitSound;
    [SerializeField] private AudioSource JumpSound;
    [SerializeField] private AudioSource MonsterAttack;

    private void Start()
    {
        PlayRunningAudio();
    }

    public void PlayRunningAudio()
    {
        runningAudio.Play();
    }
    public void StopRunningAudio()
    {
        runningAudio.Stop();
    }

    public void StartJumping()
    {
        StopRunningAudio();
        JumpSound.Play();
    }

    public void PlayerHit()
    {
        HitSound.Play();
    }

    public void EnemyAttack()
    {
        MonsterAttack.Play();
    }

}
