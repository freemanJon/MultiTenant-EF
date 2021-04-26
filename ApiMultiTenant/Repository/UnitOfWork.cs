using System;
using ApiMultiTenant.Data;
using ApiMultiTenant.Models;

namespace ApiMultiTenant.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Context _contexto;
        private Repository<Users> usersRepository;
        
        public UnitOfWork(Context context)
        {
            _contexto = context;
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public Repository<Users> UsersRepository
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new Repository<Users>(_contexto);
                }
                return usersRepository;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _contexto.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}