namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interns", "StajBelgesi", c => c.String());
            AddColumn("dbo.Interns", "StajDefteri", c => c.String());
            DropColumn("dbo.Interns", "Title");
            DropColumn("dbo.Interns", "Filepath");
            DropColumn("dbo.Interns", "TitleStajDefteri");
            DropColumn("dbo.Interns", "FilepathStajDefteri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Interns", "FilepathStajDefteri", c => c.String());
            AddColumn("dbo.Interns", "TitleStajDefteri", c => c.String());
            AddColumn("dbo.Interns", "Filepath", c => c.String());
            AddColumn("dbo.Interns", "Title", c => c.String());
            DropColumn("dbo.Interns", "StajDefteri");
            DropColumn("dbo.Interns", "StajBelgesi");
        }
    }
}
