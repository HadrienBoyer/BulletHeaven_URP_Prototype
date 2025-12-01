namespace TappyTale
{
    /// <summary>
    /// Stub to integrate Unity IAP / UGS. Wire your real store here.
    /// </summary>
    public interface IIAPService
    {
        void Purchase(string storeId, System.Action<bool> onComplete);
    }

    public class NullIAPService : IIAPService
    {
        public void Purchase(string storeId, System.Action<bool> onComplete)
        {
            // Fake purchase success for prototype
            onComplete?.Invoke(true);
        }
    }
}
