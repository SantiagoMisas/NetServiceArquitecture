namespace API.Interfaces
{
    public interface IAudit<TKey>
    {
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public TKey? CreatedBy { get; set; }

        public TKey? UpdatedBy { get; set; }
    }
}
