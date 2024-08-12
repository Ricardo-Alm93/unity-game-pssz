using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;

    //-------- Keybind Variables--------------
    // UI para mostrar y capturar las teclas actuales
    public TMP_InputField upKeyField, downKeyField, leftKeyField, rightKeyField;
    public TMP_InputField lightAttackKeyField, heavyAttackKeyField, specialAttackKeyField, defenseKeyField, dashKeyField;
    //public Button restoreDefaultsButton;

    // Nombres de las teclas
    private string upKey = "upKey", downKey = "downKey", leftKey = "leftKey", rightKey = "rightKey";
    private string lightAttackKey = "lightAttackKey", heavyAttackKey = "heavyAttackKey", specialAttackKey = "specialAttackKey";
    private string defenseKey = "defenseKey", dashKey = "dashKey";

    private TMP_InputField activeInputField = null;
    private bool isCapturingKey = false;

    //------ para actualziar teclas en otras clasess
    public delegate void KeyBindingChanged();
    public static event KeyBindingChanged OnKeyBindingChanged;


    Resolution[] resolutions;

    void Start() 
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        
        List<String> resolutionOptions_ = new List<String>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions_.Add(resolutionOption);

            if(resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions_);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //----- keybind start parameters
        // Cargar las teclas guardadas
        // Cargar teclas guardadas o asignar las por defecto
        LoadKeyBindings();
        SetAllFieldsReadOnly(true);


        // Add listeners for input fields
        AddFieldListeners(upKeyField);
        AddFieldListeners(downKeyField);
        AddFieldListeners(leftKeyField);
        AddFieldListeners(rightKeyField);

        AddFieldListeners(lightAttackKeyField);
        AddFieldListeners(heavyAttackKeyField);
        AddFieldListeners(specialAttackKeyField);
        AddFieldListeners(defenseKeyField);
        AddFieldListeners(dashKeyField);


        // Add a listener for "Enter" key to start capturing
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Update()
    {
        // Check for "Enter" key press to start capturing mode
        if (Input.GetMouseButtonDown(0)) // Input.GetKeyDown(KeyCode.Return) || 
        {
            if (activeInputField != null)
            {
                StartCapturing();
            }
        }
    }

    //------- Sound Configuration ---------

    public void Set_Volume(float volume)
    {
        audioMixer.SetFloat("VolumeMaster_Param", volume);
    }

    //------- Resolution Configuration---------

    public void Set_Quality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void Set_FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void Set_Resolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //---------------- Keybind Functions ------------

    void LoadKeyBindings()
    {
        upKeyField.text = PlayerPrefs.GetString(upKey, "W");
        downKeyField.text = PlayerPrefs.GetString(downKey, "S");
        leftKeyField.text = PlayerPrefs.GetString(leftKey, "A");
        rightKeyField.text = PlayerPrefs.GetString(rightKey, "D");

        lightAttackKeyField.text = PlayerPrefs.GetString(lightAttackKey, "H");
        heavyAttackKeyField.text = PlayerPrefs.GetString(heavyAttackKey, "J");
        specialAttackKeyField.text = PlayerPrefs.GetString(specialAttackKey, "K");
        defenseKeyField.text = PlayerPrefs.GetString(defenseKey, "L");
        dashKeyField.text = PlayerPrefs.GetString(dashKey, "Space");
    }

    void AddFieldListeners(TMP_InputField inputField)
    {
        inputField.onSelect.AddListener(delegate { OnFieldSelected(inputField); });
        inputField.onDeselect.AddListener(delegate { OnFieldDeselected(inputField); });
        inputField.onEndEdit.AddListener(delegate { OnFieldEndEdit(inputField); });
    }

    void OnFieldSelected(TMP_InputField inputField)
    {
        // Set the currently active input field
        activeInputField = inputField;
    }

    void OnFieldDeselected(TMP_InputField inputField)
    {
        // If the Enter key has not been pressed, revert the field to read-only
        if (activeInputField == inputField && !isCapturingKey)
        {
            inputField.readOnly = true;
            activeInputField = null;
        }
    }

    void OnFieldEndEdit(TMP_InputField inputField)
    {
        // When editing ends, reset capturing mode
        if (isCapturingKey)
        {
            inputField.readOnly = true;
            isCapturingKey = false;
            activeInputField = null;
        }
    }

    void StartCapturing()
    {
        if (activeInputField != null && !isCapturingKey)
        {
            isCapturingKey = true;
            activeInputField.readOnly = false;
            StartCoroutine(CaptureKey(activeInputField));
        }
    }

    private System.Collections.IEnumerator CaptureKey(TMP_InputField inputField)
    {
        inputField.text = ""; // Clear existing text
        yield return null; // Wait for the next frame

        bool keyCaptured = false;

        while (!keyCaptured)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode) && keyCode != KeyCode.Return && keyCode != KeyCode.Escape)
                {
                    string keyString = keyCode.ToString();
                    inputField.text = keyString;
                    PlayerPrefs.SetString(GetKeyPref(inputField), keyString);
                    PlayerPrefs.Save();
                    keyCaptured = true;
                    break;
                }
            }

            yield return null; // Wait for the next frame
        }

        inputField.readOnly = true; // Set the input field back to read-only
        isCapturingKey = false; // Reset capturing state
        activeInputField = null; // Clear the active field

        OnKeyBindingChanged?.Invoke();
    }

    string GetKeyPref(TMP_InputField inputField)
    {
        if (inputField == upKeyField) return upKey;
        if (inputField == downKeyField) return downKey;
        if (inputField == leftKeyField) return leftKey;
        if (inputField == rightKeyField) return rightKey;
        if (inputField == lightAttackKeyField) return lightAttackKey;
        if (inputField == heavyAttackKeyField) return heavyAttackKey;
        if (inputField == specialAttackKeyField) return specialAttackKey;
        if (inputField == defenseKeyField) return defenseKey;
        if (inputField == dashKeyField) return dashKey;
        return null;
    }

    public void Set_RestoreDefaults()
    {
        upKeyField.text = "W";
        downKeyField.text = "S";    
        leftKeyField.text = "A";
        rightKeyField.text = "D";

        lightAttackKeyField.text = "H";
        heavyAttackKeyField.text = "J";
        specialAttackKeyField.text = "K";
        defenseKeyField.text = "L";
        dashKeyField.text = "Space";

        PlayerPrefs.SetString(upKey, "W");
        PlayerPrefs.SetString(downKey, "S");
        PlayerPrefs.SetString(leftKey, "A");
        PlayerPrefs.SetString(rightKey, "D");

        PlayerPrefs.SetString(lightAttackKey, "H");
        PlayerPrefs.SetString(heavyAttackKey, "J");
        PlayerPrefs.SetString(specialAttackKey, "K");
        PlayerPrefs.SetString(defenseKey, "L");
        PlayerPrefs.SetString(dashKey, "Space");
    }

    void SetAllFieldsReadOnly(bool readOnly)
    {
        upKeyField.readOnly = readOnly;
        downKeyField.readOnly = readOnly;
        leftKeyField.readOnly = readOnly;
        rightKeyField.readOnly = readOnly;

        lightAttackKeyField.readOnly = readOnly;
        heavyAttackKeyField.readOnly = readOnly;
        specialAttackKeyField.readOnly = readOnly;
        defenseKeyField.readOnly = readOnly;
        dashKeyField.readOnly = readOnly;
    }
}
