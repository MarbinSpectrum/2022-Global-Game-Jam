using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public static Ending Instance;

    [SerializeField] private Image endImg;
    [SerializeField] private GameObject endAnimation;

    private float point = 0;
    private int ans = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void EndEvent()
    {
        if (endAnimation.activeSelf)
            return;
        point += 6;
        ans += 2;
        endImg.color = new Color(1, 1, 1, (float)point / 255f);
        if(ans > 70)
        {
            endAnimation.SetActive(true);
        }
    }


}
