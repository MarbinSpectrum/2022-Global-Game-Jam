using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObjectState : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<DayState,GameObject> objects;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Update()
    {
        ObjectToggle();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// : 오브젝트의 토글 상태
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void ObjectToggle()
    {
        for (int i = 0; i < (int)DayState.NUM; i++)
        {
            //모든 오브젝트 대상으로 상태 설정
            if (objects[(DayState)i] == null)
            {
                //NULL값처리 해당 구간으로 들어가지 않도록 설정하자.
                Debug.LogError(gameObject.name + "의 " + (DayState)i + "에 해당하는 오브젝트를 설정해주세요!!");
                return;
            }

            objects[(DayState)i].SetActive((DayState)i == DayOnOffSystem.nowState);
        }
    }
}
