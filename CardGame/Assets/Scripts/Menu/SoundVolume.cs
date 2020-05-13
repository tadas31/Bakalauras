using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundVolume : MonoBehaviour
{

    public TextMeshProUGUI volumeText;

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
