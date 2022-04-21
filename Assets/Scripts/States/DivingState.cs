using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace emresisman.Assets.Scripts
{
    public class DivingState : State
    {
        private bool belowCeiling;
        private bool crouchHeld;

        public DivingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void HandleInput()
        {
            base.HandleInput();

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }
    }
}
