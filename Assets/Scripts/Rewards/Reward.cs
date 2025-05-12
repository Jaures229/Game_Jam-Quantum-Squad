using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RewardType
{
    XP,
    GOLD,
    ITEM,
    NONE,
    UNLOCK_QUEST,
}

[CreateAssetMenu(fileName = "NewReward", menuName = "Rewards/BaseReward")]
public abstract class Reward : ScriptableObject
{
    public RewardType type = RewardType.NONE;
    public abstract void ApplyReward();
}
