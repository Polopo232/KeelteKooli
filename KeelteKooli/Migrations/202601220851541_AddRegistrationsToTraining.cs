namespace KeelteKooli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegistrationsToTraining : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nimetus = c.String(),
                        Keel = c.String(),
                        Tase = c.String(),
                        Kirjeldus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KeelekursusId = c.Int(nullable: false),
                        OpetajaId = c.Int(nullable: false),
                        AlgusKuupaev = c.DateTime(nullable: false),
                        LoppKuupaev = c.DateTime(nullable: false),
                        Hind = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxOsalejaid = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KoolitusId = c.Int(nullable: false),
                        ApplicationUserId = c.String(),
                        Staatus = c.String(),
                        TrainingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trainings", t => t.TrainingId, cascadeDelete: true)
                .Index(t => t.TrainingId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nimi = c.String(nullable: false),
                        Kvalifikatsioon = c.String(),
                        FotoPath = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainings", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Registrations", "TrainingId", "dbo.Trainings");
            DropForeignKey("dbo.Trainings", "CourseId", "dbo.Courses");
            DropIndex("dbo.Teachers", new[] { "ApplicationUserId" });
            DropIndex("dbo.Registrations", new[] { "TrainingId" });
            DropIndex("dbo.Trainings", new[] { "TeacherId" });
            DropIndex("dbo.Trainings", new[] { "CourseId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Registrations");
            DropTable("dbo.Trainings");
            DropTable("dbo.Courses");
        }
    }
}
