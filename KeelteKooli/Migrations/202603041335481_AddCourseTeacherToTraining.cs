namespace KeelteKooli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseTeacherToTraining : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trainings", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Trainings", new[] { "Course_Id" });
            RenameColumn(table: "dbo.Trainings", name: "Course_Id", newName: "CourseId");
            AlterColumn("dbo.Trainings", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trainings", "CourseId");
            AddForeignKey("dbo.Trainings", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainings", "CourseId", "dbo.Courses");
            DropIndex("dbo.Trainings", new[] { "CourseId" });
            AlterColumn("dbo.Trainings", "CourseId", c => c.Int());
            RenameColumn(table: "dbo.Trainings", name: "CourseId", newName: "Course_Id");
            CreateIndex("dbo.Trainings", "Course_Id");
            AddForeignKey("dbo.Trainings", "Course_Id", "dbo.Courses", "Id");
        }
    }
}
