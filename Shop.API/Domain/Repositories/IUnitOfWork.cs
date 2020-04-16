using System.Threading.Tasks;

namespace Shop.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}