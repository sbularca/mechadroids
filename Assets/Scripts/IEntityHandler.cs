namespace Mechadroids {
    public interface IEntityHandler {
        public EntityState EntityState { get; set; }
        public void Initialize();
        public void Tick();
        public void PhysicsTick();
        public void LateTick();
        public void Dispose();
    }
}
