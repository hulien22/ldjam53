using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues;
using UnityEngine;
using CleverCrow.Fluid.Dialogues.Nodes;
using Unity.VisualScripting;
using CleverCrow.Fluid.Databases;

namespace Assets.Scripts.StorySystem {
    [CreateMenu("Action/Arrive")]
    public class ArriveAction : ActionDataBase {
        [SerializeField]
        private Location location;

        public override void OnStart() {
            LocationManager.SetLocation(location);
        }
    }
}
