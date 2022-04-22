using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace emresisman.Assets.Scripts
{
    public class DivingState : State
    {
        private bool grounded;

        public DivingState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            grounded = false;
            Dive();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (grounded)
            {
                stateMachine.ChangeState(player._running);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            grounded = player.CheckCollisionOverlap(player.transform.position);
        }

        private void Dive()
        {
            player.ApplyImpulse(Vector2.down * player.DiveForce);
        }
    }
}
