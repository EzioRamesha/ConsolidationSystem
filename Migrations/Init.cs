namespace ConsolidationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);


            CreateTable(
                "dbo.ImportDocs",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    InternalReference = c.String(),
                    DocDate = c.DateTime(nullable: false),
                    Description = c.String(),
                    DocNo = c.String(),
                    ItemNo = c.String(),
                    Currency = c.String(),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Type = c.String(),
                    IsEnabled = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
               "dbo.ProcessResults",
               c => new
               {
                   Id = c.Guid(nullable: false, identity: true),
                   InternalRef = c.String(),
                   BatchNo = c.String(),
                   DocDate = c.DateTime(nullable: false),
                   Description = c.String(),
                   DocNo = c.String(),
                   ItemNo = c.String(),
                   Currency = c.String(),
                   Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                   CRM_RefCode = c.String(),
                   CRM_PartnerRefNo = c.String(),
                   CRM_BookingStatus = c.String(),
                   CRM_PropertyName = c.String(),
                   CRM_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                   AmountDiff = c.Decimal(nullable: false, precision: 18, scale: 2),
                   Type = c.String(),
                   IsEnabled = c.Boolean(nullable: false),
               })
               .PrimaryKey(t => t.Id);


            AddColumn("dbo.ImportDocs", "CreatedDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.ImportDocs", "CreatedUserName", c => c.String());
            AddColumn("dbo.ProcessResults", "CreatedDateTime", c => c.String(nullable: false));
            AddColumn("dbo.ProcessResults", "CreatedUserName", c => c.String());
            AddColumn("dbo.ProcessResults", "CRM_Property_Country", c => c.String());
        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ImportDocs");
            DropTable("dbo.ProcessResults");

            DropColumn("dbo.ProcessResults", "CreatedUserName");
            DropColumn("dbo.ProcessResults", "CreatedDateTime");
            DropColumn("dbo.ImportDocs", "CreatedUserName");
            DropColumn("dbo.ImportDocs", "CreatedDateTime");
            DropColumn("dbo.ProcessResults", "CRM_Property_Country");
        }
    }
}
