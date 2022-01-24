using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public static Dictionary<string, SpriteRenderer> dragSprites = new Dictionary<string, SpriteRenderer>();

    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Transform[] outline = new Transform[4];
    [HideInInspector] public Transform[] point = new Transform[2];
    [HideInInspector] public MatchPuzzle matchPuzzle;
    [HideInInspector] public int nowPoint;
    public SpriteRenderer outlineEffect;
    public int answerPoint;

    private void Start()
    {
        dragSprites[transform.name] = GetComponent<SpriteRenderer>();
        ItemPoint();
    }

    private void MoveObject()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.position = Vector3.Lerp(transform.position, pos, 0.3f);
        if (transform.position.x < outline[0].position.x)
        {
            transform.position = new Vector3(outline[0].position.x, transform.position.y, 0);
        }
        if (transform.position.x > outline[1].position.x)
        {
            transform.position = new Vector3(outline[1].position.x, transform.position.y, 0);
        }
        if (transform.position.y > outline[2].position.y)
        {
            transform.position = new Vector3(transform.position.x, outline[2].position.y, 0);
        }
        if (transform.position.y < outline[3].position.y)
        {
            transform.position = new Vector3(transform.position.x, outline[3].position.y, 0);
        }
        int sortOrder = dragSprites[transform.name].sortingOrder;
        List<SpriteRenderer> temp = new List<SpriteRenderer>();
        foreach (KeyValuePair<string, SpriteRenderer> sprite in dragSprites)
            if (sprite.Value.transform.name != transform.name)
                temp.Add(sprite.Value);
        for (int i = 0; i < temp.Count; i++)
            for (int j = i + 1; j < temp.Count; j++)
                if (temp[i].sortingOrder > temp[j].sortingOrder)
                {
                    SpriteRenderer s = temp[i];
                    temp[i] = temp[j];
                    temp[j] = s;
                }
        temp.Add(dragSprites[transform.name]);
        for (int i = 0; i < temp.Count; i++)
            temp[i].sortingOrder = 12 + i;
    }
    private void ItemPoint()
    {
        if (transform.position.x < point[0].position.x)
        {
            outlineEffect.color = Color.red;
            if (nowPoint != 1)
            {
                nowPoint = 1;
                matchPuzzle.RunPuzzleSystem();
            }
        }
        else if (transform.position.x < point[1].position.x)
        {
            outlineEffect.color = Color.green;
            if (nowPoint != 2)
            {
                nowPoint = 2;
                matchPuzzle.RunPuzzleSystem();
            }
        }
        else
        {
            outlineEffect.color = Color.blue;
            if (nowPoint != 3)
            {
                nowPoint = 3;
                matchPuzzle.RunPuzzleSystem();
            }
        }
    }

    private void OnMouseDrag()
    {
        if (matchPuzzle.puzzleStart == false)
            return;

        MoveObject();
        ItemPoint();
    }
}
