namespace Services.DbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BirthDate = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Specialization = c.String(),
                        IndexNo = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Partner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Partner_Id)
                .Index(t => t.Partner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "Partner_Id", "dbo.People");
            DropIndex("dbo.People", new[] { "Partner_Id" });
            DropTable("dbo.People");
        }
    }
}
