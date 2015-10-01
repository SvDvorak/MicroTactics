using System.Linq;
using BehaviourMachine;
using UnityEngine;

namespace Assets.Testing
{
    [NodeInfo(category = "Decorator/", icon = "PlayLoopOff")]
    public class FindGameObjectsByPartialName : DecoratorNode
    {
        public StringVar ObjectName;
        public GameObjectVar StoreGameObject;

        public override Status Update()
        {
            var objectsWithName = GameObject.FindObjectsOfType<GameObject>().Where(x => x.name.Contains(ObjectName.Value));
            foreach (var gameObject in objectsWithName)
            {
                StoreGameObject.Value = gameObject;
                child.OnTick();
            }

            return Status.Success;
        }
    }
}