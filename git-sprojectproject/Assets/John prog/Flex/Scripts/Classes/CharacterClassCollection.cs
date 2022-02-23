
namespace Fungus
{
    public class CharacterClassCollection : GenericCollection<CharacterClass>
    {
        public void ClearDestroyed()
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (collection[i] == null)
                    collection.RemoveAt(i);
            }
        }
    }
}