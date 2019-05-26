using Protocol.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskPlayer : CharacterBase
{

    private List<CardCtrl> cardCtrls;

    public GameObject CardPrafab;

    public Transform Parent;

    private void Start()
    {
        cardCtrls = new List<CardCtrl>();
    }

    private void Awake()
    {
        Bind(CharacterEvent.UPDATE_SHOW_DESK);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case CharacterEvent.UPDATE_SHOW_DESK:
                UpdateDeskCards(message as List<CardDto>);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 更新桌面卡牌显示
    /// </summary>
    /// <param name="cardList">将要显示的卡牌</param>
    private void UpdateDeskCards(List<CardDto> cardList)
    {
        if (cardList.Count < cardCtrls.Count)
        {
            //将要显示的原来少
            int index = 0;
            foreach (var cardCtrl in cardCtrls)
            {
                cardCtrl.gameObject.SetActive(true);
                cardCtrl.Init(cardList[index], index, true);
                index++;
                if(index==cardList.Count)
                {
                    break;
                }
            }
            for (int i = index; i < cardCtrls.Count; i++)
            {
                cardCtrls[i].gameObject.SetActive(false);
            }
        }
        else
        {
            //将要显示的原来的多
            int index = 0;
            foreach (var cardCtrl in cardCtrls)
            {
                cardCtrl.gameObject.SetActive(true);
                cardCtrl.Init(cardList[index], index, true);
                index++;
            }
            for (int i = index; i < cardList.Count; i++)
            {
                GameObject go = GameObject.Instantiate(CardPrafab, Parent);
                go.transform.localPosition = new Vector2(i * 0.3f, 0);
                CardCtrl cardCtrl = go.GetComponent<CardCtrl>();
                cardCtrl.Init(cardList[i], i, true);
                cardCtrls.Add(cardCtrl);
            }
        }
    }
}
