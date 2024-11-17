namespace Mechadroids {
    public interface IEntityHandler {
        public IEntityState EntityState { get; set; }
        public void Initialize();
        public void Tick();
        public void PhysicsTick();
        public void LateTick();
        public void Dispose();
    }
}
