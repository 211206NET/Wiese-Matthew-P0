namespace CustomExceptions;
//[System.Serializable]
public class DuplicateRecordException : System.Exception//Can't get this to work
{
    public DuplicateRecordException() { }
    public DuplicateRecordException(string message) : base(message) { }
    public DuplicateRecordException(string message, System.Exception inner) : base(message, inner) { }
    protected DuplicateRecordException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}