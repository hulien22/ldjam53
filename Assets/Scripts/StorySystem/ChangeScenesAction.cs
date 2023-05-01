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

[CreateMenu("Action/ChangeScenes")]
public class ChangeScenesAction : ActionDataBase
{
    [field: SerializeField]
    public string SceneName { get; set; }

    public override ActionStatus OnUpdate()
    {
        GlobalState.ChangeScenes(SceneName);
        return base.OnUpdate();
    }
}