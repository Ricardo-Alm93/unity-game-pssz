using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ExperienceCalculationMode
{
    UseAnimationCurve,
    UseGrowFactor,
    UseMinMaxExperience,
    UseSetExperienceByLevels
}

[System.Serializable]
public class GrowFactorRange
{
    public int minLevel;
    public int maxLevel;
    public float growFactor;
}

[System.Serializable]
public class GrowFactorRangeList
{
    public string listName; // Un nombre para identificar cada lista de rangos
    public List<GrowFactorRange> growFactors = new List<GrowFactorRange>();
}

[System.Serializable]
public class LevelExperienceData
{
    public int targetLevel;
    public int setExperience;
}

public class PlayerLevel : MonoBehaviour
{
    public Slider expSlider;
    public int currentLevel = 1;
    public int currentExperience = 0;
    public int experienceToNextLevel;
    public int statPoints = 0;

    public TMP_Text playerLevel_text;
    public TMP_Text playerExperience_text;

    public ExperienceCalculationMode calculationMode; // El enum para seleccionar el método
    //public bool useAnimationCurve = false; // Booleano para seleccionar el método de cálculo
    //public bool useGrowFactor = false; // Booleano para usar el factor de crecimiento
    //public bool useMinMaxExperience = false; // Booleano para usar el rango de experiencia
    //public bool useSetExperienceByLevels = false; // Booleano para usar la experiencia por niveles

    public AnimationCurve experienceCurve;
    //public float growFactor = 1.2f; // Factor de crecimiento para el método basado en crecimiento

    public int minExperience = 100; // Experiencia mínima para nivel 1
    public int maxExperience = 10000; // Experiencia máxima para nivel 99

    public float levelUp_Delay = 0.2f; // Retardo entre subidas de nivel
    public float smoothExperienceValue;

    public List<GrowFactorRangeList> growFactorRangeLists = new List<GrowFactorRangeList>();
    public List<LevelExperienceData> levelExperienceList;

    private Queue<int> experienceQueue = new Queue<int>(); // Cola de experiencia
    private Coroutine experienceProcessingCoroutine;

    // Variables para la experiencia total ganada, gastada y por gastar
    public int totalExperienceGained = 0;
    public int totalExperienceSpent = 0;
    public int totalExperienceRemaining = 0;
    private int lastExperienceToNextLevel; // Nueva variable para almacenar la experiencia anterior necesaria para el siguiente nivel
    public PlayerStats playerStats;


    void Start()
    {
        UpdateExperienceToNextLevel();
        UpdateTotalExperienceRemaining();
        playerLevel_text.text = "Level: " + currentLevel.ToString();
        Experience_Text();
        UpdateExperienceSlider();  // Actualiza la barra de experiencia
    }

    public void GainExperience(int amount)
    {
        totalExperienceGained += amount; // Incrementa la experiencia total ganada
        experienceQueue.Enqueue(amount); // Añade la experiencia a la cola

        // Solo comienza la corutina si no hay una corriendo actualmente
        if (experienceProcessingCoroutine == null)
        {
            experienceProcessingCoroutine = StartCoroutine(ProcessExperienceQueue());
        }

        UpdateTotalExperienceRemaining(); // Actualiza la experiencia restante por gastar
    }

    private IEnumerator SmoothExperienceTransition(int targetExperience, float duration)
    {
        float elapsedTime = 0f;
        int startExperience = currentExperience;

        // Almacena los valores iniciales y finales del slider
        float startSliderValue = expSlider.value;
        float targetSliderValue = Mathf.Clamp01((float)targetExperience / experienceToNextLevel);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Interpola el valor de la experiencia y el slider
            currentExperience = Mathf.RoundToInt(Mathf.Lerp(startExperience, targetExperience, t));  // Conversión explícita a int
            expSlider.value = Mathf.Lerp(startSliderValue, targetSliderValue, t);

            // Actualiza el texto de experiencia
            playerExperience_text.text = Mathf.RoundToInt(currentExperience).ToString() + " / " + experienceToNextLevel.ToString();

            yield return null; // Espera hasta el próximo frame
        }

        // Asegura que al final de la transición, los valores sean exactos
        currentExperience = targetExperience;
        expSlider.value = targetSliderValue;

