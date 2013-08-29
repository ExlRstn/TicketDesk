﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketDesk.Domain.Legacy.Migrations;

namespace TicketDesk.Domain.Legacy
{
    public class LegacyDatabaseInitializer : IDatabaseInitializer<TicketDeskLegacyContext>
    {

        private string ConnectionName { get; set; }

        public LegacyDatabaseInitializer(string connectionName)
        {
            ConnectionName = connectionName;
        }
        
        public void InitializeDatabase(TicketDeskLegacyContext context)
        {
            //if existsing TD 2.x database, run legacy migration
            if (context.Database.Exists() && IsLegacyDatabase(context))
            {
                var upgradeConfig = new Configuration();
                upgradeConfig.TargetDatabase = new DbConnectionInfo(ConnectionName);

                //this add the migration history table with the exact same migration ID as used by the standard migrator.
                var migrator = new DbMigrator(upgradeConfig);
                migrator.Update("Initial");
            }
        }

        public static bool IsLegacyDatabase(TicketDeskLegacyContext context)
        {
            var isLegacy = false;
            try
            {
                var oldVersion =
                    context.Database.SqlQuery<string>("select SettingValue from Settings where SettingName = 'Version'");
                isLegacy = (oldVersion != null && oldVersion.Any() && oldVersion.First().Equals("2.0.2"));
            }
            catch
            {
                //eat any exception, we'll assume that if the db exists, but we can't read the settings, then it is an just empty new db
            }
            return isLegacy;

        }
    }
}