using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public abstract class State
    {
        public abstract void StateEnter(Enemy owner);
        public abstract void StateUpdate(Enemy owner);
        public abstract void StateExit(Enemy Owner);
    }

    public class Walk : State
    {
        private static Walk _instance;
        public static Walk Instance
        {
            get
            {
                if (_instance == null)
                    new Walk();

                return _instance;
            }
        }

        private Walk()
        {
            if (_instance == null)
                _instance = this;
        }

        public override void StateEnter(Enemy owner)
        {

        }

        public override void StateUpdate(Enemy owner)
        {
            owner.targetPos = Player.instance.transform.position;

            owner.SetDestination();

            if (Vector3.Distance(owner.transform.position, owner.targetPos) <= owner.stoppingDistance)
                owner._stateMachine.ChangeState(States.Attack.Instance);
        }

        public override void StateExit(Enemy owner)
        {
            owner.StopNavigation();
        }
    }

    public class Attack : State
    {
        public static Attack Instance
        {
            get
            {
                if (_instance == null)
                    new Attack();

                return _instance;
            }
        }
        private static Attack _instance;

        private Attack()
        {
            if (_instance == null)
                _instance = this;
        }

        public override void StateEnter(Enemy owner)
        {

        }

        public override void StateUpdate(Enemy owner)
        {
            owner.targetPos = Player.instance.transform.position;

            owner.Attack();

            if (Vector3.Distance(owner.transform.position, owner.targetPos) > owner.stoppingDistance)
                owner._stateMachine.ChangeState(States.Walk.Instance);
        }

        public override void StateExit(Enemy Owner)
        {
            
        }
    }

    public class Run : State
    {
        public static Run Instance
        {
            get
            {
                if (_instance == null)
                    new Run();

                return _instance;
            }
        }
        private static Run _instance;

        private Run()
        {
            if (_instance == null)
                _instance = this;
        }

        public override void StateEnter(Enemy owner)
        {
            owner.targetPos = owner.spawnPos;
            owner.SetDestination();
        }

        public override void StateUpdate(Enemy owner)
        {
            if (Vector3.Distance(owner.transform.position, owner.spawnPos) <= owner.stoppingDistance)
            {
                owner._stateMachine.ChangeState(States.Walk.Instance);
            }
        }

        public override void StateExit(Enemy Owner)
        {
            Owner.StopNavigation();
        }
    }
}
