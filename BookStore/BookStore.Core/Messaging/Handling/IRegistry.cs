namespace BookStore.Core.Messaging.Handling
{
    public interface IRegistry<in TRegistree>
    {
        void Register(TRegistree registree);
    }
}