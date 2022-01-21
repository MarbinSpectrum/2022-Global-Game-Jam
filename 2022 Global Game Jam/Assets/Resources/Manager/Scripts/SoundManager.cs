using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SoundManager : SerializedMonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                //NULL값인 경우 데이터를 매니저를 가져온다.
                GameObject soundManagerObj = Instantiate(Resources.Load("Manager/SoundManager") as GameObject);
                instance = soundManagerObj.GetComponent<SoundManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    [SerializeField] private Dictionary<string, AudioClip> sounds;
    [SerializeField] [Range(0, 1)] private float seVolume;
    [SerializeField] [Range(0, 1)] private float bgmVolume;
    [SerializeField] AudioSource seSource;
    [SerializeField] AudioSource bgmSource;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : 효과음 출력 메소드
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void PlaySE(string seName)
    {
        Instance.playSE(seName);
    }
    private void playSE(string seName)
    {
        if (sounds.ContainsKey(seName) == false)
        {
            //KEY에 해당하는 사운드가 없다.
            //사운드를 추가하거나 KEY값이 잘못됬다는 이야기 
            Debug.LogError(seName + "에 해당하는 사운드가 없습니다.");
            return;
        }

        AudioClip seData = sounds[seName];
        if (seData == null)
        {
            //KEY에 해당하는 값이 null이다.
            Debug.LogError(seName + "에 해당하는 값이 NULL입니다.");
            return;
        }

        seSource.volume = seVolume;
        seSource.PlayOneShot(seData);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : 배경음 출력 메소드
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void PlayBGM(string bgmName)
    {
        Instance.playBGM(bgmName);
    }
    private void playBGM(string bgmName)
    {
        if (sounds.ContainsKey(bgmName) == false)
        {
            //KEY에 해당하는 사운드가 없다.
            //사운드를 추가하거나 KEY값이 잘못됬다는 이야기 
            Debug.LogError(bgmName + "에 해당하는 사운드가 없습니다.");
            return;
        }

        AudioClip bgmData = sounds[bgmName];
        if (bgmData == null)
        {
            //KEY에 해당하는 값이 null이다.
            Debug.LogError(bgmName + "에 해당하는 값이 NULL입니다.");
            return;
        }

        bgmSource.volume = bgmVolume;
        bgmSource.clip = bgmData;
        bgmSource.Play();
    }
}
