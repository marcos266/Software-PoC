using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alstom.Configuration.SchemaGeneration;
using Alstom.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Alstom.Repository
{
    public class BasicRepository<T> : IBasicRepository<T> where T : BaseEntity
    {
        public ISessionFactory SessionFactory { get; set; }

        public ISchemaGeneration SchemaGeneration { get; set; }

        protected ISession CurrentSession
        {
            get { return SessionFactory.GetCurrentSession(); }
        }

        public virtual T Load(int id)
        {
            return CurrentSession.Load<T>(id);
        }

        public virtual T Get(int id)
        {
            return CurrentSession.Get<T>(id);
        }

        public virtual ICollection<T> Get(ICollection<int> ids)
        {
            int[] objects = (int[])ids;
            return CurrentSession.QueryOver<T>().WhereRestrictionOn(x => x.Id).IsIn(objects).List();
        }

        public virtual IList<T> FindAllPaged(int pageSize, int firstResult, string orderBy, bool asc)
        {
            Order order = asc ? Order.Asc(orderBy) : Order.Desc(orderBy);
            ICriteria criteria = CurrentSession.CreateCriteria(typeof(T));

            return criteria.SetMaxResults(pageSize)
                           .SetFirstResult(firstResult)
                           .List<T>();
        }

        public virtual IList<T> FindAll()
        {
            return CurrentSession.QueryOver<T>().List();
        }

        public virtual long GetAllCount()
        {
            ICriteria criteria = CurrentSession.CreateCriteria(typeof(T));
            criteria.SetProjection(Projections.RowCountInt64());
            return (long)criteria.List()[0];
        }

        public virtual T Save(T persistObject)
        {
            CurrentSession.Save(persistObject);
            return persistObject;
        }

        public virtual T Update(T persistObject)
        {
            CurrentSession.Lock(persistObject, LockMode.None);
            CurrentSession.Update(persistObject);
            return persistObject;
        }

        public T ForceUpdate(T persistObject)
        {
            //remove object from cache because we want to make it impossible for nhiberante compare values of object with cached values 
            //( and if they are equals nhibernate will not call update and as result event listeners too = will not update autdit info )
            CurrentSession.Evict(persistObject);
            CurrentSession.Update(persistObject);
            return persistObject;
        }

        public virtual T SaveOrUpdate(T persistObject)
        {
            CurrentSession.SaveOrUpdate(persistObject);
            return persistObject;
        }

        public virtual void Delete(T persistObject)
        {
            CurrentSession.Lock(persistObject, LockMode.None);
            CurrentSession.Delete(persistObject);
        }

        public virtual void DeleteById(int id)
        {
            CurrentSession.Delete(CurrentSession.Load<T>(id));
        }

        public virtual void Flush()
        {
            CurrentSession.Flush();
        }

        public virtual void Lock(T persistObject)
        {
            CurrentSession.Lock(persistObject, LockMode.None);
        }

    }
}
