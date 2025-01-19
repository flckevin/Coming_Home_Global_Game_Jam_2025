using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public RawImage scrollerTargetImage;    //target to scroll
    public Vector2 scrollPos;               //scroll position

    // Update is called once per frame
    void Update()
    {
        //scroll image
        scrollerTargetImage.uvRect = new Rect(scrollerTargetImage.uvRect.position + scrollPos * Time.deltaTime,
                                                scrollerTargetImage.uvRect.size);
    }
}
