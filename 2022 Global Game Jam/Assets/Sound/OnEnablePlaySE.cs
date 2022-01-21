using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnablePlaySE : MonoBehaviour
{
    [SerializeField] private string seName;

    private void OnEnable() => SoundManager.PlaySE(seName);

}
