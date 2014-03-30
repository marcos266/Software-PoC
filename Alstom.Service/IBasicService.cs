using System.Collections.Generic;
using Alstom.Model;
using Alstom.Repository;
using Spring.Transaction.Interceptor;

namespace Alstom.Service
{
    public interface IBasicService<T> where T : BaseEntity
    {
        IBasicRepository<T> BasicRepository { get; set; }

        [Transaction(ReadOnly = false)]
        T NewInstance(T persistentObject);

        [Transaction(ReadOnly = false)]
        T ModifyInstance(T persistentObject);

        [Transaction(ReadOnly = false)]
        T SaveOrUpdate(T persistentObject);

        [Transaction(ReadOnly = true)]
        IList<T> GetAll();

        [Transaction(ReadOnly = true)]
        long GetAllCount();

        [Transaction(ReadOnly = true)]
        T GetInstance(int id);

        [Transaction(ReadOnly = true)]
        ICollection<T> GetInstances(ICollection<int> ids);

        [Transaction(ReadOnly = true)]
        IList<T> GetAllPaged(int pageSize, int firstResult, string orderBy, bool asc);

        [Transaction(ReadOnly = false)]
        void DeleteInstance(T persistentObject);

        [Transaction(ReadOnly = false)]
        void Delete(int id);
    }
}