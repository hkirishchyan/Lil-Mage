namespace Enemy.States
{
    public class AttackState : IState<AEnemy>
    {
        public IState<AEnemy> DoState(AEnemy enemy)
        {
            if (enemy.IsInAttackRange())
            {
                enemy.AttackPlayer();
                return new AttackState();
            }
            return new ChaiseState();
        }
    }
}