namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ExpenseCaption = c.String(nullable: false, maxLength: 500),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 200),
                        LastName = c.String(nullable: false, maxLength: 200),
                        Avatar = c.String(),
                        BirthDate = c.String(),
                        Budget = c.Decimal(storeType: "money"),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MeetingStatusId = c.Int(nullable: false),
                        PersonFirstName = c.String(nullable: false, maxLength: 4000),
                        PersonLastName = c.String(nullable: false, maxLength: 4000),
                        MeetingTime = c.DateTime(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeetingStatuses", t => t.MeetingStatusId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MeetingStatusId);
            
            CreateTable(
                "dbo.MeetingStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(nullable: false, maxLength: 4000),
                        Code = c.Int(),
                        CrateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "UserId", "dbo.Users");
            DropForeignKey("dbo.Meetings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Meetings", "MeetingStatusId", "dbo.MeetingStatuses");
            DropIndex("dbo.Meetings", new[] { "MeetingStatusId" });
            DropIndex("dbo.Meetings", new[] { "UserId" });
            DropIndex("dbo.Expenses", new[] { "UserId" });
            DropTable("dbo.MeetingStatuses");
            DropTable("dbo.Meetings");
            DropTable("dbo.Users");
            DropTable("dbo.Expenses");
        }
    }
}
