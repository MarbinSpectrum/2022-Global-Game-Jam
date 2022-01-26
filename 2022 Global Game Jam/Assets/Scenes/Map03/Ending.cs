using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public static Ending Instance;

    [SerializeField] private Image endImg;
    [SerializeField] private GameObject endAnimation;
    [SerializeField] private GameObject endSound;
    private float point = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void EndEvent()
    {
        if (endAnimation.activeSelf)
            return;
        point += 2;

        if(point == 50)
        {
            endSound.SetActive(true);
        }
        endImg.color = new Color(1, 1, 1, point / 100f);
        if(point > 70)
        {
            endAnimation.SetActive(true);
        }
    }


}
