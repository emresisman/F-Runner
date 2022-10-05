using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FRunner.States
{
    public class RunningState : State
    {
        private bool jump;

        public RunningState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            player.SetAnimationBool(runParam, true);
            jump = false;
        }

        public override void Exit()
        {
            player.SetAnimationBool(runParam, false);
        }

        public override void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded())
            {
                jump = true;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (jump)
            {
                stateMachine.ChangeState(player.Jumping);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}