namespace ConsolidationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class history : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        BatchNo = c.String(),
                        InvFileName = c.String(),
                        CRMFileName = c.String(),
                        CreatedDateTime = c.String(),
                        CreatedUserName = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Histories");
        }
    }
}
