using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(!SoundManager.Instacne.BGMPlayeState())
            SoundManager.Instacne.TurnOnStageBGM(EnumTypes.StageBGM.Title);
        }
    }
}
