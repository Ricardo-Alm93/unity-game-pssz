using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
    public float strengthModifier;
    public float intelligenceModifier;
    public float vitalityModifier;
    public float dexterityModifier;
    public float luckModifier;

    public void Apply(PlayerStats stats)
    {
        stats.strength.AddModifier(strengthModifier);
        stats.intelligence.AddModifier(intelligenceModifier);
        stats.vitality.AddModifier(vitalityModifier);
        stats.dexterity.AddModifier(dexterityModifier);
        stats.luck.AddModifier(luckModifier);

        stats.UpdateAll_Stats_SecondaryValues();
    }

    public void Remove(PlayerStats stats)
    {
        stats.strength.RemoveModifier(strengthModifier);
        stats.intelligence.RemoveModifier(intelligenceModifier);
        stats.vitality.RemoveModifier(vitalityModifier);
        stats.dexterity.RemoveModifier(dexterityModifier);
        stats.luck.RemoveModifier(luckModifier);

        stats.UpdateAll_Stats_SecondaryValues();
    }
}

