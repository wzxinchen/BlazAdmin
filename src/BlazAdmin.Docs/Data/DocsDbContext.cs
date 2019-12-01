using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazAdmin.Docs.Data
{
    public class DocsDbContext : IdentityDbContext
    {
        public DocsDbContext(DbContextOptions<DocsDbContext> options)
            : base(options)
        {
        }
    }
}
