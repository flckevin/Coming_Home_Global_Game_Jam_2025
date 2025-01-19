using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBoneBehaviour : MonoBehaviour
{   
    public AudioClip collisionSound;
    private AudioSource _src => GameManager.Instance.src;
    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if(!_src.isPlaying) _src.PlayOneShot(collisionSound,0.35f);
    }
}
