namespace KeelteKooli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kruto2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Registrations", "Staatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Registrations", "Staatus", c => c.String());
        }
    }
}
