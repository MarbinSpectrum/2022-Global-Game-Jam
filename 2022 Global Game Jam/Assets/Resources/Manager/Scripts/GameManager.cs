using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private static GameManager instance;
    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            //NULL���� ��� �����͸� �Ŵ����� �����´�.
    //            instance = Instantiate(Resources.Load("Managers/GameManager") as GameObject).GetComponent<GameManager>();
    //            DontDestroyOnLoad(instance.gameObject);
    //        }
    //        return instance;
    //    }
    //}

    public static bool eventRunning = false;
}
