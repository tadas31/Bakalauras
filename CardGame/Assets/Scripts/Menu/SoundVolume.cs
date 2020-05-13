using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundVolume : MonoBehaviour
{

    public TextMeshProUGUI volumeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onIncreasePress()
    {
        int volume = int.Parse(volumeText.text);
        if (volume < 100)
        {
            volume++;
            volumeText.text = volume.ToString();
        }
    }

    public void onDecreasePress()
    {
        int volume = int.Parse(volumeText.text);
        if (volume > 0)
        {
            volume--;
            volumeText.text = volume.ToString();
        }
           
    }
}
