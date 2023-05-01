using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Nodes;
using CleverCrow.Fluid.Dialogues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CleverCrow.Fluid.Dialogues.Actions.Databases;
using CleverCrow.Fluid.Databases;
using Assets.Scripts.StorySystem;
using Unity.VisualScripting;

[CreateMenu("Action/RemoveSpecialCargo")]
public class RemoveSpecialCargoAction : ActionDataBase
{
    // [field: SerializeField]
    // public Location Target { get; set; }
    // public string Name { get; set; }

    public override ActionStatus OnUpdate()
    {
        // Debug.Log($"Acquired {Target}");
        CargoState.RemoveSpecialCargo();
        return base.OnUpdate();
    }
}