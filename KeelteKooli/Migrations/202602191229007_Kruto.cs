namespace KeelteKooli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kruto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Registrations", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Registrations", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Registrations", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Registrations", "ApplicationUserId");
            AddForeignKey("dbo.Registrations", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Registrations", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Registrations", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Registrations", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Registrations", "ApplicationUserId");
            AddForeignKey("dbo.Registrations", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
