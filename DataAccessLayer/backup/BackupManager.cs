using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Backup
{
    public class BackupManager
    {
        private readonly ILogger<BackupManager> _logger;

        private readonly string _dbPath;
        private readonly string _backupDir1;
        private readonly string _backupDir2;
        private readonly int _keepBackups;
        private static readonly object _lock = new();

        public BackupManager(
            string dbPath,
            string backupDir1,
            string backupDir2,
             ILogger<BackupManager> logger,
            int keepBackups = 20)
        {
            _dbPath = dbPath;
            _backupDir1 = backupDir1;
            _backupDir2 = backupDir2;
            _keepBackups = keepBackups;
            _logger = logger;

            try
            {
                if (!string.IsNullOrWhiteSpace(_backupDir1))
                    Directory.CreateDirectory(_backupDir1);

                if (!string.IsNullOrWhiteSpace(_backupDir2))
                    Directory.CreateDirectory(_backupDir2);
            }
            catch
            {

            }
            
        }

        // ===============================
        // Make Backup
        // ===============================
        public void CreateBackup()
        {
            lock (_lock)
            {
                try
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                    string backupPath1 =
                        Path.Combine(_backupDir1, $"mydb_{timestamp}.db");

                    string? backupPath2 = null;
                    if (!string.IsNullOrWhiteSpace(_backupDir2))
                    {
                        backupPath2 =
                            Path.Combine(_backupDir2, $"mydb_{timestamp}.db");
                    }

                    using var source = new SqliteConnection($"Data Source={_dbPath}");
                    using var dest1 = new SqliteConnection($"Data Source={backupPath1}");

                    source.Open();
                    dest1.Open();

                    source.BackupDatabase(dest1);

                    if (!string.IsNullOrWhiteSpace(_backupDir2) && Directory.Exists(_backupDir2))
                    {
                        try
                        {
                            using var dest2 = new SqliteConnection($"Data Source={backupPath2}");
                            dest2.Open();
                            source.BackupDatabase(dest2);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(
                                ex,
                                "Secondary backup failed. Path: {BackupDir2}",
                                _backupDir2);
                            
                        }
                    }
                    else
                    {
                        _logger.LogWarning(
                            "Secondary backup skipped. Directory not available: {BackupDir2}",
                            _backupDir2);
                    }

                    DeleteOldBackups(_backupDir1);

                    if (!string.IsNullOrWhiteSpace(_backupDir2))
                        DeleteOldBackups(_backupDir2);

                    _logger.LogInformation(
                        "Backup created successfully. Local: {Backup1}, Secondary: {Backup2}",
                        backupPath1,
                        backupPath2 ?? "N/A");
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Backup failed. DB: {DbPath}, LocalDir: {Dir1}, SecondaryDir: {Dir2}",
                        _dbPath,
                        _backupDir1,
                        _backupDir2 ?? "N/A");

                    //Now Take A Backup To The Deufalt Folder D:\InventoryBackups
                    CreateEmergencyBackup(ex);

                    throw;
                }
            }
        }

        private void CreateEmergencyBackup(Exception originalException)
        {
            try
            {
                const string emergencyDir = @"D:\InventoryBackups";
                Directory.CreateDirectory(emergencyDir);

                string emergencyPath = Path.Combine(
                    emergencyDir,
                    $"mydb_emergency_{DateTime.Now:yyyyMMdd_HHmmss}.db"
                );

                using var source = new SqliteConnection($"Data Source={_dbPath}");
                using var dest = new SqliteConnection($"Data Source={emergencyPath}");

                source.Open();
                dest.Open();

                source.BackupDatabase(dest);

                _logger.LogCritical(
                    originalException,
                    "Primary backup failed. Emergency backup created at {EmergencyPath}",
                    emergencyPath);
            }
            catch (Exception emergencyEx)
            {
               //Last Line
                File.AppendAllText(
                    @"D:\InventoryBackups\backup-fatal.log",
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\n{emergencyEx}\n\n");
            }
        }


        // ===============================
        // DeleteOld
        // ===============================
        private void DeleteOldBackups(string backupDir)
        {
            if (string.IsNullOrWhiteSpace(backupDir))
                return;

            var files = new DirectoryInfo(backupDir)
                .GetFiles("mydb_*.db")
                .OrderByDescending(f => f.CreationTime)
                .Skip(_keepBackups);

            foreach (var file in files)
            {
                try { file.Delete(); } catch { }
            }
        }

        // ===============================
        // Backup
        // ===============================
        public void RestoreBackup(string backupFilePath)
        {
            if (!File.Exists(backupFilePath))
                throw new FileNotFoundException("ملف النسخة الاحتياطية غير موجود");

            // Try to take backup before restore (best effort)
            try
            {
                CreateBackup();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Pre-restore backup failed. Restore will continue.");
            }

            SqliteConnection.ClearAllPools();

            if (File.Exists(_dbPath))
            {
                try
                {
                    File.Copy(_dbPath, _dbPath + ".old", true);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to create .old backup before restore");
                }
            }

            File.Copy(backupFilePath, _dbPath, true);
        }
    }
}
