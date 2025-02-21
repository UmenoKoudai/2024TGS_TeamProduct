using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTheGirlButton : ChoiceButtonBase
{
    protected override void OnClickAction()
    {
        FindObjectOfType<ChildrenFirstEvent>().SetFlag();
    }
}
