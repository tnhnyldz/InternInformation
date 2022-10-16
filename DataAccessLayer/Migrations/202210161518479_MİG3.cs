namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MİG3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "TeacherMail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "TeacherMail");
        }
    }
}
