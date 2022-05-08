using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FRunner.States
{
    public class DeathState : State
    {
        public DeathState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            Debug.Log("You Died.");
        }

        public override void LogicUpdate()
        {
            
        }

        public override void PhysicsUpdate()
        {

        }
    }
}