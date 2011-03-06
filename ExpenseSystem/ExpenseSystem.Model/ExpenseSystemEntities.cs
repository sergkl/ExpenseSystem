﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.EntityClient;
using ExpenseSystem.Entities;

namespace ExpenseSystem.Model
{
    public partial class ExpenseSystemEntities : ObjectContext, IContext
    {
        public const string ConnectionString = "name=ExpenseSystemEntities";
        public const string ContainerName = "ExpenseSystemEntities";

        public ExpenseSystemEntities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        public ExpenseSystemEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        public ExpenseSystemEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }

        #region Object set properties

        public IObjectSet<User> Users
        {
            get { return _users ?? (_users = CreateObjectSet<User>("Users")); }
        }
        private IObjectSet<User> _users;

        public IObjectSet<Tag> Tags
        {
            get { return _tags ?? (_tags = CreateObjectSet<Tag>("Tags")); }
        }
        private IObjectSet<Tag> _tags;

        public IObjectSet<ExpenseRecord> ExpenseRecords
        {
            get { return _expenseRecords ?? (_expenseRecords = CreateObjectSet<ExpenseRecord>("ExpenseRecords")); }
        }
        private IObjectSet<ExpenseRecord> _expenseRecords;

        #endregion

        public string Save()
        {
            string savingResult = "Success";
            try
            {
                SaveChanges();
            }
            catch (Exception exception)
            {
                savingResult = exception.Message;
            }
            return savingResult;
        }
    }
}
