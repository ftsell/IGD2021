using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFace : MonoBehaviour
{
    
    public SkinnedMeshRenderer bodyPart;
    public List<Material> options = new List<Material>();

    private int currentOption = 0;

    public void NextOption()
    {
        currentOption++;
        if(currentOption >= options.Count)
        {
            currentOption = 0;
        }
        bodyPart.material = options[currentOption];
        

    }

    public void PreviousOption()
    {
        currentOption--;
        if(currentOption < 0)
        {
            currentOption = options.Count - 1;
        }
        bodyPart.material = options[currentOption];
    }

    public void Randomize()
    {
        currentOption = Random.Range(0, options.Count - 1);
        bodyPart.material = options[currentOption];
    }

    public Material GetCurrentSelection ()
    {
        return options[currentOption];
    }
}
