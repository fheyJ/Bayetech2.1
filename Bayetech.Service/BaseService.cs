﻿using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Bayetech.DAL;
using System.Linq.Expressions;
using Bayetech.Core.Entity;

namespace Bayetech.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        protected RepositoryBase repository = new RepositoryBase();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int Delete(object keyValue)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 查询单个对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public TEntity FindEntity(object keyValue)
        {
            return repository.FindEntity<TEntity>(keyValue);
        }

        /// <summary>
        ///查询集合 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> FindList(Expression<Func<TEntity, bool>> predicate)
        {
            return repository.IQueryable<TEntity>(predicate);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public int Insert(TEntity entity)
        {
            return repository.Insert(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            return repository.Update(entity);
        }

        /// <summary>
        /// 获取上下文
        /// </summary>
        /// <returns></returns>
        public BayetechEntities GetContext()
        {
            return repository.GetContext();
        }
    }
}