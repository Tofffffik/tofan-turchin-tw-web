
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace JetPass.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<JetPass.DAL.JettContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    } 
}