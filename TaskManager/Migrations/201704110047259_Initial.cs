namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskItemName = c.String(nullable: false),
                        Description = c.String(),
                        TaskId = c.Int(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        TaskLists_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskLists", t => t.TaskLists_Id)
                .Index(t => t.TaskLists_Id);
            
            CreateTable(
                "dbo.TaskLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false),
                        Description = c.String(),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskItems", "TaskLists_Id", "dbo.TaskLists");
            DropIndex("dbo.TaskItems", new[] { "TaskLists_Id" });
            DropTable("dbo.TaskLists");
            DropTable("dbo.TaskItems");
        }
    }
}
