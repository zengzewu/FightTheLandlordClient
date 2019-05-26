using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptPanel : UIBase
{
    private Text promptTxt;
    private CanvasGroup canvasGroup;

    [SerializeField]
    [Range(0,3)]
    private float showTime = 1f;

    private float timer=0f;

    private void Awake()
    {
        Bind(UIEvent.PROMPT_PANEL_SHOW);
    }

    public override void Execute(int eventCode, object message)
    {
        switch(eventCode)
        {
            case UIEvent.PROMPT_PANEL_SHOW:
                {
                    PromptMsg promptMsg = message as PromptMsg;
                    ShowPromptInf(promptMsg.Text, promptMsg.color);
                }
                break;
            default:
                break;
        }
    }

 

    private void Start()
    {
        promptTxt = transform.Find("PromptTxt").GetComponent<Text>();
        canvasGroup = transform.Find("PromptTxt").GetComponent<CanvasGroup>();
    }

    private Coroutine coroutine;

    private void ShowPromptInf(string text,Color color)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            canvasGroup.alpha = 0;
            timer = 0;
        }
            
        this.promptTxt.text = text;
        this.promptTxt.color = color;
        coroutine = StartCoroutine("ShowAnimation");
    }

    IEnumerator ShowAnimation()
    {
        while(canvasGroup.alpha<1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        while(timer<showTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }

}
