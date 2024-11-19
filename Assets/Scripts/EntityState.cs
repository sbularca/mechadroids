namespace Mechadroids {
    /// <summary>
    /// The main State interface template that can be implemented both by the enemy states, player states, etc.
    /// </summary>
    public interface IEntityState {
        public void Enter() { }
        public void HandleInput() { }
        public void LogicUpdate() { }
        public void PhysicsUpdate() { }
        public void Exit() { }
    }
}