        // Actualiza el texto de experiencia con los valores finales
        playerExperience_text.text = currentExperience.ToString() + " / " + experienceToNextLevel.ToString();
    }

    private IEnumerator ProcessExperience(int amount)
    {
        int targetExperience = currentExperience + amount;

        while (targetExperience >= experienceToNextLevel)
        {
            targetExperience -= experienceToNextLevel;
            totalExperienceSpent += experienceToNextLevel; // Incrementa la experiencia total gastada
            yield return StartCoroutine(SmoothExperienceTransition(experienceToNextLevel, smoothExperienceValue)); // Transición suave antes de subir de nivel
            LevelUp();
            UpdateExperienceToNextLevel(); // Actualiza la experiencia para el siguiente nivel
            Experience_Text();
            yield return new WaitForSeconds(levelUp_Delay); // Retardo entre subidas de nivel
        }

        // Transición suave para la experiencia restante después de la subida de nivel
        yield return StartCoroutine(SmoothExperienceTransition(targetExperience, 0.5f));
        UpdateTotalExperienceRemaining(); // Actualiza la experiencia restante por gastar
    }

    private IEnumerator ProcessExperienceQueue()
    {
        while (experienceQueue.Count > 0)
        {
            int amount = experienceQueue.Dequeue(); // Toma la experiencia de la cola

            yield return StartCoroutine(ProcessExperience(amount)); // Procesa la experiencia de manera suave

            yield return new WaitForSeconds(levelUp_Delay); // Espera antes de procesar la siguiente ganancia de experiencia
        }

        experienceProcessingCoroutine = null; // Indica que no hay corutina en ejecución
    }

    private void LevelUp()
    {
        if (currentLevel < 99) // Limita el nivel máximo
        {
            currentLevel++;
            statPoints += CalculateStatPoints(currentLevel);
            playerStats.statPoints += CalculateStatPoints(currentLevel);
            playerStats.Update_Available_Stats_Points_In_UI();
            playerStats.UpdateAll_Stats_SecondaryValues_Preview();
            UpdateExperienceToNextLevel(); // Actualiza la experiencia para el siguiente nivel
            playerLevel_text.text = "Level: " + currentLevel.ToString();
        }
    }

    private void UpdateExperienceSlider()
    {
        // Calcula el valor relativo de la experiencia actual en relación con la experiencia necesaria para el siguiente nivel
        float experienceRatio = (float)currentExperience / experienceToNextLevel;

        // Asegura que el slider no sobrepase el valor máximo (1.0)
        expSlider.value = Mathf.Clamp01(experienceRatio);
    }

    private void UpdateExperienceToNextLevel()
    {
        lastExperienceToNextLevel = experienceToNextLevel; // Almacena el valor anterior
        experienceToNextLevel = CalculateExperienceForLevel(currentLevel + 1);
    }

    private int CalculateExperienceForLevel(int level)
    {
        if (level > 99) level = 99; // Limita el nivel máximo

        switch (calculationMode)
        {
            case ExperienceCalculationMode.UseAnimationCurve:
                // Normaliza el nivel en el rango de 0 a 1
                float normalizedLevel = Mathf.InverseLerp(1, 99, level);
                // Usa la curva para obtener la experiencia necesaria
                float experience = experienceCurve.Evaluate(normalizedLevel);
                // Escala la experiencia a los valores mínimo y máximo
                experience = Mathf.Lerp(minExperience, maxExperience, experience);
                return Mathf.RoundToInt(experience);
            //break;
            case ExperienceCalculationMode.UseGrowFactor: // linear interpolation and -Experience Required Multiplier-
                // Método basado en crecimiento con rangos
                float baseExperience = Mathf.Lerp(minExperience, maxExperience, (float)(level - 1) / 98f);

                // Verifica que la lista de rangos no esté vacía y tenga elementos válidos
                if (growFactorRangeLists != null && growFactorRangeLists.Count > 0)
                {
                    foreach (var rangeList in growFactorRangeLists)
                    {
                        if (rangeList.growFactors != null && rangeList.growFactors.Count > 0)
                        {
                            foreach (var range in rangeList.growFactors)
                            {
                                if (level >= range.minLevel && level <= range.maxLevel)
                                {
                                    baseExperience *= range.growFactor;
                                    break; // Aplica solo el primer rango que coincida
                                }
                            }
                        }
                    }
                }

                // Asegura que la experiencia esté dentro de los límites
                float experience1 = Mathf.Clamp(baseExperience, minExperience, maxExperience);
                return Mathf.RoundToInt(experience1);

            case ExperienceCalculationMode.UseMinMaxExperience: // linear interpolation
                float experience_level2 = minExperience;
                float experience_level99 = maxExperience;

                // Interpolación lineal desde el nivel 2 al 99
                float experience0 = experience_level2 + ((level - 2) / 97f) * (experience_level99 - experience_level2);
                return Mathf.RoundToInt(experience0);

            case ExperienceCalculationMode.UseSetExperienceByLevels:
                LevelExperienceData data = levelExperienceList.Find(x => x.targetLevel == level);
                if (data != null)
                {
                    return data.setExperience;
                }
                else
                {
                    Debug.LogWarning("No experience set for this level in the list. Using the last experience value.");
                    return lastExperienceToNextLevel; // Usa la última experiencia a nivel de siguiente nivel
                }
            default:
                Debug.LogError("Experience calculation mode not recognized. Returning 0 as a fallback.");
                return 0; // Valor por defecto en caso de que el modo no sea reconocido
        }
    }


        //---------------------------------

        //if (useSetExperienceByLevels)
        //{
        //    LevelExperienceData data = levelExperienceList.Find(x => x.targetLevel == level);
        //    if (data != null)
        //    {
        //        return data.setExperience;
        //    }
        //    else
        //    {
        //        Debug.LogWarning("No experience set for this level in the list. Using the last experience value.");
        //        return lastExperienceToNextLevel; // Usa la última experiencia a nivel de siguiente nivel
        //    }
        //}
        //else if (useAnimationCurve)
        //{
        //    // Normaliza el nivel en el rango de 0 a 1
        //    float normalizedLevel = Mathf.InverseLerp(1, 99, level);
        //    // Usa la curva para obtener la experiencia necesaria
        //    float experience = experienceCurve.Evaluate(normalizedLevel);
        //    // Escala la experiencia a los valores mínimo y máximo
        //    experience = Mathf.Lerp(minExperience, maxExperience, experience);
        //    return Mathf.RoundToInt(experience);
        //}
        //else if (useGrowFactor)
        //{
        //    // Método basado en crecimiento con rangos
        //    float baseExperience = Mathf.Lerp(minExperience, maxExperience, (float)(level - 1) / 98f);
        //
        //    // Verifica que la lista de rangos no esté vacía y tenga elementos válidos
        //    if (growFactorRangeLists != null && growFactorRangeLists.Count > 0)
        //    {
        //        foreach (var rangeList in growFactorRangeLists)
        //        {
        //            if (rangeList.growFactors != null && rangeList.growFactors.Count > 0)
        //            {
        //                foreach (var range in rangeList.growFactors)
        //                {
        //                    if (level >= range.minLevel && level <= range.maxLevel)
        //                    {
        //                        baseExperience *= range.growFactor;
        //                        break; // Aplica solo el primer rango que coincida
        //                    }
        //                }
        //            }
        //        }
        //    }
        //
        //    // Asegura que la experiencia esté dentro de los límites
        //    float experience = Mathf.Clamp(baseExperience, minExperience, maxExperience);
        //    return Mathf.RoundToInt(experience);
        //}
        //else if (useMinMaxExperience)
        //{
        //    // Usa un rango entre experiencia mínima y máxima
        //    float experience = Mathf.Lerp(minExperience, maxExperience, (float)(level - 1) / 98f);
        //    return Mathf.RoundToInt(experience);
        //}
        //else
        //{
        //    // Método por defecto
        //    return minExperience;
        //}

    public void Experience_Text()
    {
        UpdateExperienceToNextLevel();
        playerExperience_text.text = currentExperience.ToString() + " / " + experienceToNextLevel.ToString();
    }

    private int CalculateStatPoints(int level)
    {
        if (level <= 10)
        {
            return 12;
        }
        else if (level <= 50)
        {
            return 18;
        }
        else if (level <= 90)
        {
            return 20;
        }
        else
        {
            return 30;
        }
        // 1560 at level 99
    }

    private void UpdateTotalExperienceRemaining()
    {
        totalExperienceRemaining = totalExperienceGained - totalExperienceSpent;
    }
}
