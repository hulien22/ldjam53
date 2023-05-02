using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues;
using UnityEngine;
using CleverCrow.Fluid.Databases;

[CreateMenu("Action/SpendCredits")]
public class SpendCreditsAction : ActionDataBase {
    [SerializeField]
    private KeyValueDefinitionInt credits;
    [SerializeField]
    private int cost;

    public override ActionStatus OnUpdate() {
        int current = GlobalDatabaseManager.Instance.Database.Ints.Get(credits.Key, credits.defaultValue);
        int next = current - cost;
        GlobalDatabaseManager.Instance.Database.Ints.Set(credits.Key, next);
        CreditsText.SetText(next);
        return base.OnUpdate();
    }
}