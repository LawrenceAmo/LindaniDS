using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace lindaniDS.Models
{

   // public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)
        //{
        //}

        //public static ApplicationDbContext Create()
        //{
        //    return new ApplicationDbContext();
        //}
    //}
    public class LindaniContext : IdentityDbContext<ApplicationUser>
    {
        private User user;

        public LindaniContext() : base("LindaniDBs")
            {
                Database.SetInitializer<LindaniContext>(new CreateDatabaseIfNotExists<LindaniContext>());
            }


        public LindaniContext(User user)
        {
            this.user = user;
        }

        public DbSet<VehicleHire> VehicleHires { get; set; }
            public DbSet<VehicleModel> VehicleModels { get; set; }
            public DbSet<BookingPackages> BookingPackages { get; set; }
                 public DbSet<Role> Roles { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
               .HasMany(u => u.Roles)
               .WithMany(r => r.Users)
               .Map(m =>
               {
                   m.ToTable("UserRoles");
                   m.MapLeftKey("UserID");
                   m.MapRightKey("RoleID");
               });
        }

            public static LindaniContext Create()
            {
                return new LindaniContext();
            }

        public System.Data.Entity.DbSet<lindaniDS.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<lindaniDS.Models.Address> Addresses { get; set; }     


       // public System.Data.Entity.DbSet<lindaniDS.Models.Payment> Payments { get; set; }

        public System.Data.Entity.DbSet<lindaniDS.Models.Learners> Learners { get; set; }

        public System.Data.Entity.DbSet<lindaniDS.Models.Licence> Licences { get; set; }

        public System.Data.Entity.DbSet<lindaniDS.Models.Payment> Payments { get; set; }

        public System.Data.Entity.DbSet<LindaniDrivingSchool.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<lindaniDS.Models.Enrollments> Enrollments { get; set; }

        public System.Data.Entity.DbSet<LindaniDrivingSchool.Models.Lesson> Lessons { get; set; }
    }


    
}