namespace Shmup
{
    public abstract class GameObject
    {
        public static long _lastId = 0;
        public bool IsActive { get; set; }
        public bool HasWoken { get; set; }
        public bool HasStarted { get; set; }
        protected long Id { get; set; }

        public virtual void Awake()
        {
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void UpdateUi()
        {
        }

        protected void Instantiate(GameObject gameObject)
        {
            gameObject.Id = ++_lastId;
            gameObject.IsActive = true;
            Program.EntitiesToAdd.Add(gameObject);
        }

        protected void Destroy(GameObject gameObject)
        {
            IsActive = false;
        }
    }
}