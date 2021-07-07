using System.Collections.Generic;
using UnityEngine;


/**
 * Class used for updating the text on the status plane
 */ 
public class StatusPlane : MonoBehaviour
{
    public string description;
    public string title;

    TextMesh descriptionTextMesh;

    // will be updated by PhaseHandler
    public float timeLeft;
    public PhaseHandler.Phase phase;
    public PhaseHandler.Team leadingTeam;
    public int roundCount;

    // Start is called before the first frame update
    void Start()
    {
        descriptionTextMesh = GameObject.Find("DescriptionText").GetComponent<TextMesh>();
    }


    // Update is called once per frame
    void Update()
    {       
            phase = PhaseHandler.phase;
            roundCount = PhaseHandler.roundCount;
            timeLeft = PhaseHandler.timeLeft;
            leadingTeam = PhaseHandler.leadingTeam;
            title = $"{phase} Phase";
            descriptionTextMesh.text = $"{title}\n{description}";

            if (phase == PhaseHandler.Phase.Action)
            {
                description = "Fight!\n";
            }

            if (phase == PhaseHandler.Phase.Decision)
            {
                description = "Select opponent and weapon!";
                descriptionTextMesh.text += $"\nTime left: {timeLeft.ToString("F0")} seconds";
            }

            if (phase == PhaseHandler.Phase.End)
            {
                string roundWord = roundCount == 1 ? "round" : "rounds";
                descriptionTextMesh.text = $"Total time: {PhaseHandler.passedGameSeconds.ToString("F0")}\nGame ended after {roundCount} {roundWord}.\nTeam {leadingTeam} won!";
        }
    }
}
