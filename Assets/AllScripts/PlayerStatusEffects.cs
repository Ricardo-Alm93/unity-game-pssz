using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : MonoBehaviour
{
    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    public void AddStatusEffect(StatusEffect newEffect)
    {
        activeEffects.Add(newEffect);
        // Implement additional logic to apply the effect immediately
    }

    private void Update()
    {
        // Update the duration of active effects and apply their effects
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            StatusEffect effect = activeEffects[i];
            effect.duration -= Time.deltaTime;

            if (effect.duration <= 0)
            {
                RemoveStatusEffect(effect);
            }
            else
            {
                // Apply the effect based on its type and intensity
                ApplyEffect(effect);
            }
        }
    }

    private void ApplyEffect(StatusEffect effect)
    {
        switch (effect.effectType)
        {
            case StatusEffectType.Burned:
                // Apply burn effect
                break;
            case StatusEffectType.Cursed:
                // Apply poison effect
                break;
            // Handle other effect types
        }
    }

    private void RemoveStatusEffect(StatusEffect effect)
    {
        activeEffects.Remove(effect);
        // Implement additional logic to remove the effect
    }
}
