namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "TeacherRole", c => c.String(maxLength: 1));
            AlterColumn("dbo.Admins", "Role", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "Role", c => c.String(maxLength: 10));
            DropColumn("dbo.Teachers", "TeacherRole");
        }
    }
}
