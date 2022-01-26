using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TalkGhost02 : ClickObject
{
    public static TalkGhost02 instance;

    [SerializeField] private PlayableDirector talkAnimation;
    [SerializeField] private PlayableDirector solveTalkAnimation;
    [SerializeField] private PlayableDirector boneSolveTalkAnimation;
    [SerializeField] string requireItem;
    [SerializeField] bool ghostRemove = false;
    private bool solvePuzzle = false;
    private float baseBGM = 0;
    private int slot = 0;

    private void Awake()
    {
        instance = this;
    }

    public override void Click()
    {
        if (GameManager.eventRunning)
        {
            //이벤트가 실행중일때는 작동불가.
            return;
        }
        if (ghostRemove)
        {
            //유령이 성불함.
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
        bookItemEvent bookevent = (bookItemEvent)Inventory.Instance.itemEvent["book"];
    }

    public void boneSolveEvent()
    {
        Inventory.AddItem("None");
        ghostRemove = true;
        baseBGM = SoundManager.GetBGMValue();
        SoundManager.SetBGMValue(baseBGM * 0.5f, slot);

        boneSolveTalkAnimation.Play();
        boneSolveTalkAnimation.stopped += BoneSolveTalkAnimationStop;
    }

    private void BoneSolveTalkAnimationStop(PlayableDirector aDirector)
    {
        //유령과 대화 이벤트 종료 처리
        GameManager.eventRunning = false;
        solveTalkAnimation.stopped -= BoneSolveTalkAnimationStop;
        SoundManager.SetBGMValue(baseBGM, slot);
    }
}
