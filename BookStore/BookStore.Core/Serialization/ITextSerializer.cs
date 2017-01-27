using System.IO;

namespace BookStore.Core.Serialization
{
    public interface ITextSerializer
    {
        void Serialize(TextWriter writer, object graph);
        object Deserialize(TextReader reader);
    }
}