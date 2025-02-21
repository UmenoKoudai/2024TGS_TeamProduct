using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : ChoiceButtonBase
{
    protected override void OnClickAction()
    {
        FindObjectOfType<GameManager>().Save();
    }
}
