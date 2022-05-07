using System.Collections;
using UnityEngine;

namespace FRunner.States
{
    public abstract class State
    {
        protected Player player;
        protected StateMachine stateMachine;

        protected int jumpParam = Animator.StringToHash("Jump");
        protected int runParam = Animator.StringToHash("Run");
        protected int diveParam = Animator.StringToHash("Dive");

        protected State(Player player, StateMachine stateMachine)
        {
            this.player = player;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {
            if (player.PlayerReachEndOfPath())
            {
                RandomTileGenerator.Instance.CreateNewScreenTiles();
            }
        }

        public virtual void PhysicsUpdate()
        {
            player.transform.position = new Vector2(player.transform.position.x + player.DeltaSpeed, player.transform.position.y);
        }

        public virtual void Exit()
        {

        }
    }
}