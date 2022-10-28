namespace SimpleStore.Core.Domain.Abstractions
{
    public interface ITimeTrackable
    {
        public DateTime CreationDate { get; }

        public DateTime? UpdateDate { get; }
    }
}
