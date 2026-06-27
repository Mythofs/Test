namespace Scripts.Units
{
    public class Unit
    {
        private UnitBase _base;
        private int health;
        private bool ally;
        public Unit(UnitBase Base, bool ally)
        {
            _base = Base;
            health = Base.MaxHP;
            this.ally = ally;
        }
        public Unit(UnitBase Base, int health, bool ally)
        {
            _base = Base;
            this.health = health;
            this.ally = ally;
        }
        public UnitBase Base => _base;
        public int Health => health;
        public bool Ally => ally;
        public void SetHealth(int health) => this.health = health;
    }
}