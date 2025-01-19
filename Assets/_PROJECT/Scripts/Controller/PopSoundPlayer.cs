using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopSoundPlayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip pop;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if(!src.isPlaying)src.PlayOneShot(pop,0.35f);
                
            }
        }
    }
}
