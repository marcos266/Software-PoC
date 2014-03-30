using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alstom.Model;
using Alstom.Repository;
using Spring.Transaction.Interceptor;

namespace Alstom.Service
{
    public class BasicService<T> : IBasicService<T> where T : BaseEntity
    {
        public IBasicRepository<T> BasicRepository { get; set; }

        [Transaction(ReadOnly = false)]
        public virtual T NewInstance(T persistentObject)
        {
            return BasicRepository.Save(persistentObject);
        }

        [Transaction(ReadOnly = false)]
        public virtual T ModifyInstance(T persistentObject)
        {
            return BasicRepository.Update(persistentObject);
        }

        [Transaction(ReadOnly = false)]
        public virtual T SaveOrUpdate(T persistentObject)
        {
            return BasicRepository.SaveOrUpdate(persistentObject);
        }

        [Transaction(ReadOnly = true)]
        public virtual IList<T> GetAll()
        {
            return BasicRepository.FindAll();
        }

        [Transaction(ReadOnly = true)]
        public virtual long GetAllCount()
        {
            return BasicRepository.GetAllCount();
        }

        [Transaction(ReadOnly = true)]
        public virtual T GetInstance(int id)
        {
            return BasicRepository.Get(id);
        }

        [Transaction(ReadOnly = true)]
        public virtual ICollection<T> GetInstances(ICollection<int> ids)
        {
            return BasicRepository.Get(ids);
        }

        [Transaction(ReadOnly = true)]
        public virtual IList<T> GetAllPaged(int pageSize, int firstResult, string orderBy, bool asc)
        {
            return BasicRepository.FindAllPaged(pageSize, firstResult, orderBy, asc);
        }

        [Transaction(ReadOnly = false)]
        public virtual void DeleteInstance(T persistentObject)
        {
            BasicRepository.Delete(persistentObject);

        }

        [Transaction(ReadOnly = false)]
        public virtual void Delete(int id)
        {
            T persistentObject = BasicRepository.Get(id);
            BasicRepository.Delete(persistentObject);
        }
    }
}
