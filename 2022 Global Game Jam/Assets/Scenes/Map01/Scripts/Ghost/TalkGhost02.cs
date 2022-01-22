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
            //�̺�Ʈ�� �������϶��� �۵��Ұ�.
            return;
        }
        if (ghostRemove)
        {
            //������ ������.
            return;
        }
        //���ɰ� ��ȭ �̺�Ʈ ����

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
        //���ɰ� ��ȭ �̺�Ʈ ���� ó��
        GameManager.eventRunning = false;
        talkAnimation.stopped -= TalkAnimationStop;
        SoundManager.SetBGMValue(baseBGM, slot);
    }

    private void SolveTalkAnimationStop(PlayableDirector aDirector)
    {
        //���ɰ� ��ȭ �̺�Ʈ ���� ó��
        GameManager.eventRunning = false;
        solveTalkAnimation.stopped -= SolveTalkAnimationStop;
        SoundManager.SetBGMValue(baseBGM, slot);
        bookItemEvent bookevent = (bookItemEvent)Inventory.Instance.itemEvent["book"];
        bookevent.ChangePageEvent();
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
        //���ɰ� ��ȭ �̺�Ʈ ���� ó��
        GameManager.eventRunning = false;
        solveTalkAnimation.stopped -= BoneSolveTalkAnimationStop;
        SoundManager.SetBGMValue(baseBGM, slot);
    }
}
