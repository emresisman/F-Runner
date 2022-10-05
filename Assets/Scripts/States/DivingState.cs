using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FRunner.States
{
    public class DivingState : State
    {
        private bool grounded;

        public DivingState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            grounded = false;
            Dive();
        }

        public override void Exit()
        {
            player.SetAnimationBool(diveParam, false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (grounded)
            {
                stateMachine.ChangeState(player.Running);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            grounded = player.IsGrounded();
        }

        private void Dive()
        {
            player.ApplyDiveForce(Vector2.down * player.DiveForce);
            player.SetAnimationBool(diveParam, true);
        }
    }
}