using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TalkGhost01 : ClickObject
{
    [SerializeField] private PlayableDirector talkAnimation;
    [SerializeField] private PlayableDirector solveTalkAnimation;
    [SerializeField] string requireItem;

    private bool solvePuzzle = false;
    private float baseBGM = 0;
    private int slot = 0;
    public override void Click()
    {
        if (GameManager.eventRunning)
        {
            //이벤트가 실행중일때는 작동불가.
            return;
        }

        //유령과 대화 이벤트 실행

        if (solvePuzzle == false && Inventory.HasItem(requireItem))
        {
            //Inventory.UseItem(requireItem);
            solvePuzzle = true;
        }

        baseBGM = SoundManager.GetBGMValue();
        SoundManager.SetBGMValue(baseBGM * 0.5f, slot);

        if (solvePuzzle)
        {
            GameManager.eventRunning = true;
            solveTalkAnimation.Play();
            solveTalkAnimation.stopped += SolveTalkAnimationStop;
        }
        else
        {
            GameManager.eventRunning = true;
            talkAnimation.Play();
            talkAnimation.stopped += TalkAnimationStop;
        }
    }

    private void TalkAnimationStop(PlayableDirector aDirector)
    {
        //유령과 대화 이벤트 종료 처리
        GameManager.eventRunning = false;
        talkAnimation.stopped -= TalkAnimationStop;
        SoundManager.SetBGMValue(baseBGM, slot);
    }

    private void SolveTalkAnimationStop(PlayableDirector aDirector)
    {
        //유령과 대화 이벤트 종료 처리
        GameManager.eventRunning = false;
        solveTalkAnimation.stopped -= SolveTalkAnimationStop;
        SoundManager.SetBGMValue(baseBGM, slot);
    }
}
