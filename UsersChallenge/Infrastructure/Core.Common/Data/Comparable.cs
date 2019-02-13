namespace Core.Common.Data
{
    public abstract class Comparable: IComparable
    {
        public virtual int CompareTo(object x)
        {
            return 0;
        }
    }
}
