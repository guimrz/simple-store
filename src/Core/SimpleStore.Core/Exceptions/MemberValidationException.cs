namespace SimpleStore.Core.Exceptions
{
    public class MemberValidationException : Exception
    {
        public string Member { get; }

        public MemberValidationException(string member, string message) : base(message)
        {
            Member = member;
        }
    }
}
