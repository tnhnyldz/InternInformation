namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "AdminMail", c => c.String(maxLength: 30));
            DropColumn("dbo.Admins", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "Username", c => c.String(maxLength: 30));
            DropColumn("dbo.Admins", "AdminMail");
        }
    }
}
