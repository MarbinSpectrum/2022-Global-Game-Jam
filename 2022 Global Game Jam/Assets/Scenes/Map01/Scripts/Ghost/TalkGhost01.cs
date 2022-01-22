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
            //�̺�Ʈ�� �������϶��� �۵��Ұ�.
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
    }
}
