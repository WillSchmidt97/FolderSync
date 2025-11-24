📘 FolderSync

    A lightweight C# application that performs one-way synchronization between a source folder and a replica folder.
    The program ensures the replica is always an exact copy of the source, including file creation, updates, and deletions.

    Synchronization can be performed periodically or only once using a command-line flag.



✨ Features

    -One-way synchronization (source → replica)

    -Periodic sync using a user-defined interval

    -One-time sync mode with the --once flag

    -Logging to console and log file

    -SHA-256 hashing to detect changes instead of timestamps

    -Automatic removal of files/directories deleted from the source



📦 How It Works

    The application takes command-line arguments defining:

        -Source folder

        -Replica folder

        -Sync interval (Not necessary if using --once)

        -Log file path

        -Optional --once flag

    On each synchronization cycle:

        -Files in the source are hashed using SHA-256

        -Files are copied only if the hash differs or the file does not exist in the replica

        -Missing files/folders in the source are deleted from the replica

        -All operations are logged

        If --once is used, synchronization runs only one time and exits.



🛠️ Usage

    Periodic synchronization
    Runs indefinitely, syncing every given seconds:

    FolderSync.exe --source "C:\path\source" --replica "C:\path\replica" --interval 5 --log "C:\path\log.txt"

    Just change the number after "-interval" for the gap you want the program to wait before the program sync again.

    If you aim to run only one time and do not want the program running in the background it is necessary to use the flag --once:

    FolderSync.exe --source "C:\path\source" --replica "C:\path\replica" --log "C:\path\log.txt" --once



🧪 How to Build

    From the project root:
    dotnet build -c Release

    The executable will be created in:
    /bin/Release/net9.0/FolderSync.exe

    IMPORTANT: This project was developed under the .NET 9.0 version, the version is also named in the folder.



🧪 How to Run (development mode)

    dotnet run --project FolderSync -- --source "your/path/source" --replica "your/path/replica" --interval 5 --log "your/path/log/log.txt"


🛡️ Hash Verification (SHA-256)

    The application uses SHA-256 hashing instead of timestamps to reliably detect file changes.

    A file is copied to the replica when:
        -It does not exist in the replica
        -OR the computed hash differs

    
📝 Logging

    Every sync operation is logged to:
        -Console
        -The log file specified by --log

    Example log entry:
    [INFO] 2025-01-14 12:33:12: File copied: notes.txt



📁 Paths:

    When giving the paths in the command line it is important to notice:

    source: The source folder is not obliged to already exist for the program to works, if it doesnt exist it will be created.
    replica: Same as with the source folder.
    log: The log file is not obliged to already exist for the program to works, if it doesn't exist a .txt file will be created. Neverthless, it is required that all the folders given in the path are already created.



🧹 Deletion Handling:

    If a file or folder exists in the replica but not in the source, it is removed to keep the replica fully synchronized.