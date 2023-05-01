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

[CreateMenu("Action/AcquireCargo")]
public class AcquireCargoAction : ActionDataBase {
    [field: SerializeField]
    public Location Target {get; set;}

    public override ActionStatus OnUpdate() {
        KeyValueDefinitionInt locationItem = JobsGenerator.GetPlanet(LocationManager.GetLocation()).SidequestItem;
        Debug.Log($"Aquired {Target}");
        GlobalDatabaseManager.Instance.Database.Ints.Set(locationItem.Key, (int)Target);
        CargoState.AddPackage(LocationManager.GetLocation(), Target);
        return base.OnUpdate();
    }
}