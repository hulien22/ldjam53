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

[CreateMenu("Action/UpgradeThrust")]
public class UpgradeThrustAction : ActionDataBase {
    [SerializeField]
    private KeyValueDefinitionInt variable;
    [SerializeField]
    private int value;

    public override ActionStatus OnUpdate() {
        GlobalDatabaseManager.Instance.Database.Ints.Set(variable.Key, value);
        UpgradeManager.SetThrust(value);
        return base.OnUpdate();
    }
}