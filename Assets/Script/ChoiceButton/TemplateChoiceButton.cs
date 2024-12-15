using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateChoiceButton : ChoiceButtonBase
{
    protected override void OnClickAction()
    {
        Debug.Log("ボタンプッシュ");
    }
}
