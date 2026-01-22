namespace KeelteKooli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTeacherTraining : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trainings", "KeelekursusId");
            DropColumn("dbo.Trainings", "OpetajaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainings", "OpetajaId", c => c.Int(nullable: false));
            AddColumn("dbo.Trainings", "KeelekursusId", c => c.Int(nullable: false));
        }
    }
}
