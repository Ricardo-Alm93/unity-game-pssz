using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public enum stat_type
    {
        str,
        inte,
        vit,
        dex,
        luk
    }

    public enum add_statVal
    {
        add,
        neg
    }
    
    public static PlayerStats Instance { get; private set; }

    // Eventos
    public delegate void StatChangedHandler();


    public event StatChangedHandler OnMaxHealthChanged;
    public event StatChangedHandler OnMaxHealthChanged_Smooth;
    public event StatChangedHandler OnMaxManaChanged;
    public event StatChangedHandler OnMaxManaChanged_Smooth;
    public event StatChangedHandler OnMaxStaminaChanged;
    public event StatChangedHandler OnMaxStaminaChanged_Smooth;
    //public event StatChangedHandler OnExperienceChanged;



    [Header("Estadísticas Principales")]
    // Estadísticas principales
    public Stat strength = new Stat(1);
    public Stat intelligence = new Stat(1);
    public Stat vitality = new Stat(1);
    public Stat dexterity = new Stat(1);
    public Stat luck = new Stat(1);

    [Header("Estadísticas Secundarias - autocalculadas")]

    // Estadísticas secundarias
    public Stat maxHealth = new Stat(0);
    public Stat maxMana = new Stat(0);
    public Stat maxStamina = new Stat(0);
    public Stat currentHealth = new Stat(0);
    public Stat currentMana = new Stat(0);
    public Stat currentStamina = new Stat(0);

    public int experienceToNextLevel;
    public int currentExperience;

    public Stat healthRegen = new Stat(0);
    public Stat manaRegen = new Stat(0f);
    public Stat staminaRegen = new Stat(0f);
    public Stat physicalDamage = new Stat(0);
    public Stat magicalDamage = new Stat(0);
    public Stat trueDamage = new Stat(0);
    public Stat attackSpeed = new Stat(0);
    public Stat movementSpeed = new Stat(0);
    public Stat criticalRate = new Stat(0f);
    public Stat castReduction = new Stat(0f);

    public Stat normal_Defense = new Stat(0);
    public Stat percent_Defense = new Stat(0f);

    public Stat normal_Shield = new Stat(0);
    public Stat percent_Shield = new Stat(0f);

    public Stat evade_Chance =  new Stat(0f);

    [Header("Estadísticas Base")]
    //------- Estadisticas Base ------
    public float health_Base;
    public float mana_Base;
    public float stamina_Base;
    public float healthRegen_Base;
    public float manaRegen_Base;
    public float staminaRegen_Base;
    public float physicalDamage_Base;
    public float magicalDamage_Base;
    public float trueDamage_Base;
    public float attackSpeed_Base;
    public float movementSpeed_Base;
    public float criticalRate_Base;
    public float castReduction_Base;

    public float normal_Defense_Base;
    public float percent_Defense_Base;

    public float normal_Shield_Base;
    public float percent_Shield_Base;

    public float evade_Chance_Base;

    //---- Preview Basic Stats-------
    public int preview_str;
    public int preview_inte;
    public int preview_vit;
    public int preview_dex;
    public int preview_luk;

    //---- Preview Secondary Stats-------
    public float health_Base_preview;
    public float mana_Base_preview;
    public float stamina_Base_preview;
    public float healthRegen_Base_preview;
    public float manaRegen_Base_preview;
    public float staminaRegen_Base_preview;
    public float physicalDamage_Base_preview;
    public float magicalDamage_Base_preview;
    public float trueDamage_Base_preview;
    public float attackSpeed_Base_preview;
    public float movementSpeed_Base_preview;
    public float criticalRate_Base_preview;
    public float castReduction_Base_preview;
           
    public float normal_Defense_Base_preview;
    public float percent_Defense_Base_preview;
           
    public float normal_Shield_Base_preview;
    public float percent_Shield_Base_preview;
           
    public float evade_Chance_Base_preview;

    [Header("TMP_ Primary Statistics")]

    public TMP_Text str_tmp;
    public TMP_Text inte_tmp;
    public TMP_Text vit_tmp;
    public TMP_Text dex_tmp;
    public TMP_Text luk_tmp;

    [Header("TMP_ Secondary Statistics")]

    public TMP_Text health_Base_TMP;
    public TMP_Text mana_Base_TMP;
    public TMP_Text stamina_Base_TMP;
    public TMP_Text healthRegen_Base_TMP;
    public TMP_Text manaRegen_Base_TMP;
    public TMP_Text staminaRegen_Base_TMP;
    public TMP_Text physicalDamage_Base_TMP;
    public TMP_Text magicalDamage_Base_TMP;
    public TMP_Text trueDamage_Base_TMP;
    public TMP_Text attackSpeed_Base_TMP;
    public TMP_Text movementSpeed_Base_TMP;
    public TMP_Text criticalRate_Base_TMP;
    public TMP_Text castReduction_Base_TMP;

    public TMP_Text normal_Defense_Base_TMP;
    public TMP_Text percent_Defense_Base_TMP;
           
    public TMP_Text normal_Shield_Base_TMP;
    public TMP_Text percent_Shield_Base_TMP;
           
    public TMP_Text evade_Chance_Base_TMP;

    [Header("Updates_Required_Points_Stats_Total")]
    public TMP_Text str_costPoint_text;
    public TMP_Text inte_costPoint_text;
    public TMP_Text vit_costPoint_text;
    public TMP_Text dex_costPoint_text;
    public TMP_Text luk_costPoint_text;

    [Header("Updates_Required_Points_Stats_ByLevel")]
    public TMP_Text str_costPoint_text_ByLvl;
    public TMP_Text inte_costPoint_text_ByLvl;
    public TMP_Text vit_costPoint_text_ByLvl;
    public TMP_Text dex_costPoint_text_ByLvl;
    public TMP_Text luk_costPoint_text_ByLvl;

    [Header("TMP_ StatusPointSpendSystem")]
    public TMP_Text remainingPoints;
    public TMP_Text pointstToSpeend;
    public TMP_Text speend_Points;

    public int str_costPoint;
    public int inte_costPoint;
    public int vit_costPoint;
    public int dex_costPoint;
    public int luk_costPoint;

    private int remainingPoints_Value;
    //private int pointstToSpend_Value;
    private int spend_Points_Value;
    private int spend_Points_BaseValue;

    public int str_pointsToInvest_Value;
    public int inte_pointsToInvest_Value;
    public int vit_pointsToInvest_Value;
    public int dex_pointsToInvest_Value;
    public int luk_pointsToInvest_Value;

    public PlayerControl playerControlScript;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public PlayerLevel playerLevel;

    private StatBonusCalculator statB_C;

    // Puntos disponibles para gastar
    public int statPoints;

    void Start()
    {
        statB_C = new StatBonusCalculator(this, playerLevel); // inicializar primero

        statPoints = playerLevel.statPoints;
        remainingPoints_Value = statPoints;
        Update_Available_Stats_Points_In_UI();

        spend_Points_Value = spend_Points_BaseValue;
        remainingPoints.text = "Puntos Disponibles: " + remainingPoints_Value.ToString();
        pointstToSpeend.text = "Puntos Gastados: " + spend_Points_Value.ToString();
        Set_Stat_TotalCostPoint_Value();

        UpdateAll_Stats_SecondaryValues();

        currentHealth.SetBaseValue(maxHealth.GetValue());
        currentMana.SetBaseValue(maxMana.GetValue());
        currentStamina.SetBaseValue(maxStamina.GetValue());
        //-------- Inicializarlos aca todos

        UpdateOtherClass_Values();

        //print("Pasando por PLAYER STATS _____________");
        OnMaxHealthChanged?.Invoke();
        OnMaxManaChanged?.Invoke();
        OnMaxStaminaChanged?.Invoke();
    }
    

    public void UpdateHealth(float amount)
    {
        currentHealth.AddModifier(amount);
        OnMaxHealthChanged?.Invoke();
    }

    public void ApplyStatIncrease(string statName)
    {
        //if (statPoints <= 0) return;

        switch (statName) // esto lo aplica // HAY QUE CREAR UNA PREVISUALIZACION
        {
            case "Strength":
                IncreaseStat(strength);
                UpdateSecondaryStat_RaiseByStrength();

                break;
            case "Intelligence":
                IncreaseStat(intelligence);
                UpdateSecondaryStat_RaiseByIntelligence();
                break;
            case "Vitality":
                IncreaseStat(vitality);
                UpdateSecondaryStat_RaiseByVitality();
                break;
            case "Dexterity":
                IncreaseStat(dexterity);
                UpdateSecondaryStat_RaiseByDexterity();
                break;
            case "Luck":
                IncreaseStat(luck);
                UpdateSecondaryStat_RaiseByLuck();
                break;
        }
    }


    //---- Stat Preview ------

    private int CalculateStatPoints_Required(int statCost)
    {
        if (statCost <= 10)
        {
            return 1;  // Hasta nivel 10, cada incremento cuesta 1 punto.
        }
        else if (statCost <= 20)
        {
            return 2;  // De nivel 11 a 20, cuesta 2 puntos por incremento.
        }
        else if (statCost <= 30)
        {
            return 3;  // De nivel 21 a 30, cuesta 3 puntos por incremento.
        }
        else if (statCost <= 40)
        {
            return 4;  // De nivel 31 a 40, cuesta 4 puntos por incremento.
        }
        else if (statCost <= 60)
        {
            return 6;  // De nivel 41 a 60, cuesta 6 puntos por incremento.
        }
        else if (statCost <= 80)
        {
            return 9;  // De nivel 61 a 80, cuesta 9 puntos por incremento.
        }
        else if (statCost <= 90)
        {
            return 12;  // De nivel 81 a 90, cuesta 12 puntos por incremento.
        }
        else
        {
            return 15;  // De nivel 91 a 99, cuesta 15 puntos por incremento.
        }
    }
    //cacular cada stats en relacion al que se entrega la info
    // devolver el cambio indivudual del stats y devolver una cifa actualizad acumulatida de 3 variables de val de puntos gastados a gastae
    public void CalcFinalCost(int baseStatVal_, int previewStatVal_, stat_type statType)
    {
        int finalCost = 0;
        int statsVal_ = baseStatVal_;
        int costVal_ToRise = previewStatVal_;


        while (costVal_ToRise > 0)
            {
                // Encuentra el siguiente múltiplo de 10
            int nextMultipleOf10 = ((statsVal_ / 10) + 1) * 10;

            // Calcula la cantidad de puntos necesarios para alcanzar el próximo múltiplo de 10
            int pointsToNextMultiple = nextMultipleOf10 - statsVal_;

            // Si la cantidad de puntos restantes es menor que los puntos necesarios para alcanzar el múltiplo de 10
            if (costVal_ToRise < pointsToNextMultiple)
            {
                // Solo incrementa lo que queda
                finalCost += CalculateStatPoints_Required(statsVal_) * costVal_ToRise;
                statsVal_ += costVal_ToRise;
                costVal_ToRise = 0;  // Finaliza el bucle
            }
            else
            {
                // Incrementa hasta el múltiplo de 10 y ajusta los valores restantes
                finalCost += CalculateStatPoints_Required(statsVal_) * pointsToNextMultiple;
                statsVal_ = nextMultipleOf10;
                costVal_ToRise -= pointsToNextMultiple;
            }
        }

        switch (statType)
        {
            case stat_type.str:
                str_pointsToInvest_Value = finalCost;
                str_costPoint_text.text = finalCost.ToString();

                int pointsRequired_str = CalculateStatPoints_Required(baseStatVal_ + previewStatVal_);
                str_costPoint_text_ByLvl.text = "(" + pointsRequired_str.ToString() + ")";
                break;
            case stat_type.inte:
                inte_pointsToInvest_Value = finalCost;
                inte_costPoint_text.text = finalCost.ToString();

                int pointsRequired_inte = CalculateStatPoints_Required(baseStatVal_ + previewStatVal_);
                inte_costPoint_text_ByLvl.text = "(" + pointsRequired_inte.ToString() + ")";
                break;
            case stat_type.vit:
                vit_pointsToInvest_Value = finalCost;
                vit_costPoint_text.text = finalCost.ToString();

                int pointsRequired_vit = CalculateStatPoints_Required(baseStatVal_ + previewStatVal_);
                vit_costPoint_text_ByLvl.text = "(" + pointsRequired_vit.ToString() + ")";
                break;
            case stat_type.dex:
                dex_pointsToInvest_Value = finalCost;
                dex_costPoint_text.text = finalCost.ToString();

                int pointsRequired_dex = CalculateStatPoints_Required(baseStatVal_ + previewStatVal_);
                dex_costPoint_text_ByLvl.text = "(" + pointsRequired_dex.ToString() + ")";
                break;
            case stat_type.luk:
                luk_pointsToInvest_Value = finalCost;
                luk_costPoint_text.text = finalCost.ToString();

                int pointsRequired_luk = CalculateStatPoints_Required(baseStatVal_ + previewStatVal_);
                luk_costPoint_text_ByLvl.text = "(" + pointsRequired_luk.ToString() + ")";
                break;
            default:
                break;
        }

        // puntos a gastar_ previsualizacion
        spend_Points_Value = (str_pointsToInvest_Value + inte_pointsToInvest_Value +
            vit_pointsToInvest_Value + dex_pointsToInvest_Value + luk_pointsToInvest_Value);

        //preview de cuantos tengo disponible = (puntos actuales - puntos a gastar)
        remainingPoints_Value = statPoints - spend_Points_Value;
        if (statPoints > 0 && spend_Points_Value > 0)
        {
            remainingPoints.text = "Puntos Disponibles: " + remainingPoints_Value.ToString() + "( -" + spend_Points_Value.ToString() + ")";
            pointstToSpeend.text = "Puntos Gastados: " + (spend_Points_BaseValue + spend_Points_Value).ToString() + "( +" + spend_Points_Value.ToString() + ")";
        }
        else
        {
            remainingPoints.text = "Puntos Disponibles: " + remainingPoints_Value.ToString();
            pointstToSpeend.text = "Puntos Gastados: " + (spend_Points_BaseValue).ToString();
        }
        
    }

    public void Apply_Raise_Stats()
    {
        // aumento las estadisticas- asigno nuevo valor
        int capture_strength = Mathf.RoundToInt(strength.GetValue());
        int capture_intelligence = Mathf.RoundToInt(intelligence.GetValue());
        int capture_vitality = Mathf.RoundToInt(vitality.GetValue());
        int capture_dexterity = Mathf.RoundToInt(dexterity.GetValue());
        int capture_luck = Mathf.RoundToInt(luck.GetValue());

        // Actualizar stats primarios
        strength = new Stat(capture_strength + preview_str);
        intelligence = new Stat(capture_intelligence + preview_inte);
        vitality = new Stat(capture_vitality + preview_vit);
        dexterity = new Stat(capture_dexterity + preview_dex);
        luck = new Stat(capture_luck + preview_luk);

        // Actualizar stats secundarios
        maxHealth = new Stat(maxHealth.GetValue() + health_Base_preview);
        maxMana = new Stat(maxMana.GetValue() + mana_Base_preview);
        maxStamina = new Stat(maxStamina.GetValue() + stamina_Base_preview);

        healthRegen = new Stat(healthRegen.GetValue() + healthRegen_Base_preview);
        manaRegen = new Stat(manaRegen.GetValue() + manaRegen_Base_preview);
        staminaRegen = new Stat(staminaRegen.GetValue() + stamina_Base_preview);

        physicalDamage = new Stat(physicalDamage.GetValue() + stamina_Base_preview);
        magicalDamage = new Stat(magicalDamage.GetValue() + magicalDamage_Base_preview);
        trueDamage = new Stat(trueDamage.GetValue() + trueDamage_Base_preview);

        attackSpeed = new Stat(attackSpeed.GetValue() + attackSpeed_Base_preview);
        movementSpeed = new Stat(movementSpeed.GetValue() + movementSpeed_Base_preview);

        criticalRate = new Stat(criticalRate.GetValue() + criticalRate_Base_preview);
        castReduction = new Stat(castReduction.GetValue() + castReduction_Base_preview);

        normal_Defense = new Stat(normal_Defense.GetValue() + normal_Defense_Base_preview);
        percent_Defense = new Stat(percent_Shield.GetValue() + percent_Defense_Base_preview);

        normal_Shield = new Stat(normal_Shield.GetValue() + normal_Shield_Base_preview);
        percent_Shield = new Stat(percent_Shield.GetValue() + percent_Shield_Base_preview);

        evade_Chance = new Stat(evade_Chance.GetValue() + evade_Chance_Base_preview);

        // actualizo el valor actual descontando los puntos a gastar en general
        int update_StatsPoints = statPoints;
        statPoints = update_StatsPoints - spend_Points_Value;
        int spedValueFinal = spend_Points_BaseValue + spend_Points_Value;
        spend_Points_BaseValue = spedValueFinal;
        pointstToSpeend.text = spend_Points_Value.ToString();

        UpdateOtherClass_Values();
        // Reinicio los valores de otras variables
        Cancel_Raise_Stats();
    }

    public void Cancel_Raise_Stats()
    {
        preview_str = 0;
        preview_inte = 0;
        preview_vit = 0;
        preview_dex = 0;
        preview_luk = 0;

        // la liena de abajo peude ser innecesaria ya que se llamana las fnciones Decrease
        //str_costPoint_text.text = CalculateStatPoints_Required(Mathf.RoundToInt(strength.GetValue())).ToString();

        DecreaseStrength();
        DecreaseIntelligence();
        DecreaseVitality();
        DecreaseDexterity();
        DecreaseLuck();
        remainingPoints_Value = statPoints;
        spend_Points_Value = 0;
        //spend_Points_Value = 0;

        remainingPoints.text = "Puntos Disponibles: " + remainingPoints_Value.ToString();
        pointstToSpeend.text = "Puntos Gastados: " + (spend_Points_BaseValue).ToString();
    }

    public void Reset_All_Status()
    {
        strength = new Stat(1);
        intelligence = new Stat(1);
        vitality = new Stat(1);
        dexterity = new Stat(1);
        luck = new Stat(1);

        maxHealth = new Stat(0);
        maxMana = new Stat(0);
        maxStamina = new Stat(0);
        //currentHealth = new Stat(0);
        //currentMana = new Stat(0);
        //currentStamina = new Stat(0);

        healthRegen = new Stat(0);
        manaRegen = new Stat(0f);
        staminaRegen = new Stat(0f);
        physicalDamage = new Stat(0);
        magicalDamage = new Stat(0);
        trueDamage = new Stat(0);
        attackSpeed = new Stat(0);
        movementSpeed = new Stat(0);
        criticalRate = new Stat(0f);
        castReduction = new Stat(0f);

        normal_Defense = new Stat(0);
        percent_Defense = new Stat(0f);

        normal_Shield = new Stat(0);
        percent_Shield = new Stat(0f);

        evade_Chance = new Stat(0f);

        preview_str = 0;
        preview_inte = 0;
        preview_vit = 0;
        preview_dex = 0;
        preview_luk = 0;

        DecreaseStrength();
        DecreaseIntelligence();
        DecreaseVitality();
        DecreaseDexterity();
        DecreaseLuck();

        UpdateAll_Stats_SecondaryValues();
        UpdateOtherClass_Values();

        statPoints = playerLevel.statPoints; // obtiene el valor original obtenido de status points
        remainingPoints_Value = statPoints;
        spend_Points_Value = 0;
        spend_Points_BaseValue = 0;

        remainingPoints.text = "Puntos Disponibles: " + remainingPoints_Value.ToString();// +" - "+ "("+ spend_Points_BaseValue.ToString() + ")";
        pointstToSpeend.text = "Puntos Gastados: " + (spend_Points_BaseValue).ToString();
    }

    public void Set_Stat_TotalCostPoint_Value()
    {
        
        int pointsRequired_str = CalculateStatPoints_Required(Mathf.FloorToInt(strength.GetValue()) + preview_str);
        str_costPoint_text_ByLvl.text = "(" + pointsRequired_str.ToString() + ")";

        int pointsRequired_inte = CalculateStatPoints_Required(Mathf.FloorToInt(intelligence.GetValue()) + preview_inte);
        inte_costPoint_text_ByLvl.text = "(" + pointsRequired_inte.ToString() + ")";

        int pointsRequired_vit = CalculateStatPoints_Required(Mathf.FloorToInt(vitality.GetValue()) + preview_vit);
        vit_costPoint_text_ByLvl.text = "(" + pointsRequired_vit.ToString() + ")";

        int pointsRequired_dex = CalculateStatPoints_Required(Mathf.FloorToInt(dexterity.GetValue()) + preview_dex);
        dex_costPoint_text_ByLvl.text = "(" + pointsRequired_dex.ToString() + ")";

        int pointsRequired_luk = CalculateStatPoints_Required(Mathf.FloorToInt(luck.GetValue()) + preview_luk);
        luk_costPoint_text_ByLvl.text = "(" + pointsRequired_luk.ToString() + ")";
    }

    public void Update_Available_Stats_Points_In_UI()
    {
        remainingPoints_Value = statPoints;
        remainingPoints.text = "Puntos Disponibles: " + remainingPoints_Value.ToString();
    }
    



    public void IncreaseStat_Preview( stat_type statusType_, add_statVal addStat_v)
    {
        //print("El valor de Raining points value = " + remainingPoints_Value);

        switch (statusType_) 
        {
           case stat_type.str:
               bool apply = false;
               switch (addStat_v)
               {
                   case add_statVal.add:
                       if (remainingPoints_Value > 0)
                       {
                           preview_str += 1;
                       }
                        else
                        {
                            if (preview_str <= 0)
                            {
                                apply = true;
                            }
                            
                        }
                       
                       //int cost = CalculateStatPoints_Required(Mathf.FloorToInt(strength.GetValue()) + preview_str);
                       break;
                   case add_statVal.neg:
                       if (preview_str > 0)
                           preview_str -= 1;

                       if (preview_str == 0)
                           apply = true;
                       break;
                   default:
                       break;
               }
               if (apply)
               {
                   str_tmp.text = strength.baseValue.ToString();
               }
               else
               {
                   str_tmp.text = strength.baseValue.ToString() + "+" + preview_str.ToString();
               }
               UpdateSecondaryStat_RaiseByStrength_Preview();           //----------Secondary Stats

               break;
           case stat_type.inte:
               bool apply2 = false;
               switch (addStat_v)
               {
                   case add_statVal.add:
                        if (remainingPoints_Value > 0)
                        {
                            preview_inte += 1;
                        }
                        else
                        {
                            if (preview_inte <= 0)
                            {
                                apply2 = true;
                            }
                        }
                        break;
                   case add_statVal.neg:
                       if (preview_inte > 0)
                           preview_inte -= 1;
                       if (preview_inte == 0)
                       {
                           apply2 = true;
                       }
                       break;
                   default:
                       break;
               }
               if (apply2)
               {
                   inte_tmp.text = intelligence.baseValue.ToString();
               }
               else
               {
                   inte_tmp.text = intelligence.baseValue.ToString() + "+" + preview_inte.ToString();
               }

               UpdateSecondaryStat_RaiseByIntelligence_Preview();           //----------Secondary Stats

               break;
           case stat_type.vit:
               bool apply3 = false;
               switch (addStat_v)
               {
                   case add_statVal.add:
                        if (remainingPoints_Value > 0)
                        {
                            preview_vit += 1;
                        }
                        else
                        if (preview_vit <= 0)
                        {
                            apply3 = true;
                        }
                        break;
                   case add_statVal.neg:
                       if (preview_vit > 0)
                           preview_vit -= 1;
                       if (preview_vit == 0)
                       {
                           apply3 = true;
                       }
                       break;
                   default:
                       break;
               }
               if (apply3)
               {
                   vit_tmp.text = vitality.baseValue.ToString();
               }
               else
               {
                   vit_tmp.text = vitality.baseValue.ToString() + "+" + preview_vit.ToString();
               }

               UpdateSecondaryStat_RaiseByVitality_Preview();            //----------Secondary Stats

               break;
           case stat_type.dex:
               bool apply4 = false;
               switch (addStat_v)
               {
                   case add_statVal.add:
                        if (remainingPoints_Value > 0)
                        {
                            preview_dex += 1;
                        }
                        else
                        {
                            if (preview_dex <= 0)
                            {
                                apply4 = true;
                            }
                        }
                        break;
                   case add_statVal.neg:
                       if (preview_dex > 0)
                           preview_dex -= 1;
                       if (preview_dex == 0)
                       {
                           apply4 = true;
                       }
                       break;
                   default:
                       break;
               }
               if (apply4)
               {
                   dex_tmp.text = dexterity.baseValue.ToString();
               }
               else
               {
                   dex_tmp.text = dexterity.baseValue.ToString() + "+" + preview_dex.ToString();
               }

               UpdateSecondaryStat_RaiseByDexterity_Preview();

               break;
           case stat_type.luk:
               bool apply5 = false;
               switch (addStat_v)
               {
                   case add_statVal.add:
                        if (remainingPoints_Value > 0)
                        {
                            preview_luk += 1;
                        }
                        else
                        {
                            if (preview_luk <= 0)
                            {
                                apply5 = true;
                            }
                        }
                        break;
                   case add_statVal.neg:
                       if (preview_luk > 0)
                           preview_luk -= 1;
                       if (preview_luk == 0)
                       {
                           apply5 = true;
                       }
                       break;
                   default:
                       break;
               }
               if (apply5)
               {
                   luk_tmp.text = luck.baseValue.ToString();
               }
               else
               {
                   luk_tmp.text = luck.baseValue.ToString() + "+" + preview_luk.ToString();
               }

               UpdateSecondaryStat_RaiseByLuck_Preview();

               break;
           default:
               break;
        
        }
    }

    public void IncreaseStrength()
    {
        IncreaseStat_Preview(stat_type.str, add_statVal.add);
        CalcFinalCost(Mathf.RoundToInt(strength.GetValue()), preview_str, stat_type.str);
    }

    public void DecreaseStrength()
    {
        IncreaseStat_Preview(stat_type.str, add_statVal.neg);
        CalcFinalCost(Mathf.RoundToInt(strength.GetValue()), preview_str, stat_type.str);
    }

    public void IncreaseIntelligence()
    {
        IncreaseStat_Preview(stat_type.inte, add_statVal.add);
        CalcFinalCost(Mathf.RoundToInt(intelligence.GetValue()), preview_inte, stat_type.inte);
    }

    public void DecreaseIntelligence()
    {
        IncreaseStat_Preview(stat_type.inte, add_statVal.neg);
        CalcFinalCost(Mathf.RoundToInt(intelligence.GetValue()), preview_inte, stat_type.inte);
    }
    public void IncreaseVitality()
    {
        IncreaseStat_Preview(stat_type.vit, add_statVal.add);
        CalcFinalCost(Mathf.RoundToInt(vitality.GetValue()), preview_vit, stat_type.vit);
    }

    public void DecreaseVitality()
    {
        IncreaseStat_Preview(stat_type.vit, add_statVal.neg);
        CalcFinalCost(Mathf.RoundToInt(vitality.GetValue()), preview_vit, stat_type.vit);
    }

    public void IncreaseDexterity()
    {
        IncreaseStat_Preview(stat_type.dex, add_statVal.add);
        CalcFinalCost(Mathf.RoundToInt(dexterity.GetValue()), preview_dex, stat_type.dex);
    }

    public void DecreaseDexterity()
    {
        IncreaseStat_Preview(stat_type.dex, add_statVal.neg);
        CalcFinalCost(Mathf.RoundToInt(dexterity.GetValue()), preview_dex, stat_type.dex);
    }

    public void IncreaseLuck()
    {
        IncreaseStat_Preview(stat_type.luk, add_statVal.add);
        CalcFinalCost(Mathf.RoundToInt(luck.GetValue()), preview_luk, stat_type.luk);
    }

    public void DecreaseLuck()
    {
        IncreaseStat_Preview(stat_type.luk, add_statVal.neg);
    }

    private void UpdateStatPreview(ref float previewValue, Stat stat, float baseStat,  float bonus, float old_Bonus, TMP_Text textComponent, int decimalQty)
    {

        if (bonus > old_Bonus)
        {
            float realBonus = bonus - old_Bonus;
            previewValue = realBonus;

            if (decimalQty == 0)
            {
                textComponent.text = stat.GetValue().ToString() + "+" + previewValue.ToString();
            }
            else if (decimalQty == 1)
            {
                textComponent.text = stat.GetValue().ToString("F1") + "+" + previewValue.ToString("F1");
            }
            else if (decimalQty == 2)
            {
                textComponent.text = stat.GetValue().ToString("F2") + "+" + previewValue.ToString("F2");
            }
        }
        else if (bonus <= old_Bonus)
        {
            if (decimalQty == 0)
            {
                textComponent.text = stat.GetValue().ToString();
            }
            else if (decimalQty == 1)
            {
                textComponent.text = stat.GetValue().ToString("F1");
            }
            else if (decimalQty == 2)
            {
                textComponent.text = stat.GetValue().ToString("F2");
            }
            previewValue = 0;
        }
        
    }

    private void Return_Stats_Old_Values(out int _str, out int _int, out int _vit, out int _dex, out int _luk)
    {
       _str = Mathf.RoundToInt(strength.GetValue());
       _int = Mathf.RoundToInt(intelligence.GetValue());
       _vit = Mathf.RoundToInt(vitality.GetValue());
       _dex = Mathf.RoundToInt(dexterity.GetValue());
       _luk = Mathf.RoundToInt(luck.GetValue());
    }

    private void Return_Stats_Values(out int _str2, out int _int2, out int _vit2, out int _dex2, out int _luk2)
    {
        _str2 = Mathf.RoundToInt(strength.GetValue()) + preview_str;
        _int2 = Mathf.RoundToInt(intelligence.GetValue()) + preview_inte;
        _vit2 = Mathf.RoundToInt(vitality.GetValue()) + preview_vit;
        _dex2 = Mathf.RoundToInt(dexterity.GetValue()) + preview_dex;
        _luk2 = Mathf.RoundToInt(luck.GetValue()) + preview_luk;
    }

    private void UpdateSecondaryStat_RaiseByStrength_Preview()
    {
        int _str; int _int; int _vit; int _dex; int _luk;
        int _str2; int _int2; int _vit2; int _dex2; int _luk2;
        Return_Stats_Values(out _str, out _int, out _vit, out _dex, out _luk);
        Return_Stats_Old_Values(out _str2, out _int2, out _vit2, out _dex2, out _luk2);

        UpdateStatPreview(ref physicalDamage_Base_preview, physicalDamage, physicalDamage_Base,
            statB_C.CalculateBonus_Damage_Physical(_str, _dex, _luk),
            statB_C.CalculateBonus_Damage_Physical(_str2, _dex2, _luk2),
            physicalDamage_Base_TMP,0);

        UpdateStatPreview(ref stamina_Base_preview, maxStamina, stamina_Base,
            statB_C.CalculateBonus_Stamina(_str, _vit),
            statB_C.CalculateBonus_Stamina(_str2, _vit2),
            stamina_Base_TMP,0);

        UpdateStatPreview(ref staminaRegen_Base_preview, staminaRegen, staminaRegen_Base,
            statB_C.CalculateBonus_Regen_Stamina(_str),
            statB_C.CalculateBonus_Regen_Stamina(_str2),
            staminaRegen_Base_TMP, 0);

        UpdateStatPreview(ref attackSpeed_Base_preview,attackSpeed, attackSpeed_Base,
            statB_C.CalculateBonus_AttackSpeed(_dex, _str),
            statB_C.CalculateBonus_AttackSpeed(_dex2, _str2),
            attackSpeed_Base_TMP,2);

        UpdateStatPreview(ref movementSpeed_Base_preview, movementSpeed, movementSpeed_Base,
            statB_C.CalculateBonus_MovementSpeed(_dex, _str, _vit),
             statB_C.CalculateBonus_MovementSpeed(_dex2, _str2, _vit2),
            movementSpeed_Base_TMP,1);

        UpdateStatPreview(ref normal_Defense_Base_preview, normal_Defense, normal_Defense_Base,
            statB_C.CalculateBonus_Normal_Defense(_vit, _str),
            statB_C.CalculateBonus_Normal_Defense(_vit2, _str2),
            normal_Defense_Base_TMP, 1);

        UpdateStatPreview(ref percent_Defense_Base_preview, percent_Defense, percent_Defense_Base,
            statB_C.CalculateBonus_Percent_Defense(_vit, _str),
            statB_C.CalculateBonus_Percent_Defense(_vit2, _str2),
            percent_Defense_Base_TMP, 1);

        OnMaxStaminaChanged?.Invoke();  //Aplicar un previwe en dicha barra (valor actual + "Preview") ejemplo: 101 + 5
    }

    private void UpdateSecondaryStat_RaiseByIntelligence_Preview()
    {
        int _str; int _int; int _vit; int _dex; int _luk;
        int _str2; int _int2; int _vit2; int _dex2; int _luk2;
        Return_Stats_Values(out _str, out _int, out _vit, out _dex, out _luk);
        Return_Stats_Old_Values(out _str2, out _int2, out _vit2, out _dex2, out _luk2);


        UpdateStatPreview(ref mana_Base_preview, maxMana, mana_Base,
            statB_C.CalculateBonus_Mana(_int),
            statB_C.CalculateBonus_Mana(_int2),
            mana_Base_TMP, 0);

        UpdateStatPreview(ref manaRegen_Base_preview, manaRegen, manaRegen_Base,
            statB_C.CalculateBonus_Regen_Mana(_int),
             statB_C.CalculateBonus_Regen_Mana(_int2), 
             manaRegen_Base_TMP, 0);

        UpdateStatPreview(ref magicalDamage_Base_preview, magicalDamage, magicalDamage_Base,
            statB_C.CalculateBonus_Damage_Magical(_int, _dex, _luk),
            statB_C.CalculateBonus_Damage_Magical(_int2, _dex2, _luk2), 
            magicalDamage_Base_TMP, 0);

        UpdateStatPreview(ref castReduction_Base_preview, castReduction, castReduction_Base,
            statB_C.CalculateBonus_CastReduction(_dex, _int, _luk),
            statB_C.CalculateBonus_CastReduction(_dex2, _int2, _luk2), 
            castReduction_Base_TMP,2);

        UpdateStatPreview(ref movementSpeed_Base_preview, movementSpeed, movementSpeed_Base,
            statB_C.CalculateBonus_MovementSpeed(_dex, _str, _luk),
            statB_C.CalculateBonus_MovementSpeed(_dex2, _str2, _luk2), 
            movementSpeed_Base_TMP, 1);
        OnMaxManaChanged?.Invoke();
    }

    private void UpdateSecondaryStat_RaiseByVitality_Preview()
    {
        int _str; int _int; int _vit; int _dex; int _luk;
        int _str2; int _int2; int _vit2; int _dex2; int _luk2;
        Return_Stats_Values(out _str, out _int, out _vit, out _dex, out _luk);
        Return_Stats_Old_Values(out _str2, out _int2, out _vit2, out _dex2, out _luk2);

        UpdateStatPreview(ref health_Base_preview, maxHealth, health_Base,
            statB_C.CalculateBonus_Health(_vit),
            statB_C.CalculateBonus_Health(_vit2), 
            health_Base_TMP, 0);

        UpdateStatPreview(ref healthRegen_Base_preview, healthRegen, healthRegen_Base,
            statB_C.CalculateBonus_Regen_Health(_vit),
            statB_C.CalculateBonus_Regen_Health(_vit2), 
            healthRegen_Base_TMP, 0);

        UpdateStatPreview(ref staminaRegen_Base_preview, staminaRegen, stamina_Base,
            statB_C.CalculateBonus_Regen_Stamina(_str),
            statB_C.CalculateBonus_Regen_Stamina(_str2), 
            staminaRegen_Base_TMP, 0);

        UpdateStatPreview(ref normal_Defense_Base_preview, normal_Defense, normal_Defense_Base,
            statB_C.CalculateBonus_Normal_Defense(_vit, _str),
            statB_C.CalculateBonus_Normal_Defense(_vit2, _str2), 
            normal_Defense_Base_TMP, 1);

        UpdateStatPreview(ref percent_Defense_Base_preview, percent_Defense,percent_Defense_Base,
            statB_C.CalculateBonus_Percent_Defense(_vit, _str),
            statB_C.CalculateBonus_Percent_Defense(_vit2, _str2), 
            percent_Defense_Base_TMP, 1);

        UpdateStatPreview(ref stamina_Base_preview, maxStamina, stamina_Base,
            statB_C.CalculateBonus_Stamina(_str, _vit),
            statB_C.CalculateBonus_Stamina(_str2, _vit2), 
            stamina_Base_TMP, 0);

        OnMaxHealthChanged?.Invoke();
        OnMaxStaminaChanged?.Invoke(); 
    }

    private void UpdateSecondaryStat_RaiseByDexterity_Preview()
    {
        int _str; int _int; int _vit; int _dex; int _luk;
        int _str2; int _int2; int _vit2; int _dex2; int _luk2;
        Return_Stats_Values(out _str, out _int, out _vit, out _dex, out _luk);
        Return_Stats_Old_Values(out _str2, out _int2, out _vit2, out _dex2, out _luk2);

        UpdateStatPreview(ref physicalDamage_Base_preview, physicalDamage, physicalDamage_Base,
            statB_C.CalculateBonus_Damage_Magical(_int, _dex, _luk),
            statB_C.CalculateBonus_Damage_Magical(_int2, _dex2, _luk2), 
            magicalDamage_Base_TMP, 0);

        UpdateStatPreview(ref magicalDamage_Base_preview, magicalDamage, magicalDamage_Base,
            statB_C.CalculateBonus_Damage_Physical(_str, _dex, _luk),
            statB_C.CalculateBonus_Damage_Physical(_str2, _dex2, _luk2), 
            physicalDamage_Base_TMP, 0);

        UpdateStatPreview(ref attackSpeed_Base_preview, attackSpeed, attackSpeed_Base,
            statB_C.CalculateBonus_AttackSpeed(_dex, _str),
            statB_C.CalculateBonus_AttackSpeed(_dex2, _str2), 
            attackSpeed_Base_TMP, 2);

        UpdateStatPreview(ref criticalRate_Base_preview, criticalRate, criticalRate_Base,
            statB_C.CalculateBonus_CriticalRate(_luk, _dex),
            statB_C.CalculateBonus_CriticalRate(_luk2, _dex2), 
            criticalRate_Base_TMP, 1);

        UpdateStatPreview(ref movementSpeed_Base_preview, movementSpeed, movementSpeed_Base,
            statB_C.CalculateBonus_MovementSpeed(_dex, _str, _vit),
            statB_C.CalculateBonus_MovementSpeed(_dex2, _str2, _vit2), 
            movementSpeed_Base_TMP, 1);

        UpdateStatPreview(ref castReduction_Base_preview, castReduction, castReduction_Base,
            statB_C.CalculateBonus_CastReduction(_dex, _int, _vit),
            statB_C.CalculateBonus_CastReduction(_dex2, _int2, _vit2), 
            castReduction_Base_TMP, 2);

        UpdateStatPreview(ref evade_Chance_Base_preview, evade_Chance, evade_Chance_Base,
           statB_C.CalculateBonus_EvadeChance(_luk, _dex),
           statB_C.CalculateBonus_EvadeChance(_luk2, _dex2),
           evade_Chance_Base_TMP, 1);

    }

    private void UpdateSecondaryStat_RaiseByLuck_Preview()
    {
        int _str; int _int; int _vit; int _dex; int _luk;
        int _str2; int _int2; int _vit2; int _dex2; int _luk2;
        Return_Stats_Values(out _str, out _int, out _vit, out _dex, out _luk);
        Return_Stats_Old_Values(out _str2, out _int2, out _vit2, out _dex2, out _luk2);

        UpdateStatPreview(ref criticalRate_Base_preview, criticalRate, criticalRate_Base,
            statB_C.CalculateBonus_CriticalRate(_luk, _dex),
            statB_C.CalculateBonus_CriticalRate(_luk2, _dex2), 
            criticalRate_Base_TMP, 1);

        UpdateStatPreview(ref evade_Chance_Base_preview, evade_Chance, evade_Chance_Base,
           statB_C.CalculateBonus_EvadeChance(_luk, _dex),
           statB_C.CalculateBonus_EvadeChance(_luk2, _dex2), 
           evade_Chance_Base_TMP, 1);

        UpdateStatPreview(ref castReduction_Base_preview, castReduction, castReduction_Base,
           statB_C.CalculateBonus_CastReduction(_dex, _int, _luk),
           statB_C.CalculateBonus_CastReduction(_dex2, _int2, _luk2),
           castReduction_Base_TMP, 2);

        UpdateStatPreview(ref magicalDamage_Base_preview, magicalDamage, magicalDamage_Base,
           statB_C.CalculateBonus_Damage_Magical(_int,_dex,_luk),
           statB_C.CalculateBonus_Damage_Magical(_int2, _dex2, _luk2),
           magicalDamage_Base_TMP, 0);

    }

    

    private void IncreaseStat(Stat stat)
    {
        int cost = CalculateCost(stat);
        if (statPoints >= cost)
        {
            statPoints -= cost;
            stat.baseValue++;
        }
    }

    private int CalculateCost(Stat stat)
    {
        return Mathf.FloorToInt(stat.baseValue / 10) + 1; // Ejemplo de curva de coste
    }

    public void UpdateAll_Stats_SecondaryValues()
        // este m etodo deberia dividirse en varios, para ser mas especificos... /
        //  quizas por tpo de estatidsica incrementada que afecte a dicho secondary stats a actualizar....

        // por mientras pondre todos los metodos de stats  para TESTEO
    
    {
        UpdateSecondaryStat_RaiseByStrength();
        UpdateSecondaryStat_RaiseByIntelligence();
        UpdateSecondaryStat_RaiseByDexterity();
        UpdateSecondaryStat_RaiseByVitality();  
        UpdateSecondaryStat_RaiseByLuck();
    }

    public void UpdateAll_Stats_SecondaryValues_Preview()
    {
        UpdateSecondaryStat_RaiseByStrength_Preview();
        UpdateSecondaryStat_RaiseByIntelligence_Preview();
        UpdateSecondaryStat_RaiseByDexterity_Preview();
        UpdateSecondaryStat_RaiseByVitality_Preview();
        UpdateSecondaryStat_RaiseByLuck_Preview();
    }


    private void UpdateSecondaryStat_RaiseByStrength()
    {
        int _str = Mathf.RoundToInt(strength.GetValue());
        int _int = Mathf.RoundToInt(intelligence.GetValue());
        int _vit = Mathf.RoundToInt(vitality.GetValue());
        int _dex = Mathf.RoundToInt(dexterity.GetValue());
        int _luk = Mathf.RoundToInt(luck.GetValue());

        physicalDamage.baseValue = physicalDamage_Base + statB_C.CalculateBonus_Damage_Physical(_str, _dex, _luk);
        physicalDamage_Base_TMP.text = physicalDamage.baseValue.ToString();

        maxStamina.baseValue = stamina_Base + statB_C.CalculateBonus_Stamina(_str,_vit);
        stamina_Base_TMP.text = maxStamina.baseValue.ToString();

        staminaRegen.baseValue = staminaRegen_Base + statB_C.CalculateBonus_Regen_Stamina(_str);
        stamina_Base_TMP.text = staminaRegen.baseValue.ToString();

        attackSpeed.baseValue = attackSpeed_Base + statB_C.CalculateBonus_AttackSpeed(_dex, _str);
        attackSpeed_Base_TMP.text = attackSpeed.baseValue.ToString("F2");


        movementSpeed.baseValue = movementSpeed_Base + statB_C.CalculateBonus_MovementSpeed(_dex, _str,_vit);
        movementSpeed_Base_TMP.text = movementSpeed.baseValue.ToString("F1");


        normal_Defense.baseValue = normal_Defense_Base + statB_C.CalculateBonus_Normal_Defense(_vit, _str);
        normal_Defense_Base_TMP.text = normal_Defense.baseValue.ToString("F1");


        percent_Defense.baseValue = percent_Defense_Base + statB_C.CalculateBonus_Percent_Defense(_vit, _str);
        percent_Defense_Base_TMP.text = percent_Defense.baseValue.ToString("F1");
        //-----

        OnMaxStaminaChanged?.Invoke();
    }

    private void UpdateSecondaryStat_RaiseByIntelligence()
    {
        int _str = Mathf.RoundToInt(strength.GetValue());
        int _int = Mathf.RoundToInt(intelligence.GetValue());
        int _vit = Mathf.RoundToInt(vitality.GetValue());
        int _dex = Mathf.RoundToInt(dexterity.GetValue());
        int _luk = Mathf.RoundToInt(luck.GetValue());

        maxMana.baseValue = mana_Base + statB_C.CalculateBonus_Mana(_int);
        mana_Base_TMP.text = maxMana.baseValue.ToString();

        manaRegen.baseValue = manaRegen_Base + statB_C.CalculateBonus_Regen_Mana(_int);
        manaRegen_Base_TMP.text = manaRegen.baseValue.ToString();

        magicalDamage.baseValue = magicalDamage_Base + statB_C.CalculateBonus_Damage_Magical(_int, _dex, _luk);
        magicalDamage_Base_TMP.text = magicalDamage.baseValue.ToString();

        castReduction.baseValue = castReduction_Base + statB_C.CalculateBonus_CastReduction(_dex, _int, _luk);
        castReduction_Base_TMP.text = castReduction.baseValue.ToString("F2");

        movementSpeed.baseValue = movementSpeed_Base + statB_C.CalculateBonus_MovementSpeed(_dex, _str,_vit);
        movementSpeed_Base_TMP.text = movementSpeed.baseValue.ToString("F1");
        //-----

        OnMaxManaChanged?.Invoke();
    }

    private void UpdateSecondaryStat_RaiseByVitality()
    {
        int _str = Mathf.RoundToInt(strength.GetValue());
        int _int = Mathf.RoundToInt(intelligence.GetValue());
        int _vit = Mathf.RoundToInt(vitality.GetValue());
        int _dex = Mathf.RoundToInt(dexterity.GetValue());
        int _luk = Mathf.RoundToInt(luck.GetValue());

        maxHealth.baseValue = health_Base + statB_C.CalculateBonus_Health(_vit);
        health_Base_TMP.text = maxHealth.baseValue.ToString();

        healthRegen.baseValue = healthRegen_Base + statB_C.CalculateBonus_Regen_Health(_vit);
        healthRegen_Base_TMP.text = healthRegen.baseValue.ToString();

        staminaRegen.baseValue = staminaRegen_Base + statB_C.CalculateBonus_Regen_Stamina(_str);
        staminaRegen_Base_TMP.text = staminaRegen.baseValue.ToString();

        normal_Defense.baseValue = normal_Defense_Base + statB_C.CalculateBonus_Normal_Defense(_vit,_str);
        normal_Defense_Base_TMP.text = normal_Defense.baseValue.ToString("F1");

        percent_Defense.baseValue = percent_Defense_Base + statB_C.CalculateBonus_Percent_Defense(_vit,_str);
        percent_Defense_Base_TMP.text = percent_Defense.baseValue.ToString("F1");

        movementSpeed.baseValue = movementSpeed_Base + statB_C.CalculateBonus_MovementSpeed(_dex, _str,_vit);
        movementSpeed_Base_TMP.text = movementSpeed.baseValue.ToString("F1");

        maxStamina.baseValue = stamina_Base + statB_C.CalculateBonus_Stamina(_str, _vit);
        stamina_Base_TMP.text = maxStamina.baseValue.ToString();

        //-----

        OnMaxHealthChanged?.Invoke();
        OnMaxStaminaChanged?.Invoke();
    }

    private void UpdateSecondaryStat_RaiseByDexterity()
    {
        int _str = Mathf.RoundToInt(strength.GetValue());
        int _int = Mathf.RoundToInt(intelligence.GetValue());
        int _vit = Mathf.RoundToInt(vitality.GetValue());
        int _dex = Mathf.RoundToInt(dexterity.GetValue());
        int _luk = Mathf.RoundToInt(luck.GetValue());

        physicalDamage.baseValue = physicalDamage_Base + statB_C.CalculateBonus_Damage_Physical(_str, _dex, _luk);
        physicalDamage_Base_TMP.text = physicalDamage.baseValue.ToString();

        magicalDamage.baseValue = magicalDamage_Base + statB_C.CalculateBonus_Damage_Magical(_int, _dex, _luk);
        magicalDamage_Base_TMP.text = magicalDamage.baseValue.ToString();

        attackSpeed.baseValue = attackSpeed_Base + statB_C.CalculateBonus_AttackSpeed(_dex, _str);
        attackSpeed_Base_TMP.text = attackSpeed.baseValue.ToString("F2");

        criticalRate.baseValue = criticalRate_Base + statB_C.CalculateBonus_CriticalRate(_luk, _dex);
        criticalRate_Base_TMP.text = criticalRate.baseValue.ToString("F1");

        movementSpeed.baseValue = movementSpeed_Base + statB_C.CalculateBonus_MovementSpeed(_dex,_str,_vit);
        movementSpeed_Base_TMP.text = movementSpeed.baseValue.ToString("F1");

        castReduction.baseValue = castReduction_Base + statB_C.CalculateBonus_CastReduction(_dex,_int,_luk);
        castReduction_Base_TMP.text = castReduction.baseValue.ToString("F2");

        //-----

    }

    private void UpdateSecondaryStat_RaiseByLuck()
    {
        int _str = Mathf.RoundToInt(strength.GetValue());
        int _int = Mathf.RoundToInt(intelligence.GetValue());
        int _vit = Mathf.RoundToInt(vitality.GetValue());
        int _dex = Mathf.RoundToInt(dexterity.GetValue());
        int _luk = Mathf.RoundToInt(luck.GetValue());

        magicalDamage.baseValue = magicalDamage_Base + statB_C.CalculateBonus_Damage_Magical(_int,_dex,_luk);
        magicalDamage_Base_TMP.text = magicalDamage.baseValue.ToString();

        criticalRate.baseValue = criticalRate_Base + statB_C.CalculateBonus_CriticalRate(_luk,_dex);
        criticalRate_Base_TMP.text = criticalRate.baseValue.ToString("F1");

        castReduction.baseValue = castReduction_Base + statB_C.CalculateBonus_CastReduction(_dex,_int,_luk);
        castReduction_Base_TMP.text = castReduction.baseValue.ToString("F2");

        evade_Chance.baseValue = evade_Chance_Base + statB_C.CalculateBonus_EvadeChance(_luk, _dex);
        evade_Chance_Base_TMP.text = evade_Chance.baseValue.ToString("F1");
        //-----

    }

    public void UpdateOtherClass_Values()
    {
        playerControlScript.playerSpeed = movementSpeed.GetValue();
    }



    // ----
    // ---- Modificadores de Estadisticas por Buffs/Debuffs -----

    public void ApplyModifiers(StatModifier modifier)
    {
        // Aplica un modificador externo como equipo o efectos de estado
        modifier.Apply(this);
    }

    public void RemoveModifiers(StatModifier modifier)
    {
        // Elimina el modificador al expirar el efecto o cambiar de equipo
        modifier.Remove(this);
    }
    // ----
    // ----
    // ----


    // ------------
    // -------- Metodos de cambios de Recursos [Health, Mana, Stamina] --------

    public void TakeDamage(float amount)
    {
        currentHealth.AddModifier(-amount);
        currentHealth.SetBaseValue(Mathf.Clamp(currentHealth.GetValue(), 0, maxHealth.GetValue()));
        //OnMaxHealthChanged?.Invoke();
        OnMaxHealthChanged_Smooth?.Invoke();
    }

    public void Heal(float amount)
    {
        currentHealth.AddModifier(amount);
        currentHealth.SetBaseValue(Mathf.Clamp(currentHealth.GetValue(), 0, maxHealth.GetValue()));
        //OnMaxHealthChanged?.Invoke();
        OnMaxHealthChanged_Smooth?.Invoke();
    }
    // ----
    // ----
    // ----


    //--------------
    //------ Method for Get (Equipment / Skills [Bonus]) ------
    //--------------

    //-------------- health

    public float GetEquipmentBonus_Health()
    {
        float equipmentHealthBonus = 0;
        return equipmentHealthBonus;
    }
    public float GetPassiveBonus_Health()
    {
        float passiveHealthBonus = 0;
        return passiveHealthBonus;
    }
    public float GetActiveBonus_Health()
    {
        float getActiveHealthBonus = 0;
        return getActiveHealthBonus;
    }

    //-------------- mana

    public float GetEquipmentBonus_Mana()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    public float GetPassiveBonus_Mana()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveBonus_Mana()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- sta

    public float GetEquipmentBonus_Stamina()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveBonus_Stamina()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveBonus_Stamina()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- health regen

    public float GetEquipment_RegenBonus_Health()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_RegenBonus_Health()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_RegenBonus_Health()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- regen mana

    public float GetEquipment_RegenBonus_Mana()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_RegenBonus_Mana()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_RegenBonus_Mana()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- regen sta

    public float GetEquipment_RegenBonu_Staminas()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_RegenBonus_Stamina()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_RegenBonus_Stamina()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- Physical

    public float GetEquipment_DamageBonus_Physical()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_DamageBonus_Physical()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_DamageBonus_Physical()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- Magical

    public float GetEquipment_DamageBonus_Magical()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_DamageBonus_Magical()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_DamageBonus_Magical()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- True damage

    public float GetEquipment_DamageBonus_True()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_DamageBonus_True()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_DamageBonus_True()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- AttackSpeed

    public float GetEquipment_AttackSpeedBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_AttackSpeedBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_AttackSpeedBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- Movement Speed

    public float GetEquipment_MovementSpeedBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_MovementSpeedBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_MovementSpeedBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- critial rate

    public float GetEquipment_CriticalRateBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_CriticalRateBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_CriticalRateBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- cast reduction

    public float GetEquipment_CastReductionBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_CastReductionBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_CastReductionBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- normal defense

    public float GetEquipment_NormalDefenseBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_NormalDefenseBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_NormalDefenseBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- Percent defense

    public float GetEquipment_PercentDefenseBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_PercentDefenseBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_PercentDefenseBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- normal shield

    public float GetEquipment_NormalShieldBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_NormalShieldBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_NormalShieldBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- percent shield

    public float GetEquipment_PercentShieldBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_PercentShieldBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_PercentShieldBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //-------------- evade chance

    public float GetEquipment_EvadeChanceBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetPassiveSkill_EvadeChanceBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }
    public float GetActiveSkill_EvadeChanceBonus()
    {
        float finalBonus = 0;
        return finalBonus;
    }

    //--------------
    //--------------
    //--------------

}

