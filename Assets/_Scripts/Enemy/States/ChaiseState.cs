namespace Enemy.States
{
    public class ChaiseState : IState<HumanoidEnemy>
    {
        public IState<HumanoidEnemy> DoState(HumanoidEnemy enemy)
        {
            if (!enemy.IsInAttackRange())
            {
                enemy.ChasePlayer();
                return new ChaiseState();
            }
            return new AttackState();
        }
    }
}