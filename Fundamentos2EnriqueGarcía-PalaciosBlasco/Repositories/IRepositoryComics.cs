using Fundamentos2EnriqueGarcía_PalaciosBlasco.Models;

namespace Fundamentos2EnriqueGarcía_PalaciosBlasco.Repositories
{
    public interface IRepositoryComics
    {
        List<Comic> GetComics();

        void InsertComics(Comic comic);
    }
}
