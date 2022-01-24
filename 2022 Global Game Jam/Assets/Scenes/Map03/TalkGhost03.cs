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
            //이벤트가 실행중일때는 작동불가.
            return;
        }

        //유령과 대화 이벤트 실행

        baseBGM = SoundManager.GetBGMValue();
        SoundManager.SetBGMValue(baseBGM * 0.5f, slot);

        GameManager.eventRunning = true;

        nowAnimation = dragObject.answerPoint == dragObject.nowPoint ? 0 : 1;
        talkAnimation[nowAnimation].Play();
        talkAnimation[nowAnimation].stopped += TalkAnimationStop;
    }

    private void TalkAnimationStop(PlayableDirector aDirector)
    {
        //유령과 대화 이벤트 종료 처리
        GameManager.eventRunning = false;
        talkAnimation[nowAnimation].stopped -= TalkAnimationStop;
        SoundManager.SetBGMValue(baseBGM, slot);
    }
}
