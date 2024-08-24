using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBonusCalculator
{
    private PlayerStats playerStats;
    private PlayerLevel playerLevel;

    public StatBonusCalculator(PlayerStats stats, PlayerLevel level)
    {
        playerStats = stats;
        playerLevel = level;
    }

    public float CalculateBonus_Health(float vit)
    {
        int bonusHealth_1 = 0; // obliga a tomar numeros redondos?
        float bonusHealth_2 = 0;

        // Bonus 0: Cada 1 puntos de Vitality otorgan 2 de vida adicional
        bonusHealth_1 += Mathf.FloorToInt(vit / 1) * 2;

        // Bonus 1: Cada 5 puntos de Vitality otorgan 50 de vida adicional
        bonusHealth_1 += Mathf.FloorToInt(vit / 5) * 50;

        // Bonus 2: Cada 20 puntos de Vitality otorgan 200 de vida adicional
        bonusHealth_1 += Mathf.FloorToInt(vit / 20) * 200;

        // Bonus 3: Cada 30 niveles de jugador otorgan 600 de vida adicional
        bonusHealth_1 += Mathf.FloorToInt((playerLevel.currentLevel / 30)) * 600;

        // Bonus 4: Al alcanzar el nivel 90, se gana 1000 de vida
        if (playerLevel.currentLevel >= 90)
        {
            bonusHealth_2 += 1000;
        }

        // Bonus 5: Al alcanzar el nivel 99, se gana 500 de vida
        if (playerLevel.currentLevel >= 99)
        {
            bonusHealth_2 += 500;
        }

        // Bonus 6: Bonificación de vida por equipo (esto es solo un placeholder)
        bonusHealth_2 += playerStats.GetEquipmentBonus_Health();

        // Bonus 7: Bonificación de vida por habilidad pasiva (esto es solo un placeholder)
        bonusHealth_2 += playerStats.GetPassiveBonus_Health();

        // Bonus 8: Bonificación de vida por habilidad activa (esto es solo un placeholder)
        bonusHealth_2 += playerStats.GetActiveBonus_Health();

        float finalBonus = bonusHealth_1 + bonusHealth_2;

        return finalBonus;
    }

    public float CalculateBonus_Mana(float inte)
    {
        int bonus_1 = 0; // obliga a tomar numeros redondos?
        float bonus_2 = 0;

        // Bonus 1: Cada 3 puntos de int otorgan 40 de mana adicional
        bonus_1 += Mathf.FloorToInt(inte / 3) * 20;

        // Bonus 2: Cada 15 puntos de Int otorgan 100 de Mana adicional
        bonus_1 += Mathf.FloorToInt(inte / 15) * 50;

        // Bonus 3: Cada 20 niveles de jugador otorgan 600 de mana adicional
        bonus_1 += Mathf.FloorToInt((playerLevel.currentLevel / 20)) * 100;

        // Bonus 4: Al alcanzar el nivel 90, se gana 200 de mana
        if (playerLevel.currentLevel >= 90)
        {
            bonus_2 += 250;
        }

        // Bonus 5: Al alcanzar el nivel 99, se gana 100 de mana
        if (playerLevel.currentLevel >= 99)
        {
            bonus_2 += 100;
        }

        // Bonus 6: Bonificación de mana por equipo (esto es solo un placeholder)
        bonus_2 += playerStats.GetEquipmentBonus_Mana();

        // Bonus 7: Bonificación de mana por habilidad pasiva (esto es solo un placeholder)
        bonus_2 += playerStats.GetPassiveBonus_Mana();

        // Bonus 8: Bonificación de mana por habilidad activa (esto es solo un placeholder)
        bonus_2 += playerStats.GetActiveBonus_Mana();

        float finalBonus = bonus_1 + bonus_2;

        return finalBonus;
    }

    public float CalculateBonus_Stamina(float str, float vit)
    {
        int bonus_1 = 0; // obliga a tomar numeros redondos?
        float bonus_2 = 0;

        // Bonus 1: Cada 2 puntos de str otorgan 10 de stamina adicional
        bonus_1 += Mathf.FloorToInt(str / 2) * 10;
        //  Cada 1 puntos de str otorgan 2 de stamina adicional
        bonus_1 += Mathf.FloorToInt(vit / 1) * 2;

        // Bonus 2: Cada 10 puntos de str otorgan 5 de stamina adicional
        bonus_1 += Mathf.FloorToInt(str / 10) * 5;

        // Bonus 3: Cada 10 niveles de jugador otorgan 20 de stamina adicional
        bonus_1 += Mathf.FloorToInt((playerLevel.currentLevel / 20)) * 2;

        // Bonus 4: Al alcanzar el nivel 90, se gana 50 de stamina
        if (playerLevel.currentLevel >= 90)
        {
            bonus_2 += 50;
        }

        // Bonus 5: Al alcanzar el nivel 99, se gana 20 de stamina
        if (playerLevel.currentLevel >= 99)
        {
            bonus_2 += 20;
        }

        // Bonus 6: Bonificación de sta por equipo (esto es solo un placeholder)
        bonus_2 += playerStats.GetEquipmentBonus_Stamina();

        // Bonus 7: Bonificación de sta por habilidad pasiva (esto es solo un placeholder)
        bonus_2 += playerStats.GetPassiveBonus_Stamina();

        // Bonus 8: Bonificación de sta por habilidad activa (esto es solo un placeholder)
        bonus_2 += playerStats.GetActiveBonus_Stamina();

        float finalBonus = bonus_1 + bonus_2;

        return finalBonus;
    }

    public float CalculateBonus_Regen_Health(float vit)
    {
        int bonus_1 = 0;
        float bonus_2 = 0;


        // Bonus 1: Cada 15 puntos de Vitality otorgan 1 de regeneración de salud adicional
        bonus_1 += Mathf.FloorToInt(vit / 15) * 1;

        // Bonus 2: Cada 20 puntos de Vitality otorgan 10 de regeneración de salud adicional
        bonus_1 += Mathf.FloorToInt(vit / 20) * 10;

        // Bonus 3: Cada 45 puntos de Vitality otorgan 20 de regeneración de salud adicional
        bonus_1 += Mathf.FloorToInt(vit / 45) * 20;

        // Bonus 4: Bonificación de regeneración de salud por equipo
        bonus_2 += playerStats.GetEquipment_RegenBonus_Health();

        // Bonus 5: Bonificación de regeneración de salud por habilidad pasiva
        bonus_2 += playerStats.GetPassiveSkill_RegenBonus_Health();

        // Bonus 6: Bonificación de regeneración de salud por habilidad activa
        bonus_2 += playerStats.GetActiveSkill_RegenBonus_Health();

        // Bonus 7: Obtener 1 de regen cada 5 niveles
        bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 5) * 1;

        // Bonus 8: Obtener 2 de regen cada 20 niveles
        bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 20) * 2;

        // Bonus 9: Obtener 5 de regen a nivel 70, 80, 90
        if (playerLevel.currentLevel >= 70)
        {
            bonus_2 += 5;
        }
        if (playerLevel.currentLevel >= 80)
        {
            bonus_2 += 5;
        }
        if (playerLevel.currentLevel >= 90)
        {
            bonus_2 += 5;
        }

        // Bonus 10: Obtener 10 de regen a nivel 99
        if (playerLevel.currentLevel >= 99)
        {
            bonus_2 += 10;
        }

        float finalBonus = bonus_1 + bonus_2;
        return finalBonus;
    }
    
    public float CalculateBonus_Regen_Mana(float inte)
    {
        int bonus_1 = 0;
        float bonus_2 = 0;

        // Bonus 1: Cada 5 puntos de Intelligence otorgan 5 de regeneración de mana adicional
        bonus_1 += Mathf.FloorToInt(inte / 5) * 5;

        // Bonus 2: Cada 15 puntos de Intelligence otorgan 5 de regeneración de mana adicional
        bonus_1 += Mathf.FloorToInt(inte / 15) * 5;

        // Bonus 3: Cada 25 puntos de Intelligence otorgan 10 de regeneración de mana adicional
        bonus_1 += Mathf.FloorToInt(inte / 25) * 10;

        // Bonus 4: Bonificación de regeneración de mana por equipo
        bonus_2 += playerStats.GetEquipment_RegenBonus_Mana();

        // Bonus 5: Bonificación de regeneración de mana por habilidad pasiva
        bonus_2 += playerStats.GetPassiveSkill_RegenBonus_Mana();

        // Bonus 6: Bonificación de regeneración de mana por habilidad activa
        bonus_2 += playerStats.GetActiveSkill_RegenBonus_Mana();

        // Bonus 7: Obtener 1 de regeneración de mana cada 5 niveles
        bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 5) * 1;

        // Bonus 8: Obtener 1 de regeneración de mana cada 9 niveles
        bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 9) * 1;

        // Bonus 9: Regeneración de mana por nivel específico (50, 60, 70, 80, 90)
        if (playerLevel.currentLevel >= 50) bonus_1 += 2; // Nivel 50
        if (playerLevel.currentLevel >= 60) bonus_1 += 2; // Nivel 60
        if (playerLevel.currentLevel >= 70) bonus_1 += 1; // Nivel 70
        if (playerLevel.currentLevel >= 80) bonus_1 += 1; // Nivel 80
        if (playerLevel.currentLevel >= 90) bonus_1 += 2; // Nivel 90

        float finalBonus = bonus_1 + bonus_2;
        return finalBonus;
    }

    public float CalculateBonus_Regen_Stamina(float str)
    {
        int bonus_1 = 0;
        float bonus_2 = 0;


        // Bonus 1: Cada 5 puntos de Strength otorgan 1 de regeneración de stamina adicional
        bonus_1 += Mathf.FloorToInt(str / 5) * 1;
    
        // Bonus 2: Cada 15 puntos de Strength otorgan 2 de regeneración de stamina adicional
        bonus_1 += Mathf.FloorToInt(str / 15) * 2;
    
        // Bonus 3: Cada 30 puntos de Strength otorgan 5 de regeneración de stamina adicional
        bonus_1 += Mathf.FloorToInt(str/ 30) * 5;
    
        // Bonus 4: Bonificación de regeneración de stamina por equipo
        bonus_2 += playerStats.GetEquipment_RegenBonu_Staminas();
    
        // Bonus 5: Bonificación de regeneración de stamina por habilidad pasiva
        bonus_2 += playerStats.GetPassiveSkill_RegenBonus_Stamina();
    
        // Bonus 6: Bonificación de regeneración de stamina por habilidad activa
        bonus_2 += playerStats.GetActiveSkill_RegenBonus_Stamina();
    
        // Bonus 7: Obtener 1 de regeneración de stamina cada 10 niveles
        bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 10) * 1;
    
        // Bonus 8: Bonos de regeneración de stamina por niveles específicos (50, 70, 90, 99)
        if (playerLevel.currentLevel >= 50) bonus_1 += 10; // Nivel 50
        if (playerLevel.currentLevel >= 70) bonus_1 += 20; // Nivel 70
        if (playerLevel.currentLevel >= 90) bonus_1 += 20; // Nivel 90
        if (playerLevel.currentLevel >= 99) bonus_1 += 10; // Nivel 99
    
        float finalBonus = bonus_1 + bonus_2;
        return finalBonus;
    }

    public float CalculateBonus_Damage_Physical(float str, float dex, float luck)
    {
        int bonus_1 = 0;
        float bonus_2 = 0;

        // Bonus 1: Cada 1 puntos de Strength otorgan 2 de daño físico adicional
        bonus_1 += Mathf.FloorToInt(str / 1) * 2;
        // Cada 5 puntos de Strength otorgan 10 de daño físico adicional
        bonus_1 += Mathf.FloorToInt(str / 5) * 10;
        // Cada 10 puntos de Strength otorgan 20 de daño físico adicional
        bonus_1 += Mathf.FloorToInt(str / 10) * 20;

        // Bonus 2: Cada 10 puntos de Dexterity otorgan 20 de daño físico adicional
        bonus_1 += Mathf.FloorToInt(dex / 10) * 20;

        // Bonus 3: Cada 15 puntos de Luck otorgan 10 de daño físico adicional
        bonus_1 += Mathf.FloorToInt(luck / 15) * 10;

        // Bonus 4: Bonificación de daño físico por equipo
        bonus_2 += playerStats.GetEquipment_DamageBonus_Physical();

        // Bonus 5: Bonificación de daño físico por habilidad pasiva
        bonus_2 += playerStats.GetPassiveSkill_DamageBonus_Physical();

        // Bonus 6: Bonificación de daño físico por habilidad activa
        bonus_2 += playerStats.GetActiveSkill_DamageBonus_Physical();

        // Bonus 7: Obtener 2 de daño físico adicional cada 5 niveles
        bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 5) * 2;

        // Bonus 8: Bonos de daño físico por niveles específicos (50, 75, 90)
        if (playerLevel.currentLevel >= 50) bonus_1 += 20; // Nivel 50
        if (playerLevel.currentLevel >= 75) bonus_1 += 20; // Nivel 75
        if (playerLevel.currentLevel >= 90) bonus_1 += 40; // Nivel 90

        float finalBonus = bonus_1 + bonus_2;
        return finalBonus;
    }

    public float CalculateBonus_Damage_Magical(float inte, float dex, float luck)
    {
        int bonus_1 = 0;
        float bonus_2 = 0;

        // Bonus 1: Cada 1 punto de Intelligence otorga 3 de daño mágico adicional
        bonus_1 += Mathf.FloorToInt(inte / 1) * 3;
        // Cada 5 puntos de Intelligence otorgan 15 de daño mágico adicional
        bonus_1 += Mathf.FloorToInt(inte / 5) * 15;
        // Cada 10 puntos de Intelligence otorgan 30 de daño mágico adicional
        bonus_1 += Mathf.FloorToInt(inte / 10) * 30;

        // Bonus 2: Cada 10 puntos de Dexterity otorgan 15 de daño mágico adicional
        bonus_1 += Mathf.FloorToInt(dex / 10) * 15;

        // Bonus 3: Cada 20 puntos de Luck otorgan 20 de daño mágico adicional
        bonus_1 += Mathf.FloorToInt(luck / 20) * 20;

        // Bonus 4: Bonificación de daño mágico por equipo
        bonus_2 += playerStats.GetEquipment_DamageBonus_Magical();

        // Bonus 5: Bonificación de daño mágico por habilidad pasiva
        bonus_2 += playerStats.GetPassiveSkill_DamageBonus_Magical();

        // Bonus 6: Bonificación de daño mágico por habilidad activa
        bonus_2 += playerStats.GetActiveSkill_DamageBonus_Magical();

        // Bonus 7: Obtener 3 de daño mágico adicional cada 5 niveles
        bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 5) * 3;

        // Bonus 8: Obtener 6 de daño mágico adicional cada 10 niveles
        bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 10) * 6;

        // Bonus 9: Bonos de daño mágico por niveles específicos (50, 75, 90)
        if (playerLevel.currentLevel >= 50) bonus_1 += 30; // Nivel 50
        if (playerLevel.currentLevel >= 75) bonus_1 += 50; // Nivel 75
        if (playerLevel.currentLevel >= 90) bonus_1 += 70; // Nivel 90

        float finalBonus = bonus_1 + bonus_2;
        return finalBonus;
    }

    public float CalculateBonus_Damage_True(Stat stat)
{
        int bonus_1 = 0;
        float bonus_2 = 0;


        // Bonus 1: Obtener 1 de daño verdadero adicional cada 5 niveles
        bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 5) * 1;

        // Bonus 2: Obtener 10 de daño verdadero adicional cada 20 niveles
        bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 20) * 10;

        // Bonus 3: Bonificación de daño verdadero por equipo
        bonus_2 += playerStats.GetEquipment_DamageBonus_True();

        // Bonus 4: Bonificación de daño verdadero por habilidad pasiva
        bonus_2 += playerStats.GetPassiveSkill_DamageBonus_True();

        // Bonus 5: Bonificación de daño verdadero por habilidad activa
        bonus_2 += playerStats.GetActiveSkill_DamageBonus_True();

        // Bonus 6: Obtener 5 de daño verdadero adicional cada 30 niveles
        bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 30) * 5;

        // Bonus 7: Obtener 15 de daño verdadero al nivel 50
        if (playerLevel.currentLevel >= 50) bonus_1 += 15;

        // Bonus 8: Obtener 20 de daño verdadero al nivel 75
        if (playerLevel.currentLevel >= 75) bonus_1 += 20;

        // Bonus 9: Obtener 25 de daño verdadero al nivel 90
        if (playerLevel.currentLevel >= 90) bonus_1 += 25;

        // Bonus 10: Obtener 30 de daño verdadero al nivel 99
        if (playerLevel.currentLevel >= 99) bonus_1 += 30;

        float finalBonus = bonus_1 + bonus_2;
        return finalBonus;
    }

    public float CalculateBonus_AttackSpeed(float dex, float str)
    {
        float bonus_1 = 0f;
        float bonus_2 = 0f;


        // Bonus 1: Cada 10 puntos de Dexterity otorgan 0.1 de attack speed
        bonus_1 += (dex / 1f) * 0.05f;

        // Bonus 2: Cada 20 puntos de Dexterity otorgan 0.2 de attack speed adicional
        bonus_1 += (dex / 50f) * 0.1f;

        // Bonus 3: Cada 45 puntos de Dexterity otorgan 0.4 de attack speed adicional
        bonus_1 += (dex / 45f) * 0.4f;

        // Bonus 4: Cada 10 puntos de Strength otorgan 0.05 de attack speed
        bonus_1 += (str / 10f) * 0.05f;

        // Bonus 5: Bonificación de attack speed por equipo
        bonus_2 += playerStats.GetEquipment_AttackSpeedBonus();

        // Bonus 6: Bonificación de attack speed por habilidad pasiva
        bonus_2 += playerStats.GetPassiveSkill_AttackSpeedBonus();

        // Bonus 7: Bonificación de attack speed por habilidad activa
        bonus_2 += playerStats.GetActiveSkill_AttackSpeedBonus();

        // Calcular el bonus total
        float finalBonus = bonus_1 + bonus_2;

        // Limitar la velocidad máxima a 2.5 ----- Lo idal quizas seria un 3.5 maximo, quie sabe....
        //finalBonus = Mathf.Clamp(finalBonus, 0f, 2.5f);

        return finalBonus;
    }

    public float CalculateBonus_MovementSpeed(float dex, float str, float vit)
    {
        float bonus_1 = 0; // Incrementos por estadísticas
        float bonus_2 = 0; // Incrementos por equipo y habilidades


        // Bonus por Dexterity
        // Cada 10 puntos de Dexterity otorgan 1 de movimiento adicional (duplicado)
        bonus_1 += (dex / 10f) * 1f;

        // Bonus por Strength
        // Cada 20 puntos de Strength otorgan 2 de movimiento adicional (duplicado)
        bonus_1 += (str / 20f) * 1f;

        // Bonus por Vitality
        // Cada 30 puntos de Vitality otorgan 2 de movimiento adicional (duplicado)
        bonus_1 += (vit / 30f) * 2f;

        // Bonus por Niveles
        // Obtener 0.2 de movimiento adicional cada 10 niveles (duplicado)
        if (playerLevel.currentLevel >= 10) bonus_1 += 0.5f; // Nivel 10
        if (playerLevel.currentLevel >= 25) bonus_1 += 0.5f; // Nivel 25
        if (playerLevel.currentLevel >= 30) bonus_1 += 1f; // Nivel 30
        if (playerLevel.currentLevel >= 55) bonus_1 += 1f; // Nivel 55
        if (playerLevel.currentLevel >= 70) bonus_1 += 1f; // Nivel 70
        if (playerLevel.currentLevel >= 85) bonus_1 += 1f; // Nivel 85
        if (playerLevel.currentLevel >= 90) bonus_1 += 1f; // Nivel 90
        if (playerLevel.currentLevel >= 99) bonus_1 += 1f; // Nivel 99

        // Bonus por equipo y habilidades (duplicado ejemplo)
        bonus_2 += playerStats.GetEquipment_MovementSpeedBonus();
        bonus_2 += playerStats.GetPassiveSkill_MovementSpeedBonus();
        bonus_2 += playerStats.GetActiveSkill_MovementSpeedBonus();

        // Sumar todos los bonuses
        float finalBonus = bonus_1 + bonus_2;

        // Limitar el bonus máximo a 30 (se puede ajustar según sea necesario)
        //finalBonus = Mathf.Min(finalBonus, 30f);

        return finalBonus;
    }

    public float CalculateBonus_CriticalRate(float luck, float dex)
    {
        float bonus_1 = 0; // Incrementos por estadísticas
        float bonus_2 = 0; // Incrementos por equipo y habilidades


        // Bonus por Luck
        // Cada 1 puntos de Luck otorgan 0.125% de tasa crítica adicional (ajustado)
        bonus_1 += (luck / 1f) * 0.125f;

        // Cada 2 puntos de Dex otorgan 0.125% de tasa crítica adicional (ajustado)
        bonus_1 += (dex / 2f) * 0.125f;

        // Cada 10 puntos de Luck otorgan 1% de tasa crítica adicional (ajustado)
        bonus_1 += (luck / 10f) * 1.0f;

        // Cada 20 puntos de Luck otorgan 3% de tasa crítica adicional (ajustado)
        bonus_1 += (luck / 20f) * 3.0f;

        // Cada 30 puntos de Luck otorgan 5% de tasa crítica adicional (ajustado)
        bonus_1 += (luck / 30f) * 5.0f;

        // Cada 15 niveles otorgan 2% de tasa crítica adicional (ajustado)
        if (playerLevel.currentLevel >= 15) bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 15) * 2.0f;

        // A nivel 99 obtienes un 5% adicional (ajustado)
        if (playerLevel.currentLevel >= 99) bonus_1 += 5.0f;

        // Cada 30 puntos de Dexterity otorgan 1% de tasa crítica adicional (ajustado)
        bonus_1 += (dex / 30f) * 1.0f;

        // Bonus por equipo y habilidades (ajustado ejemplo)
        bonus_2 += playerStats.GetEquipment_CriticalRateBonus(); // Por ejemplo, 20%
        bonus_2 += playerStats.GetPassiveSkill_CriticalRateBonus(); // Asumido 0% si no se proporciona
        bonus_2 += playerStats.GetActiveSkill_CriticalRateBonus(); // Asumido 0% si no se proporciona

        // Sumar todos los bonuses
        float finalBonus = bonus_1 + bonus_2;

        return finalBonus;
    }

    public float CalculateBonus_CastReduction(float dex, float inte, float luck)
    {
        float bonus_1 = 0; // Incrementos por estadísticas
        float bonus_2 = 0; // Incrementos por equipo y habilidades


        // Bonus por Dexterity
        // Cada 1 puntos de Dexterity otorgan 0.1% de reducción de tiempo de lanzamiento
        bonus_1 += (dex / 1f) * 0.1f;

        // Cada 2 puntos de Intelligence otorgan 0.5% de reducción de tiempo de lanzamiento
        bonus_1 += (inte / 2f) * 0.1f;

        // Bonus por Luck
        // Cada 20 puntos de Luck otorgan 1% de reducción de tiempo de lanzamiento
        bonus_1 += (luck / 20f) * 1f;

        // Bonus por Niveles
        // Obtener 0.5% de reducción de tiempo de lanzamiento cada 10 niveles
        if (playerLevel.currentLevel >= 10) bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 10) * 0.5f;

        // A nivel 50, obtienes un 2% adicional de reducción de tiempo de lanzamiento
        if (playerLevel.currentLevel >= 50) bonus_1 += 2.0f;

        // Cada 30 niveles adicionales obtienes 1% de reducción de tiempo de lanzamiento (a partir del nivel 70)
        if (playerLevel.currentLevel >= 70) bonus_1 += Mathf.FloorToInt((playerLevel.currentLevel - 70) / 30f) * 1f;

        // Bonus por equipo y habilidades
        bonus_2 += playerStats.GetEquipment_CastReductionBonus(); // Ejemplo: 5%
        bonus_2 += playerStats.GetPassiveSkill_CastReductionBonus(); // Ejemplo: 3%
        bonus_2 += playerStats.GetActiveSkill_CastReductionBonus(); // Ejemplo: 2%

        // Sumar todos los bonuses
        float finalBonus = bonus_1 + bonus_2;

        // Limitar el bonus máximo a 50% (se puede ajustar según sea necesario)
        finalBonus = Mathf.Min(finalBonus, 50f);

        return finalBonus;
    }
    
    public float CalculateBonus_Normal_Defense(float vit, float str)
    {
        float bonus_1 = 0; // Incrementos por estadísticas
        float bonus_2 = 0; // Incrementos por equipo y habilidades


        // Bonus por Vitality
        // Cada 2 puntos de Vitality otorgan 1 de defensa normal
        bonus_1 += (vit / 2f) * 1f;

        // Cada 4 puntos de Vitality otorgan 5 de defensa normal
        bonus_1 += (vit / 4f) * 5f;

        // Cada 10 puntos de Vitality otorgan 2 de defensa normal
        bonus_1 += (vit / 10f) * 2f;

        // Cada 20 puntos de Vitality otorgan 5 de defensa normal
        bonus_1 += (vit / 20f) * 5f;

        // Bonus por Strength (menor impacto)
        // Cada 30 puntos de Strength otorgan 2 de defensa normal
        bonus_1 += (str / 30f) * 2f;

        // Bonus por Niveles

        if (playerLevel.currentLevel >= 99) bonus_1 += 100; // Nivel 99
        if (playerLevel.currentLevel >= 95) bonus_1 += 30; // Nivel 95
        if (playerLevel.currentLevel >= 90) bonus_1 += 20; // Nivel 90
        if (playerLevel.currentLevel >= 85) bonus_1 += 15; // Nivel 85
        if (playerLevel.currentLevel >= 75) bonus_1 += 10; // Nivel 75
        if (playerLevel.currentLevel >= 50) bonus_1 += 10; // Nivel 50
        if (playerLevel.currentLevel >= 25) bonus_1 += 5;  // Nivel 25

        // Obtener 10 de defensa normal adicional cada 10 niveles
        if (playerLevel.currentLevel >= 10) bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 10) * 10f;

        // Bonus por equipo y habilidades
        bonus_2 += playerStats.GetEquipment_NormalDefenseBonus(); // Ejemplo: 10
        bonus_2 += playerStats.GetPassiveSkill_NormalDefenseBonus(); // Ejemplo: 5
        bonus_2 += playerStats.GetActiveSkill_NormalDefenseBonus(); // Ejemplo: 3

        // Sumar todos los bonuses
        float finalBonus = bonus_1 + bonus_2;

        // Limitar el bonus máximo (si es necesario, ajustar según sea necesario)
        // Aquí no se aplica clamp, se deja el valor real
        return finalBonus;
    }

    public float CalculateBonus_Percent_Defense(float vit, float str)
    {
        float bonus_1 = 0; // Incrementos por estadísticas
        //float bonus_2 = 0; // Incrementos por equipo y habilidades (no se consideran en este cálculo)


        // Bonus por Vitality
        // Cada 1 puntos de Vitality otorgan 0.1% de defensa porcentual
        bonus_1 += (vit / 1f) * 0.1f;

        // Cada 20 puntos de Vitality otorgan 1% de defensa porcentual
        bonus_1 += (vit / 20f) * 1f;

        // Cada 45 puntos de Vitality otorgan 2.5% de defensa porcentual
        bonus_1 += (vit / 40f) * 2.5f;

        // Bonus por Strength
        // Cada 2 puntos de Strength otorgan 0.2% de defensa porcentual
        bonus_1 += (str / 2f) * 0.2f;

        // Cada 20 puntos de Strength otorgan 0.5% de defensa porcentual
        bonus_1 += (str / 20f) * 0.5f;

        // Bonus por Niveles
        // Obtener 1% adicional cada 15 niveles
        if (playerLevel.currentLevel >= 15) bonus_1 += Mathf.FloorToInt(playerLevel.currentLevel / 15) * 1f;

        bonus_1 += playerStats.GetEquipment_PercentDefenseBonus(); // Ejemplo: 10
        bonus_1 += playerStats.GetPassiveSkill_PercentDefenseBonus(); // Ejemplo: 5
        bonus_1 += playerStats.GetActiveSkill_PercentDefenseBonus(); // Ejemplo: 3

        // Bonificaciones específicas por niveles
        if (playerLevel.currentLevel >= 99) bonus_1 += 2f; // Nivel 99
        if (playerLevel.currentLevel >= 95) bonus_1 += 1.5f; // Nivel 95
        if (playerLevel.currentLevel >= 90) bonus_1 += 1.2f; // Nivel 90
        if (playerLevel.currentLevel >= 85) bonus_1 += 1f; // Nivel 85
        if (playerLevel.currentLevel >= 75) bonus_1 += 0.8f; // Nivel 75
        if (playerLevel.currentLevel >= 50) bonus_1 += 0.5f; // Nivel 50
        if (playerLevel.currentLevel >= 25) bonus_1 += 0.2f; // Nivel 25

        // Sumar todos los bonuses
        float finalBonus = bonus_1;

        // Limitar el bonus máximo (ajustar según sea necesario)
        // No se aplica clamp para obtener el valor real
        return finalBonus;
    }

    public float CalculateBonus_Normal_Shield()
    {
        float bonus_1 = 0; // Incrementos por equipo y habilidades


        // Bonus por equipo y habilidades
        bonus_1 += playerStats.GetEquipment_NormalShieldBonus(); // Ejemplo: 20
        bonus_1 += playerStats.GetPassiveSkill_NormalShieldBonus(); // Ejemplo: 15
        bonus_1 += playerStats.GetActiveSkill_NormalShieldBonus(); // Ejemplo: 10

        // Sumar todos los bonuses
        float finalBonus = bonus_1;

        // No se aplica clamp para obtener el valor real
        return finalBonus;
    }

    public float CalculateBonus_Percent_Shield()
    {
        float bonus_1 = 0; // Incrementos por equipo y habilidades


        // Bonus por equipo y habilidades
        bonus_1 += playerStats.GetEquipment_PercentShieldBonus(); // Ejemplo: 10%
        bonus_1 += playerStats.GetPassiveSkill_PercentShieldBonus(); // Ejemplo: 8%
        bonus_1 += playerStats.GetActiveSkill_PercentShieldBonus(); // Ejemplo: 5%

        // Sumar todos los bonuses
        float finalBonus = bonus_1;

        // No se aplica clamp para obtener el valor real
        return finalBonus;
    }

    public float CalculateBonus_EvadeChance(float luck, float dex)
    {
        float bonus_1 = 0; // Incrementos por estadísticas
        float bonus_2 = 0; // Incrementos por equipo y habilidades


        // Bonus por Luck
        // Cada 1 puntos de Luck otorgan 0.2% de evasión adicional
        bonus_1 += (luck / 1f) * 0.2f;

        // Cada 10 puntos de Luck otorgan 1% de evasión adicional
        bonus_1 += (luck / 10f) * 1f;

        // Bonus por Dexterity (menor impacto)
         // Cada 2 puntos de Dexterity otorgan 0.1% de evasión adicional
        bonus_1 += (dex / 2f) * 0.1f;
        // Cada 20 puntos de Dexterity otorgan 0.5% de evasión adicional
        bonus_1 += (dex / 20f) * 0.5f;

        // Bonus por equipo y habilidades
        bonus_2 += playerStats.GetEquipment_EvadeChanceBonus(); // Ejemplo: 5%
        bonus_2 += playerStats.GetPassiveSkill_EvadeChanceBonus(); // Ejemplo: 3%
        bonus_2 += playerStats.GetActiveSkill_EvadeChanceBonus(); // Ejemplo: 2%

        // Sumar todos los bonuses
        float finalBonus = bonus_1 + bonus_2;

        // No se aplica clamp para obtener el valor real
        return finalBonus;
    }


}
