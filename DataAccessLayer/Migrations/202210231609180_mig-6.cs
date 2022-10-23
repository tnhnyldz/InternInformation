namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interns", "TitleStajDefteri", c => c.String());
            AddColumn("dbo.Interns", "FilepathStajDefteri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interns", "FilepathStajDefteri");
            DropColumn("dbo.Interns", "TitleStajDefteri");
        }
    }
}
