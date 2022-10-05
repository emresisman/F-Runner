using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FRunner.States
{
    public class JumpingState : State
    {
        private bool grounded;
        private bool diving;

        public JumpingState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            grounded = false;
            diving = false;
            Jump();
        }

        public override void Exit()
        {
            player.SetAnimationBool(jumpParam, false);
        }

        public override void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                diving = true;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (grounded)
            {
                stateMachine.ChangeState(player.Running);
            }
            if (diving)
            {
                stateMachine.ChangeState(player.Diving);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            grounded = player.IsGrounded();
        }

        private void Jump()
        {
            player.transform.Translate(Vector2.up * (player.CollisionOverlapRadius));
            player.ApplyImpulse();
            player.SetAnimationBool(jumpParam, true);
        }
    }
}