namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Helps",
                c => new
                    {
                        HelpID = c.Int(nullable: false, identity: true),
                        HelpTitle = c.String(),
                        HelpTitle2 = c.String(),
                        HelpLogo = c.String(),
                        HelpContent1 = c.String(),
                        HelpContent2 = c.String(),
                        HelpContent3 = c.String(),
                    })
                .PrimaryKey(t => t.HelpID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Helps");
        }
    }
}
