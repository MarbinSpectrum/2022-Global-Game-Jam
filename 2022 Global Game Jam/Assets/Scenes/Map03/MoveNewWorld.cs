using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MoveNewWorld : MonoBehaviour
{
    private float time = 10;
    private float atime = 1.95f;
    private float btime = 1.95f;
    [SerializeField] private GameObject newWorld;
    [SerializeField] private PlayableDirector moveWorld;
    [SerializeField] private PlayableDirector XylophoneAni;
    [SerializeField] private List<Animation> menAni = new List<Animation>();

   private void Update()
   {
        if (time > 0)
        {
            time -= Time.deltaTime;
            btime -= Time.deltaTime;
            if (btime < 0)
            {
                atime *= 0.8f;
                btime = atime;
                DayOnOffSystem.nowState = (DayOnOffSystem.nowState == DayState.MORNING ? DayState.NIGHT : DayState.MORNING);
            }
        }
        else if(newWorld.activeSelf == false)
        {
            newWorld.SetActive(true);
            DayOnOffSystem.nowState = DayState.NEW_WORLD;
            moveWorld.stopped += MoveWorldAnimationStop;
        }
    }

    private void MoveWorldAnimationStop(PlayableDirector aDirector)
    {
        //유령과 대화 이벤트 종료 처리
        GameManager.eventRunning = false;
        foreach(Animation ani in menAni)
        {
            ani.Play();
        }
        XylophoneAni.Play();
    }
}
