﻿using AccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.DbContexts
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
    }
}
