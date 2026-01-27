namespace KeelteKooli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trainings", "CourseId", "dbo.Courses");
            DropIndex("dbo.Trainings", new[] { "CourseId" });
            RenameColumn(table: "dbo.Trainings", name: "CourseId", newName: "Course_Id");
            AddColumn("dbo.Trainings", "CourseName", c => c.String());
            AlterColumn("dbo.Trainings", "Course_Id", c => c.Int());
            CreateIndex("dbo.Trainings", "Course_Id");
            AddForeignKey("dbo.Trainings", "Course_Id", "dbo.Courses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainings", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Trainings", new[] { "Course_Id" });
            AlterColumn("dbo.Trainings", "Course_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Trainings", "CourseName");
            RenameColumn(table: "dbo.Trainings", name: "Course_Id", newName: "CourseId");
            CreateIndex("dbo.Trainings", "CourseId");
            AddForeignKey("dbo.Trainings", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
