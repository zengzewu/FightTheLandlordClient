using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCtrl : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public bool IsSelect { get; set; }

    public CardDto cardDto { get; set; }

    private bool isMine;

    public void Init(CardDto cardDto, int index, bool isMine)
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.cardDto = cardDto;
        this.isMine = isMine;
        this.IsSelect = false;
        string path = string.Empty;
        if (this.isMine)
        {
            path = "poker/" + cardDto.Name;
            this.spriteRenderer.sprite = Resources.Load<Sprite>(path);
        }
        else
        {
            path = "poker/CardBack";
            this.spriteRenderer.sprite = Resources.Load<Sprite>(path);
        }
        this.spriteRenderer.sortingOrder = index;
    }

    private void OnMouseDown()
    {
        if (isMine)
        {
            IsSelect = !IsSelect;
            if (IsSelect)
            {
                this.transform.localPosition += new Vector3(0, 20, 0);
            }
            else
            {
                this.transform.localPosition -= new Vector3(0, 20, 0);
            }
        }
    }
}
