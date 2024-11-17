namespace Mechadroids {
    public interface IEntityState {
        public void Enter() { }
        public void HandleInput() { }
        public void LogicUpdate() { }
        public void PhysicsUpdate() { }
        public void Exit() { }
    }
}
