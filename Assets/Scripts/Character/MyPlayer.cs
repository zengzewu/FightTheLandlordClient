using Protocol.Code;
using Protocol.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : CharacterBase
{
    private List<CardCtrl> cardCtrls;

    public GameObject CardPrefab;

    private Transform cardPoint;

    private PromptMsg promptMsg;

    private SocketMsg socketMsg;

    private void Awake()
    {
        Bind(CharacterEvent.MY_PLAYER, CharacterEvent.MY_PLAYER_ADD_CARD, CharacterEvent.DEAL_CARD, CharacterEvent.REMOVE_MY_CARD);
    }

    private void Start()
    {
        this.cardCtrls = new List<CardCtrl>();
        this.cardPoint = transform.Find("CardPoint");

        promptMsg = new PromptMsg();
        socketMsg = new SocketMsg();
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case CharacterEvent.MY_PLAYER:
                StartCoroutine(InitCards(message as List<CardDto>));
                break;
            case CharacterEvent.MY_PLAYER_ADD_CARD:
                AddCard(message as GrabDto);
                break;
            case CharacterEvent.DEAL_CARD:
                DealCard();
                break;
            case CharacterEvent.REMOVE_MY_CARD:
                ReduceCards((message as DealDto).RemainCards);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 出牌
    /// </summary>
    private void DealCard()
    {
        List<CardDto> selectCards = GetSelectCards();
        DealDto dealDto = new DealDto(selectCards, Caches.UserDto.Id);
        if (dealDto.IsRegular == false)
        {
            promptMsg.Change("请选择合理的手牌", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_PANEL_SHOW, promptMsg);
            Dispatch(AreaCode.UI, UIEvent.SHOW_OR_HIDE_DEAL_BTN, true);
            return;
        }
        else
        {
            socketMsg.Change(OpCode.FIGHT, FightCode.DEAL_CREQ, dealDto);
            Dispatch(AreaCode.NET, 0, socketMsg);
        }
    }

    /// <summary>
    /// 移除卡牌的游戏物体
    /// </summary>
    private void ReduceCards(List<CardDto> remainCards)
    {
        //隐藏按钮
        Dispatch(AreaCode.UI, UIEvent.SHOW_OR_HIDE_DEAL_BTN, false);

        int index = 0;
        foreach (var cc in cardCtrls)
        {
            if (remainCards.Count == 0)
                break;
            else
            {
                cc.gameObject.SetActive(true);
                if(cc.IsSelect)
                {
                    cc.transform.localPosition -= new Vector3(0, 20, 0);
                    cc.IsSelect = false;
                }
                cc.Init(remainCards[index], index, true);
                index++;
                if (index == remainCards.Count)
                {
                    break;
                }
            }
        }
        for (int i = index; i < cardCtrls.Count; i++)
        {
            cardCtrls[i].IsSelect = false;
            Destroy(cardCtrls[i].gameObject);
        }
        cardCtrls.RemoveRange(index, cardCtrls.Count - index);
    }


    /// <summary>
    /// 获取选中的牌
    /// </summary>
    private List<CardDto> GetSelectCards()
    {
        List<CardDto> selectCardList = new List<CardDto>();
        foreach (var cardCtrl in cardCtrls)
        {
            if (cardCtrl.IsSelect)
            {
                selectCardList.Add(cardCtrl.cardDto);
            }
        }
        return selectCardList;
    }

    /// <summary>
    /// 增加底牌
    /// </summary>
    /// <param name="grabDto">底牌</param>
    private void AddCard(GrabDto grabDto)
    {
        List<CardDto> tableCards = grabDto.TableCards;
        List<CardDto> playerCards = grabDto.PlayerCards;

        int index = 0;
        foreach (var cardCtrl in cardCtrls)
        {
            cardCtrl.gameObject.SetActive(true);
            cardCtrl.Init(playerCards[index], index, true);
            index++;
        }
        //再创建三张卡牌
        for (int i = index; i < playerCards.Count; i++)
        {
            GameObject go = GameObject.Instantiate(CardPrefab, cardPoint);
            go.transform.localPosition += new Vector3(i * 35, 0, 0);
            CardCtrl cardCtrl = go.GetComponent<CardCtrl>();
            cardCtrl.Init(playerCards[i], i, true);
            cardCtrls.Add(cardCtrl);
        }
    }

    private IEnumerator InitCards(List<CardDto> cardDtoList)
    {
        for (int i = 0; i < cardDtoList.Count; i++)
        {
            GameObject go = GameObject.Instantiate(CardPrefab, cardPoint);
            go.transform.localPosition += new Vector3(i * 35, 0, 0);
            CardCtrl cardCtrl = go.GetComponent<CardCtrl>();
            cardCtrl.Init(cardDtoList[i], i, true);
            this.cardCtrls.Add(cardCtrl);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
