namespace Mechadroids {
    public abstract class EntityState {
        protected IEntityHandler entityHandler;

        protected EntityState(IEntityHandler entityHandler) {
            this.entityHandler = entityHandler;
        }

        public abstract void Enter();
        public abstract void HandleInput();
        public abstract void LogicUpdate();
        public abstract void PhysicsUpdate();
        public abstract void Exit();
    }
}
