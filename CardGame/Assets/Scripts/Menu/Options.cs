using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Options : MonoBehaviour
{
    public TMP_Dropdown resolutionsDorpdown;
    private List<Resolution> resolutions;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions.ToList();

        foreach (Resolution r in resolutions)
        {
            resolutionsDorpdown.options.Add(new TMP_Dropdown.OptionData() { text = r.width + "x" + r.height });
        }

        //resolutionsDorpdown.AddOptions(resolutions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void onAplyPress()
    {
        Resolution selecteResolution = resolutions[resolutionsDorpdown.value];

        Screen.SetResolution(selecteResolution.width, selecteResolution.height, Screen.fullScreen);
    }
}
