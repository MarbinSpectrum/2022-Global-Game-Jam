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
                //NULL���� ��� �����͸� �Ŵ����� �����´�.
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
    /// : ȿ���� ��� �޼ҵ�
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void PlaySE(string seName)
    {
        Instance.playSE(seName);
    }
    private void playSE(string seName)
    {
        if (sounds.ContainsKey(seName) == false)
        {
            //KEY�� �ش��ϴ� ���尡 ����.
            //���带 �߰��ϰų� KEY���� �߸���ٴ� �̾߱� 
            Debug.LogError(seName + "�� �ش��ϴ� ���尡 �����ϴ�.");
            return;
        }

        AudioClip seData = sounds[seName];
        if (seData == null)
        {
            //KEY�� �ش��ϴ� ���� null�̴�.
            Debug.LogError(seName + "�� �ش��ϴ� ���� NULL�Դϴ�.");
            return;
        }

        seSource.volume = seVolume;
        seSource.PlayOneShot(seData);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : ����� ��� �޼ҵ�
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void PlayBGM(string bgmName)
    {
        Instance.playBGM(bgmName);
    }
    private void playBGM(string bgmName)
    {
        if (sounds.ContainsKey(bgmName) == false)
        {
            //KEY�� �ش��ϴ� ���尡 ����.
            //���带 �߰��ϰų� KEY���� �߸���ٴ� �̾߱� 
            Debug.LogError(bgmName + "�� �ش��ϴ� ���尡 �����ϴ�.");
            return;
        }

        AudioClip bgmData = sounds[bgmName];
        if (bgmData == null)
        {
            //KEY�� �ش��ϴ� ���� null�̴�.
            Debug.LogError(bgmName + "�� �ش��ϴ� ���� NULL�Դϴ�.");
            return;
        }

        bgmSource.volume = bgmVolume;
        bgmSource.clip = bgmData;
        bgmSource.Play();
    }
}
