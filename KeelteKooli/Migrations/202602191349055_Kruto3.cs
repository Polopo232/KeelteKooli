namespace KeelteKooli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Kruto3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainings", "CourseKeel", c => c.String());
            AddColumn("dbo.Trainings", "CourseTase", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Trainings", "CourseTase");
            DropColumn("dbo.Trainings", "CourseKeel");
        }
    }
}

