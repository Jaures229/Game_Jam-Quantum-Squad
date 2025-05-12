using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "XPReward", menuName = "Rewards/XP")]
public class XPReward : Reward
{
    public int experience;
    public override void ApplyReward()
    {
       PlayerStats.Instance.AddExperience(experience);
       Debug.Log("Récompense : +" + experience + " XP");
    }
}
