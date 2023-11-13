namespace Enemy.States
{
    public class ChaiseState : IState<AEnemy>
    {
        public IState<AEnemy> DoState(AEnemy enemy)
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