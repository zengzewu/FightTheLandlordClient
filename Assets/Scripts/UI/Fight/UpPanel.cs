using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpPanel : UIBase
{
    /// <summary>
    /// 底牌数组
    /// </summary>
    private Image[] images;

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SET_BOTTOM_CARD:
                SetBottomCard(message as CardDto[]);
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        Bind(UIEvent.SET_BOTTOM_CARD);
    }

    private void Start()
    {
        images = new Image[3];
        images[0] = transform.Find("Card1").GetComponent<Image>();
        images[1] = transform.Find("Card2").GetComponent<Image>();
        images[2] = transform.Find("Card3").GetComponent<Image>();
    }

    private void SetBottomCard(CardDto[] cards)
    {
        images[0].sprite = Resources.Load<Sprite>("Poker/" + cards[0].Name);
        images[1].sprite = Resources.Load<Sprite>("Poker/" + cards[1].Name);
        images[2].sprite = Resources.Load<Sprite>("Poker/" + cards[2].Name);
    }
}
