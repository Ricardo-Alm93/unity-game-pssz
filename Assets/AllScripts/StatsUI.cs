using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public PlayerStats playerStats;

    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI intelligenceText;
    public TextMeshProUGUI vitalityText;
    public TextMeshProUGUI dexterityText;
    public TextMeshProUGUI luckText;

    public Button applyButton;
    public Button revertButton;

    void Start()
    {
        UpdateUI();
    }

    public void IncreaseStrength()
    {
        playerStats.ApplyStatIncrease("Strength");
        UpdateUI();
    }

    public void IncreaseIntelligence()
    {
        playerStats.ApplyStatIncrease("Intelligence");
        UpdateUI();
    }

    public void IncreaseVitality()
    {
        playerStats.ApplyStatIncrease("Vitality");
        UpdateUI();
    }

    public void IncreaseDexterity()
    {
        playerStats.ApplyStatIncrease("Dexterity");
        UpdateUI();
    }

    public void IncreaseLuck()
    {
        playerStats.ApplyStatIncrease("Luck");
        UpdateUI();
    }

    public void ApplyChanges()
    {
        // Guarda los cambios definitivamente (puede implementar un sistema de confirmación)
    }

    public void RevertChanges()
    {
        // Revertir los cambios temporales
        playerStats.UpdateAll_Stats_SecondaryValues();
        UpdateUI();
    }

    private void UpdateUI()
    {
        strengthText.text = playerStats.strength.GetValue().ToString();
        intelligenceText.text = playerStats.intelligence.GetValue().ToString();
        vitalityText.text = playerStats.vitality.GetValue().ToString();
        dexterityText.text = playerStats.dexterity.GetValue().ToString();
        luckText.text = playerStats.luck.GetValue().ToString();
    }
}
