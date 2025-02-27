using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface RunningItem
{
    void ApplyItemEffect(Player player);
    void ResetItemAnim();
}
