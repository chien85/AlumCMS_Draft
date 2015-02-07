using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Users.Models.Entities;
using Users.Models;
using System;

namespace Users.Infrastructure {
    public class AppIdentityDbContext : IdentityDbContext<AppUser> {

        public AppIdentityDbContext() : base("EFContext3") {

        }
        
        public DbSet<Content> Contents { get; set; }
        public DbSet<CatSubCat> CatSubCats { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Category> Categories { get; set; }

        //saving setting in database.
        public DbSet<Setting> Settings { get; set; }
      
        
        static AppIdentityDbContext() {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<Content>().ToTable("Contents");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Subcategory>().ToTable("Subcategories");
            modelBuilder.Entity<CatSubCat>().ToTable("CatSubCats");


            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<Setting>()
            .HasKey(x => new { x.Name, x.Type });

            modelBuilder.Entity<Setting>()
                        .Property(x => x.Value)
                        .IsOptional();

        }
        public static AppIdentityDbContext Create() {
            return new AppIdentityDbContext();
        }
    }

    
    public class IdentityDbInit
            : DropCreateDatabaseIfModelChanges<AppIdentityDbContext> {

        protected override void Seed(AppIdentityDbContext context) {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(AppIdentityDbContext context) {
            //setup users and roles
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));
            string roleName = "Administrators";
            string userName = "Admin";
            string password = "MySecret";
            string email = "admin@example.com";
            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }
            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser { UserName = userName, Email = email },
                password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }

            
            
            // initial configuration will go here
            
            Content mycontent = new Content()
            {
                Body = "Hello",
                Byline = "By line hehe",
                CreationDate = DateTime.Now,
                Seo = "Helllo seo",
                TagLine = "Tag line",
                Teaser = "Hi Teaser",
                Title = "Det me title",
                UpdatedDate = DateTime.Now,
                ViewCount=0,
                
                UserId = userMgr.FindByName("Admin").Id

            };
            Setting seedsetting1 = new Setting();
            seedsetting1.Name = "AdminEmail";
            seedsetting1.Type = "GeneralSettings";
            seedsetting1.Value = "chienqx@yahoo.com";
            Setting seedsetting2 = new Setting();
            seedsetting2.Name = "SiteName";
            seedsetting2.Type = "GeneralSettings";
            seedsetting2.Value = "Alumc";

            Setting seedsetting3 = new Setting();
            seedsetting3.Name = "HomeMetaDescription";
            seedsetting3.Type = "SeoSettings";
            seedsetting3.Value = "Alumc Seosettings";
            Setting seedsetting4 = new Setting();
            seedsetting4.Name = "HomeMetaTitle";
            seedsetting4.Type = "SeoSettings";
            seedsetting4.Value = "Alumc Title settings";

            context.Settings.Add(seedsetting1);
            context.Settings.Add(seedsetting2);
            context.Settings.Add(seedsetting3);
            context.Settings.Add(seedsetting4);

            context.Contents.Add(mycontent);
           context.SaveChanges();

        }
    }
}