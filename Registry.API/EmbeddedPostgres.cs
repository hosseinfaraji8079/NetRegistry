using System.Diagnostics;

namespace Registry.API;

public static class EmbeddedPostgres
{
    private static Process? _postgresProcess;

    public static void Start()
    {
        string pgDirectory = "pg_bin";
        string dataDirectory = $"{pgDirectory}/data";

        if (!Directory.Exists(dataDirectory))
        {
            // Initialize PostgreSQL data directory
            Process initdb = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = $"{pgDirectory}/initdb",
                    Arguments = $"-D \"{dataDirectory}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };
            initdb.Start();
            initdb.WaitForExit();
        }

        // Start PostgreSQL server
        _postgresProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = $"{pgDirectory}/pg_ctl",
                Arguments = $"-D \"{dataDirectory}\" -l logfile start",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            }
        };
        _postgresProcess.Start();
    }

    public static void Stop()
    {
        _postgresProcess?.Kill();
    }
}