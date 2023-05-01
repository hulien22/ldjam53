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

[CreateMenu("Action/RemoveCargo")]
public class RemoveCargoAction : SetLocalVariableBase<int> {
    [SerializeField]
    private KeyValueDefinitionInt variable;
    [SerializeField]
    private int credits;

    protected override KeyValueDefinitionBase<int> Variable => variable;

    protected override IKeyValueData<int> GetDatabase(IDialogueController dialogue) {
        return GlobalDatabaseManager.Instance.Database.Ints;
    }

    public override ActionStatus OnUpdate() {
        CargoState.RemovePackage();
        //CargoState.AddMoney(credits);
        return base.OnUpdate();
    }
}