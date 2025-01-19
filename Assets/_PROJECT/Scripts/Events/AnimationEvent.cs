using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public ParticleSystem _particle;
    public void PlayParticle()
    {
        _particle.Play();
    }
}
