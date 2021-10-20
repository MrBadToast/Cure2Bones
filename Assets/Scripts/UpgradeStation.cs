using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : Interactable_Base
{
    public override void Interact()
    {
        UpgradeUIManager.Instance.EnableUpgradeUI();
    }
}
