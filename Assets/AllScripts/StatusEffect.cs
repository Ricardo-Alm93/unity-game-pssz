using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectType
{
    Burned,
    Cursed,
    Poisoned,
    Frozen,
    Paralyzed,
    Bleeding,
    Confused,
    Weakened,
    Blinded,
    Silenced
}

[System.Serializable]
public class StatusEffect : StatModifier
{
    public StatusEffectType effectType;
    public float duration; // Duración en segundos
    private float timeRemaining;

    public StatusEffect(StatusEffectType type, float duration, float strengthMod, float intelligenceMod, float vitalityMod, float dexterityMod, float luckMod)
    {
        this.effectType = type;
        this.duration = duration;
        this.timeRemaining = duration;
        this.strengthModifier = strengthMod;
        this.intelligenceModifier = intelligenceMod;
        this.vitalityModifier = vitalityMod;
        this.dexterityModifier = dexterityMod;
        this.luckModifier = luckMod;
    }

    public void UpdateEffect(PlayerStats stats)
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            Remove(stats);
        }
    }
}

