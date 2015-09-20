using AsNum.Xmj.Entity;
using System.ComponentModel.Composition;
using System.Data.Entity;

namespace AsNum.Xmj.Data {
    [Export(typeof(DbContext))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Entities : DbContext {

        public DbSet<Order> Orders {
            get;
            set;
        }

        public DbSet<OrderDetail> OrderDetails {
            get;
            set;
        }

        public DbSet<OrderNote> OrderNotes {
            get;
            set;
        }

        public DbSet<OrderMessage> OrderMessages {
            get;
            set;
        }

        public DbSet<Buyer> Buyers {
            get;
            set;
        }

        public DbSet<BuyerLevel> BuyerLevels { get; set; }

        public DbSet<Receiver> Receivers {
            get;
            set;
        }

        public DbSet<AdjReceiver> AdjReceivers { get; set; }

        public DbSet<Country> Countries {
            get;
            set;
        }

        public DbSet<Owner> Owners {
            get;
            set;
        }

        public DbSet<EUBShipper> EUBShippers {
            get;
            set;
        }

        public DbSet<PPCatelog> PPCatelogs {
            get;
            set;
        }

        //public DbSet<FreightPartition> FreightPartitions {
        //    get;
        //    set;
        //}

        //public DbSet<FreightPartitionCountry> FreightPartitionCountries {
        //    get;
        //    set;
        //}

        public DbSet<LogisticFee> LogisticFees { get; set; }

        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }

        public DbSet<OrdeLogistic> OrderLogistics { get; set; }

        public DbSet<LogisticServices> LogisticServices { get; set; }

        static Entities() {
            Database.SetInitializer<Entities>(null);
        }

        public Entities()
            : base("name=ANDataContext") {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

            modelBuilder.Entity<Order>().HasRequired(o => o.OrgReceiver).WithRequiredPrincipal(r => r.OrderFor);
            modelBuilder.Entity<Order>().HasOptional(o => o.AdjReceiver).WithOptionalPrincipal(r => r.OrderFor);

            //TPC
            modelBuilder.Entity<AdjReceiver>().Map(m => {
                m.MapInheritedProperties();
                m.ToTable("AdjReceivers");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
