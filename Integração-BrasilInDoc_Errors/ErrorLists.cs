namespace Errors
{
    public class ErrorLists : Exception
    {
        public IReadOnlyList<string> Errors { get; }
    
        public ErrorLists(IEnumerable<string> errors)
            : base("")
        {
            Errors = errors?.ToList() ?? [];
        }
    
        public ErrorLists(string message, IEnumerable<string> errors)
            : base(message)
        {
            Errors = errors?.ToList() ?? [];
        }

        public override string ToString()
        {
            if (Errors.Count == 0)
                return Message;
            if (Errors.Count == 1 && string.IsNullOrWhiteSpace(Message))
                return $"{Errors[0]}";
            if (string.IsNullOrWhiteSpace(Message))
                return string.Join(",\n", Errors);
            return $"{Message}\n {string.Join(",\n", Errors)}";
        }
    }
}