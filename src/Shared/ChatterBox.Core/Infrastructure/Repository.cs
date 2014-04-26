using System;
using System.Collections.Generic;
using System.Linq;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Infrastructure.Queries;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace ChatterBox.Core.Infrastructure
{
    public class Repository<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
        private readonly IQueryModel<TAggregateRoot> _queryModel;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HashSet<TAggregateRoot> _addedItems = new HashSet<TAggregateRoot>();
        private readonly HashSet<TAggregateRoot> _modifiedItems = new HashSet<TAggregateRoot>();
        private readonly HashSet<TAggregateRoot> _removedItems = new HashSet<TAggregateRoot>();

        public Repository(IQueryModel<TAggregateRoot> queryModel, IUnitOfWork unitOfWork)
        {
            if (queryModel == null) 
                throw new ArgumentNullException("queryModel");
           
            if (unitOfWork == null) 
                throw new ArgumentNullException("unitOfWork");

            _queryModel = queryModel;
            _unitOfWork = unitOfWork;
            _unitOfWork.Completed += OnUnitOfWorkCompleted;
            _unitOfWork.Abandoned += OnUnitOfWorkAbandoned;
        }

        public TAggregateRoot GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "id");

            var item = _queryModel.GetById(id);

            if (item == null)
                return null;

            _unitOfWork.Enlist(item);
            _modifiedItems.Add(item);
            return item;
        }

        public void Add(TAggregateRoot item)
        {
            if (item.Id == Guid.Empty) 
                throw new InvalidOperationException("Aggregate roots must have IDs assigned before being added to a repository.");

            _unitOfWork.Enlist(item);
            _addedItems.Add(item);
            _queryModel.Add(item);
        }

        public void Remove(TAggregateRoot item)
        {
            if (item == null) 
                throw new ArgumentNullException("item");

            _unitOfWork.Enlist(item);
            _removedItems.Add(item);
            _queryModel.Remove(item);
        }

        public TAggregateRoot[] Query(Func<IQueryable<TAggregateRoot>, TAggregateRoot[]> query)
        {
            if (query == null) 
                throw new ArgumentNullException("query");

            var results = query(_queryModel.Items);
            results.Do(item => _unitOfWork.Enlist(item))
                   .Done();
            
            return results;
        }

        public TProjection Query<TProjection>(Func<IQueryable<TAggregateRoot>, TProjection> query)
        {
            if (query == null)
                throw new ArgumentNullException("query");

            var results = query(_queryModel.Items);
            
            if (results is IEnumerable<TAggregateRoot>)
                (results as IEnumerable<TAggregateRoot>)
                    .Do(item => _unitOfWork.Enlist(item))
                    .Done();

            if (results is TAggregateRoot)
                _unitOfWork.Enlist(results as TAggregateRoot);

            return results;
        }

        private void OnUnitOfWorkCompleted(object sender, EventArgs e)
        {
        }

        private void OnUnitOfWorkAbandoned(object sender, EventArgs e)
        {
            _queryModel.Revert(_addedItems, _modifiedItems, _removedItems);
        }
    }
}