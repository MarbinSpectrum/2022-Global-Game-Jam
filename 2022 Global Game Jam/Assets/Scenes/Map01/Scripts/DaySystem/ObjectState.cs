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
    /// : ������Ʈ�� ��� ����
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void ObjectToggle()
    {
        for (int i = 0; i < (int)DayState.NUM; i++)
        {
            //��� ������Ʈ ������� ���� ����
            if (objects[(DayState)i] == null)
            {
                //NULL��ó�� �ش� �������� ���� �ʵ��� ��������.
                Debug.LogError(gameObject.name + "�� " + (DayState)i + "�� �ش��ϴ� ������Ʈ�� �������ּ���!!");
                return;
            }

            objects[(DayState)i].SetActive((DayState)i == DayOnOffSystem.nowState);
        }
    }
}