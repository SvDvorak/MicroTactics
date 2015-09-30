using BehaviourMachine;

namespace BehaviourMachine
{
    public class TestSuccess : StateBehaviour
    {
        public void Start()
        {
            IntegrationTest.Pass();
        }
    }
}