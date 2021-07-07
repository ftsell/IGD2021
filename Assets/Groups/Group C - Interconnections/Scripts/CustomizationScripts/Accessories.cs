using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessories : MonoBehaviour
{
    public MeshFilter hatShape;
    public List<Mesh> options = new List<Mesh>();

    private int currentOption = 0;

    public void NextOption()
    {
        currentOption++;
        if(currentOption >= options.Count)
        {
            currentOption = 0;
        }
        hatShape.mesh = options[currentOption];
        

    }

    public void PreviousOption()
    {
        currentOption--;
        if(currentOption < 0)
        {
            currentOption = options.Count - 1;
        }
        hatShape.mesh = options[currentOption];
    }

    public void Randomize()
    {
        currentOption = Random.Range(0, options.Count - 1);
        hatShape.mesh = options[currentOption];
    }

    public Mesh GetCurrentSelection()
    {
        return options[currentOption];
    }
}
