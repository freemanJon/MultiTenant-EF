using System;
using System.Linq;
using ApiMultiTenant.Connection;
using ApiMultiTenant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiMultiTenant.Data
{
    public class Context : DbContext
    {
        private Client client;
        private readonly HttpContext _httpContext;
        private readonly string connectionString;
        public DbSet<Users> Users { get; set; }

        public Context(DbContextOptions<Context> options, IHttpContextAccessor httpContextAccessor = null, IConfiguration configuration = null) :
            base(options)
        {
            _httpContext = httpContextAccessor?.HttpContext;
             var clientSection = configuration.GetSection(_httpContext.Request.Path.Value.Split("/", StringSplitOptions.RemoveEmptyEntries)[0]);
            client = clientSection.Get<Client>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder opts)
        {
            if (!opts.IsConfigured)
            {
                var typeDB = client.typeBD;
                switch (typeDB)
                {
                    case "SqlServer":
                        opts.UseSqlServer(client.connectionString);
                        break;
                    case "PostgreSQL":
                        opts.UseNpgsql(client.connectionString);
                        break;
                }

            }
        }
    }
}

