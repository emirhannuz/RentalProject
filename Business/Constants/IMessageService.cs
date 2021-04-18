namespace Business.Constants
{
    public interface IMessageService
    {
        string CarNameInvalid { get; }
        string CarListed { get; }
        string CarNotFound { get; }
        string CarFound { get; }
        string CarAdded { get; }
        string CarNotAdded { get; }
        string CarUpdated { get; }
        string CarDeleted { get; }
    }
}
