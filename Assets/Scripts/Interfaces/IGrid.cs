namespace Scripts.Interfaces
{
    public interface IGrid
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsEmpty { get; set; }
    }
}