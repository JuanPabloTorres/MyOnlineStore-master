namespace MyOnlineStore.Shared.Utils
{
    public interface IResponse<TFailure, TSuccess> where TFailure : IFailure where TSuccess : ISuccess
    {
        TFailure Failure { get; }
        TSuccess Success { get; }
        bool IsSuccess { get; set; }
        void DisplayAllMessages();
    }
}