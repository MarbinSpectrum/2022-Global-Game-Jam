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
                //NULL���� ��� �����͸� �Ŵ����� �����´�.
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
            //�̱������κ��� ������¸� �о�´�.
            return Instance.NowState;
        }
        set
        {
            Instance.NowState = value;
        }
    }
}
