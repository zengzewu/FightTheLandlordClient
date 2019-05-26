using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPlayer : CharacterBase
{
    private List<CardCtrl> cardCtrls;
    public GameObject CardPrefab;
    private Transform cardPoint;
    private void Awake()
    {
        Bind(CharacterEvent.LEFT_PLAYER,CharacterEvent.LEFT_PLAYER_ADD_CARD,CharacterEvent.REMOVE_LEFT_CARD);
    }

    private void Start()
    {
        this.cardCtrls = new List<CardCtrl>();
        this.cardPoint = transform.Find("CardPoint");
    }
    private void AddCards()
    {
        for (int i = 17; i < 20; i++)
        {
            GameObject go = GameObject.Instantiate(CardPrefab, cardPoint);
            go.transform.localPosition += new Vector3(i * 10, 0, 0);
            CardCtrl cardCtrl = go.GetComponent<CardCtrl>();
            cardCtrl.Init(null, i, false);
            this.cardCtrls.Add(cardCtrl);
        }
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case CharacterEvent.LEFT_PLAYER:
                StartCoroutine(InitCards());
                break;
            case CharacterEvent.LEFT_PLAYER_ADD_CARD:
                AddCards();
                break;
            case CharacterEvent.REMOVE_LEFT_CARD:
                ReduceCards((message as DealDto).RemainCards.Count);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 移除卡牌的游戏物体
    /// </summary>
    private void ReduceCards(int remainCount)
    {
        for (int i = remainCount; i <cardCtrls.Count; i++)
        {
            cardCtrls[i].gameObject.SetActive(false);
        }
    }

    private IEnumerator InitCards()
    {
        for (int i = 0; i < 17; i++)
        {
            GameObject go = GameObject.Instantiate(CardPrefab, cardPoint);
            go.transform.localPosition += new Vector3(i * 10, 0, 0);
            CardCtrl cardCtrl = go.GetComponent<CardCtrl>();
            cardCtrl.Init(null, i, false);
            this.cardCtrls.Add(cardCtrl);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
