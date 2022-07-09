using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disabler : MonoBehaviour
{
    void MyOnDisableSVG()
    {
        SVGImage image = GetComponent<SVGImage>();
        image.CrossFadeAlpha(0, 0.5f, false);
    }

    void MyOnDisable()
    {
        Image image = GetComponent<Image>();
        image.CrossFadeAlpha(0, 0.5f, false);
    }
}
