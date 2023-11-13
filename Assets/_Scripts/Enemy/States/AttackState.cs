namespace Enemy.States
{
    public class AttackState : IState<HumanoidEnemy>
    {
        public IState<HumanoidEnemy> DoState(HumanoidEnemy enemy)
        {
            if (enemy.IsInAttackRange())
            {
                enemy.Attack();
                return new AttackState();
            }
            return new ChaiseState();
        }
    }
}