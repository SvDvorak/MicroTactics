using System.Collections.Generic;
using System.Linq;
using BehaviourMachine;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Assets.Testing
{
    [NodeInfo(category = "Test/")]
    public class IsChildOfAssertion : ActionNode
    {
        public GameObjectVar Parent;
        public GameObjectVar Child;

        public override Status Update()
        {
            var actualChildren = Parent.Value.GetChildren().ToList();
            var isChildOfParent = actualChildren.Contains(Child.Value);
            isChildOfParent.MustBeTrue(string.Format("{0} should be child of {1}\nActual children: {2}", Child.Value, Parent.Value, EnumerateChildren(actualChildren)));
            return Status.Success;
        }

        private static string EnumerateChildren(List<GameObject> actualChildren)
        {
            return string.Join("", actualChildren.Select(x => "\n   " + x.ToString()).ToArray());
        }
    }
}