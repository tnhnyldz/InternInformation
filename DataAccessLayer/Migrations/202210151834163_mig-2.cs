namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "StudentPassword", c => c.String());
            AddColumn("dbo.Teachers", "TeacherPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "TeacherPassword");
            DropColumn("dbo.Students", "StudentPassword");
        }
    }
}
