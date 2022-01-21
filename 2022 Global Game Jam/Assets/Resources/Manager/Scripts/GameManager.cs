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
    //            //NULL값인 경우 데이터를 매니저를 가져온다.
    //            instance = Instantiate(Resources.Load("Managers/GameManager") as GameObject).GetComponent<GameManager>();
    //            DontDestroyOnLoad(instance.gameObject);
    //        }
    //        return instance;
    //    }
    //}

    public static bool eventRunning = false;
}
