namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskLists", "TaskName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskLists", "TaskName", c => c.String(nullable: false));
        }
    }
}
