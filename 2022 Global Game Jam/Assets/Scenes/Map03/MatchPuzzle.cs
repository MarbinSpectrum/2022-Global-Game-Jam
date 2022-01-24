using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MatchPuzzle : MonoBehaviour
{
    [SerializeField] private List<DropItem> DropItem = new List<DropItem>();
    [SerializeField] private List<DragObject> DragItem = new List<DragObject>();
    [SerializeField] private Transform[] outline = new Transform[4];
    [SerializeField] private Transform[] point = new Transform[2];
    [SerializeField] private GameObject[] OffCandle = new GameObject[2];
    [SerializeField] private GameObject[] OnCandle = new GameObject[2];
    [SerializeField] private Transform center;

    private bool moveItems = false;
    private bool createMagicCircle = false;
    private List<Animation> DragAni = new List<Animation>();
    [HideInInspector] public bool puzzleStart = false;


    private void Awake()
    {
        for (int i = 0; i < DropItem.Count; i++)
        {
            DropItem[i].matchPuzzle = this;
        }
        for (int i = 0; i < DragItem.Count; i++)
        {
            DragItem[i].matchPuzzle = this;
            DragItem[i].outline = outline;
            DragItem[i].point = point;
            DragAni.Add(DragItem[i].GetComponent<Animation>());
        }
    }

    private void CandleHint(int ans)
    {
        if (ans == 0)
        {
            OnCandle[0].SetActive(true);
            OnCandle[1].SetActive(false);
            OffCandle[0].SetActive(false);
            OffCandle[1].SetActive(true);
        }
        else if (ans == 1)
        {
            OnCandle[0].SetActive(false);
            OnCandle[1].SetActive(true);
            OffCandle[0].SetActive(false);
            OffCandle[1].SetActive(true);
        }
        else if (ans == 2)
        {
            OnCandle[0].SetActive(false);
            OnCandle[1].SetActive(true);
            OffCandle[0].SetActive(true);
            OffCandle[1].SetActive(false);
        }
        else if (ans == 3)
        {
            OnCandle[0].SetActive(true);
            OnCandle[1].SetActive(false);
            OffCandle[0].SetActive(true);
            OffCandle[1].SetActive(false);
        }
    }

    public bool AllObjectDrop()
    {
        foreach (DropItem items in DropItem)
            if (items.dropEvent == false)
                return false;
        return true;
    }

    public void PuzzleSystemStart()
    {
        puzzleStart = true;
        foreach (DragObject items in DragItem)
            items.outlineEffect.gameObject.SetActive(true);
        CandleHint(0);
    }

    public void RunPuzzleSystem()
    {
        if (puzzleStart == false)
            return;
        int answer = 0;
        foreach (DragObject items in DragItem)
        {
            if (items.answerPoint == 0)
                continue;
            if (items.nowPoint == items.answerPoint)
                answer++;
        }
        CandleHint(answer);
        if (answer != 3)
            return;

        //여기까지왓으면 퍼즐완료
        foreach (DragObject items in DragItem)
        {
            items.enabled = false;
            items.GetComponent<BoxCollider2D>().enabled = false;
        }
        foreach (Animation ani in DragAni)
            ani.Play();

        GameManager.eventRunning = true;
        Invoke("PuzzleClearAni", 1f);
        
    }
    private void PuzzleClearAni()
    {
        moveItems = true;
    }

    private void Update()
    {
        if (moveItems == false)
            return;

        foreach (DragObject items in DragItem)
        {
            items.transform.position = Vector3.Lerp(items.transform.position, center.position, 0.05f);
        }

        bool check = true;
        foreach (DragObject items in DragItem)
        {
            if (Vector2.Distance(center.position, items.transform.position) > 0.3f)
                check = false;
        }

        if (createMagicCircle == true || check == false)
            return;

        SoundManager.StopAllBGM();
    }
}

