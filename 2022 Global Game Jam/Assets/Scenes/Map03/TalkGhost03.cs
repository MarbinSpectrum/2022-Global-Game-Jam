using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TalkGhost03 : ClickObject
{
    [SerializeField] private PlayableDirector []talkAnimation = new PlayableDirector[2];
    [SerializeField] private DragObject dragObject;

    private float baseBGM = 0;
    private int slot = 0;
    private int nowAnimation = 0;
    public override void Click()
    {
        if (GameManager.eventRunning)
        {
            //�̺�Ʈ�� �������϶��� �۵��Ұ�.
            return;
        }

        //���ɰ� ��ȭ �̺�Ʈ ����

        baseBGM = SoundManager.GetBGMValue();
        SoundManager.SetBGMValue(baseBGM * 0.5f, slot);

        GameManager.eventRunning = true;

        nowAnimation = dragObject.answerPoint == dragObject.nowPoint ? 0 : 1;
        talkAnimation[nowAnimation].Play();
        talkAnimation[nowAnimation].stopped += TalkAnimationStop;
    }

    private void TalkAnimationStop(PlayableDirector aDirector)
    {
        //���ɰ� ��ȭ �̺�Ʈ ���� ó��
        GameManager.eventRunning = false;
        talkAnimation[nowAnimation].stopped -= TalkAnimationStop;
        SoundManager.SetBGMValue(baseBGM, slot);
    }
}
