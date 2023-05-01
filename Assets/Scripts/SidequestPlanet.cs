using Assets.Scripts.StorySystem;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Assets.Scripts.StorySystem.JobsGenerator;

public class SidequestPlanet : MonoBehaviour {
    [SerializeField] private Location[] easy;
    [SerializeField] private Location[] medium;
    [SerializeField] private Location[] hard;
    [field: SerializeField] public string SidequestName { get; private set; }
    [field: SerializeField] public string SidequestItemName { get; private set; }
    [field: SerializeField] public KeyValueDefinitionInt SidequestItem { get; private set; }
    [field: SerializeField] public ActorDefinition Actor { get; private set; }
    
    public Location GetJobTarget(JobsGenerator.Difficulty difficulty) {
        switch (difficulty) {
            case Difficulty.Medium:
                return medium[Random.Range(0, medium.Length)];
            case Difficulty.Hard:
                return hard[Random.Range(0, hard.Length)];
            default:
                return easy[Random.Range(0, easy.Length)];
        }
    }

    public Difficulty GetDifficultyOfJob(Location target) {
        if (easy.Contains(target)) return Difficulty.Easy;
        if (medium.Contains(target)) return Difficulty.Medium;
        if (hard.Contains(target)) return Difficulty.Hard;
        Debug.LogError("Got difficulty of side job that is not known.");
        return Difficulty.Easy;
    }
}
