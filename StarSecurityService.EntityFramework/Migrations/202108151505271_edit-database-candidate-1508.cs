﻿namespace StarSecurityService.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editdatabasecandidate1508 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidate", "CreateAt", c => c.DateTime());
            DropColumn("dbo.Candidate", "StartDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Candidate", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Candidate", "CreateAt");
        }
    }
}
