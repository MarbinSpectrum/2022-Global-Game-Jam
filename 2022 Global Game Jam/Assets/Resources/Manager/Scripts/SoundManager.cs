using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum SoundEvent
{
    Awake,
    Start,
    OnEnable
}
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
    const int MAX_BGM_SOURCE = 3;
    [SerializeField] AudioSource[] bgmSource = new AudioSource[MAX_BGM_SOURCE];
    IEnumerator corEvent;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static float GetBGMValue()
    {
        return Instance.bgmVolume;
    }
    public static void SetBGMValue(float value,int slot = -1)
    {
        if(slot == -1)
        {
            for (int i = 0; i < MAX_BGM_SOURCE; i++)
            {
                Instance.bgmSource[i].volume = value;
            }
        }
        else
        {
            Instance.bgmSource[slot].volume = value;
        }
    }

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
    /// : 배경음 슬롯에 할당
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static void SetBGM(string bgmName, int slot = 0)
    {
        Instance.setBGM(bgmName, slot);
    }
    private void setBGM(string bgmName, int slot)
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

        bgmSource[slot].clip = bgmData;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : 배경음 출력 메소드
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void PlayBGM(int slot= 0)
    {
        Instance.playBGM(slot);
    }
    private void playBGM(int slot)
    {
        bgmSource[slot].Play();
    }

    public static void OnlyPlayBGM(int slot,float changeTime)
    {
        Instance.onlyPlayBGM(slot, changeTime);
    }

    private void onlyPlayBGM(int slot, float changeTime)
    {
        if(corEvent != null)
        {
            StopCoroutine(corEvent);
        }
        corEvent = ChangeBGM(slot, changeTime);
        StartCoroutine(corEvent);
    }

    IEnumerator ChangeBGM(int slot, float changeTime)
    {
        if(changeTime > 0)
        {
            float v = bgmVolume / (changeTime * 10);

            for (float f = changeTime; f > 0; f -= 0.1f)
            {
                for (int i = 0; i < MAX_BGM_SOURCE; i++)
                {
                    float volume = bgmSource[i].volume;
                    if (slot == i)
                    {
                        volume = Mathf.Min(volume + v, bgmVolume);
                        bgmSource[i].volume = volume;
                    }
                    else
                    {
                        volume = Mathf.Max(volume - v, 0);
                        bgmSource[i].volume = volume;
                    }
                }
                yield return new WaitForSeconds(.1f);
            }
        }

        for (int i = 0; i < MAX_BGM_SOURCE; i++)
        {
            float volume = bgmSource[i].volume;
            if (slot == i)
            {
                bgmSource[i].volume = bgmVolume;
            }
            else
            {
                bgmSource[i].volume = 0;
            }
        }
    }

    public static void StopBGM(int slot = 0)
    {
        Instance.stopBGM(slot);
    }
    private void stopBGM(int slot)
    {
        bgmSource[slot].Stop();
    }

    public static void StopAllBGM()
    {
        Instance.stopAllBGM();
    }
    private void stopAllBGM()
    {
        for (int i = 0; i < MAX_BGM_SOURCE; i++)
            bgmSource[i].Stop();
    }
}
