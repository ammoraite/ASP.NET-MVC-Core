namespace EmailSenderWebApi.Services.CountersServices
{
    public interface ICounter<T>
    {
        T Count { get; set; }
    }
}
