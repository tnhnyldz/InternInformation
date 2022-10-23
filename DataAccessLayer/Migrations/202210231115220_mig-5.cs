namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interns", "Title", c => c.String());
            AddColumn("dbo.Interns", "Filepath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interns", "Filepath");
            DropColumn("dbo.Interns", "Title");
        }
    }
}
