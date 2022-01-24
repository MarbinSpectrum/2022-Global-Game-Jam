using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DropItem : ClickObject
{
    [SerializeField] private PlayableDirector dropAnimation;

    [HideInInspector] public bool dropEvent = false;
    [HideInInspector] public MatchPuzzle matchPuzzle;

    public override void Click()
    {
        if(dropEvent)
            return;
        dropEvent = true;
        dropAnimation.stopped += DropEnd;
        dropAnimation.Play();
    }

    private void DropEnd(PlayableDirector aDirector)
    {
        if (matchPuzzle.AllObjectDrop())
        {
            matchPuzzle.PuzzleSystemStart();
        }
    }


}
