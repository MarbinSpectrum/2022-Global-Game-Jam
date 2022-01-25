using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayState
{
    MORNING,
    NIGHT,
    NEW_WORLD,
    NUM
}

public class DayOnOffSystem : MonoBehaviour
{
    private static DayOnOffSystem instance;
    public static DayOnOffSystem Instance
    {
        get 
        {
            if(instance == null)
            {
                //NULL값인 경우 데이터를 매니저를 가져온다.
                GameObject dayOnOffSystemManagerObj = Instantiate(Resources.Load("DayOnOffSystem") as GameObject);
                instance = dayOnOffSystemManagerObj.GetComponent<DayOnOffSystem>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    [SerializeField] private DayState NowState;

    public static DayState nowState
    {
        get 
        { 
            //싱글톤으로부터 현재상태를 읽어온다.
            return Instance.NowState;
        }
        set
        {
            Instance.NowState = value;
        }
    }
}
