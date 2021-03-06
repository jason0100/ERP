﻿using API.Data.Interface;
using ErrorCodeLib;
using ErrorCodeLib.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.UnitOfWork
{
    /// <summary>
    /// 實作Entity Framework Unit Of Work的class
    /// </summary>
    public class EFUnitOfWork<TDbContext> :IUnitOfWork where TDbContext : ErpDbContext
    {
        private readonly TDbContext _context;

        private bool _disposed;
        private Hashtable _repositories;

        /// <summary>
        /// 設定此Unit of work(UOF)的Context。
        /// </summary>
        /// <param name="context">設定UOF的context</param>
        public EFUnitOfWork(TDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 儲存所有異動。
        /// </summary>
        public Exception Save()
        {
            var result = new ResultModel();
            try
            {
                _context.SaveChanges();
                
            }
            catch (Exception e)
            {
                //result = ErrorCode.CUSTOM_ERROR(e, "cht", 500, "cht");
                return e;
            }
            //result = ErrorCode.SUCCESS(null, "", "cht");
            return null;
        }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        /// <param name="disposing">是否在清理中？</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <typeparam name="T">此Context裡面的Entity Type</typeparam>
        /// <returns>Entity的Repository</returns>
        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(IGenericRepository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[type];
        }
    }
}
