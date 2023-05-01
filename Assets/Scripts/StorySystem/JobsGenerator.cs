using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Assets.Scripts.StorySystem.JobsGenerator;

namespace Assets.Scripts.StorySystem {
    public class JobsGenerator : MonoBehaviour {
        public enum Difficulty {
            Easy,
            Medium,
            Hard
        }
        private static JobsGenerator instance;

        [SerializeField] private SidequestPlanet terrus;
        [SerializeField] private SidequestPlanet donatus;
        [SerializeField] private SidequestPlanet kantora;
        [SerializeField] private SidequestPlanet alban;
        [SerializeField] private SidequestPlanet scrapton;
        [SerializeField] private SidequestPlanet haldor;
        [SerializeField] private SidequestPlanet erabus;
        [SerializeField] private SidequestPlanet iris1;
        [SerializeField] private SidequestPlanet iris2;
        [SerializeField] private SidequestPlanet iris3;
        [SerializeField] private SidequestPlanet lunas;
        [SerializeField] private SidequestPlanet miram;
        [SerializeField] private SidequestPlanet taldoris;

        [SerializeField] private int easyReward;
        [SerializeField] private int mediumReward;
        [SerializeField] private int hardReward;

        private void Awake() {
            if (instance != null) {
                Debug.LogError("Duplicate audio manager detected.");
            }
            instance = this;
        }

        public static Location GetJob(Location currentPlanet, Difficulty difficulty) {
            SidequestPlanet planet = GetPlanet(currentPlanet);
            return planet.GetJobTarget(difficulty);
        }

        public static SidequestPlanet GetPlanet(Location location) {
            switch (location) {
                case Location.Terrus:
                    return instance.terrus;
                case Location.Donatus:
                    return instance.donatus;
                case Location.Kantora:
                    return instance.kantora;
                case Location.Alban:
                    return instance.alban;
                case Location.Scrapton:
                    return instance.scrapton;
                case Location.Haldor:
                    return instance.haldor;
                case Location.Erabus:
                    return instance.erabus;
                case Location.Iris1:
                    return instance.iris1;
                case Location.Iris2:
                    return instance.iris2;
                case Location.Iris3:
                    return instance.iris3;
                case Location.Lunas:
                    return instance.lunas;
                case Location.Miram:
                    return instance.miram;
                case Location.Taldoris:
                    return instance.taldoris;
                default:
                    Debug.LogError($"Got side quest from {location} which does not have side quests.");
                    return instance.terrus;
            }
        }

        public static ActorDefinition GetActor(Location planet) {
            return GetPlanet(planet).Actor;
        }

        public static string GetTextForJob(Location target) {
            return $"Delivery to {GetPlanet(target).SidequestName}.";
        }

        public static string GetTextForAlreadyHaveJob(Location start) {
            Debug.Log($"already have from {start}");
            KeyValueDefinitionInt item = GetPlanet(start).SidequestItem;
            Location target = (Location)GlobalDatabaseManager.Instance.Database.Ints.Get(item.Key, item.defaultValue);
            if (target == Location.None) {
                return $"Complete the job I just gave you first.";
            }
            Debug.Log($"already have to {target}");
            return $"You already have a job to {GetPlanet(target).SidequestName} from me.";
        }

        public static string GetTextForRequest(Location easy, Location medium, Location hard) {
            string easyName = GetPlanet(easy).SidequestName;
            string mediumName = GetPlanet(medium).SidequestName;
            string hardName = GetPlanet(hard).SidequestName;
            return $"There is an easier delivery to {easyName} for {instance.easyReward} credits, a standard job to {mediumName} for {instance.mediumReward}, and a harder job to {hardName} for {instance.hardReward}.";
        }

        public static string GetTextForAcceptance(Location target) {
            string name = GetPlanet(target).SidequestName;
            return $"I'll take the job to {name}.";
        }

        public static string GetTextForAcquired(Location target) {
            SidequestPlanet planet = GetPlanet(target);
            string planetName = planet.SidequestName;
            string itemName = planet.SidequestItemName;
            return $"New cargo acquired: {itemName} from {planetName}.";
        }

        public static Difficulty GetDifficultyOfJob(Location start, Location end) {
            SidequestPlanet planet = GetPlanet(start);
            return planet.GetDifficultyOfJob(end);
        }

        public static int GetReward(Difficulty difficulty) {
            switch (difficulty) {
                case Difficulty.Medium:
                    return instance.mediumReward;
                case Difficulty.Hard:
                    return instance.hardReward;
                default:
                    return instance.easyReward;
            }
        }
    }
}
