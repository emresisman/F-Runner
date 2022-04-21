using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace emresisman.Assets.Scripts
{
    public class JumpingState : State
    {
        private bool grounded;
        private bool diving;
        //private int jumpParam = Animator.StringToHash("Jump");
        //private int landParam = Animator.StringToHash("Land");

        public JumpingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //SoundManager.Instance.PlaySound(SoundManager.Instance.jumpSounds);
            grounded = false;
            diving = false;
            Jump();
        }

        public override void HandleInput()
        {
            base.HandleInput();
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
                //character.TriggerAnimation(landParam);
                //SoundManager.Instance.PlaySound(SoundManager.Instance.landing);
                stateMachine.ChangeState(player._running);
            }
            if (diving)
            {
                stateMachine.ChangeState(player._diving);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            grounded = player.CheckCollisionOverlap(player.transform.position);
        }

        private void Jump()
        {
            player.transform.Translate(Vector2.up * (player.CollisionOverlapRadius + 0.1f));
            player.ApplyImpulse(Vector2.up * player.JumpForce);
            //player.TriggerAnimation(jumpParam);
        }
    }
}
