using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    private const string RESOLUTION_KEY = "resolution";
    private const string WINDOW_MODE_KEY = "windowed";
    private const string MUSIC_KEY = "music";
    private const string EFFECTS_KEY = "effects";

    public TMP_Dropdown resolutionsDorpdown;
    public Toggle windowModeToggle;
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI effectsText;

    private List<Resolution> resolutions;

    // Start is called before the first frame update
    void Start()
    {
        // Gets all supported resolutions
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToList();
        foreach (Resolution r in resolutions)
        {
            resolutionsDorpdown.options.Add(new TMP_Dropdown.OptionData() { text = r.width + "x" + r.height});
        }


        // Gets saved values
        int togleValue = PlayerPrefs.GetInt(WINDOW_MODE_KEY, 0);
        resolutionsDorpdown.value = PlayerPrefs.GetInt(RESOLUTION_KEY, resolutions.Count);

        Resolution selecteResolution = resolutions[resolutionsDorpdown.value];

        // Sets resolution
        if (togleValue == 0)
        {
            Screen.SetResolution(selecteResolution.width, selecteResolution.height, true);
            windowModeToggle.isOn = false;
        }

        else
        {
            Screen.SetResolution(selecteResolution.width, selecteResolution.height, false);
            windowModeToggle.isOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Applies all changes
    /// </summary>
    public void onApplyPress()
    {
        PlayerPrefs.SetInt(RESOLUTION_KEY, resolutionsDorpdown.value);
        PlayerPrefs.SetInt(MUSIC_KEY, int.Parse(musicText.text));
        PlayerPrefs.SetInt(EFFECTS_KEY, int.Parse(effectsText.text));

        if (windowModeToggle.isOn)
            PlayerPrefs.SetInt(WINDOW_MODE_KEY, 1);
        else if (!windowModeToggle.isOn)
            PlayerPrefs.SetInt(WINDOW_MODE_KEY, 0);

        Resolution selecteResolution = resolutions[resolutionsDorpdown.value];

        Screen.SetResolution(selecteResolution.width, selecteResolution.height, !windowModeToggle.isOn);
    }

    /// <summary>
    /// Displays selected values
    /// </summary>
    public void displaySelectedValues()
    {
        resolutionsDorpdown.value = PlayerPrefs.GetInt(RESOLUTION_KEY, resolutions.Count);
        int togleValue = PlayerPrefs.GetInt(WINDOW_MODE_KEY, 0);
        musicText.text = PlayerPrefs.GetInt(MUSIC_KEY, 100).ToString();
        effectsText.text = PlayerPrefs.GetInt(EFFECTS_KEY, 100).ToString();

        // Sets resolution
        if (togleValue == 0)
        {
            windowModeToggle.isOn = false;
        }

        else
        {
            windowModeToggle.isOn = true;
        }
    }
}
