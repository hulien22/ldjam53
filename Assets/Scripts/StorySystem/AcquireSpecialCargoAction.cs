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

[CreateMenu("Action/AcquireSpecialCargo")]
public class AcquireSpecialCargoAction : ActionDataBase
{
    [field: SerializeField]
    public Location Target { get; set; }

    [field: SerializeField]
    public string Name { get; set; }

    public override ActionStatus OnUpdate()
    {
        Debug.Log($"Acquired {Target}");
        CargoState.AddSpecialCargo(LocationManager.GetLocation(), Target, Name);
        return base.OnUpdate();
    }
}