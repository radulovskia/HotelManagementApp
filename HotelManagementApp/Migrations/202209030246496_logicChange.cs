namespace HotelManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logicChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UnitRooms", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.UnitRooms", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.UnitRooms", new[] { "Unit_UnitId" });
            DropIndex("dbo.UnitRooms", new[] { "Room_Id" });
            AddColumn("dbo.Rooms", "Unit_UnitId", c => c.Int());
            CreateIndex("dbo.Rooms", "Unit_UnitId");
            AddForeignKey("dbo.Rooms", "Unit_UnitId", "dbo.Units", "UnitId");
            DropTable("dbo.UnitRooms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UnitRooms",
                c => new
                    {
                        Unit_UnitId = c.Int(nullable: false),
                        Room_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Unit_UnitId, t.Room_Id });
            
            DropForeignKey("dbo.Rooms", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.Rooms", new[] { "Unit_UnitId" });
            DropColumn("dbo.Rooms", "Unit_UnitId");
            CreateIndex("dbo.UnitRooms", "Room_Id");
            CreateIndex("dbo.UnitRooms", "Unit_UnitId");
            AddForeignKey("dbo.UnitRooms", "Room_Id", "dbo.Rooms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UnitRooms", "Unit_UnitId", "dbo.Units", "UnitId", cascadeDelete: true);
        }
    }
}
