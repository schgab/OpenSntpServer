# OpenSntpServer
This is a simple implementation of the sntp protocol.
A simple asynchronous server example is provided in the Program.cs and it can be run directly after compiling.

## Running the server
Start the program in debug mode or use `dotnet run`. Also make sure the port 123 is available and shutdown any service or application which uses that port.

**Note**: To run the server on Linux root privileges are required

## Testing
To test if the server is functioning use the commandline with the following command while the server is running:

- Windows: `w32tm /stripchart /computer:127.0.0.1`
- Linux: `ntpdate -q 127.0.0.1`



