namespace lindaniDS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddreessID = c.Int(nullable: false, identity: true),
                        Country = c.String(maxLength: 50),
                        Province = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        Surbub = c.String(maxLength: 50),
                        Street = c.String(maxLength: 50),
                        ZipCode = c.Int(nullable: false),
                        VehicleHire_vehicleID = c.Int(),
                    })
                .PrimaryKey(t => t.AddreessID)
                .ForeignKey("dbo.VehicleHires", t => t.VehicleHire_vehicleID)
                .Index(t => t.VehicleHire_vehicleID);
            
            CreateTable(
                "dbo.BookingPackages",
                c => new
                    {
                        PackageID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Cost = c.Double(nullable: false),
                        AvailableFrom = c.DateTime(),
                        AvailableTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PackageID);
            
            CreateTable(
                "dbo.Learners",
                c => new
                    {
                        LearnerID = c.Int(nullable: false, identity: true),
                        Names = c.String(),
                        Email = c.String(),
                        Province = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        Surbub = c.String(maxLength: 50),
                        Street = c.String(maxLength: 50),
                        ZipCode = c.Int(nullable: false),
                        Phone = c.Int(nullable: false),
                        IDNum = c.Int(nullable: false),
                        Location = c.String(),
                        BookingDate = c.DateTime(nullable: false),
                        Session = c.String(),
                    })
                .PrimaryKey(t => t.LearnerID);
            
            CreateTable(
                "dbo.Licences",
                c => new
                    {
                        LearnerID = c.Int(nullable: false, identity: true),
                        Names = c.String(),
                        Email = c.String(),
                        Province = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        Surbub = c.String(maxLength: 50),
                        Street = c.String(maxLength: 50),
                        ZipCode = c.Int(nullable: false),
                        Location = c.String(),
                        BookingDate = c.DateTime(nullable: false),
                        BookingDate2 = c.DateTime(nullable: false),
                        Picture = c.String(),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LearnerID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        PayType = c.String(),
                        UserID = c.Int(nullable: false),
                        vehicleID = c.Int(),
                        PackageID = c.Int(),
                        Date = c.DateTime(),
                        BankName = c.String(maxLength: 50),
                        BranchCode = c.Int(nullable: false),
                        CardNumber = c.Int(nullable: false),
                        Expire = c.DateTime(nullable: false),
                        CVV = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.BookingPackages", t => t.PackageID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.VehicleHires", t => t.vehicleID)
                .Index(t => t.UserID)
                .Index(t => t.vehicleID)
                .Index(t => t.PackageID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        EmailVerify = c.Boolean(nullable: false),
                        ActivationCode = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.VehicleHires",
                c => new
                    {
                        vehicleID = c.Int(nullable: false, identity: true),
                        modelID = c.Int(nullable: false),
                        Email = c.String(maxLength: 50),
                        color = c.String(),
                        regNo = c.String(),
                        noPlate = c.String(),
                        cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        condition = c.Boolean(nullable: false),
                        availability = c.Boolean(nullable: false),
                        Picture = c.Binary(),
                        Photo = c.String(),
                        PickDate = c.DateTime(),
                        DropDate = c.DateTime(),
                        AddreessID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.vehicleID)
                .ForeignKey("dbo.VehicleModels", t => t.modelID, cascadeDelete: true)
                .Index(t => t.modelID);
            
            CreateTable(
                "dbo.VehicleModels",
                c => new
                    {
                        modelID = c.Int(nullable: false, identity: true),
                        vehicleName = c.String(nullable: false),
                        vehicleMake = c.String(nullable: false),
                        vehicleBodyType = c.String(nullable: false),
                        modelName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.modelID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.RoleID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "vehicleID", "dbo.VehicleHires");
            DropForeignKey("dbo.VehicleHires", "modelID", "dbo.VehicleModels");
            DropForeignKey("dbo.Addresses", "VehicleHire_vehicleID", "dbo.VehicleHires");
            DropForeignKey("dbo.Payments", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserID", "dbo.Users");
            DropForeignKey("dbo.Payments", "PackageID", "dbo.BookingPackages");
            DropIndex("dbo.UserRoles", new[] { "RoleID" });
            DropIndex("dbo.UserRoles", new[] { "UserID" });
            DropIndex("dbo.VehicleHires", new[] { "modelID" });
            DropIndex("dbo.Payments", new[] { "PackageID" });
            DropIndex("dbo.Payments", new[] { "vehicleID" });
            DropIndex("dbo.Payments", new[] { "UserID" });
            DropIndex("dbo.Addresses", new[] { "VehicleHire_vehicleID" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.VehicleModels");
            DropTable("dbo.VehicleHires");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Payments");
            DropTable("dbo.Licences");
            DropTable("dbo.Learners");
            DropTable("dbo.BookingPackages");
            DropTable("dbo.Addresses");
        }
    }
}
