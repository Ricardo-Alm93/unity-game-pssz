using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Player_StatBarType
{
    Health,
    Mana,
    Stamina,
    Experience
}

public class PlayerBar : MonoBehaviour
{
    public Player_StatBarType ResourceBarType;


    [Header("Bars Variables")]
    public Slider statSlider; // Referencia al componente Slider en Unity
    public Image statFillImage; // Referencia al componente de imagen dentro del Slider para modificar su color

    // Variables relacionadas al stat
    public float maxValue = 0f;
    private float currentValue;
    public float regenRate = 2f; // Tasa de regeneración del stat

    // Variables de texto de stat
    public TextMeshProUGUI statAmountText; // Para mostrar el valor actual del stat

    // Variable opcional para manejar regeneración automática
    public bool autoRegen = false;
    [Header("Bars Colours")]

    public Color color1_OverBar;
    public Color color2_MediumBar;
    public Color color3_minimumBar;
    [Header("Smooth Update Bars")]

    public float smooth_Value;

    public PlayerStats playerStatsRef;


    private Coroutine smoothUpdateCoroutine;


    private void OnEnable()
    {
        if (PlayerStats.Instance != null)
        {
            // Suscribirse al evento adecuado usando un método de tipo Invoke
            switch (ResourceBarType)
            {
                case Player_StatBarType.Health:
                    playerStatsRef.OnMaxHealthChanged += UpdateStatBar_Instant;
                    playerStatsRef.OnMaxHealthChanged_Smooth += UpdateStatBar_Smooth;
                    break;
                case Player_StatBarType.Mana:
                    playerStatsRef.OnMaxManaChanged += UpdateStatBar_Instant;
                    playerStatsRef.OnMaxManaChanged_Smooth += UpdateStatBar_Smooth;
                    break;
                case Player_StatBarType.Stamina:
                    playerStatsRef.OnMaxStaminaChanged += UpdateStatBar_Instant;
                    playerStatsRef.OnMaxStaminaChanged_Smooth += UpdateStatBar_Smooth;
                    break;
            }
        }
    }

    private void OnDisable()
    {
        switch (ResourceBarType)
        {
            case Player_StatBarType.Health:
                playerStatsRef.OnMaxHealthChanged -= UpdateStatBar_Instant;
                break;
            case Player_StatBarType.Mana:
                playerStatsRef.OnMaxManaChanged -= UpdateStatBar_Instant;
                break;
            case Player_StatBarType.Stamina:
                playerStatsRef.OnMaxStaminaChanged -= UpdateStatBar_Instant;
                break;
        }
    }

    void Update() //------------------ UPDATE ---------------------------
    {
        // Regeneración automática del stat
        if (autoRegen && currentValue < maxValue)
        {
            currentValue += regenRate * Time.deltaTime;
            currentValue = Mathf.Clamp(currentValue, 0, maxValue);
            UpdateStatBar_Instant();
            print("pasando por update");
        }

    }

    private (float currentValue, float maxValue, string valName) GetStatValues_Previews() // hacer un
    {
        switch (ResourceBarType)
        {
            case Player_StatBarType.Health:
                return (playerStatsRef.currentHealth.GetValue(), playerStatsRef.health_Base_preview, "Health");
            case Player_StatBarType.Mana:
                return (playerStatsRef.currentMana.GetValue(), playerStatsRef.mana_Base_preview, "Mana");
            case Player_StatBarType.Stamina:
                return (playerStatsRef.currentStamina.GetValue(), playerStatsRef.stamina_Base_preview, "Stamina");
            default:
                return (0f, 1f, ""); // Valores por defecto
        }
    }
    //--------------------------------
    private (float currentValue, float maxValue, string valName) GetStatValues() // hacer un
    {
        switch (ResourceBarType)
        {
            case Player_StatBarType.Health:
                return (playerStatsRef.currentHealth.GetValue(), playerStatsRef.maxHealth.GetValue(), "Health");
            case Player_StatBarType.Mana:
                return (playerStatsRef.currentMana.GetValue(), playerStatsRef.maxMana.GetValue(), "Mana");
            case Player_StatBarType.Stamina:
                return (playerStatsRef.currentStamina.GetValue(), playerStatsRef.maxStamina.GetValue(), "Stamina");
            default:
                return (0f, 1f, ""); // Valores por defecto
        }
    }
    //------------- Instans -Status Bar Update -----------
    private void UpdateStatBar_Instant()
    {
        var (currentValue_same, maxValueUpdate, valName_2) = GetStatValues_Previews();
        var (currentValue, maxValue, valName_1) = GetStatValues();
        statSlider.value = currentValue / maxValue;
        if (maxValueUpdate > 0)
        {
            statAmountText.text = currentValue + "/" +Mathf.RoundToInt(maxValue + maxValueUpdate).ToString();
        }
        else
        {
            statAmountText.text = currentValue + "/" + Mathf.RoundToInt(maxValue).ToString();
        }
        //  statAmountText.text = Mathf.RoundToInt(currentValue + ).ToString();
        statFillImage.color = color1_OverBar;
        //print("current_" + valName_1 + " ="+ currentValue +" , max_" + valName_1 + " =" + + maxValue);
    }

    //------------- Smooth - Status Bar Update -----------

    private void UpdateStatBar_Smooth()
    {
        var (currentValue, maxValue, valName) = GetStatValues();
        SmoothUpdateBar(currentValue, maxValue, smooth_Value);
        //print("current_" + valName + " =" + currentValue + " , max_" + valName + " =" + +maxValue + " _SMOOTH_TRANSITION");
    }
    public void SmoothUpdateBar(float newValue, float maxValue, float duration)
    {
        if (smoothUpdateCoroutine != null)
        {
            StopCoroutine(smoothUpdateCoroutine);
        }
        smoothUpdateCoroutine = StartCoroutine(SmoothUpdateCoroutine(newValue, maxValue, duration));
    }

    private IEnumerator SmoothUpdateCoroutine(float currentValue, float maxValue, float duration)
    {
        float elapsedTime = 0f;
        float startingValue = statSlider.value;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startingValue, currentValue / maxValue, elapsedTime / duration);
            statSlider.value = newValue;
            statAmountText.text = Mathf.RoundToInt(newValue * maxValue).ToString();
            yield return null;
        }

        statSlider.value = currentValue / maxValue;
        statAmountText.text = Mathf.RoundToInt(currentValue).ToString();
    }

    private void ApplyEffects()
    {
        // Ejemplo de parpadeo cuando el stat está por debajo del 25%
        if (currentValue / maxValue <= 0.25f)
        {
            float alpha = Mathf.PingPong(Time.time * 2, 1);
            statFillImage.color = new Color(statFillImage.color.r, statFillImage.color.g, statFillImage.color.b, alpha);
        }
    }

}
