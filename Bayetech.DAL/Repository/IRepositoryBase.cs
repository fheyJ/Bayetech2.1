﻿/*******************************************************************************
 * Copyright © 2017 平台组 版权所有
 * Author: Zhaoyz
 * Description: 快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/

using Bayetech.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Bayetech.DAL
{
    public interface IRepositoryBase : IDisposable
    {
        IRepositoryBase BeginTrans();
        int Commit();
        int Insert<TEntity>(TEntity entity) where TEntity : class;
        int Insert<TEntity>(List<TEntity> entitys) where TEntity : class;
        int Update<TEntity>(TEntity entity) where TEntity : class;
        int Delete<TEntity>(TEntity entity) where TEntity : class;
        int Delete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        TEntity FindEntity<TEntity>(object keyValue) where TEntity : class;
        TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        IQueryable<TEntity> IQueryable<TEntity>() where TEntity : class;
        IQueryable<TEntity> IQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        List<TEntity> FindList<TEntity>(string strSql) where TEntity : class;
        List<TEntity> FindList<TEntity>(string strSql, DbParameter[] dbParameter) where TEntity : class;
        List<TEntity> FindList<TEntity>(Pagination pagination,Expression<Func<TEntity, bool>> predicate) where TEntity : class,new();
        List<TEntity> FindList<TEntity>(Pagination pagination, out Pagination newPage,Expression<Func<TEntity, bool>> predicate) where TEntity : class, new();
        JObject GetList<TEntity>(Pagination pagination, out Pagination NewPage, Expression<Func<TEntity, bool>> predicate = null) where TEntity : class, new();

    }
}
