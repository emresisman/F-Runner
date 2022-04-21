using System.Collections;
using UnityEngine;

namespace emresisman.Assets.Scripts
{
    public abstract class State
    {
        protected Player player;
        protected StateMachine stateMachine;

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

        }

        public virtual void PhysicsUpdate()
        {
            player.transform.position = new Vector3(player.transform.position.x + player.DeltaSpeed, player.transform.position.y, 0);
        }

        public virtual void Exit()
        {

        }
    }
}