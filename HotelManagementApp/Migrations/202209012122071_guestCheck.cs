namespace HotelManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guestCheck : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "GuestId", "dbo.Guests");
            DropIndex("dbo.Rooms", new[] { "GuestId" });
            RenameColumn(table: "dbo.Rooms", name: "GuestId", newName: "Guest_GuestId");
            AlterColumn("dbo.Rooms", "Guest_GuestId", c => c.Int());
            CreateIndex("dbo.Rooms", "Guest_GuestId");
            AddForeignKey("dbo.Rooms", "Guest_GuestId", "dbo.Guests", "GuestId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "Guest_GuestId", "dbo.Guests");
            DropIndex("dbo.Rooms", new[] { "Guest_GuestId" });
            AlterColumn("dbo.Rooms", "Guest_GuestId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Rooms", name: "Guest_GuestId", newName: "GuestId");
            CreateIndex("dbo.Rooms", "GuestId");
            AddForeignKey("dbo.Rooms", "GuestId", "dbo.Guests", "GuestId", cascadeDelete: true);
        }
    }
}
