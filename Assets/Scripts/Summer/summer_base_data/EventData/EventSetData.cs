namespace Summer
{
    /// <summary>
    /// 
    /// </summary>
    public class EventSetData
    {
        protected bool _inUse;

        public virtual void OnInit() { }

        public virtual void Pop()
        {
            _inUse = false;
            Reset();
        }

        public virtual void Push()
        {
            _inUse = false;
            Reset();
        }

        public virtual void Reset() { }

        public virtual string ToDes()
        {
            return string.Empty;
        }
    }
}
