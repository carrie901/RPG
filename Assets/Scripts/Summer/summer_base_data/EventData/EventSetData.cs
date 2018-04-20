namespace Summer
{
    /// <summary>
    /// 
    /// </summary>
    public class EventSetData
    {
        protected bool in_use;

        public virtual void OnInit() { }

        public virtual void Pop()
        {
            in_use = false;
            Reset();
        }

        public virtual void Push()
        {
            in_use = false;
            Reset();
        }

        public virtual void Reset() { }

        public virtual string ToDes()
        {
            return string.Empty;
        }
    }
}
