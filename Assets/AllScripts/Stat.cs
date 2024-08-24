using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public float baseValue; // Valor base de la estadística
    private float currentValue; // Valor actual, que puede ser modificado temporalmente

    private List<float> modifiers = new List<float>(); // Lista de modificadores temporales

    public Stat(float value)
    {
        baseValue = value;
        currentValue = value;
        modifiers = new List<float>();
    }

    public float GetBaseValue()
    {
        return baseValue;
    }
    
    public float GetValue()
    {
        float finalValue = baseValue;
        if (modifiers == null) modifiers = new List<float>();
        foreach (float modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    // Método para agregar un modificador temporal
    public void AddModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    // Método para eliminar un modificador temporal
    public void RemoveModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }

    // Método para reiniciar el valor actual al valor base
    public void ResetValue()
    {
        modifiers.Clear();
    }

    // Método para establecer un nuevo valor base
    public void SetBaseValue(float value)
    {
        baseValue = value;
        ResetValue();
    }

    // Método para establecer el valor actual (usado en casos específicos)
    public void SetCurrentValue(float value)
    {
        currentValue = value;
    }
}
