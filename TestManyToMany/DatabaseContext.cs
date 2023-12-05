using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManyToMany
{
    internal class DatabaseContext : DbContext
    {
        #region Constants

        /// <summary>
        /// A place holder value that can be inserted into connection strings to later be replaced with the application folder. 
        /// </summary>
        private const string AppplicationFolderPlaceHolder = "%AppFolder%";

        /// <summary>
        /// The database name in the configuration file.
        /// </summary>
        private const string ConfigDatabaseName = "TestManyToMany";

        #endregion

        #region Fields

        /// <summary>
        /// The configuration.
        /// </summary>
        private static readonly IConfigurationRoot _configuration;

        #endregion

        #region Constructors

        /// <summary>
        /// A static constructor
        /// </summary>
        static DatabaseContext()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

            if (string.IsNullOrEmpty(_configuration.GetConnectionString(ConfigDatabaseName)))
            {
                throw new InvalidOperationException("Failed to find a connection string for the database in the setting file.");
            }

            EnsureEmbeddedDatabaseFolderExists();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The DbSet for persons.
        /// </summary>
        public DbSet<PersonEntity> Persons { get; set; }

        /// <summary>
        /// The DbSet for books.
        /// </summary>
        public DbSet<BookEntity> Books { get; set; }

        /// <summary>
        /// The DbSet for categories.
        /// </summary>
        public DbSet<CategoryEntity> Categories { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Ensures that the database folder exists when there's an embedded database in the connection string.
        /// </summary>
        private static void EnsureEmbeddedDatabaseFolderExists()
        {
            string[] connectionStringParts = GetProcessedConnectionString().Split(new char[] { ';' });

            foreach (string part in connectionStringParts)
            {
                if (part.Contains("AttachDBFilename", StringComparison.CurrentCultureIgnoreCase))
                {
                    string[] rowParts = part.Split(new char[] { '=' });

                    if (rowParts.Length > 1)
                    {
                        string? databaseFolder = Path.GetDirectoryName(rowParts[1]);

                        if (!string.IsNullOrEmpty(databaseFolder) && !Directory.Exists(databaseFolder))
                        {
                            Directory.CreateDirectory(databaseFolder);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns the connection string with all placeholders replaced with actual values. 
        /// </summary>
        /// <returns>A <see cref="string"/></returns>
        private static string GetProcessedConnectionString()
        {
            return _configuration.GetConnectionString(ConfigDatabaseName)!.Replace(
               AppplicationFolderPlaceHolder, AppDomain.CurrentDomain.BaseDirectory).Replace(@"\\", @"\");
        }

        /// <summary>
        /// Configures the database and other options.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(GetProcessedConnectionString());
            optionsBuilder.UseLazyLoadingProxies(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
