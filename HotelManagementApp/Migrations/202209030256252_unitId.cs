namespace HotelManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unitId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.Rooms", new[] { "Unit_UnitId" });
            RenameColumn(table: "dbo.Rooms", name: "Unit_UnitId", newName: "UnitId");
            AlterColumn("dbo.Rooms", "UnitId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rooms", "UnitId");
            AddForeignKey("dbo.Rooms", "UnitId", "dbo.Units", "UnitId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "UnitId", "dbo.Units");
            DropIndex("dbo.Rooms", new[] { "UnitId" });
            AlterColumn("dbo.Rooms", "UnitId", c => c.Int());
            RenameColumn(table: "dbo.Rooms", name: "UnitId", newName: "Unit_UnitId");
            CreateIndex("dbo.Rooms", "Unit_UnitId");
            AddForeignKey("dbo.Rooms", "Unit_UnitId", "dbo.Units", "UnitId");
        }
    }
}
